using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using More;
using More.Types;

namespace More
{
  public class Thumbnail
  {
    private IntPtr thumb_ptr_;
    public WindowMore SourceWindow { get; private set; }
    public WindowMore TargetWindow { get; private set; }

    public PSIZE SourceSize
    {
      get
      {
        var size = new PSIZE();
        if (null != thumb_ptr_) {
          DWM.DwmQueryThumbnailSourceSize(thumb_ptr_, out size);
        }
        return size;
      }
    }

    public Thumbnail(WindowMore source)
    {
      SourceWindow = source;
    }

    public void Unregister()
    {
      if (thumb_ptr_ == IntPtr.Zero) return;

      DWM.DwmUnregisterThumbnail(thumb_ptr_);
      thumb_ptr_ = IntPtr.Zero;
    }

    public void Register(WindowMore target)
    {
      Unregister();
      TargetWindow = target;

      if (0 != DWM.DwmRegisterThumbnail(TargetWindow.Handle, SourceWindow.Handle, out thumb_ptr_))
      {
        throw new Exception("Fix me: Unable to register thumbnail. Need to report the error.");
      }

      Update();
    }

    public void Update()
    {
      if (thumb_ptr_ == IntPtr.Zero) return;

      var sourceSize = SourceSize;
      var destSize = TargetWindow.ClientSize;
      var dest = new RECT();

      var scaleX = (double)destSize.x / (double)sourceSize.x;
      var scaleY = (double)destSize.y / (double)sourceSize.y;
      var width = ((scaleX > scaleY) ? (int)(sourceSize.x * scaleY) : destSize.x);
      var height = ((scaleY > scaleX) ? (int)(sourceSize.y * scaleX) : destSize.y);

      dest.Left = (destSize.x / 2) - (width / 2);
      dest.Top = (destSize.y / 2) - (height / 2);
      dest.Right = dest.Left + width;
      dest.Bottom = dest.Top + height;

      var props = new DWM.DWM_THUMBNAIL_PROPERTIES()
      {
        dwFlags = DWM.DWM_TNP_VISIBLE | DWM.DWM_TNP_RECTDESTINATION | DWM.DWM_TNP_OPACITY | DWM.DWM_TNP_SOURCECLIENTAREAONLY,
        fVisible = true,
        fSourceClientAreaOnly = true,
        opacity = 255,
        rcDestination = dest
      };

      DWM.DwmUpdateThumbnailProperties(thumb_ptr_, ref props);
    }

    public override string ToString()
    {
      return "FIXME"; //FIXME: get the title using hwnd.
    }
  }
}
