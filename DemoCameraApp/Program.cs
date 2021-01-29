using Microsoft.Win32;
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

        static bool Is_Windows_Locked = false;
        static void Main(string[] args)
        {
            //// DELEGATE TO CAPTURE WINDOWS LOCK UNLOCK EVENTS
            Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);


            //   Console.WriteLine("Hello World!");
            StartWithWhileLoop_TO_Save_Image_20_Seconds_Interval11();
        }

        static void StartWithWhileLoop_TO_Save_Image_20_Seconds_Interval11()
        {
            try
            {



                var videoCapture = new VideoCapture(0);
                bool CameraFlag = false;
                while (true)
                {
                    if (Is_Windows_Locked == true)
                    {
                        try
                        {
                            videoCapture.Release();
                        }
                        catch (Exception)
                        {
                        }
                        CameraFlag = true;
                        goto EndOfWhile;
                    }
                    else
                    {
                        if (CameraFlag == true)
                        {
                            CameraFlag = false;
                            videoCapture = new VideoCapture(0);

                            //Thread.Sleep(10000);
                        }

                    }

                    var frame = videoCapture.RetrieveMat();

                    if (process)
                    {  //OPENCV_VIDEOIO_PRIORITY_MSMF=0

                     //   Cv2
                        
                        Cv2.ImShow("DemoCameraApp", frame);
                        Cv2.WaitKey(1);
                    }
                    process = !process;

                EndOfWhile:

                    string str = "";
                }

                // _quitEvent.WaitOne();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        static void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            try
            {
                if (e.Reason == SessionSwitchReason.SessionLock)
                {
                    Is_Windows_Locked = true;
                    //////I left my desk
                    ////loger.WriteLog("sts", "Windows Locked");
                }
                else if (e.Reason == SessionSwitchReason.SessionUnlock)
                {
                    Is_Windows_Locked = false;
                    ////////I returned to my desk
                    //////loger.WriteLog("sts", "Windows UnLocked");
                }
            }
            catch (Exception ex)
            {
               // loger.WriteLog("err", "In SystemEvents_SessionSwitch - " + ex.Message);
            }
        }


    }
}
