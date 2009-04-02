using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.WinForm.FireEvent
{
    public delegate void carStartedEventHandler(object sender, CarEventArg e);
    class Car
    {
        private void SendCarStartedMessage()
        {
            CarEventArg e = new CarEventArg();
            e.Message = "Car Started";
            carStarted(this, e);
        }
        public void startingCar()
        {
            if (carStarted != null)
            {
                SendCarStartedMessage();
            }
        }
        public event carStartedEventHandler carStarted;


    }
}
