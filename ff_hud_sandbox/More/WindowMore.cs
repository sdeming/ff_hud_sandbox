using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interop;
using More.Types;

namespace More
{  
  public class WindowMore : System.Windows.Interop.IWin32Window
  {
    private Stack<WindowMoreState> states_stack_;
    public IntPtr Handle { get; private set; }

    public User32.WINDOWPLACEMENT Placement
    {
      get
      {
        var placement = new User32.WINDOWPLACEMENT();
        placement.length = (uint)System.Runtime.InteropServices.Marshal.SizeOf(placement);
        User32.GetWindowPlacement(Handle, ref placement);
        return placement;
      }
    }

    public PSIZE Size
    {
      get
      {
        var placement = Placement;
        var size = new Types.PSIZE() {
          x = placement.rcNormalPosition.Right - placement.rcNormalPosition.Left,
          y = placement.rcNormalPosition.Bottom - placement.rcNormalPosition.Top
        };
        return size;
      }
    }

    public RECT ClientRect
    {
      get
      {
        var r = new RECT();
        User32.GetClientRect(Handle, out r);
        return r;
      }
    }

    public PSIZE ClientSize
    {
      get
      {
        var r = ClientRect;
        return new PSIZE() { x = r.Right, y = r.Bottom };
      }
    }

    public String Title
    {
      get
      {
        var data = new StringBuilder(256);
        User32.GetWindowText(Handle, data, data.Capacity);
        return data.ToString();
      }
    }


    public WindowMore(IntPtr handle)
    {
      states_stack_ = new Stack<WindowMoreState>();
      Handle = handle;
    }

    public WindowMore(System.Windows.Window window)
    {
      states_stack_ = new Stack<WindowMoreState>();
      Handle = new System.Windows.Interop.WindowInteropHelper(window).Handle;
    }

    public void PushState()
    {
      states_stack_.Push(WindowMoreState.Get(Handle));
    }

    public void PopState()
    {
      if (states_stack_.Count > 0)
      {
        WindowMoreState.Apply(Handle, states_stack_.Pop());
      }
    }

    public void Hide()
    {
      var placement = Placement;
      placement.showCmd = User32.SW_HIDE;
      User32.SetWindowPlacement(Handle, ref placement);
    }

    public void Show()
    {
      var placement = Placement;
      placement.showCmd = User32.SW_SHOW;
      User32.SetWindowPlacement(Handle, ref placement);
    }

    public void Glassify()
    {
      var margins = new DWM.MARGINS() { leftWidth = -1 };
      HwndSource.FromHwnd(Handle).CompositionTarget.BackgroundColor = System.Windows.Media.Colors.Transparent;
      DWM.DwmExtendFrameIntoClientArea(Handle, ref margins);
    }
  }
}
