using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;

namespace HROUTOFFICE
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CultureInfo culture = new CultureInfo("en-US");
            //CultureInfo culture = new CultureInfo("th-TH");
            culture.DateTimeFormat.DateSeparator = "/";
            culture.DateTimeFormat.TimeSeparator = ":";
            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            culture.DateTimeFormat.ShortTimePattern = "HH:mm";
            culture.DateTimeFormat.LongDatePattern = "dddd, MMMM dd, yyyy";
            culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
            culture.DateTimeFormat.FullDateTimePattern = "dd/MM/yyyy HH:mm:ss";

            culture.NumberFormat.NumberGroupSeparator = ",";
            culture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            Application.Run(new FormMain ());
        }
    }
}
