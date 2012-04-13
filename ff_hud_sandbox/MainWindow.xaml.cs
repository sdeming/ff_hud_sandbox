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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Permissions;
using More;

namespace ff_hud_sandbox
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public List<HudWindows.BackdropWindow> Backdrops { get; set; }
    public List<HudWindows.ThumbnailViewWindow> Thumbnails { get; set; }
    public System.IO.FileSystemWatcher Watcher { get; set; }
    public uint ShellMessageNotifyID { get; set; }

    public MainWindow()
    {
      InitializeComponent();
      
      Thumbnails = new List<HudWindows.ThumbnailViewWindow>();
      Backdrops = new List<HudWindows.BackdropWindow>();
      Backdrops.Add(new HudWindows.BackdropWindow());
      Backdrops.ForEach((b) => b.Show());

      Topmost = true;      

      fsMessages.Items.Add(String.Format("Watching: {0}\n", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)));
      fsMessages.SelectedIndex = 0;

      Watcher = new System.IO.FileSystemWatcher(System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
      Watcher.IncludeSubdirectories = true;
      Watcher.Created += (sender, e) => this.Dispatcher.Invoke(new Action<object, System.IO.FileSystemEventArgs>(FileChangedEvent), sender, e);
      Watcher.Deleted += (sender, e) => this.Dispatcher.Invoke(new Action<object, System.IO.FileSystemEventArgs>(FileChangedEvent), sender, e);
      Watcher.Changed += (sender, e) => this.Dispatcher.Invoke(new Action<object, System.IO.FileSystemEventArgs>(FileChangedEvent), sender, e);
      Watcher.Renamed += (sender, e) => this.Dispatcher.Invoke(new Action<object, System.IO.RenamedEventArgs>(FileRenamedEvent), sender, e);
      Watcher.EnableRaisingEvents = true;      
    }

    private void Window_SourceInitialized(object sender, EventArgs e)
    {
      var handle = new More.WindowMore(this).Handle;
      var src = HwndSource.FromHwnd(handle);
      src.AddHook(new HwndSourceHook(WndProc));

      ShellMessageNotifyID = User32.RegisterWindowMessageA("SHELLHOOK");
      User32.RegisterShellHookWindow(handle);
    }

    private void Window_Closed(object sender, EventArgs e)
    {
      var handle = new More.WindowMore(this).Handle;
      var src = HwndSource.FromHwnd(handle);
      src.RemoveHook(new HwndSourceHook(this.WndProc));

      User32.DeregisterShellHookWindow(handle);
    }

    public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
      if (msg == ShellMessageNotifyID) {
        var what = (User32.ShellEvents)wParam;
        shellMessages.Items.Insert(0, String.Format("shell {0} [{1}] -> {2}, {3}", msg, what.ToString(), wParam, lParam));
      }
      else {
        //shellMessages.Items.Insert(0, String.Format("shell {0},{1},{2}", ShellMessageNotifyID, wParam, lParam));
      }
      handled = false;
      return IntPtr.Zero;
    }

    private void FileChangedEvent(object sender, System.IO.FileSystemEventArgs e)
    {
      fsMessages.Items.Insert(0, String.Format("{0}: {1}", e.ChangeType.ToString(), e.FullPath));
    }

    private void FileRenamedEvent(object sender, System.IO.RenamedEventArgs e)
    {
      fsMessages.Items.Insert(0, String.Format("{0}: {1} -> {2}", e.ChangeType.ToString(), e.OldFullPath, e.FullPath));
      fsMessages.SelectedItem = null;
      fsMessages.SelectedItem = fsMessages.Items[0];
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
    }

    private void Window_Unloaded(object sender, RoutedEventArgs e)
    {
      Backdrops.ForEach((b) => b.Close());
      Thumbnails.ForEach((t) => t.Close());
    }

    private void exitButton_Click(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    private void backdropButton_Click(object sender, RoutedEventArgs e)
    {
      Topmost = false;
      Backdrops.ForEach((b) => b.Toggle());
      Topmost = true;
    }

    private void thumbnailsButton_Click(object sender, RoutedEventArgs e)
    {
      if (Thumbnails.Count > 0)
      {
        Thumbnails.ForEach((t) => t.Close());
        Thumbnails.Clear();
        return;
      }

      // Get the application windows
      Thumbnails.AddRange(
        User32.GetWindows((h) => 
          (h != new System.Windows.Interop.WindowInteropHelper(this).Handle // not the application window
          && ((User32.GetWindowLong(h, User32.GWL_STYLE) & User32.TARGETWINDOW) == User32.TARGETWINDOW))). // not sure I like this
        Select((h) => new HudWindows.ThumbnailViewWindow(h))
      );

      Thumbnails.Add(new HudWindows.ThumbnailViewWindow(User32.GetShellWindow()));

      // Show the thumbnail views
      Thumbnails.ForEach((t) => t.Show());
    }

    private void moveOffscreenButton_Click(object sender, RoutedEventArgs e)
    {
      if (Thumbnails.Count == 0) return;
      Thumbnails.ForEach((t) => t.HideSource());
      Thumbnails.Reverse();
    }

    private void moveOnscreenButton_Click(object sender, RoutedEventArgs e)
    {
      if (Thumbnails.Count == 0) return;
      Thumbnails.ForEach((t) => t.RestoreSource());
      Thumbnails.Reverse();
    }

    private void showPlacementsButton_Click(object sender, RoutedEventArgs e)
    {
      new HudWindows.PositionSelectionWindow().Show();
    }
  }
}
