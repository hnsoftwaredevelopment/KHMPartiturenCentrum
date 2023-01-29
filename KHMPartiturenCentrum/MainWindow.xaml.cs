using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace KHMPartiturenCentrum;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    #region Button Close | Restore | Minimize 
    #region Button Close
    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
    #endregion

    #region Button Restore
    private void btnRestore_Click(object sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.Normal)
            WindowState = WindowState.Maximized;
        else
            WindowState = WindowState.Normal;
    }
    #endregion

    #region Button Minimize
    private void btnMinimize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
    #endregion
    #endregion

    #region Drag Widow
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }
    #endregion

    #region MenuLeft PopupButton
    #region Run Menu
    private void btnRun_Click(object sender, RoutedEventArgs e)
    {
        fContainer.Navigate(new System.Uri("Views/Run.xaml", UriKind.RelativeOrAbsolute));
    }
    private void btnRun_MouseEnter(object sender, MouseEventArgs e)
    {
        if (Tg_Btn.IsChecked == false)
        {
            Popup.PlacementTarget = btnRun;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Generate Alure Factsheets";
        }
    }

    private void btnRun_MouseLeave(object sender, MouseEventArgs e)
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion

    #region Factsheets
    private void btnFactsheets_Click(object sender, RoutedEventArgs e)
    {
        fContainer.Navigate(new System.Uri("Views/Factsheets.xaml", UriKind.RelativeOrAbsolute));
    }

    private void btnFactsheets_MouseEnter(object sender, MouseEventArgs e)
    {
        if (Tg_Btn.IsChecked == false)
        {
            Popup.PlacementTarget = btnFactsheets;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Maintain Alure Factsheets";
        }
    }

    private void btnFactsheets_MouseLeave(object sender, MouseEventArgs e)
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion

    #region History
    private void btnHistory_Click(object sender, RoutedEventArgs e)
    {
        fContainer.Navigate(new System.Uri("Views/History.xaml", UriKind.RelativeOrAbsolute));
    }

    private void btnHistory_MouseEnter(object sender, MouseEventArgs e)
    {
        if (Tg_Btn.IsChecked == false)
        {
            Popup.PlacementTarget = btnHistory;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Show History of generated Factsheets";
        }
    }

    private void btnHistory_MouseLeave(object sender, MouseEventArgs e)
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion

    #region Settings
    private void btnSettings_Click(object sender, RoutedEventArgs e)
    {
        fContainer.Navigate(new System.Uri("Views/Settings.xaml", UriKind.RelativeOrAbsolute));
    }

    private void btnSettings_MouseEnter(object sender, MouseEventArgs e)
    {
        if (Tg_Btn.IsChecked == false)
        {
            Popup.PlacementTarget = btnSettings;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Settings for Factsheets";
        }
    }

    private void btnSettings_MouseLeave(object sender, MouseEventArgs e)
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion
}

