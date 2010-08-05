using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Adapters
{
  public abstract class AbstractThumbnailAdapter
  {
    public virtual IntPtr Handle { get; private set; }
    public virtual double Top { get; set; }
    public virtual double Left { get; set; }
    public virtual double Width { get; set; }
    public virtual double Height { get; set; }
    public virtual double Bottom { get; set; }
    public virtual double Right { get; set; }
  }
}
