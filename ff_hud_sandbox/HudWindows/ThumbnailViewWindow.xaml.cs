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
using System.Windows.Media.Animation;
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
    public WindowMore SourceWindow { get; private set; }

    public ThumbnailViewWindow(IntPtr sourceHandle)
    {
      //WindowStyle = System.Windows.WindowStyle.None;
      InitializeComponent();      
      
      thumbnail_source_ = null;
      SourceWindow = new WindowMore(sourceHandle);

      Title = SourceWindow.Title;
      WindowStyle = System.Windows.WindowStyle.None;
      ResizeMode = System.Windows.ResizeMode.NoResize;
      BorderThickness = new Thickness(0.0);

      Left = 0.0;
      Top = 0.0;
      Width = 0.0;
      Height = 0.0;
      Opacity = 0.0;
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
      new WindowMore(this).Glassify();

      // register thumbnail source
      thumbnail_source_ = new Thumbnail(SourceWindow);
      thumbnail_source_.Register(new WindowMore(this));

      var targetTopLeft = PointToScreen(new Point(SourceWindow.Placement.rcNormalPosition.Left, SourceWindow.Placement.rcNormalPosition.Top));
      var targetBottomRight = PointToScreen(new Point(SourceWindow.Placement.rcNormalPosition.Right, SourceWindow.Placement.rcNormalPosition.Bottom));

      BeginAnimation(Window.OpacityProperty, new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(1.0))));
      BeginAnimation(Window.LeftProperty, new DoubleAnimation(targetTopLeft.X, new Duration(TimeSpan.FromSeconds(1.0))));
      BeginAnimation(Window.TopProperty, new DoubleAnimation(targetTopLeft.Y, new Duration(TimeSpan.FromSeconds(1.0))));
      BeginAnimation(Window.WidthProperty, new DoubleAnimation((targetBottomRight.X - targetTopLeft.X) / 2, new Duration(TimeSpan.FromSeconds(1.0))));
      BeginAnimation(Window.HeightProperty, new DoubleAnimation((targetBottomRight.Y - targetTopLeft.Y) / 2, new Duration(TimeSpan.FromSeconds(1.0))));
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (null != thumbnail_source_) thumbnail_source_.Update();
    }  
  }
}
  