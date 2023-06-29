namespace Api.Data.Api.Requests.FolderController
{
    public class RenameFolderRequest
    {
        public string? Token { get; set; }
        public long? FolderId { get; set; }
        public string? NewFolderName { get; set; }

        public bool IsValid()
        {
            return Token != null && FolderId != null && NewFolderName != null;
        }
    }
}