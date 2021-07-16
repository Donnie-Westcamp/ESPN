using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESPN
{
    public class Utilities
    {
        //This is a function that passes in the driver and element to allow browser to wait UP TO a total of 30 seconds. If not found will error out.
        internal static bool WaitForElementToAppear(IWebDriver Driver, By by)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
                wait.Until(x =>
                {
                    try
                    {
                        return Driver.FindElement(by).Displayed;
                    }
                    catch
                    {
                        return false;
                    }

                });
            }
            catch
            {
                return false;
            }
            
            return false;
        }
    }
}
