using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SandBox.Winform.Biztalk.Administrator;
namespace SandBox.Winform.Biztalk.Administrator.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class TestAdapterSetting
    {
        Dictionary<string, AdapterSetting> mAdapterSettings;
        public TestAdapterSetting()
        {            
            mAdapterSettings = WMIMethods.GetAdapterSettings();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        [DeploymentItem("TestAdapterSetting.xlsx")]
        [DataSource("System.Data.OleDb", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=TestAdapterSetting.xlsx;Persist Security Info=False;Mode=Read;Extended Properties=\"Excel 12.0;HDR=YES\"", "Sheet1$", DataAccessMethod.Sequential)]
        public void SendAdapterSetting_DataDriven()
        {
            string testName = TestContext.DataRow["TestName"].ToString();
            string adapterName = TestContext.DataRow["AdapterName"].ToString();
            string hostType = TestContext.DataRow["HostType"].ToString();
            bool ThirtyTwoBitOnly = (bool)TestContext.DataRow["ThirtyTwoBitOnly"];
            Direction d = (Direction)Enum.Parse(typeof(Direction), TestContext.DataRow["Direction"].ToString());
            bool Result = (bool)TestContext.DataRow["Result"];

            Console.WriteLine(testName);
            SandBox.Winform.Biztalk.Administrator.Host h = new Host();
            h.ThirtyTwoBitOnly = ThirtyTwoBitOnly;
            h.Type = hostType;
            AdapterSetting asg = mAdapterSettings[adapterName];

            Assert.Equals(Result, asg.AvaliableToHost(h, Direction.send));

        }
    }
}
