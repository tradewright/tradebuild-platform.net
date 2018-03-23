#region License

// The MIT License (MIT)
//
// Copyright (c) 2017-2018 Richard L King (TradeWright Software Systems)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#endregion

using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using ChartSkil27;
using TWUtilities40;

namespace ChartOHLCVFromArray
{

    public partial class ChartForm : Form
    {
        private int[] arr = new int[42];

        // the minimum tick size for the security
        double mTickSize;

        ChartRegion mPriceRegion;
        ChartRegion mMACDRegion;
        ChartRegion mVolumeRegion;

        // used to create the price bars
        BarSeries mBarSeries;

        // used to create the volume histogram display
        DataPointSeries mVolumeSeries;

        // used to create the moving average display
        DataPointSeries mMA;

        // used to create the MACD display
        DataPointSeries mMACD;

        // used to create the MACD Signal display
        DataPointSeries mMACDSignal;

        // used to create the MACD Histogram display
        DataPointSeries mMACDHistogram;

        public ChartForm() {
            InitializeComponent();

            setupLogging();

            // The bar data to be displayed in the chart is created in the
            // DataGenerator class via a static initialiser.

            // we can calculate the minimum tick size from the data itself,
            // though typically this would be obtained from a contract object
            mTickSize = DataGenerator.MinimumTick;

        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            try {
                // create the chart styles
                ChartStyles.SetupChartStyles();

                // load the available chart styles into the combo
                foreach (ChartStyle c in Globals.ChartSkil.ChartStylesManager) {
                    ChartStylesCombo.Items.Add(c.Name);
                }
                ChartStylesCombo.SelectedIndex = 0;
            } catch (Exception ex) {
                Globals.TW.LogMessage(ex.ToString(), LogLevels.LogLevelSevere);
                throw;
            }
        }

        private void ClearChartButton_Click(object sender, EventArgs e) {
            ClearChartButton.Enabled = false;
            Chart.ClearChart();
            LoadDataButton.Enabled = true;
            this.AcceptButton = LoadDataButton;
        }

        private async void LoadDataButton_Click(object sender, EventArgs e) {
            LoadDataButton.Enabled = false;

            // Prevent the chart drawing while loading the data, which is
            // much quicker
            Chart.DisableDrawing();

            // the chart needs to know the time period of the bars. We could 
            // perhaps try to calculate it from the data, but generally speaking
            // this information is already known to a program that wants to chart
            // bars
            Chart.TimePeriod = Globals.TW.GetTimePeriod(1, TimePeriodUnits.TimePeriodMinute);

            CreateChartRegions();

            // setup the bar series to which we'll add the OHLC data
            mBarSeries = setupBarSeries();

            // setup the datapoint series to which we'll add the volume data
            mVolumeSeries = setupVolumeSeries();

            // setup the datapoint series to which we'll add the moving average data
            mMA = setupMASeries();

            // etc
            mMACD = setupMACDSeries();
            mMACDSignal = setupMACDSignalSeries();
            mMACDHistogram = setupMACDHistogramSeries();

            // Link up the toolbar
            ChartToolbar.Initialise(Chart.Controller, mPriceRegion, mBarSeries);

            // load the OHLC data into the chart
            await LoadOHLCData();

            // Now enable drawing so the charted data is visible
            Chart.EnableDrawing();

            ClearChartButton.Enabled = true;
            this.AcceptButton = ClearChartButton;
        }

        private void ManageChartStylesButton_Click(object sender, EventArgs e) {
            var f = new ChartStylesOrganizer();
            f.Show(this);
        }

        private void ChartStylesCombo_SelectedIndexChanged(object sender, EventArgs e) {
            Chart.Style = Globals.ChartSkil.ChartStylesManager.Item((string)ChartStylesCombo.SelectedItem);
            Chart.ChartBackColor = ColorTranslator.FromOle(Chart.Style.DefaultRegionStyle.get_BackGradientFillColors()[0]);
        }

        private void CreateChartRegions() { 
            // Set up the regions of the chart that will display the price bars, the moving average
            // indicator and the volume histogram. 
            //
            // You can have as many regions as you like on a chart. They are arranged vertically, 
            // and the first parameter to Chart.Regions.Add() specifies the percentage of the available 
            // space that the region should occupy. A value of 100 means use all the available space 
            // left over after taking account of regions with smaller percentages. Since this is the 
            // first region created, it uses all the space. NB: you should create at least one region 
            // (preferably the first) that uses all available space rather than a specific percentage 
            // - if you don't, then resizing regions by dragging the dividers gives odd results!

            mPriceRegion = setupPriceRegion();
            mMACDRegion = setupMACDRegion();
            mVolumeRegion = setupVolumeRegion();
        }

