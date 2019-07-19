using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;

namespace AppiumUIA
{
    class Program
    {
        WindowsDriver<WindowsElement> session = null;
        static void Main(string[] args)
        {
            Program program = new Program();
        }

        [SetUp] 
    public void TestInit()
    {
            StartWinAppDriverProcess();
            session = InitializeWindowsDriver();
        }

        [TearDown] 
     public void TestCleanup()
     { 
         if (session != null) 
         { 
             session.Quit(); 
             session = null; 
         } 
     }


private WindowsDriver<WindowsElement> InitializeWindowsDriver()
        {
            WindowsDriver<WindowsElement> session = null;
            try
            {
                //Initialize WindowsDriver session
                DesiredCapabilities cap = new DesiredCapabilities();
                cap.SetCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
                cap.SetCapability("deviceName", "WindowsPC");
                //Create a session to access UI elements of selected application
                session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"), cap);               
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in initializing Windows driver session. \n"+ex.Message);                
            }
            return session;                          
        }

        private void StartWinAppDriverProcess()
        {  
            try
            {
                //Start the WinAppDriver process
                Process proc = new Process();
                proc.StartInfo.FileName = @"C:\Users\PDV8COB\source\repos\AppiumUIA\bin\Debug\StartWinAppDriver.bat";
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in starting WinAppDriver process. \n"+ ex.Message);
            }
        }

        [Test] 
     public void Addition()
     { 
         session.FindElementByName("Five").Click(); 
         session.FindElementByName("Plus").Click(); 
         session.FindElementByName("Seven").Click(); 
         session.FindElementByName("Equals").Click(); 
          
         var calculatorResult = GetCalculatorResultText(); 
         Assert.AreEqual("12", calculatorResult); 
     } 
  
     [Test] 
     public void Division()
     { 
         session.FindElementByAccessibilityId("num8Button").Click(); 
         session.FindElementByAccessibilityId("num8Button").Click(); 
         session.FindElementByAccessibilityId("divideButton").Click(); 
         session.FindElementByAccessibilityId("num1Button").Click(); 
         session.FindElementByAccessibilityId("num1Button").Click(); 
         session.FindElementByAccessibilityId("equalButton").Click(); 
  
         Assert.AreEqual("8", GetCalculatorResultText()); 
     } 
  
     [Test] 
     public void Multiplication()
     { 
         session.FindElementByXPath("//Button[@Name='Nine']").Click(); 
         session.FindElementByXPath("//Button[@Name='Multiply by']").Click(); 
         session.FindElementByXPath("//Button[@Name='Nine']").Click(); 
         session.FindElementByXPath("//Button[@Name='Equals']").Click(); 
  
         Assert.AreEqual("81", GetCalculatorResultText()); 
     } 
  
     [Test] 
     public void Subtraction()
     { 
         session.FindElementByXPath("//Button[@AutomationId='num9Button']").Click(); 
         session.FindElementByXPath("//Button[@AutomationId='minusButton']").Click(); 
         session.FindElementByXPath("//Button[@AutomationId='num1Button']").Click(); 
         session.FindElementByXPath("//Button[@AutomationId='equalButton']").Click(); 
  
         Assert.AreEqual("8", GetCalculatorResultText()); 
     } 
  
     [Test] 
     [TestCase("One", "Plus", "Seven", "8")] 
     [TestCase("Nine", "Minus", "One", "8")] 
     [TestCase("Eight", "Divide by", "Eight", "1")] 
     [TestCase("One", "Multiply by", "Seven", "7")]
     public void Templatized(string input1, string operation, string input2, string expectedResult)
     { 
         session.FindElementByName(input1).Click(); 
         session.FindElementByName(operation).Click(); 
         session.FindElementByName(input2).Click(); 
         session.FindElementByName("Equals").Click(); 
  
         Assert.AreEqual(expectedResult, GetCalculatorResultText()); 
     } 
  
     private string GetCalculatorResultText()
     { 
         return session.FindElementByAccessibilityId("CalculatorResults").Text.Replace("Display is", string.Empty).Trim(); 
     } 

    }
}
