using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using More;

namespace ff_hud_sandbox.HudWindows
{
  /// <summary>
  /// Interaction logic for ThumbnailViewWindow.xaml
  /// </summary>
  public partial class ThumbnailViewWindow : Window
  {
    private Thumbnail thumbnail_source_;
    public WinodwMore SourceWindow { get; private set; }

    public ThumbnailViewWindow(IntPtr sourceHandle)
    {
      InitializeComponent();
      thumbnail_source_ = null;
      SourceWindow = new WinodwMore(sourceHandle);
    }

    public void HideSource()
    {
      SourceWindow.PushState();
      SourceWindow.Hide();
    }

    public void RestoreSource()
    {
      SourceWindow.PopState();
      SourceWindow.Show();
    }

    private void Window_Unloaded(object sender, RoutedEventArgs e)
    {
      if (null != thumbnail_source_)
      {
        thumbnail_source_.Unregister();
        thumbnail_source_ = null;
      }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      // enable glass background
      new WinodwMore(this).Glassify();

      // register thumbnail source
      thumbnail_source_ = new Thumbnail(SourceWindow);
      thumbnail_source_.Register(new WinodwMore(this));
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (null != thumbnail_source_) thumbnail_source_.Update();
    }  
  }
}
