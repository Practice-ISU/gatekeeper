namespace Api.Data.Api.Requests.FolderController
{
    public class AddFolderRequest
    {
        public string? Token { get; set; }
        public string? FolderName { get; set; }

        public bool IsValid()
        {
            return Token != null && FolderName != null;
        }
    }
}
