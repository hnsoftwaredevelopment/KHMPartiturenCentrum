using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using Syncfusion.XlsIO;

using IStyle = Syncfusion.XlsIO.IStyle;

namespace KHM.Views;
public partial class ScoreExport : Page
{
    private readonly ScoreExportViewModel _vm;

    public ScoreExport()
    {
        InitializeComponent();
        _vm = new ScoreExportViewModel();
        DataContext = _vm;
        Loaded += ScoreExport_Loaded;
    }

    private async void ScoreExport_Loaded(object sender, RoutedEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Wait;

        try
        {
            await _vm.LoadScoresAsync();
        }
        finally
        {
            Mouse.OverrideCursor = null;
        }
    }

    private void GenerateExcelButton_Click(object sender, RoutedEventArgs e)
    {
        ExportVisibleColumnsToExcel();
    }

    private void ExportVisibleColumnsToExcel()
    {
        using (ExcelEngine excelEngine = new ExcelEngine())
        {
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Excel2016;

            IWorkbook workbook = application.Workbooks.Create(1);
            IWorksheet sheet = workbook.Worksheets[0];

            var visibleColumns = ScoresDataGrid.Columns
                .Where(c => c.Visibility == Visibility.Visible)
                .ToList();

            int headerRow = 1;
            int row = headerRow;
            int col = 1;

            // Write headers
            foreach (var column in visibleColumns)
            {
                sheet[row, col].Text = column.Header?.ToString() ?? string.Empty;
                col++;
            }

            // Write data rows
            var items = ScoresDataGrid.ItemsSource as IEnumerable<object>;
            if (items != null)
            {
                foreach (var item in items)
                {
                    row++;
                    col = 1;

                    foreach (var column in visibleColumns)
                    {
                        if (column is DataGridBoundColumn boundColumn)
                        {
                            var binding = boundColumn.Binding as System.Windows.Data.Binding;
                            var bindingPath = binding?.Path?.Path;
                            if (!string.IsNullOrEmpty(bindingPath))
                            {
                                var prop = item.GetType().GetProperty(bindingPath);
                                var value = prop?.GetValue(item, null);
                                sheet[row, col].Text = value?.ToString() ?? string.Empty;
                            }
                        }
                        else if (column is DataGridCheckBoxColumn checkColumn)
                        {
                            var binding = checkColumn.Binding as System.Windows.Data.Binding;
                            var bindingPath = binding?.Path?.Path;
                            if (!string.IsNullOrEmpty(bindingPath))
                            {
                                var prop = item.GetType().GetProperty(bindingPath);
                                var value = prop?.GetValue(item, null);
                                sheet[row, col].Text = (value is bool b && b) ? "Ja" : "Nee";
                            }
                        }
                        else
                        {
                            // fallback: probeer cell text via column's Binding.ToString()
                            sheet[row, col].Text = string.Empty;
                        }
                        col++;
                    }
                }
            }

            int totalRows = row;
            int totalCols = visibleColumns.Count;

            // Apply header style (compatible with 25.x)
            IStyle headerStyle = workbook.Styles.Add("HeaderStyle");
            headerStyle.Font.Bold = true;
            headerStyle.Color = System.Drawing.Color.LightBlue; // System.Drawing.Color gebruiken
            headerStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
            headerStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Grey_25_percent;

            IRange headerRange = sheet.Range[headerRow, 1, headerRow, totalCols];
            headerRange.CellStyle = headerStyle;

            // Convert to Excel table (IListObject) en toepassen van built-in style (TableBuiltInStyles)
            string tableName = "ScoresTable";
            IListObject table = sheet.ListObjects.Create(tableName, sheet.Range[headerRow, 1, totalRows, totalCols]);
            table.BuiltInTableStyle = TableBuiltInStyles.TableStyleMedium2; // correct enum voor 25.x

            // Autofit
            sheet.UsedRange.AutofitColumns();

            // Opslaan
            var saveDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FileName = "KHM-Partiturenoverzicht.xlsx"
            };

            if (saveDialog.ShowDialog() == true)
            {
                using (var stream = new FileStream(saveDialog.FileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.SaveAs(stream);
                }
            }

            workbook.Close();
        }
    }

}