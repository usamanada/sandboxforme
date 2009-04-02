using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SandBox.WinForm.FireEvent
{
    public partial class Form1 : Form
    {
        Car c = new Car();
        private int _PropertyName;
        public int PropertyName
        {
            get
            {
                return _PropertyName;
            }
            set
            {
                _PropertyName = value;
            }
        }

        public Form1()
        {
            InitializeComponent();
            c.carStarted += new carStartedEventHandler(c_carStarted);
        }

        void c_carStarted(object sender, CarEventArg e)
        {
            lblMessage.Text = e.Message;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            c.startingCar();
        }
    }
}