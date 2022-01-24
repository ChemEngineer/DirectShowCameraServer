using DirectShowCameraServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHomeServer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DirectShowCameraServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CameraImageController : ControllerBase
    {
        private readonly ILogger<CameraImageController> _logger;
        private WebCam2 cam { get; set; } = new WebCam2();
        private List<CameraImage> lstImages { get; set; } = new List<CameraImage>();

        public CameraImageController(ILogger<CameraImageController> logger)
        {
            _logger = logger;
            cam.Start();
        }

        [HttpGet]
        public IActionResult Get()
        {
            cam.Stop();
            //var path = Path.Combine("c:\\", "bmp.jpg");
            //if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            //byte[] b = System.IO.File.ReadAllBytes(path);
            cam.Start();
            if(db.lastImage != null)
                return File(db.lastImage, "image/png");

            return File(new byte[5], "image/png");

        }

    }
}
