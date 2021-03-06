﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Services.Entity;
using Services.ImageTools;

namespace ImageTests2
{
    [TestClass]
    public class ColorTests
    {
        [TestMethod]
        public void GetColorRow()
        {
            var c = new ColourFinder();

            var f = new FileInfo("calibration_sheet.JPG");
            var bm = new Image<Bgr, byte>(f.FullName);

            var colourCalibration = c.FindColors(bm.Bitmap, true, 17);


            var f2 = new FileInfo("test_sequence.jpg");
            var bm2 = new Image<Bgr, byte>(f2.FullName);

            var colourRun = c.FindColors(bm2.Bitmap, false, 16);

            var json = JsonConvert.SerializeObject(colourRun);

            var music = new MusicEntry {SerialisedData = json};

            var jsonMusic = JsonConvert.SerializeObject(music);

            File.WriteAllText("colourRun.json", jsonMusic);

            var result = c.CompareColours(colourCalibration, colourRun);

            var resultImage = c.VisualiseZones(result);

            resultImage.Save("result_output.jpg", ImageFormat.Jpeg);
        }
    }
}
