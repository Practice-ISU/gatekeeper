namespace Api.Data.Api.Requests
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
