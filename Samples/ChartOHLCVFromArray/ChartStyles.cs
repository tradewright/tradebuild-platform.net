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

using ChartSkil27;

using System.Drawing;

namespace ChartOHLCVFromArray
{
    static class ChartStyles {

        public const string ChartStyleNameAppDefault = "Application default";
        public const string ChartStyleNameBlack = "Black";
        public const string ChartStyleNameBlackNoAxes = "BlackNoAxes";
        public const string ChartStyleNameDarkBlueFade = "Dark blue fade";
        public const string ChartStyleNameGoldFade = "Gold fade";
        public const string ChartStyleNameGermanFlag = "German Flag (well, sort of!...)";

        internal static void SetupChartStyles() {
            setupChartStyleAppDefault();
            setupChartStyleBlack();
            setupChartStyleBlackNoAxes();
            setupChartStyleDarkBlueFade();
            setupChartStyleGermanFlag();
            setupChartStyleGoldFade();
        }

        static void setupChartStyleAppDefault() {
            if (Globals.ChartSkil.ChartStylesManager.Contains(ChartStyleNameAppDefault))
                return;

            var lCursorTextStyle = new TextStyle {
                Align = TextAlignModes.AlignBoxTopCentre,
                Box = true,
                BoxFillWithBackgroundColor = true,
                BoxStyle = LineStyles.LineInvisible,
                BoxThickness = 0,
                Color = 0x80,
                PaddingX = 2,
                PaddingY = 0,
                Font = Globals.CreateOleFont("Courier New", bold: true, size: 7)
            };

            var lDefaultRegionStyle = Globals.ChartSkil.GetDefaultChartDataRegionStyle().clone();
            Globals.SetBackgroundGradient(lDefaultRegionStyle, 192, 192, 192, 248, 248, 248);

            var lxAxisRegionStyle = Globals.ChartSkil.GetDefaultChartXAxisRegionStyle().clone();
            lxAxisRegionStyle.XCursorTextStyle = lCursorTextStyle;
            Globals.SetBackgroundGradient(lxAxisRegionStyle, 230, 236, 207, 222, 236, 215);

            var lDefaultYAxisRegionStyle = Globals.ChartSkil.GetDefaultChartYAxisRegionStyle().clone();
            lDefaultYAxisRegionStyle.YCursorTextStyle = lCursorTextStyle;
            Globals.SetBackgroundGradient(lDefaultYAxisRegionStyle, 234, 246, 254, 226, 246, 255);

            var lCrosshairLineStyle = new LineStyle();
            lCrosshairLineStyle.Color = 0x7F;

            var style = Globals.ChartSkil.ChartStylesManager.Add(ChartStyleNameAppDefault,
                                    Globals.ChartSkil.ChartStylesManager.DefaultStyle,
                                    lDefaultRegionStyle,
                                    lxAxisRegionStyle,
                                    lDefaultYAxisRegionStyle,
                                    lCrosshairLineStyle);
            style.ChartBackColor = (uint)style.DefaultRegionStyle.get_BackGradientFillColors()[0];
            style.HorizontalScrollBarVisible = true;
        }

