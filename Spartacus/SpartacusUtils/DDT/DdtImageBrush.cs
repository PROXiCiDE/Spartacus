using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ProjectCeleste.GameFiles.Tools.Bar;
using ProjectCeleste.GameFiles.Tools.Ddt;
using SpartacusUtils.Bar;
using SpartacusUtils.Helpers;
using Size = System.Windows.Size;

namespace SpartacusUtils.DDT
{
    public class DdtImageBrush
    {
        public Size ImageSize { get; set; }
        public ImageBrush Brush { get; set; }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public DdtImageBrush(BarFileReader barFileReader, BarEntry barEntry)
        {
            var bitmap = new DdtFile(barFileReader.EntryToBytes(barEntry)).Bitmap;
            
            using (var intPtr = new SafeHBitmapHandle(bitmap.GetHbitmap(), true))
            {
                var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(intPtr.DangerousGetHandle(),
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions()
                );

                ImageSize = new Size(bitmap.Width, bitmap.Height);
                bitmap.Dispose();
                Brush = new ImageBrush(bitmapSource);
            }
        }
    }
}
