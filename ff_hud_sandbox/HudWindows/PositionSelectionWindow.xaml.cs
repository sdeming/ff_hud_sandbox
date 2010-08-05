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
  /// Interaction logic for PositionSelectionWindow.xaml
  /// </summary>
  public partial class PositionSelectionWindow : Window
  {
    private List<bool> selected_items_;
    public PositionSelectionWindow()
    {
      InitializeComponent();

      selected_items_ = new List<bool>();
      selectionGrid.ItemsSource = selected_items_;

      selected_items_.Add(true);

    }

  }
}
