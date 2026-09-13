using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace MyFirstAutomation_Rozetka
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void OpenBrowser_ShouldStartFirefox()
        {
            // ===== ARRANGE =====
            string url = "https://rozetka.com.ua";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // ===== ACT =====
            // 1. Відкриваємо сайт Розетки
            driver.Navigate().GoToUrl(url);

            // 2. Очікуємо кнопку входу за її data-testid
            IWebElement loginButton = wait.Until(d => d.FindElement(By.CssSelector("button[data-testid='header-auth-btn']")));

            // Налаштовуємо JavaScript для надійного кліку
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", loginButton);

            // 3. Чекаємо, поки з'явиться форма та поле введення телефону
            IWebElement phoneField = wait.Until(d => d.FindElement(By.CssSelector("input[type='tel']")));

            // Вводимо невалідні дані
            phoneField.SendKeys("1234567890");

            // 4. Знаходимо зелену кнопку "Продовжити"
            IWebElement continueButton = driver.FindElement(By.CssSelector("button.button--green, button[type='submit']"));

            // Клікаємо "Продовжити" за допомогою JavaScript
            js.ExecuteScript("arguments[0].click();", continueButton);

            // Залишаємо паузу в 4 секунди, щоб встигнути побачити введені цифри
            System.Threading.Thread.Sleep(4000);
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
