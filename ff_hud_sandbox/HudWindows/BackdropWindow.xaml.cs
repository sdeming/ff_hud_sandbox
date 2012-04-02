using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
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
  /// Interaction logic for BackdropWindow.xaml
  /// </summary>
  public partial class BackdropWindow : Window
  {
    public Thumbnail DesktopThumbnail { get; private set; }

    public BackdropWindow()
    {
      InitializeComponent();
      ShowInTaskbar = false;
    }

    public void Toggle()
    {
      if (Opacity > 0.0)
      {
        FadeOut(0.1);
      }
      else
      {
        FadeIn(0.1, 0.9);
      }
    }

    private void FadeIn(double duration, double opacity)
    {
      SizeToFit();
      Topmost = true;
      BeginAnimation(Window.OpacityProperty, new DoubleAnimation(opacity, new Duration(TimeSpan.FromSeconds(duration))));      
    }

    private void FadeOut(double duration)
    {
      BeginAnimation(Window.OpacityProperty, new DoubleAnimation(0.0, new Duration(TimeSpan.FromSeconds(duration))));
    }

    private void SizeToFit()
    {      
      Height = System.Windows.SystemParameters.VirtualScreenHeight;
      Width = System.Windows.SystemParameters.VirtualScreenWidth;
      Top = System.Windows.SystemParameters.VirtualScreenTop;
      Left = System.Windows.SystemParameters.VirtualScreenTop;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      //DesktopThumbnail = new Thumbnail(new CoreWindow(User32.GetShellWindow()));
      //DesktopThumbnail.Register(new CoreWindow(this));      
      FadeIn(0.1, 0.9);
    }

    private void Window_Unloaded(object sender, RoutedEventArgs e)
    {
      //DesktopThumbnail.Unregister();
    }
  }
}