        static void setupChartStyleBlack() {
            if (Globals.ChartSkil.ChartStylesManager.Contains(ChartStyleNameBlack))
                return;

            var lCursorTextStyle = new TextStyle {
                Align = TextAlignModes.AlignBoxTopCentre,
                Box = true,
                BoxFillWithBackgroundColor = true,
                BoxStyle = LineStyles.LineInvisible,
                BoxThickness = 0,
                Color = 255,
                PaddingX = 2,
                PaddingY = 0,
                Font = Globals.CreateOleFont("Courier New", bold: true, size: 8)
            };

            var lDefaultRegionStyle = Globals.ChartSkil.GetDefaultChartDataRegionStyle().clone();

            Globals.SetBackgroundGradient(lDefaultRegionStyle, 40, 40, 40, 40, 40, 40);

            var lGridLineStyle = new LineStyle {
                Color = Globals.ToOleColor(56, 56, 56)
            };
            lDefaultRegionStyle.XGridLineStyle = lGridLineStyle;
            lDefaultRegionStyle.YGridLineStyle = lGridLineStyle;

            lGridLineStyle = new LineStyle {
                Color = Globals.ToOleColor(64, 64, 64),
                LineStyle = LineStyles.LineDash
            };
            lDefaultRegionStyle.SessionEndGridLineStyle = lGridLineStyle;

            lGridLineStyle = new LineStyle {
                Color = Globals.ToOleColor(64, 64, 64),
                Thickness = 3
            };
            lDefaultRegionStyle.SessionStartGridLineStyle = lGridLineStyle;

            var lxAxisRegionStyle = Globals.ChartSkil.GetDefaultChartXAxisRegionStyle().clone();
            Globals.SetBackgroundGradient(lxAxisRegionStyle, 36, 36, 48, 36, 36, 48);
            lxAxisRegionStyle.XCursorTextStyle = lCursorTextStyle;

            var lGridTextStyle = new TextStyle {
                Box = true,
                BoxFillWithBackgroundColor = true,
                BoxStyle = LineStyles.LineInvisible,
                Color = 0xD0D0D0
            };
            lxAxisRegionStyle.XGridTextStyle = lGridTextStyle;

            var lDefaultYAxisRegionStyle = Globals.ChartSkil.GetDefaultChartYAxisRegionStyle().clone();
            Globals.SetBackgroundGradient(lDefaultYAxisRegionStyle, 36, 36, 48, 36, 36, 48);
            lDefaultYAxisRegionStyle.YCursorTextStyle = lCursorTextStyle;
            lDefaultYAxisRegionStyle.YGridTextStyle = lGridTextStyle;

            var lCrosshairLineStyle = new LineStyle {
                Color = Globals.ToOleColor(128, 0, 0)
            };

            var style = Globals.ChartSkil.ChartStylesManager.Add(ChartStyleNameBlack,
                                    Globals.ChartSkil.ChartStylesManager.Item(ChartStyleNameAppDefault),
                                    lDefaultRegionStyle,
                                    lxAxisRegionStyle,
                                    lDefaultYAxisRegionStyle,
                                    lCrosshairLineStyle);
            style.ChartBackColor = (uint)style.DefaultRegionStyle.get_BackGradientFillColors()[0];
        }

        static void setupChartStyleBlackNoAxes() {
            if (Globals.ChartSkil.ChartStylesManager.Contains(ChartStyleNameBlackNoAxes))
                return;

            var lDefaultRegionStyle = Globals.ChartSkil.ChartStylesManager.Item(ChartStyleNameBlack).DefaultRegionStyle.clone();

            lDefaultRegionStyle.CursorTextPosition = CursorTextPositions.CursorTextPositionBelowCursor;
            lDefaultRegionStyle.CursorTextMode = CursorTextModes.CursorTextModeCombined;
            lDefaultRegionStyle.CursorTextStyle = new TextStyle {
                Align = TextAlignModes.AlignTopCentre,
                Box = true,
                BoxStyle = LineStyles.LineInvisible,
                BoxFillWithBackgroundColor = true,
                Color = ColorTranslator.ToOle(Color.LightSalmon),
                Offset = Globals.ChartSkil.NewSize(0, -0.1),
                PaddingX = 2,
                PaddingY = 0,
                Font = Globals.CreateOleFont("Consolas", size: 10)
            };


            lDefaultRegionStyle.HasXGridText = true;
            lDefaultRegionStyle.XGridTextStyle = new TextStyle {
                Align = TextAlignModes.AlignBottomCentre,
                Box = true,
                BoxStyle=LineStyles.LineInvisible,
                BoxFillWithBackgroundColor = true,
                Color = ColorTranslator.ToOle(Color.Gray),
                PaddingX = 2,
                PaddingY = 0,
                Font = Globals.CreateOleFont("Consolas", size: 10)
            };

            lDefaultRegionStyle.HasYGridText = true;
            lDefaultRegionStyle.YGridTextStyle = lDefaultRegionStyle.XGridTextStyle.clone();
            lDefaultRegionStyle.YGridTextStyle.Align = TextAlignModes.AlignCentreLeft;

            var style = Globals.ChartSkil.ChartStylesManager.Add(ChartStyleNameBlackNoAxes,
                                    Globals.ChartSkil.ChartStylesManager.Item(ChartStyleNameAppDefault),
                                    lDefaultRegionStyle);

            style.HorizontalScrollBarVisible = false;
            style.XAxisVisible = false;
            style.YAxisVisible = false;
        }

