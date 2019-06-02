using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace SpartacusUtils.Helpers
{
    public class SafeHBitmapHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        [SecurityCritical]
        public SafeHBitmapHandle(IntPtr preexistingHandle, bool ownsHandle)
            : base(ownsHandle)
        {
            SetHandle(preexistingHandle);
        }

        [DllImport("gdi32.dll")]
        public static extern int DeleteObject(IntPtr hObject);

        protected override bool ReleaseHandle()
        {
            return DeleteObject(handle) > 0;
        }
    }
}