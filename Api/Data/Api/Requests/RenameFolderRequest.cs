namespace Api.Data.Api.Requests
{
    public class RenameFolderRequest
    {
        public string? Token { get; set; }
        public Int64? FolderId { get; set; }
        public string? NewFolderName { get; set; }

        public bool IsValid()
        {
            return Token != null && FolderId != null && NewFolderName != null;
        }
    }
}