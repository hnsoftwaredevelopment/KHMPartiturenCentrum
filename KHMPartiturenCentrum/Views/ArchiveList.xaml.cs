using System.Data;
//using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using KHMPartiturenCentrum.ViewModels;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace KHMPartiturenCentrum.Views;
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
                    break;
                case "rbdefaultrepertoire":
                    var filteredList2 = scores.Scores.Where(x=> x.RepertoireName.Contains("Standaard"));
                    ScoresDataGrid.ItemsSource = filteredList2;
                    lblListTitle.Content = "Overzicht het standaard repertoire, moet blijvend gekend zijn";
                    break;
                case "rbpersonalchristmasarchive":
                    var filteredList3 = scores.Scores.Where(x=> x.ArchiveName.Contains("Huisarchief"));
                    filteredList3 = filteredList3.Where ( x => x.GenreName.Contains ( "Kerst" ) );
                    ScoresDataGrid.ItemsSource = filteredList3;
                    lblListTitle.Content = "Overzicht van alle kerst partituren in het huisarchief";
                    break;
                case "rbchoirarchive":
                    ScoresDataGrid.ItemsSource = scores.Scores;
                    lblListTitle.Content = "Overzicht van alle partituren";
                    break;
                case "rbchoirchristmasarchive":
                    var filteredList5 = scores.Scores.Where(x=> x.GenreName.Contains("Kerst"));
                    ScoresDataGrid.ItemsSource = filteredList5;
                    lblListTitle.Content = "Overzicht van alle kerst partituren";
                    break;
            }
        }
    }

    private void GeneratePDFButton_Click ( object sender, System.Windows.RoutedEventArgs e )
    {
        //Create a new PDF document.
        PdfDocument document = new PdfDocument();
        //Add a page to the document.
        PdfPage page = document.Pages.Add();
        //Create PDF graphics for the page.
        PdfGraphics graphics = page.Graphics;
        //Set the standard font.
        PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
        //Load the image from the disk.
        //Load the image from the disk.
        FileStream imageStream = new FileStream("../../../Resources/Images/logo.png", FileMode.Open, FileAccess.Read);
        PdfBitmap image = new PdfBitmap(imageStream);
        //Draw the image
        graphics.DrawImage ( image, -10, -10, 50, 40 );
        //Draw the text.
        graphics.DrawString ( lblListTitle.Content.ToString ( ), font, PdfBrushes.Black, new PointF ( 70, 15 ) );
        //Create a PdfGrid.
        PdfGrid pdfGrid = new PdfGrid();
        //Create a DataTable.
        DataTable dataTable = new DataTable();
        //Add columns to the DataTable.
        dataTable.Columns.Add ( "Nr" );
        dataTable.Columns.Add ( "Titel" );
        dataTable.Columns.Add ( "Archief" );
        dataTable.Columns.Add ( "Duur" );
        //Add rows to the DataTable.
        foreach ( var Score in ScoresDataGrid.Items )
        {
            dataTable.Rows.Add ( new object [ ]
            {
                ((KHMPartiturenCentrum.Models.ScoreModel)Score).Score,
                ((KHMPartiturenCentrum.Models.ScoreModel)Score).ScoreTitle,
                ((KHMPartiturenCentrum.Models.ScoreModel)Score).ArchiveName,
                ((KHMPartiturenCentrum.Models.ScoreModel)Score).Duration
            } );
        }
        //dataTable.Rows.Add ( new object [ ] { "CA-1098", "Queso Cabrales", "12", "14", "1", "167" } );
        //dataTable.Rows.Add ( new object [ ] { "LJ-0192-M", "Singaporean Hokkien Fried Mee", "10", "20", "3", "197" } );
        //dataTable.Rows.Add ( new object [ ] { "SO-B909-M", "Mozzarella di Giovanni", "15", "65", "10", "956" } );
        //Assign data source.
        pdfGrid.DataSource = dataTable;
        //Draw grid to the page of PDF document.
        pdfGrid.Draw ( page, new Syncfusion.Drawing.PointF ( 5, 100 ) );

        //Create file stream.
        using ( FileStream outputFileStream = new FileStream ( Path.GetFullPath ( @"../../../Output.pdf" ), FileMode.Create, FileAccess.ReadWrite ) )
        {
            //Save the PDF document to file stream.
            document.Save ( outputFileStream );
        }
        //Close the document.
        document.Close ( true );

    }
}
