using ArduinoUploader;
using ArduinoUploader.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadSketch
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var uploader = new ArduinoSketchUploader(
                new ArduinoSketchUploaderOptions()
                {
                    FileName = @"C:\MyHexFiles\UnoHexFile.ino.hex",
                    PortName = "COM3",
                    ArduinoModel = ArduinoModel.UnoR3
                }
            );
            uploader.UploadSketch();*/
        }
        public void upload()
        {
            var uploader = new ArduinoSketchUploader(
                new ArduinoSketchUploaderOptions()
                {
                    FileName = @"C:\MyHexFiles\UnoHexFile.ino.hex",
                    PortName = "COM3",
                    ArduinoModel = ArduinoModel.UnoR3
                }
            );
            uploader.UploadSketch();
        }
    }
}
