using System;

namespace SubirSketch
{
    public class UploadSketch
    {

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
}
