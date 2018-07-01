using ArduinoUploader;
using ArduinoUploader.Hardware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using UploadSketchArduino;


namespace UploadSketchArduino
{
    class Program
    {
        static void Main(string[] args)
        {
            /*   var uploader = new ArduinoSketchUploader(
                   new ArduinoSketchUploaderOptions()
                   {
                       FileName = @"C:\Users\bpalma\Desktop\ARDUINO DHT11 CASI\sketch_jun26c\sketch_jun26c.ino.standard.hex",
                       PortName = "COM5",
                       ArduinoModel = ArduinoModel.UnoR3
                   }
               );
               uploader.UploadSketch();*/
            Upload();
        }
        public static void Upload()
        {
                var installDir = @"C:\Program Files (x86)\Arduino\hardware\tools";
                
                // These files must be part of the installation.
                // They come from the Arduino installation directory arduino/hardware/tools/avr/bin
                if (!File.Exists(installDir + "\\avr\\bin\\avrdude.exe"))
                {
                    Console.WriteLine("avrdude tool not installed", "AVRUpdate error");
                    return;
                }
                if (!File.Exists(installDir + "\\avr\\etc\\avrdude.conf"))
                {
                    Console.WriteLine("avrdude config file not installed", "AVRUpdate error");
                    return;
                }
                if (!File.Exists(installDir + "\\avr\\cygwin1.dll"))
                {
                Console.WriteLine("avrdude cygwin dll not installed", "AVRUpdate error");
                    return;
                }
                if (!File.Exists(installDir + "\\avr\\bin\\libusb0.dll"))
                {
                    Console.WriteLine("avrdude usb dll not installed", "AVRUpdate error");
                    return;
                }

                // THis file is the new image to be uploaded to the Arduino board...
                if (!File.Exists(installDir + "\\avr\\AVRImage.hex"))
                {
                    Console.WriteLine("AVR image not installed", "AVRUpdate error");
                    return;
                }
                
                Console.WriteLine("DO NOT RESET OR TURN OFF TILL THIS COMPLETES)\r\n");
                Console.WriteLine("Click OK to Start", "AVR Update");

                string avrport = "COM5";
                string dir = installDir;
                dir.Replace("\\", "/");
                Process avrprog = new Process();
                StreamReader avrstdout, avrstderr;
                StreamWriter avrstdin;
                ProcessStartInfo psI = new ProcessStartInfo("cmd");


                psI.UseShellExecute = false;
                psI.RedirectStandardInput = true;
                psI.RedirectStandardOutput = true;
                psI.RedirectStandardError = true;
                psI.CreateNoWindow = true;
                avrprog.StartInfo = psI;
                avrprog.Start();
                avrstdin = avrprog.StandardInput;
                avrstdout = avrprog.StandardOutput;
                avrstderr = avrprog.StandardError;
                avrstdin.AutoFlush = true;
                //avrstdin.WriteLine(installDir + "\\avr\\avrdude.exe -Cavr/avrdude.conf -patmega328p -cstk500v1 -P" + avrport + " -b57600 -D -Uflash:w:" + dir + "/avr/AVRImage.hex:i");
                avrstdin.WriteLine("avr\\avrdude.exe -Cavr/avrdude.conf -patmega328p -cstk500v1 -P" + avrport + " -b57600 -D -Uflash:w:avr/AVRImage.hex:i");
                avrstdin.Close();
                /*textBox_Trace.Text = avrstdout.ReadToEnd();
                textBox_Trace.Text += avrstderr.ReadToEnd();*/
         }
        
    }
}
