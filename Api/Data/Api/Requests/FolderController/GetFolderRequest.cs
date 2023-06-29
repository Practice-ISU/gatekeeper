namespace Api.Data.Api.Requests.FolderController
{
    public class GetFolderRequest
    {
        public string? Token { get; set; }
        public Int64? FolderId { get; set; }

        public bool IsValid()
        {
            return Token != null && FolderId != null;
        }
    }
}
