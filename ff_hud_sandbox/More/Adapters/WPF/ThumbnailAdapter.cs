using System;
using System.Windows;
using System.Windows.Interop;
using Core;
using Core.Types;

namespace Core.Adapters.WPF
{
  public class ThumbnailAdapter : AbstractThumbnailAdapter
  {
    private WindowInteropHelper helper_;
    private Window window_;

    public override IntPtr Handle { get { return helper_.Handle; } }
    public override double Top { get { return 0.0; /* window_.Top; */ } }
    public override double Left { get { return 0.0; /* window_.Left; */ } }
    public override double Width { get { return window_.Width; } set { window_.Width = value; } }
    public override double Height { get { return window_.Height; } set { window_.Height = value; } }
    public override double Bottom { get { return Top + Height; } }
    public override double Right { get { return Left + Width; } }

    public ThumbnailAdapter(Window window)
    {
      window_ = window;
      helper_ = new WindowInteropHelper(window_);
    }
  }
}
