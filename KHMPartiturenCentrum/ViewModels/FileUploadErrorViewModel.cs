namespace KHM.ViewModels;
public partial class FileUploadErrorViewModel : ObservableObject
{
    [ObservableProperty]
    public string? fileName;

    [ObservableProperty]
    public string? reason;

    public ObservableCollection<FileUploadErrorModel>? UploadErrorFiles { get; set; }
}
