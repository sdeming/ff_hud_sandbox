using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using More;

namespace More
{
  public class WindowMoreState
  {
    private User32.WINDOWPLACEMENT placement_;

    public static WindowMoreState Get(IntPtr hwnd)
    {
      var state = new WindowMoreState();

      // get placement
      state.placement_ = new User32.WINDOWPLACEMENT();
      state.placement_.length = (uint)System.Runtime.InteropServices.Marshal.SizeOf(state.placement_);
      User32.GetWindowPlacement(hwnd, ref state.placement_);

      return state;
    }

    public static void Apply(IntPtr hwnd, WindowMoreState state)
    {
      User32.SetWindowPlacement(hwnd, ref state.placement_);
    }
  }
}
