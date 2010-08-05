using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using More.Types;

namespace More
{
  public static class User32
  {
    public delegate bool EnumWindowsCallback(IntPtr hwnd, int lParam);

    #region PInvoke

    public const int GWL_ID = (-12);
    public const int GWL_STYLE = (-16);
    public const int GWL_EXSTYLE = (-20);

    // Window Styles 
    public const UInt32 WS_OVERLAPPED = 0;
    public const UInt32 WS_POPUP = 0x80000000;
    public const UInt32 WS_CHILD = 0x40000000;
    public const UInt32 WS_MINIMIZE = 0x20000000;
    public const UInt32 WS_VISIBLE = 0x10000000;
    public const UInt32 WS_DISABLED = 0x8000000;
    public const UInt32 WS_CLIPSIBLINGS = 0x4000000;
    public const UInt32 WS_CLIPCHILDREN = 0x2000000;
    public const UInt32 WS_MAXIMIZE = 0x1000000;
    public const UInt32 WS_CAPTION = 0xC00000;      // WS_BORDER or WS_DLGFRAME  
    public const UInt32 WS_BORDER = 0x800000;
    public const UInt32 WS_DLGFRAME = 0x400000;
    public const UInt32 WS_VSCROLL = 0x200000;
    public const UInt32 WS_HSCROLL = 0x100000;
    public const UInt32 WS_SYSMENU = 0x80000;
    public const UInt32 WS_THICKFRAME = 0x40000;
    public const UInt32 WS_GROUP = 0x20000;
    public const UInt32 WS_TABSTOP = 0x10000;
    public const UInt32 WS_MINIMIZEBOX = 0x20000;
    public const UInt32 WS_MAXIMIZEBOX = 0x10000;
    public const UInt32 WS_TILED = WS_OVERLAPPED;
    public const UInt32 WS_ICONIC = WS_MINIMIZE;
    public const UInt32 WS_SIZEBOX = WS_THICKFRAME;
    public const UInt32 TARGETWINDOW = WS_BORDER | WS_VISIBLE;

    // Extended Window Styles 
    public const UInt32 WS_EX_DLGMODALFRAME = 0x0001;
    public const UInt32 WS_EX_NOPARENTNOTIFY = 0x0004;
    public const UInt32 WS_EX_TOPMOST = 0x0008;
    public const UInt32 WS_EX_ACCEPTFILES = 0x0010;
    public const UInt32 WS_EX_TRANSPARENT = 0x0020;
    public const UInt32 WS_EX_MDICHILD = 0x0040;
    public const UInt32 WS_EX_TOOLWINDOW = 0x0080;
    public const UInt32 WS_EX_WINDOWEDGE = 0x0100;
    public const UInt32 WS_EX_CLIENTEDGE = 0x0200;
    public const UInt32 WS_EX_CONTEXTHELP = 0x0400;
    public const UInt32 WS_EX_RIGHT = 0x1000;
    public const UInt32 WS_EX_LEFT = 0x0000;
    public const UInt32 WS_EX_RTLREADING = 0x2000;
    public const UInt32 WS_EX_LTRREADING = 0x0000;
    public const UInt32 WS_EX_LEFTSCROLLBAR = 0x4000;
    public const UInt32 WS_EX_RIGHTSCROLLBAR = 0x0000;
    public const UInt32 WS_EX_CONTROLPARENT = 0x10000;
    public const UInt32 WS_EX_STATICEDGE = 0x20000;
    public const UInt32 WS_EX_APPWINDOW = 0x40000;
    public const UInt32 WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
    public const UInt32 WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
    public const UInt32 WS_EX_LAYERED = 0x00080000;
    public const UInt32 WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
    public const UInt32 WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
    public const UInt32 WS_EX_COMPOSITED = 0x02000000;
    public const UInt32 WS_EX_NOACTIVATE = 0x08000000;

    public const UInt32 SW_HIDE = 0;
    public const UInt32 SW_SHOWNORMAL = 1;
    public const UInt32 SW_NORMAL = 1;
    public const UInt32 SW_SHOWMINIMIZED = 2;
    public const UInt32 SW_SHOWMAXIMIZED = 3;
    public const UInt32 SW_MAXIMIZE = 3;
    public const UInt32 SW_SHOWNOACTIVATE = 4;
    public const UInt32 SW_SHOW = 5;
    public const UInt32 SW_MINIMIZE = 6;
    public const UInt32 SW_SHOWMINNOACTIVE = 7;
    public const UInt32 SW_SHOWNA = 8;
    public const UInt32 SW_RESTORE = 9;

    // Shell events
    public enum ShellEvents {
      HSHELL_WINDOWCREATED = 1,
      HSHELL_WINDOWDESTROYED = 2,
      HSHELL_ACTIVATESHELLWINDOW = 3,
      HSHELL_WINDOWACTIVATED = 4,
      HSHELL_GETMINRECT = 5,
      HSHELL_REDRAW = 6,
      HSHELL_TASKMAN = 7,
      HSHELL_LANGUAGE = 8,
      HSHELL_ACCESSIBILITYSTATE = 11
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPLACEMENT
    {
      public uint length;
      public uint flags;
      public uint showCmd;
      public POINT ptMinPosition;
      public POINT ptMaxPosition;
      public RECT rcNormalPosition;
    }

    [DllImport("user32.dll")]
    public static extern int EnumWindows(EnumWindowsCallback lpEnumFunc, int lParam);

    [DllImport("user32.dll", SetLastError = false)]
    public static extern IntPtr GetDesktopWindow();

    [DllImport("user32.dll")]
    public static extern IntPtr GetShellWindow();
    
    [DllImport("user32.dll")]
    public static extern void GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

    [DllImport("user32.dll")]
    public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

    #if !_WIN64
      [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
      public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);
    #else
      [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
      public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);
    #endif

    [DllImport("user32.dll")]
    public static extern UInt32 RegisterWindowMessageA(string lpString);

    [DllImport("user32.dll")]
    public static extern bool RegisterShellHookWindow(IntPtr hwnd);

    [DllImport("user32.dll")]
    public static extern bool DeregisterShellHookWindow(IntPtr hwnd);

    #endregion

    public static List<IntPtr> GetWindows(Predicate<IntPtr> filter)
    {
      var windows = new List<IntPtr>();

      EnumWindows((h, p) =>
      {
        if (filter(h)) windows.Add(h);
        return true;
      }, 0);

      return windows;
    }

  }
}