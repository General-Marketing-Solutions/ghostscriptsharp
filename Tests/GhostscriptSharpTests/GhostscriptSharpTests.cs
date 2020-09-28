using System;
using NUnit.Framework;
using GhostscriptSharp;
using System.IO;
using GhostscriptSharp.Settings;
using System.Drawing;

namespace GhostscriptSharpTests
{
    [TestFixture]
    public class GhostscriptSharpTests
    {
        string OutputPath = AppDomain.CurrentDomain.BaseDirectory;

        private readonly string TEST_FILE_LOCATION      = "test.pdf";
        private readonly string SINGLE_FILE_LOCATION    = "output.jpg";
        private readonly string MULTIPLE_FILE_LOCATION  = "output%d.jpg";

        private readonly int MULTIPLE_FILE_PAGE_COUNT = 10;

        [Test]
        public void GenerateSinglePageThumbnail()
        {
            GhostscriptWrapper.GeneratePageThumb( OutputPath + TEST_FILE_LOCATION, OutputPath + SINGLE_FILE_LOCATION, 1, 100, 100);
            Assert.IsTrue(File.Exists( OutputPath + SINGLE_FILE_LOCATION ) );
        }

        [Test]
        public void GenerateSinglePageOutput()
        {
            var settings = new GhostscriptSettings
            {
                Page = { AllPages = false, Start = 1, End = 1 },
                Size = { Native = GhostscriptPageSizes.a2 },
                Device = GhostscriptDevices.png16m,
                Resolution = new Size( 150, 122 )
            };

            GhostscriptWrapper.GenerateOutput( OutputPath + TEST_FILE_LOCATION, OutputPath + SINGLE_FILE_LOCATION, settings );
            Assert.IsTrue( File.Exists( OutputPath + SINGLE_FILE_LOCATION ) );
        }

        [Test]
        public void GenerateMultiplePageThumbnails()
        {
            GhostscriptWrapper.GeneratePageThumbs( OutputPath + TEST_FILE_LOCATION, OutputPath + MULTIPLE_FILE_LOCATION, 1, MULTIPLE_FILE_PAGE_COUNT, 100, 100);
            for (var i = 1; i <= MULTIPLE_FILE_PAGE_COUNT; i++)
                Assert.IsTrue(File.Exists( OutputPath + String.Format("output{0}.jpg", i)));
        }

        [TearDown]
        public void Cleanup()
        {
            File.Delete(SINGLE_FILE_LOCATION);
            for (var i = 1; i <= MULTIPLE_FILE_PAGE_COUNT; i++)
                File.Delete(String.Format("output{0}.jpg", i));
        }
    }
}