        static void setupChartStyleDarkBlueFade() {
            if (Globals.ChartSkil.ChartStylesManager.Contains(ChartStyleNameDarkBlueFade)) return;

            var lCursorTextStyle = new TextStyle {
                Align = TextAlignModes.AlignBoxTopCentre,
                Box = true,
                BoxFillWithBackgroundColor = true,
                BoxStyle = LineStyles.LineInvisible,
                BoxThickness = 0,
                Color = 0x80,
                PaddingX = 2,
                PaddingY = 0,
                Font = Globals.CreateOleFont("Courier New", bold: true, size: 8)
            };

            var lDefaultRegionStyle = Globals.ChartSkil.GetDefaultChartDataRegionStyle().clone();
            Globals.SetBackgroundGradient(lDefaultRegionStyle, 0x643232, 0xF8F8F8);

            var lGridLineStyle = new LineStyle {
                Color = 0xC0C0C0
            };
            lDefaultRegionStyle.XGridLineStyle = lGridLineStyle;
            lDefaultRegionStyle.YGridLineStyle = lGridLineStyle;


            lGridLineStyle = new LineStyle {
                Color = 0xC0C0C0,
                LineStyle = LineStyles.LineDash
            };
            lDefaultRegionStyle.SessionEndGridLineStyle = lGridLineStyle;

            lGridLineStyle = new LineStyle {
                Color = 0xC0C0C0,
                Thickness = 3
            };
            lDefaultRegionStyle.SessionStartGridLineStyle = lGridLineStyle;

            var lCrosshairLineStyle = new LineStyle {
                Color = 0xFF
            };

            var style = Globals.ChartSkil.ChartStylesManager.Add(ChartStyleNameDarkBlueFade,
                                                Globals.ChartSkil.ChartStylesManager.Item(ChartStyleNameAppDefault),
                                                lDefaultRegionStyle,
                                                pCrosshairLineStyle:lCrosshairLineStyle);

            style.ChartBackColor = (uint)style.DefaultRegionStyle.get_BackGradientFillColors()[0];
        }

        static void setupChartStyleGermanFlag() {
            if (Globals.ChartSkil.ChartStylesManager.Contains(ChartStyleNameGermanFlag))
                return;

            var lDefaultRegionStyle = Globals.ChartSkil.ChartStylesManager.Item(ChartStyleNameBlack).DefaultRegionStyle.clone();
            lDefaultRegionStyle.set_BackGradientFillColors(new int[] {
                ColorTranslator.ToOle(Color.Black),
                ColorTranslator.ToOle(Color.Black),
                ColorTranslator.ToOle(Color.Yellow),
                ColorTranslator.ToOle(Color.Yellow),
                ColorTranslator.ToOle(Color.Red)
                // can't have more than 5 colours here
            });

            Globals.ChartSkil.ChartStylesManager.Add(ChartStyleNameGermanFlag,
                                                            Globals.ChartSkil.ChartStylesManager.Item(ChartStyleNameBlack),
                                                            lDefaultRegionStyle);
        }

        static void setupChartStyleGoldFade() {
            if (Globals.ChartSkil.ChartStylesManager.Contains(ChartStyleNameGoldFade)) return;

            var lCursorTextStyle = new TextStyle {
                Align = TextAlignModes.AlignTopCentre,
                Box = true,
                BoxFillWithBackgroundColor = true,
                BoxStyle = LineStyles.LineInvisible,
                BoxThickness = 0,
                Color = 0x80,
                PaddingX = 2,
                PaddingY = 0,
                Font = Globals.CreateOleFont("Courier New", bold: true, size: 8)
            };

            var lDefaultRegionStyle = Globals.ChartSkil.GetDefaultChartDataRegionStyle().clone();

            Globals.SetBackgroundGradient(lDefaultRegionStyle, 0x82DFE6, 0xEBFAFB);

            var lGridLineStyle = new LineStyle {
                Color = 0xC0C0C0
            };
            lDefaultRegionStyle.XGridLineStyle = lGridLineStyle;
            lDefaultRegionStyle.YGridLineStyle = lGridLineStyle;

            lGridLineStyle = new LineStyle {
                Color = 0xC0C0C0,
                LineStyle = LineStyles.LineDash
            };
            lDefaultRegionStyle.SessionEndGridLineStyle = lGridLineStyle;

            lGridLineStyle = new LineStyle {
                Color = 0xC0C0C0,
                Thickness = 3
            };
            lDefaultRegionStyle.SessionStartGridLineStyle = lGridLineStyle;

            var lCrosshairLineStyle = new LineStyle {
                Color = 0x7F
            };

            var style = Globals.ChartSkil.ChartStylesManager.Add(ChartStyleNameGoldFade,
                                                Globals.ChartSkil.ChartStylesManager.Item(ChartStyleNameAppDefault),
                                                lDefaultRegionStyle,
                                                pCrosshairLineStyle:lCrosshairLineStyle);

            style.ChartBackColor = (uint)style.DefaultRegionStyle.get_BackGradientFillColors()[0];
        }

    }
}
