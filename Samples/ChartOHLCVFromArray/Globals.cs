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

using ChartSkil27;
using TWUtilities40;

namespace ChartOHLCVFromArray
{
    static class Globals
    {
        // these provide access to what are essentially static methods of
        // the TWUtilities com component and the ChartSkil charting component
        internal static TWUtilities TW = new TWUtilities();
        internal static ChartSkil ChartSkil = new ChartSkil();

        // a convenience function for creating font for use with COM objects
        internal static stdole.StdFont CreateOleFont(String name = "Arial",
                                    bool bold = false,
                                    bool italic = false,
                                    double size = 8.25,
                                    bool strikethrough = false,
                                    bool underline = false) {

            return new stdole.StdFont {
                Name = name,
                Bold = bold,
                Italic = italic,
                Size = (decimal)size,
                Strikethrough = strikethrough,
                Underline = underline
            };
        }

        internal static int ToOleColor(int red, int green, int blue) {
            return ColorTranslator.ToOle(Color.FromArgb(red, green, blue));
        }

        internal static void SetBackgroundGradient(ChartRegionStyle style, int R1, int G1, int B1, int R2, int G2, int B2) {
            style.set_BackGradientFillColors(new int[] { ToOleColor(R1, G1, B1), ToOleColor(R2, G2, B2) });
        }

        internal static void SetBackgroundGradient(ChartRegionStyle style, int color1, int color2) {
            style.set_BackGradientFillColors(new int[] { color1, color2 });
        }

        // this is the error handler for unhandled errors occurring within the
        // TradeBuild COM components
        internal static void HandleError(TWUtilities40.ErrorEventData e) {
            TW.LogMessage("***** Unhandled COM error *****", TWUtilities40.LogLevels.LogLevelSevere);
            var s = $"Error {e.ErrorCode}: {e.ErrorMessage}\n{e.ErrorSource}";
            TW.LogMessage(s, TWUtilities40.LogLevels.LogLevelSevere);
            Environment.FailFast($"***** Unhandled COM error *****\n{s}");
        }


        // this is the error handler for unhandled exceptions occurring within the
        // .Net code
        internal static void HandleError(Exception e) {
            TW.LogMessage("***** Unhandled exception *****", TWUtilities40.LogLevels.LogLevelSevere);
            TW.LogMessage(e.ToString(), TWUtilities40.LogLevels.LogLevelSevere);
            Environment.FailFast("***** Unhandled exception *****", e);
        }
    }
}
