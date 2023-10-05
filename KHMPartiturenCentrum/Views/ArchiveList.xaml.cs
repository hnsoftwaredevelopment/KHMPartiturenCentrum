using System;
using System.Data;
//using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using KHM.ViewModels;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Color = Syncfusion.Drawing.Color;
using PointF = Syncfusion.Drawing.PointF;
using RectangleF = Syncfusion.Drawing.RectangleF;

namespace KHM.Views;
/// <summary>
/// Interaction logic for ArchiveList.xaml
/// </summary>
public partial class ArchiveList : Page
{
    public ScoreViewModel? scores;
    public ArchiveList ( )
    {
        InitializeComponent ( );

        scores = new ScoreViewModel ( );
        ScoresDataGrid.ItemsSource = scores.Scores;
        //DataContext = scores;
    }

    private void rbChecked ( object sender, System.Windows.RoutedEventArgs e )
    {
        RadioButton rb = sender as RadioButton;

        if ( rb != null )
        {
            switch ( rb.Name.ToLower ( ) )
            {
                case "rbpersonalarchive":
                    var filteredList1 = scores.Scores.Where(x=> x.ArchiveName.Contains("Huisarchief"));
                    ScoresDataGrid.ItemsSource = filteredList1;
                    lblListTitle.Content = "Overzicht van alle partituren in het huisarchief";
                    tbFileName.Text = "Huisarchief";
                    break;
                case "rbdefaultrepertoire":
                    var filteredList2 = scores.Scores.Where(x=> x.RepertoireName.Contains("Standaard"));
                    ScoresDataGrid.ItemsSource = filteredList2;
                    lblListTitle.Content = "Overzicht het standaard repertoire, moet blijvend gekend zijn";
                    tbFileName.Text = "StandaardRepertoire";
                    break;
                case "rbpersonalchristmasarchive":
                    var filteredList3 = scores.Scores.Where(x=> x.ArchiveName.Contains("Huisarchief"));
                    filteredList3 = filteredList3.Where ( x => x.GenreName.Contains ( "Kerst" ) );
                    ScoresDataGrid.ItemsSource = filteredList3;
                    lblListTitle.Content = "Overzicht van alle kerst partituren in het huisarchief";
                    tbFileName.Text = "Kerst-Huisarchief";
                    break;
                case "rbchoirarchive":
                    ScoresDataGrid.ItemsSource = scores.Scores;
                    lblListTitle.Content = "Overzicht van alle partituren";
                    tbFileName.Text = "KHMarchief";
                    break;
                case "rbchoirchristmasarchive":
                    var filteredList5 = scores.Scores.Where(x=> x.GenreName.Contains("Kerst"));
                    ScoresDataGrid.ItemsSource = filteredList5;
                    lblListTitle.Content = "Overzicht van alle kerst partituren";
                    tbFileName.Text = "Kerst-KHMarchief";
                    break;
            }
        }
    }

    private void GeneratePDFButton_Click ( object sender, System.Windows.RoutedEventArgs e )
    {
        // Variables for logo and output filename
        var LogoPath = "../../../Resources/Images/logo.png";
        int MarginLeft = 8, MarginRight=8, MarginTop=6, MarginBottom=6;

        var OutputPath = @"C:/Data/";
        string[] _datetime = DateTime.Now.ToString().Split(' ');
        string[] _date = _datetime[0].Split("-");
        _date [ 0 ] = int.Parse ( _date [ 0 ] ).ToString ( "00" );
        _date [ 1 ] = int.Parse ( _date [ 1 ] ).ToString ( "00" );
        string[] _time = _datetime[1].Split(":");
        //var OutputFilename = $"{OutputPath}{_date[2]}{_date[1]}{_date[0]} {tbFileName.Text} {_time[0]}{_time[1]}{_time[2]}.pdf";
        var OutputFilename = $"{OutputPath}{_date[2]}{_date[1]}{_date[0]} {tbFileName.Text}.pdf";

        //Create a new PDF document.
        PdfDocument document = new PdfDocument();

        #region Page Settings
        // Add Page settings
        document.PageSettings.Orientation = PdfPageOrientation.Portrait;
        document.PageSettings.Size = PdfPageSize.A4;
        document.PageSettings.Margins.Left = MarginLeft;
        document.PageSettings.Margins.Right = MarginRight;
        document.PageSettings.Margins.Top = MarginTop;
        document.PageSettings.Margins.Bottom = MarginBottom;
        #endregion

        //Add a page to the document.
        PdfPage page = document.Pages.Add();
        PdfLayoutResult result = new PdfLayoutResult(page, new RectangleF(0, 0, page.Graphics.ClientSize.Width / 2, 95));
        PdfGraphics graphics = page.Graphics;

        #region Header Section of the PDF
        // Add Header section
        RectangleF bounds = new RectangleF(0, 0, document.Pages[0].GetClientSize().Width, 50);
        PdfPageTemplateElement header = new PdfPageTemplateElement(bounds);

        //Load the image from the disk.
        FileStream imageStream = new FileStream( LogoPath, FileMode.Open, FileAccess.Read);
        PdfBitmap image = new PdfBitmap(imageStream);

        //Draw the image in the header
        header.Graphics.DrawImage ( image, 7, 0, 40, 32 );

        //Set the standard font.
        PdfFont fontHeader = new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold);
        header.Graphics.DrawString ( lblListTitle.Content.ToString ( ), fontHeader, PdfBrushes.Black, new PointF ( 75, 10 ) );

