using Syncfusion.Windows.PdfViewer;

namespace KHM.Views;

/// <summary>
/// Interaction logic for PDFView.xaml
/// </summary>
public partial class PDFView : Window
{
    public PDFView ( string _path, string _file )
    {
        InitializeComponent ( );

        pdfViewer.Load ( @_path );
    }

    #region Helper Methods
    #region FileMenuOption Tool
    private void HideFileMenuOptionTool ( string _part )
    {
        // Get the instance of the toolbar using its template name 
        DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;

        // Get the instance of the file menu button using its template name.
        ToggleButton _button = (ToggleButton)toolbar.Template.FindName("PART_FileToggleButton", toolbar);

        //Get the instance of the file menu button context menu and the item collection.
        ContextMenu FileContextMenu = _button.ContextMenu;
        foreach ( MenuItem FileMenuItem in FileContextMenu.Items )
        {
            //Get the instance of the open menu item using its template name and disable its visibility.
            if ( FileMenuItem.Name == _part )
            {
                //Set the visibility of the item to collapsed.
                FileMenuItem.Visibility = Visibility.Collapsed;
            }
        }
        //_button.Visibility=Visibility.Collapsed;
    }
    #endregion

    #region Hide Option Menu button
    private void HideOptionTool ( string _part )
    {
        // Get the instance of the toolbar using its template name 
        DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;

        // Get the instance of the file menu button using its template name.
        ToggleButton _button = (ToggleButton)toolbar.Template.FindName( _part, toolbar);

        _button.Visibility = Visibility.Collapsed;
    }
    #endregion

    #region Hide Toggle Button
    private void HideToggleButton ( string _part )
    {
        //Get the instance of the left pane using its template name 
        OutlinePane outlinePane = pdfViewer.Template.FindName("PART_OutlinePane", pdfViewer) as OutlinePane;

        //Get the instance of the thumbnail button using its template name 
        ToggleButton _button = (ToggleButton)outlinePane.Template.FindName( _part, outlinePane);

        //Set the visibility of the button to collapsed.
        _button.Visibility = Visibility.Collapsed;
    }
    #endregion

    #region Hide option in toolbar
    private void HideTool ( string _part )
    {
        //Get the instance of the toolbar using its template name.
        DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;

        //Get the instance of the open file button using its template name.
        Button _button = (Button)toolbar.Template.FindName(_part, toolbar);

        //Set the visibility of the button to collapsed.
        _button.Visibility = System.Windows.Visibility.Collapsed;
    }
    #endregion

    #region Vertical Toolbar
    private void HideVerticalToolbar ( )
    {
        // Hides the thumbnail icon. 
        pdfViewer.ThumbnailSettings.IsVisible = false;
        // Hides the bookmark icon. 
        pdfViewer.IsBookmarkEnabled = false;
        // Hides the layer icon. 
        pdfViewer.EnableLayers = false;
        // Hides the organize page icon. 
        pdfViewer.PageOrganizerSettings.IsIconVisible = false;
        // Hides the redaction icon. 
        pdfViewer.EnableRedactionTool = false;
        // Hides the form icon. 
        pdfViewer.FormSettings.IsIconVisible = false;
    }
    #endregion
    #endregion

    #region Handlers
    private void Window_Loaded ( object sender, RoutedEventArgs e )
    {
        HideFileMenuOptionTool ( "PART_OpenMenuItem" );
        HideFileMenuOptionTool ( "PART_SaveMenuItem" );
        //HideFileMenuOptionTool ( "PART_SaveAsMenuItem" );
        //HideFileMenuOptionTool ( "PART_PrintMenuItem" );
        //HideThumbnailTool ( );
        HideToggleButton ( "PART_ThumbnailButton" );
        HideOptionTool ( "PART_SelectTool" );
        HideTool ( "PART_ButtonTextSearch" );
        HideTool ( "PART_ButtonSignature" );
        HideOptionTool ( "PART_Stamp" );
        HideVerticalToolbar ( );
    }
    #endregion
}
