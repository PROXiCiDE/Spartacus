using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ProjectCeleste.GameFiles.Tools.Bar;
using ProjectCeleste.GameFiles.Tools.Ddt;
using SpartacusUtils.Bar;
using SpartacusUtils.Helpers;

namespace SpartacusUtils.DDT
{
    public class DdtImageBrush
    {
        public DdtImageBrush(BarFileSystem barFileReader, BarFileEntry barEntry)
        {
            var bitmap = new DdtFile(barFileReader.ReadEntry<byte[]>(barEntry)).Bitmap;

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

        public Size ImageSize { get; set; }
        public ImageBrush Brush { get; set; }
    }
}