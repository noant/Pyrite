using HierarchicalData;
using System;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Uni.Initialize();
            //SAL.SetDefaults();

            //Pool.ActionItems.Add(
            //    new ActionItem()
            //    {
            //        Action = new TestAction(),
            //        Checker = new TestChecker(),
            //        Name = "name1",
            //        ServerCommand = "command1",
            //        UseServerThreading = true
            //    }
            //);

            //SAL.Save();


            HierarchicalData.HierarchicalObject a = new HierarchicalData.HierarchicalObject();
            a[1] = 2;
            a[3] = DateTime.Now;

            var b = a.ToString();
            var c = HierarchicalObject.FromXml(b);
            var cc = HierarchicalObject.FromXml(b);

        }
    }
}
