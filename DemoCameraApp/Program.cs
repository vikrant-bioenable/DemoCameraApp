using OpenCvSharp;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace DemoCameraApp
{
    [StructLayout(LayoutKind.Sequential)]
    struct DEVMODE
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmFormName;
        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    }
    class Program
    {
        [DllImport("user32.dll")]
        static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        private static bool process = true;
        static ManualResetEvent _quitEvent = new ManualResetEvent(false);
        static int count = 0;
        static void Main(string[] args)
        {
            //   Console.WriteLine("Hello World!");
            StartWithWhileLoop_TO_Save_Image_20_Seconds_Interval11();
        }

        static void StartWithWhileLoop_TO_Save_Image_20_Seconds_Interval11()
        {
            try
            {
                var videoCapture = new VideoCapture(0);

                while (true)
                {
                    var frame = videoCapture.RetrieveMat();

                    if (process)
                    {   Cv2.ImShow("DemoCameraApp", frame);
                        Cv2.WaitKey(1);
                    }
                    process = !process;
                }

                // _quitEvent.WaitOne();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
