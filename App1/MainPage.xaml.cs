using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Devices.I2c;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private I2cDevice arduio; // Used to Connect to Arduino
        private DispatcherTimer timer = new DispatcherTimer();
        public MainPage()
        {
            this.InitializeComponent();
        }
        public async void Initialiasecom()
        {
            var settings = new I2cConnectionSettings(0x40); // Slave Address of Arduino Uno 
            settings.BusSpeed = I2cBusSpeed.FastMode; // this bus has 400Khz speed
            string aqs = I2cDevice.GetDeviceSelector("I2C1"); // This will return Advanced Query String which is used to select i2c device
            var dis = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(aqs);
            arduio = await I2cDevice.FromIdAsync(dis[0].Id, settings);
            timer.Tick += Timer_Tick; // We will create an event handler 
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500); // Timer_Tick is executed every 500 milli second
            timer.Start();
        }

        private async void Timer_Tick(object sender, object e)
        {
            byte[] response = new byte[3];
            try
            {
                arduio.Read(response); // this funtion will read data from Arduino 
                SendDataArduino(response);
            }
            catch (Exception p)
            {
                Windows.UI.Popups.MessageDialog msg = new Windows.UI.Popups.MessageDialog(p.Message);
                await msg.ShowAsync(); // this will show error message(if Any)
            }
        }

        private void SendDataArduino(byte[] response)
        {
            int temp = (int)response[1];
            if (temp <= 18)
            {
                arduio.Write(Encoding.ASCII.GetBytes("si"));
            }
            else
            {
                arduio.Write(Encoding.ASCII.GetBytes("no"));
            }
        }

    }
}
