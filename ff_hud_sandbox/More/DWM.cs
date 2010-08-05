using System;
using System.Runtime.InteropServices;
using More.Types;

namespace More
{
  public static class DWM
  {
    #region PInvoke
    
    public const int DWM_TNP_RECTDESTINATION = 0x1;
    public const int DWM_TNP_RECTSOURCE = 0x2;
    public const int DWM_TNP_OPACITY = 0x4;
    public const int DWM_TNP_VISIBLE = 0x8;
    public const int DWM_TNP_SOURCECLIENTAREAONLY = 0x10;

    [Flags]
    public enum DWM_BB
    {
      Enable = 1,
      BlurRegion = 2,
      TransitionMaximized = 4
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DWM_BLURBEHIND
    {
      public DWM_BB dwFlags;
      public bool fEnable;
      public IntPtr hRgnBlur;
      public bool fTransitionOnMaximized;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DWM_THUMBNAIL_PROPERTIES
    {
      public int dwFlags;
      public RECT rcDestination;
      public RECT rcSource;
      public byte opacity;
      public bool fVisible;
      public bool fSourceClientAreaOnly;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
      public int leftWidth;
      public int rightWidth;
      public int topHeight;
      public int bottomHeight;
    }

    [DllImport("dwmapi.dll", PreserveSig = false)]
    static extern bool DwmIsCompositionEnabled();

    [DllImport("dwmapi.dll")]
    public static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);

    [DllImport("dwmapi.dll")]
    public static extern int DwmUnregisterThumbnail(IntPtr thumb);

    [DllImport("dwmapi.dll")]
    public static extern int DwmQueryThumbnailSourceSize(IntPtr thumb, out PSIZE size);

    [DllImport("dwmapi.dll")]
    public static extern int DwmUpdateThumbnailProperties(IntPtr hThumb, ref DWM_THUMBNAIL_PROPERTIES props);

    [DllImport("dwmapi.dll")]
    public static extern void DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);

    [DllImport("dwmapi.dll", PreserveSig = true)]
    public static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

    [DllImport("dwmapi.dll", PreserveSig = false)]
    public static extern void DwmGetColorizationColor(out uint ColorizationColor, [MarshalAs(UnmanagedType.Bool)]out bool ColorizationOpaqueBlend);

    [DllImport("dwmapi.dll", EntryPoint = "#104")]
    public static extern int DwmpSetColorization(UInt32 ColorizationColor, bool ColorizationOpaqueBlend, UInt32 Opacity);

    #endregion
  }
}