        //Add the header at the top.
        document.Template.Top = header;

        //Declare and define the table cell style. 
        PdfGridCellStyle headerStyle = new PdfGridCellStyle();

        headerStyle.Borders.All = new PdfPen ( Color.White );

        PdfFont fontTableHeader = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
        headerStyle.Font = fontTableHeader;
        #endregion

        #region Footer Section of the PDF
        // Create a Page template that can be used as footer.
        PdfPageTemplateElement footer = new PdfPageTemplateElement ( bounds );
        PdfFont _footerFont = new PdfStandardFont(PdfFontFamily.Helvetica, 7);
        PdfBrush brush = new PdfSolidBrush(Color.Black);

        //Create page number field.
        PdfPageNumberField pageNumber = new PdfPageNumberField(_footerFont, brush);

        //Create page count field.
        PdfPageCountField count = new PdfPageCountField(_footerFont, brush);

        //Add the fields in composite fields.
        PdfCompositeField compositeField = new PdfCompositeField(_footerFont, brush, "Pagina {0} van {1}", pageNumber, count);
        compositeField.Bounds = footer.Bounds;

        //Draw the composite field in footer.
        compositeField.Draw ( footer.Graphics, new PointF ( 470, 20 ) );

        //Add the footer template at the bottom.
        document.Template.Bottom = footer;
        #endregion

        //Create a PdfGrid.
        PdfGrid pdfGrid = new PdfGrid();

        //Create a DataTable.
        DataTable dataTable = new DataTable();

        //Add columns to the DataTable.
        dataTable.Columns.Add ( "Nr" );
        dataTable.Columns.Add ( "Titel" );
        dataTable.Columns.Add ( "Componist" );
        dataTable.Columns.Add ( "Archief" );

        //Add rows to the DataTable.
        foreach ( var Score in ScoresDataGrid.Items )
        {
            dataTable.Rows.Add ( new object [ ]
            {
                ((KHM.Models.ScoreModel)Score).Score,
                ((KHM.Models.ScoreModel)Score).ScoreTitle,
                ((KHM.Models.ScoreModel)Score).Composer,
                ((KHM.Models.ScoreModel)Score).ArchiveName
            } );
        }


        //Assign pdfGrid data source to the data table.
        pdfGrid.DataSource = dataTable;

        //Set a repeating header for the table. 
        pdfGrid.RepeatHeader = true;

        // Apply the cell style for the header cells.
        for ( int i = 0 ; i < pdfGrid.Headers [ 0 ].Cells.Count ; i++ )
        {
            //Get the header cell.
            PdfGridCell headerCell = pdfGrid.Headers[0].Cells[i];

            //Apply the header style. 
            headerCell.Style = headerStyle;
        }

        //Declare and define the grid style.
        PdfGridStyle gridStyle = new PdfGridStyle();

        //Set the cell padding, which specifies the space between the border and content of the cell.
        gridStyle.CellPadding = new PdfPaddings ( 2, 2, 2, 2 );

        //Set cell spacing, which specifies the space between the adjacent cells.
        gridStyle.CellSpacing = 0;

        //Enable to adjust PDF table row width based on the text length.
        gridStyle.AllowHorizontalOverflow = true;

        //Apply style.
        pdfGrid.Style = gridStyle;

        //Initialize grid built-in style.
        PdfGridBuiltinStyleSettings tableStyleOption = new PdfGridBuiltinStyleSettings();
        tableStyleOption.ApplyStyleForBandedRows = true;
        tableStyleOption.ApplyStyleForHeaderRow = true;

        //Apply built-in table style For coloring header and Rows
        pdfGrid.ApplyBuiltinStyle ( PdfGridBuiltinStyle.GridTable4Accent1 );

        //Set properties to paginate the grid.
        PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
        layoutFormat.Break = PdfLayoutBreakType.FitPage;
        layoutFormat.Layout = PdfLayoutType.Paginate;

        //Create an instance of PdfGridRowStyle.
        PdfGridRowStyle pdfGridRowStyle = new PdfGridRowStyle();
        pdfGridRowStyle.Font = new PdfStandardFont ( PdfFontFamily.Helvetica, 8 );
        pdfGridRowStyle.TextBrush = PdfBrushes.DarkBlue;

        for ( int i = 0 ; i < pdfGrid.Rows.Count ; i++ )
        {
            pdfGrid.Rows [ i ].Style = pdfGridRowStyle;
        }

        //Create and customize the string formats.
        PdfStringFormat format=new PdfStringFormat();
        format.Alignment = PdfTextAlignment.Left;
        format.LineAlignment = PdfVerticalAlignment.Middle;

        //Apply string formatting for the whole table.
        for ( int i = 0 ; i < pdfGrid.Columns.Count ; i++ )
        {
            pdfGrid.Columns [ i ].Format = format;
        }



        //Draw grid to the page of PDF document.
        pdfGrid.Draw ( page, new Syncfusion.Drawing.PointF ( 5, 50 ) );

        //Create file stream.
        using ( FileStream outputFileStream = new FileStream ( Path.GetFullPath ( OutputFilename ), FileMode.Create, FileAccess.ReadWrite ) )
        {
            //Save the PDF document to file stream.
            document.Save ( outputFileStream );
        }
        //Close the document.
        document.Close ( true );

    }
}
