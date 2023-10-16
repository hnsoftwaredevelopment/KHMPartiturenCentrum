namespace KHM.ViewModels;
public partial class FileUploadOkViewModel : ObservableObject
{
    [ObservableProperty]
    public string? fileName;

    public ObservableCollection<FileUploadOkModel>? FilesUploadOk { get; set; }

    //public FileUploadOkViewModel ( string _fileName )
    //{
    //    //FilesUploadOk = new ObservableCollection<FileUploadOkModel> ();
    //    FilesUploadOk.Add(new FileUploadOkModel { FileName = _fileName });
    //}
}
