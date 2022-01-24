using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace DirectShowCameraServer
{
    public class WebCam2
    {
        private VideoCaptureDevice videoSource { get; set; }

        public WebCam2()
        {
            Start();
        }

        /// <summary>
        /// Select the first webcam and listen for new images
        /// </summary>
        public void Start()
        {
            if (videoSource != null)
                return;

            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
                return;
            if (videoSource == null)
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            for (int i = 0; i < videoDevices.Count; i++)
            {
                Console.WriteLine(videoDevices[i].MonikerString);
            }


            videoSource.NewFrame += (s, e) =>
            {
                if (DateTime.Now > lastUpdated.AddSeconds(msDelay / 1000))
                {
                    //Bitmap current = e.Frame;
                    //var path = Path.Combine("c:\\","bmp.jpg");
                    try
                    {
                        //if (File.Exists(path))
                        //    File.Delete(path);
                        //e.Frame.Save(path);
                        ImageConverter converter = new ImageConverter();
                        db.lastImage = (byte[])converter.ConvertTo(e.Frame, typeof(byte[]));
                    }
                    catch (Exception e2)
                    {

                        Console.WriteLine(e2.Message);
                    }
                    lastUpdated = DateTime.Now;
                }
            };
            videoSource.Start();
        }

        public void Stop()
        {
            try
            {
                videoSource.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private int msDelay = 1500;
        DateTime lastUpdated = DateTime.Now;
        private void video_NewFrame(object sender,
                NewFrameEventArgs eventArgs)
        {
            // get new frame
            Bitmap bitmap = eventArgs.Frame;
            if (DateTime.Now > lastUpdated.AddSeconds(msDelay / 1000))
            {

                lastUpdated = DateTime.Now;
            }
        }
    }
}
