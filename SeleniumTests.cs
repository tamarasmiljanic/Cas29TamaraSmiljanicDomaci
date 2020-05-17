using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Cas29DomaciTamaraSmiljanic
{
    class SeleniumTests
    {
        IWebDriver driver;
        WebDriverWait wait;
        string url = "http://test.qa.rs/";

        private string Test_Data_email = UsefulFunctions.RandomEmail();
        private string Test_Data_pass = UsefulFunctions.RandomPassword(15);

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 150));
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

        [Test]
        public void TestRegistration()
        {
            try
            {
                IWebElement newUser = wait.Until(EC.ElementIsVisible(By.LinkText("Kreiraj novog korisnika")));
                if (newUser.Displayed && newUser.Enabled)
                {
                    newUser.Click();
                }
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Element showing took too long.");
            }

            try
            {
                IWebElement RegisterBtn = driver.FindElement(By.Name("register"));
                if (RegisterBtn.Displayed && RegisterBtn.Enabled)
                {
                    IWebElement form_name = driver.FindElement(By.Name("ime"));
                    if (form_name.Displayed && form_name.Enabled)
                    {
                        form_name.Clear();
                        form_name.SendKeys("Tamara");
                    }

                    IWebElement form_lastname = driver.FindElement(By.Name("prezime"));
                    if (form_lastname.Displayed && form_lastname.Enabled)
                    {
                        form_lastname.Clear();
                        form_lastname.SendKeys("Smiljanić");
                    }

                    IWebElement form_Username = driver.FindElement(By.Name("korisnicko"));
                    if (form_Username.Displayed && form_Username.Enabled)
                    {
                        form_Username.Clear();
                        form_Username.SendKeys("Smiljka1979");
                    }

                    IWebElement form_email = driver.FindElement(By.Name("email"));
                    if (form_email.Displayed && form_email.Enabled)
                    {
                        form_email.Clear();
                        form_email.SendKeys(Test_Data_email);
                    }

                    IWebElement form_phone = driver.FindElement(By.Name("telefon"));
                    if (form_phone.Displayed && form_phone.Enabled)
                    {
                        form_phone.Clear();
                        form_phone.SendKeys("+381695555555");
                    }

                    IWebElement form_state = driver.FindElement(By.Id("zemlja"));
                    if (form_state.Enabled)
                    {
                        SelectElement state = new SelectElement(form_state);
                        state.SelectByValue("ita");
                    }

                    try
                    {
                        IWebElement form_city = wait.Until(EC.ElementToBeClickable(By.XPath("//select[@name='grad']")));
                        {
                            if (form_city.Enabled)
                            {
                                SelectElement city = new SelectElement(form_city);
                                city.SelectByValue("Ancona");
                                System.Threading.Thread.Sleep(4000);
                            }
                        }

                    }
                    catch (WebDriverTimeoutException)
                    {
                        Assert.Fail("Test failed = it took too long for element to show");
                    }

                    IWebElement form_address = driver.FindElement(By.XPath("//div[contains(text(),'Ulica i broj')]//following::input"));
                    if (form_address.Displayed && form_address.Enabled)
                    {
                        form_address.Clear();
                        form_address.SendKeys("Ive Andrica 11");
                    }

                    IWebElement form_sex = driver.FindElement(By.Id("pol_z"));
                    if (form_sex.Displayed && form_sex.Enabled)
                    {
                        form_sex.Click();
                    }

                    IWebElement form_newsletter = driver.FindElement(By.Id("newsletter"));
                    if (form_newsletter.Displayed && form_newsletter.Enabled)
                    {
                        form_newsletter.Click();
                    }

                    RegisterBtn.Click();
                    Assert.Pass();
                }
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void RegistrationCheckTest()
        {
            try
            {
                driver.Navigate().GoToUrl(url);
                
                IWebElement izlistaj = wait.Until(EC.ElementIsVisible(By.LinkText("Izlistaj sve korisnike")));
                if (izlistaj.Displayed && izlistaj.Enabled)
                {
                    izlistaj.Click();
                }

                IWebElement table = wait.Until(EC.ElementIsVisible(By.XPath("//table//td")));
                if (table.Displayed && table.Enabled)
                {
                    bool registered = false;
                    ReadOnlyCollection<IWebElement> listOfEmails = table.FindElements(By.XPath("//table//td[contains(text(),'@')]"));
                    foreach (IWebElement email in listOfEmails)
                    {
                        if (email.Text == "tamara@gmail.com")
                        {
                            registered = true;
                        }
                    }
                    if (registered)
                    {
                        Assert.Pass();
                    }else
                    {
                        Assert.Fail();
                    }
                }
            }
            catch (NoSuchElementException)
            {
                
            }
        }
    }
}

    



   

