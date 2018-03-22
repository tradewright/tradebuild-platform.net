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
using System.Windows.Forms;

namespace ChartOHLCVFromArray
{
    static class Program
    {
        /// <summary>;
        /// The main entry point for the application.;
        /// </summary>;
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.Automatic);
            Application.ThreadException += (s, e) => Globals.HandleError(e.Exception);

            // set up the TradeBuild unhandled exception handler - this catches errors
            // raised internally within the TradeBuild components which are not
            // passed back to the caller. An example is passing extreme values
            // into a Bar object's Tick() method, which can subsequently cause an 
            // overflow that cannot be sensibly handled in any other way. Another example
            // is an unexpected run-time error within an event handler in the 
            // TradeBuild components (though these indicate bugs and are hopefully 
            // unlikely to occur).
            Globals.TW.UnhandledErrorHandler.UnhandledError += UnhandledComErrorHandler;
            Application.Run(new ChartForm());
        }

        private static void UnhandledComErrorHandler(ref TWUtilities40.ErrorEventData ev) {
            Globals.HandleError(ev);
        }
    }
}
