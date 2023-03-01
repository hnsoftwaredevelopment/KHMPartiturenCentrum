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
using KHMPartiturenCentrum.Views;

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
    #region Score Menu
    #region On Click
    private void btnScores_Click(object sender, RoutedEventArgs e)
    {
        fContainer.Navigate(new System.Uri("Views/Scores.xaml", UriKind.RelativeOrAbsolute));
    }
    #endregion

    #region On Mouse Enter
    private void btnScores_MouseEnter(object sender, MouseEventArgs e)
    {
        if (Tg_Btn.IsChecked == false)
        {
            Popup.PlacementTarget = btnScores;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Partituren overzicht";
        }
    }
    #endregion

    #region On Mouse Leave
    private void btnScores_MouseLeave(object sender, MouseEventArgs e)
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion

    #region Available scores Menu
    #region On Click
    private void btnAvailableScores_Click(object sender, RoutedEventArgs e)
    {
        fContainer.Navigate(new System.Uri("Views/AvailableScores.xaml", UriKind.RelativeOrAbsolute));
    }
    #endregion

    #region On Mouse Enter
    private void btnAvailableScores_MouseEnter(object sender, MouseEventArgs e)
    {
        if (Tg_Btn.IsChecked == false)
        {
            Popup.PlacementTarget = btnFreeNumbers;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Beschikbare partituurnummers";
        }
    }
    #endregion

    #region On Mouse Leave
    private void btnAvailableScores_MouseLeave(object sender, MouseEventArgs e)
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion

    #region Licenses Menu
    #region On Click
    private void btnLicenses_Click(object sender, RoutedEventArgs e)
    {
        fContainer.Navigate(new System.Uri("Views/History.xaml", UriKind.RelativeOrAbsolute));
    }
    #endregion

    #region On Mouse Enter
    private void btnLicenses_MouseEnter(object sender, MouseEventArgs e)
    {
        if (Tg_Btn.IsChecked == false)
        {
            Popup.PlacementTarget = btnLicenses;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Toon de aangeschafte partituren per per partituur";
        }
    }
    #endregion

    #region On Mouse Leave
    private void btnLicenses_MouseLeave(object sender, MouseEventArgs e)
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion

    #region Settings
    #region On Click
    private void btnSettings_Click(object sender, RoutedEventArgs e)
    {
        fContainer.Navigate(new System.Uri("Views/Settings.xaml", UriKind.RelativeOrAbsolute));
    }
    #endregion

    #region On Mouse Enter
    private void btnSettings_MouseEnter(object sender, MouseEventArgs e)
    {
        if (Tg_Btn.IsChecked == false)
        {
            Popup.PlacementTarget = btnSettings;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Gebruikers instellingen";
        }
    }
    #endregion

    #region On Mouse Leave
    private void btnSettings_MouseLeave(object sender, MouseEventArgs e)
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion
    #endregion
}

