using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using Windows.Devices.SerialCommunication;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text;
// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace ArduinoDht11
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private I2cDevice Device;

        private Timer periodicTimer;

        private I2cDevice arduio; // Used to Connect to Arduino
        private DispatcherTimer timer = new DispatcherTimer();
        public MainPage()
        {
            this.InitializeComponent();
            //Asd();
            //Asd2();
            //this.initcomunica();
            //funca 
            Initialiasecom();
            //ConnectToArduino();
        }
        public async void asdasd()
        {
            var devices2 = await DeviceInformation.FindAllAsync();
            var arduinoDevices = devices2.Where(x => x.Name.StartsWith("Arduino")).ToArray();
        }
        public async Task Asd2()
        {
            var devices = DeviceInformation.FindAllAsync(SerialDevice.GetDeviceSelector()).AsTask();
            devices.Wait();

            foreach (var dev in devices.Result)
            {
                Debug.WriteLine(dev.Name);
            }
        }
        public async void Asd()
        {

           

            var devices = DeviceInformation.FindAllAsync(SerialDevice.GetDeviceSelector()).AsTask();
            devices.Wait();

            foreach (var dev in devices.Result)
            {
                Debug.WriteLine(dev.Name);
            }
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
            }
            catch (Exception p)
            {
                Windows.UI.Popups.MessageDialog msg = new Windows.UI.Popups.MessageDialog(p.Message);
                await msg.ShowAsync(); // this will show error message(if Any)
            }

            
            humedad.Text = response[0].ToString();
            temperatura.Text = response[1].ToString();
            SendDataArduino(response[1].ToString());
            //humedadTierra.Text = response[2].ToString();
        }

        private async void ConnectToArduino()
        {
            //Enumerate devices.
            var devices = DeviceInformation.FindAllAsync(SerialDevice.GetDeviceSelector()).AsTask();
            devices.Wait();

            //This will probably get you the connected arduino. (You can also use vendor id to be more accurate).
            var serialDevice = devices.Result.FirstOrDefault(x => x.Name == "USB Serial Device");

            if (serialDevice != null)
            {
                Debug.WriteLine("Found Arduino: " + serialDevice.Name + " " + serialDevice.Id);

                // Create a serial port.
                var serialPort = await SerialDevice.FromIdAsync(serialDevice.Id);
                serialPort.WriteTimeout = TimeSpan.FromMilliseconds(1000);
                serialPort.ReadTimeout = TimeSpan.FromMilliseconds(1000);
                serialPort.BaudRate = 9600;
                serialPort.Parity = SerialParity.None;
                serialPort.StopBits = SerialStopBitCount.One;
                serialPort.DataBits = 8;

                /* //Write to serial port.
                 DataWriter writer = new DataWriter(serialPort.OutputStream);
                 writer.WriteString("Hello World!");
                 await writer.StoreAsync();

                 //Done.
                 writer.DetachStream();*/
                timer.Tick += Timer_Tick; // We will create an event handler 
                timer.Interval = new TimeSpan(0, 0, 0, 0, 500); // Timer_Tick is executed every 500 milli second
                timer.Start();
            }
            else
            {
                Debug.WriteLine("Arduino not found!");
            }
        }
        private async void TimerConnectToArduino(object sender, object e)
        {
            byte[] response = new byte[3];
            try
            {
                arduio.Read(response); // this funtion will read data from Arduino 
            }
            catch (Exception p)
            {
                Windows.UI.Popups.MessageDialog msg = new Windows.UI.Popups.MessageDialog(p.Message);
                await msg.ShowAsync(); // this will show error message(if Any)
            }


            humedad.Text = response[0].ToString();
            temperatura.Text = response[1].ToString();
            SendDataArduino(response[1].ToString());
        }

        private void SendDataArduino(String temp) {
            int grados = Int32.Parse(temp);
            if (grados <= 18) {
                arduio.Write(Encoding.ASCII.GetBytes("si"));
            }else
            {
                arduio.Write(Encoding.ASCII.GetBytes("no"));
            }
        }

        private async void initcomunica()

        {

            var settings = new I2cConnectionSettings(0x40); // Arduino address

            settings.BusSpeed = I2cBusSpeed.StandardMode;

            string aqs = I2cDevice.GetDeviceSelector("I2C1");

            var dis = await DeviceInformation.FindAllAsync(aqs);

            Device = await I2cDevice.FromIdAsync(dis[0].Id, settings);

            periodicTimer = new Timer(this.TimerCallback, null, 0, 100); // Create a timmer

        }

        private void TimerCallback(object state)
        {
            byte[] RegAddrBuf = new byte[] { 0x40 };
            byte[] ReadBuf = new byte[5];
            try
            {
                Device.Read(ReadBuf); // read the data
            }
            catch (Exception f)
            {
                Debug.WriteLine(f.Message);
            }

            char[] cArray = System.Text.Encoding.UTF8.GetString(ReadBuf, 0, 5).ToCharArray();  // Converte  Byte to Char
            char[] tArray = System.Text.Encoding.UTF8.GetString(ReadBuf, 0, 5).ToCharArray();  // Converte  Byte to Char

            String c = new String(cArray);

            Debug.WriteLine(c);

            // refresh the screen, note Im using a textbock @ UI

            var task = this.Dispatcher.RunAsync(
                    Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        temperatura.Text = c;
                    }
                );

        }

    }
}

