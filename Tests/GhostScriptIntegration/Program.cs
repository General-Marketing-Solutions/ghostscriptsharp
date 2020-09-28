using GhostscriptSharp;
using GhostscriptSharp.Settings;
using System;
using System.Drawing;
using System.IO;

namespace GhostScriptIntegration
{
    class Program
    {
        static void Main( string[] args )
        {
            WriteOut(934, new[] { 0 }  );
        }

        public static void WriteOut( int pJpegWidth = 0, int[] pages = null )
        {
            var OutputPath = AppDomain.CurrentDomain.BaseDirectory;
            var sourceFile = "test.pdf";

            var jpegFile = Path.ChangeExtension( sourceFile, "jpg" );

            var settings = new GhostscriptSettings
            {
                Page = { AllPages = false, Start = 1, End = 1 },
                Size = { Native = GhostscriptPageSizes.a2 },
                Device = GhostscriptDevices.png16m,
                Resolution = new Size( 150, 122 )
            };

            if ( pages.Length > 1 )
            {
                // PDF contains multiple pages, make each one into a jpg
                var iPageNumber = 0;
                foreach ( var page in pages )
                {
                    //FORMAT: GhostscriptWrapper.GenerateOutput(<pdf file path>, <destination jpg path>, <settings>);
                    GhostscriptWrapper.GenerateOutput( OutputPath + sourceFile, OutputPath + jpegFile.Replace( ".jpg", "_" + iPageNumber + ".jpg" ), settings );  // add page number into jpg string
                    iPageNumber++;
                    settings.Page.Start = settings.Page.End = iPageNumber + 1;
                }
            }
            else
            {
                //FORMAT: GhostscriptWrapper.GenerateOutput(<pdf file path>, <destination jpg path>, <settings>);
                GhostscriptWrapper.GenerateOutput( OutputPath + sourceFile, OutputPath + jpegFile, settings );
            }
        }
    }
}