        private async Task LoadOHLCData() {

            // Note that we could do this data loading on a threadpool thread,
            // but that actually makes things very slow. This is because
            // the ChartSkil component is an apartment-threaded COM component,
            // so any attempt to call its methods from another thread results
            // in that call being marshalled over to the UI thread since that is
            // where the chart object was created. This is much worse than simply
            // a managed-unmanaged transition (about 10 times as slow). 

            DataLoadProgressLabel.Text = "Loading bar data";
            DataLoadProgressBar.Maximum = DataGenerator.OHLCVBars.Length;
            DataLoadProgressBar.Visible = true;

            for (int i = 0; i < DataGenerator.OHLCVBars.Length; i++) {
                var bar = DataGenerator.OHLCVBars[i];
                var lChartBar = mBarSeries.Add(bar.Timestamp);
                lChartBar.Tick(bar.Open);
                lChartBar.Tick(bar.High);
                lChartBar.Tick(bar.Low);
                lChartBar.Tick(bar.Close);

                var lVolume = mVolumeSeries.Add(bar.Timestamp);
                lVolume.set_DataValue(bar.Volume);

                var ma = DataGenerator.MA[i];
                if (ma != null) {
                    var lDataPoint = mMA.Add(bar.Timestamp);
                    lDataPoint.set_DataValue(ma.Value);
                }

                var macd = DataGenerator.MACD[i];
                if (macd != null) {
                    var lDataPoint = mMACD.Add(bar.Timestamp);
                    lDataPoint.set_DataValue(macd.Value);
                }

                var macdsignal = DataGenerator.MACDSignal[i];
                if (macdsignal != null) {
                    var lDataPoint = mMACDSignal.Add(bar.Timestamp);
                    lDataPoint.set_DataValue(macdsignal.Value);
                }

                var macdhist = DataGenerator.MACDHistogram[i];
                if (macdhist != null) {
                    var lDataPoint = mMACDHistogram.Add(bar.Timestamp);
                    lDataPoint.set_DataValue(macdhist.Value);
                }

                if (i % 50 == 0) {
                    // this frees up the UI, since we're running all of this on the UI thread
                    await Task.Delay(1);
                    if (UpdateBarsCheck.Checked) {
                        Chart.EnableDrawing();
                        Chart.DisableDrawing();
                    }
                    DataLoadProgressLabel.Text = $"Bars loaded: {i}";
                    DataLoadProgressLabel.Refresh();
                    DataLoadProgressBar.Value = i;
                }
            }

            DataLoadProgressLabel.Text = "";
            DataLoadProgressBar.Visible = false;
        }

        private ChartRegion setupPriceRegion() {
            // don't let this region drop to less than 25 percent of the chart by resizing other
            // regions
            var lRegion = Chart.Regions.Add(100, 25);

            // the region needs to know the tick size so that vertical cursor movements can snap to 
            // tick boundaries when required
            lRegion.YScaleQuantum = mTickSize;

            // we can tell the region to maintain a minimum height
            lRegion.MinimumHeight = 10 * mTickSize;

            lRegion.Title.set_Text("Some OHLC data");
            setTitleAttributes(lRegion);

            return lRegion;
        }

        private static void setTitleAttributes(ChartRegion region) {
            region.Title.Color = ColorTranslator.ToOle(Color.Blue);
            region.Title.Box = true;
            region.Title.BoxFillColor = 0xc0c0c0;
            region.Title.BoxStyle = LineStyles.LineInvisible;
        }

        private ChartRegion setupMACDRegion() {
            var lRegion = Chart.Regions.Add(15, 0);
            lRegion.YScaleQuantum = 0.001;
            lRegion.Title.set_Text("MACD");
            setTitleAttributes(lRegion);
            lRegion.HasXGridText = false;
            return lRegion;
        }

        private ChartRegion setupVolumeRegion() {
            var lRegion = Chart.Regions.Add(15, 0);
            lRegion.YScaleQuantum = 1;
            lRegion.Title.set_Text("Volume");
            lRegion.HasXGridText = false;
            setTitleAttributes(lRegion);
            return lRegion;
        }

        // Note that there's nothing stop you setting up multiple bar series
        // in the same region should you want to, and you can of course have multiple 
        // regions each with its own set of bar series.
        private BarSeries setupBarSeries() {
            var lBarSeries = (BarSeries)mPriceRegion.AddGraphicObjectSeries((_IGraphicObjectSeries)new BarSeries());
            lBarSeries.Style = setupBarStyle();
            return lBarSeries;
        }

