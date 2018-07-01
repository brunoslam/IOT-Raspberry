using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace SoilMeasure
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Asd();
        }
        GpioPin _pin = null;
        
        public void Asd()
        {
            GpioController controller = GpioController.GetDefault();
            if (controller != null)
            {
                _pin = GpioController.GetDefault().OpenPin(21, GpioSharingMode.Exclusive);
                _pin.Write(GpioPinValue.High);
                _pin.SetDriveMode(GpioPinDriveMode.Input);
                _pin.DebounceTimeout = TimeSpan.FromMilliseconds(50);
                _pin.ValueChanged += buttonPin_ValueChanged;
            }
        }
        private void buttonPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e)
        {

            // toggle the state of the LED every time the button is pressed
            if (e.Edge == GpioPinEdge.FallingEdge)
            {
                /*ledPinValue = (ledPinValue == GpioPinValue.Low) ?
                    GpioPinValue.High : GpioPinValue.Low;
                ledPin.Write(ledPinValue);*/
                LblEsasd.Text = "entró";


            }
            else
            {
                LblEsasd.Text = "salió";
            }

            // need to invoke UI updates on the UI thread because this event
            // handler gets invoked on a separate thread.
            var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                if (e.Edge == GpioPinEdge.FallingEdge)
                {
                    /*ledEllipse.Fill = (ledPinValue == GpioPinValue.Low) ?
                        redBrush : grayBrush;
                    GpioStatus.Text = "Button Pressed";*/
                }
                else
                {
                    //GpioStatus.Text = "Button Released";
                }
            });
        }
    }
}
