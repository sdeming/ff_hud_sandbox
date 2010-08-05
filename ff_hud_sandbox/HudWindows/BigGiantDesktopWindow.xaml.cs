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
using System.Windows.Shapes;

namespace ff_hud_sandbox.HudWindows
{
  /// <summary>
  /// Interaction logic for BigGiantDesktopWindow.xaml
  /// </summary>
  public partial class BigGiantDesktopWindow : Window
  {
    public BigGiantDesktopWindow()
    {
      InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      WindowStyle = System.Windows.WindowStyle.None;
      Width = 32000;
      Height = 32000;
      Top = 0;
      Left = 0;
    }

    private void close_button_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }
  }
}
