namespace Api.Data.Api.Requests.FolderController
{
    public class GetAllFoldersRequest
    {
        public string? Token { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Token);
        }
    }
}
