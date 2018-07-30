using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationDemo.TestSettings
{
    public class TestProperties
    {
        public string BrowserName
        {
            get { return TestSettings.Default.Browser.ToLower(); }
        }

        public string ApplicationUrl
        {
            get { return TestSettings.Default.AppUrl; }
        }

        public bool IsChromeHeadless
        {
            get { return TestSettings.Default.IsChromeHeadless; }
        }
    }
}
