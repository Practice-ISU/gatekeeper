﻿namespace Api.Data.Api.Requests.FolderController
{
    public class DeleteFolderRequest
    {
        public string? Token { get; set; }
        public long? FolderId { get; set; }

        public bool IsValid()
        {
            return Token != null && FolderId != null;
        }
    }
}