using OpenQA.Selenium;
using System;
using System.Threading;

//Nugetから取得
//Selenium.Support
//Selenium.WebDriver
//Selenium.WebDriver.ChromeDriver
//Selenium.WebDriver.IEDriver
//WebDriver.GeckoDriver     // firefox用

// ドライバのインストールが必要
// http://chromedriver.storage.googleapis.com/index.html

namespace SeleniumLabo
{
    /// <summary>
    /// Seleniumの練習
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region Google検索するサンプル
            //// AppSettings.BrowserName.Firefoxを変更することによって対象のブラウザを変更できます
            //using (IWebDriver webDriver = WebDriverFactory.CreateInstance(BrowserName.Chrome))
            //{
            //    // https://www.google.co.jp に遷移させる
            //    webDriver.Url = @"https://www.google.co.jp";

            //    // #lst-ibの要素を取得する
            //    IWebElement element = webDriver.FindElement(By.CssSelector("#lst-ib"));

            //    // 上記取得した要素に対してテキストを入力してサブミット
            //    element.SendKeys("Selenium2");
            //    element.Submit();

            //    // 一瞬で完了するため3秒スリープ
            //    Thread.Sleep(TimeSpan.FromSeconds(3));

            //    // ブラウザを閉じる
            //    webDriver.Quit();
            //}
            #endregion

            #region 
            try
            {
                // AppSettings.BrowserName.Firefoxを変更することによって対象のブラウザを変更できます
                using (IWebDriver webDriver = WebDriverFactory.CreateInstance(BrowserName.Chrome))
                {
                    string target = "もっと評価されるべき";

                    // URLを開く
                    // もっと評価されるべき動画を再生数が少ない順
                    webDriver.Navigate().GoToUrl($"http://www.nicovideo.jp/tag/{target}?ref=tagconcerned");

                    Console.WriteLine("ログインします。");
                    webDriver.FindElement(By.XPath($"//*[@id=\"siteHeaderNotification\"]/a/span")).Click();
                    webDriver.FindElement(By.XPath($"//*[@id=\"input__mailtel\"]")).SendKeys("mail@address.com");
                    webDriver.FindElement(By.XPath($"//*[@id=\"input__password\"]")).SendKeys("password");
                    webDriver.FindElement(By.XPath($"//*[@id=\"login__submit\"]")).Click();

                    Console.WriteLine("ソート条件を再生数が少ない順に変更します。");
                    webDriver.Navigate().GoToUrl($"http://www.nicovideo.jp/tag/{target}?ref=tagconcerned");
                    webDriver.FindElement(By.XPath($"/html/body/div[3]/div/div[1]/div[1]/div[1]/div[1]/a")).Click();
                    webDriver.FindElement(By.XPath($"/html/body/div[3]/div/div[1]/div[1]/div[2]/div/div[1]/a[1]")).Click();
                    webDriver.FindElement(By.XPath($"/html/body/div[3]/div/div[1]/div[1]/div[2]/div/div[1]/ul[2]/li[2]/a")).Click();

                    int page = 1;
                    while (true)
                    {
                        webDriver.Navigate().GoToUrl($"http://www.nicovideo.jp/tag/{target}?page={page}&sort=v&order=a");

                        for (int i = 1; i <= 36; i++)
                        {
                            Console.WriteLine($"{i}回目");
                            webDriver.FindElement(By.XPath($"/html/body/div[3]/div/div[1]/div[3]/ul[2]/li[{i}]/div[2]/p/a")).Click();
                            // 「タグ編集」があればクリック
                            var tagEditButton = webDriver.FindElements(By.XPath("//*[@id=\"js-app\"]/div/div[3]/div[1]/div[5]/div[1]/div/span/button[1]"));
                            if (tagEditButton.Count > 0)
                            {
                                tagEditButton[0].Click();
                                var tagItems = webDriver.FindElements(By.ClassName($"TagItem"));
                                Console.WriteLine($"タグは{tagItems.Count}件あります。");
                                for (int j = 0; j < tagItems.Count; j++)
                                {
                                    Console.WriteLine($"{j}番目:{tagItems[j].Text}");
                                    if (tagItems[j].Text.Contains(target))
                                    {
                                        Console.WriteLine($"「{target}」がありました。");
                                        var xButton = webDriver.FindElements(By.XPath($"//*[@id=\"js-app\"]/div/div[3]/div[1]/div[5]/div[2]/ul/li[{j + 1}]/button"));
                                        if (xButton.Count > 0)
                                        {
                                            xButton[0].Click();
                                            // 37分スリープさせる
                                            Thread.Sleep(TimeSpan.FromMinutes(37));
                                        }
                                        else
                                        {
                                            Console.WriteLine("タグロックされているようです。");
                                        }
                                    }
                                }
                                webDriver.Navigate().Back();
                            }
                            else
                            {
                                Console.WriteLine("タグ編集ボタンが無いので戻ります。");
                                webDriver.Navigate().Back();
                            }
                        }
                        page++;
                    }

                    //// 自動終了しないようにする
                    //Console.WriteLine("何かキーを押すことで終了します");
                    //Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("エラー");
                Console.ReadKey();
                throw;
            }
            #endregion
        }
    }
}
