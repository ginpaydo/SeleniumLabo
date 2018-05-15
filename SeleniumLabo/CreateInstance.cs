using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using SeleniumLabo;
using System;
using System.Collections.Generic;
using System.Text;

public class WebDriverFactory
{
    public static IWebDriver CreateInstance(BrowserName browserName)
    {
        switch (browserName)
        {
            case BrowserName.None:
                throw new ArgumentException(string.Format("Not Definition. BrowserName:{0}", browserName));

            case BrowserName.Chrome:
                return new ChromeDriver();

            case BrowserName.Firefox:
                FirefoxDriverService driverService = FirefoxDriverService.CreateDefaultService();
                driverService.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                driverService.HideCommandPromptWindow = true;
                driverService.SuppressInitialDiagnosticInformation = true;
                return new FirefoxDriver(driverService);

            case BrowserName.InternetExplorer:
                return new InternetExplorerDriver();

            case BrowserName.Edge:
                return new EdgeDriver();

            case BrowserName.Safari:
                return new SafariDriver();

            default:
                throw new ArgumentException(string.Format("Not Definition. BrowserName:{0}", browserName));
        }
    }
}