        // You can display multiple datapoint series in a single region.
        private DataPointSeries setupVolumeSeries() {
            var lDataPointSeries = (DataPointSeries)mVolumeRegion.AddGraphicObjectSeries((_IGraphicObjectSeries)new DataPointSeries());
            lDataPointSeries.Style = new DataPointStyle() {
                DisplayMode = DataPointDisplayModes.DataPointDisplayModeHistogram,

                // display this series as a histogram
                UpColor = ColorTranslator.ToOle(Color.DarkGreen),
                DownColor = ColorTranslator.ToOle(Color.DarkRed)
            };
            return lDataPointSeries;
        }

        private DataPointSeries setupMASeries() {
            var lDataPointSeries = (DataPointSeries)mPriceRegion.AddGraphicObjectSeries((_IGraphicObjectSeries)new DataPointSeries());
            lDataPointSeries.Style = new DataPointStyle() {
                DisplayMode = DataPointDisplayModes.DataPointDisplayModeLine,
                Color = ColorTranslator.ToOle(Color.DarkGreen)
            };
            return lDataPointSeries;
        }

        private DataPointSeries setupMACDSeries() {
            var lDataPointSeries = (DataPointSeries)mMACDRegion.AddGraphicObjectSeries((_IGraphicObjectSeries)new DataPointSeries());
            lDataPointSeries.Style = new DataPointStyle() {
                DisplayMode = DataPointDisplayModes.DataPointDisplayModeLine,
                Color = ColorTranslator.ToOle(Color.Teal)
            };
            return lDataPointSeries;
        }

        private DataPointSeries setupMACDSignalSeries() {
            var lDataPointSeries = (DataPointSeries)mMACDRegion.AddGraphicObjectSeries((_IGraphicObjectSeries)new DataPointSeries());
            lDataPointSeries.Style = new DataPointStyle() {
                DisplayMode = DataPointDisplayModes.DataPointDisplayModeLine,
                Color = ColorTranslator.ToOle(Color.Red)
            };
            return lDataPointSeries;
        }

        private DataPointSeries setupMACDHistogramSeries() {
            var lDataPointSeries = (DataPointSeries)mMACDRegion.AddGraphicObjectSeries((_IGraphicObjectSeries)new DataPointSeries());
            lDataPointSeries.Style = new DataPointStyle() {
                DisplayMode = DataPointDisplayModes.DataPointDisplayModeHistogram,
                Color = ColorTranslator.ToOle(Color.Gray)
            };
            return lDataPointSeries;
        }

        private static BarStyle setupBarStyle() {
            return new BarStyle() {

                // specifies how wide each bar is. If this value
                // were set to 1, the sides of the bars would touch
                Width = 0.6f,

                // the thickness in pixels of a candlestick outline
                // (ignored if displaying as bars)
                OutlineThickness = 1,

                // the thickness in pixels of candlestick tails
                // (ignored if displaying as bars)
                TailThickness = 1,

                // the thickness in pixels of the lines used to
                // draw bars (ignored if displaying as candlesticks)
                Thickness = 2,

                // how the bars will be displayed initially - 
                // draw this bar series as bars not candlesticks
                DisplayMode = BarDisplayModes.BarDisplayModeBar,

                // draw up candlesticks with open bodies
                // (ignored if displaying as bars)
                SolidUpBody = true,

                // the color to use for up bars
                UpColor = 0x1D9311,

                // the color to use for down bars
                DownColor = 0x43FC2
            };
        }

        private static void setupLogging() {
            // We use TradeBuild's logging because all the TradeBuild
            // components already make use of it. 
            //
            // By default, you can find the logfile in:
            //    %APPDATA%\Roaming\<ApplicationGroupName>\<ApplicationName>\<ApplicationName>.log
            //
            // The following command line switches are also supported:
            //
            //        -log:<logfile path>
            //        -loglevel:[N or Normal | D or Detail | M or Medium | H or High]

            Globals.TW.ApplicationGroupName = "TradeWright";
            Globals.TW.ApplicationName = Application.ProductName;

            // you can change the default logging level like this (but
            // your logfile can get quite big for high logging levels):
            //Globals.TW.DefaultLogLevel = LogLevels.LogLevelHighDetail;

            Globals.TW.SetupDefaultLogging(Environment.CommandLine);
        }

    }
}
