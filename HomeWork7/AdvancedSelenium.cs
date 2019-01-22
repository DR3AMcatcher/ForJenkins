using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace HomeWork7
{
    [TestFixture]
    public class AdvancedSelenium : SetUpFixture
    {
        
        //Go to http://www.leafground.com/home.html
        //Open “HyperLink” page in new tab
        //Hover on “Go to Home Page” link
        //Take a screenshot and save it somewhere
        [Test]
        public void Test1()
        {
            Assert.True(true);
        }

        //Go to https://jqueryui.com/demos/
        //Navigate to “Droppable” demo(Interactions section)
        //Switch to frame
        //Drag & Drop the small box into a big one
        //Verify that big box now contains text “Dropped!”
        [Test]
        public void Test2()
        {
            By Droppable_link = By.LinkText("Droppable");
            By Testing_Frame = By.CssSelector(".demo-frame");

            By Draggable_el = By.Id("draggable");
            By Droppable_el = By.Id("droppable");
            var actions = new Actions(driver);

            driver.Navigate().GoToUrl("https://jqueryui.com/demos/");
            driver.FindElement(Droppable_link).Click();

            //Switch to frame and drag
            driver.SwitchTo().Frame(driver.FindElement(Testing_Frame));
            actions.DragAndDrop(driver.FindElement(Draggable_el), driver.FindElement(Droppable_el)).Perform();

            //verify text
            Assert.That(driver.FindElement(By.CssSelector("#droppable p")).Text.Contains("Dropped!"));

        }

        [Test]
        public void Test3()
        {
            Assert.True(true);
        }
    }
}