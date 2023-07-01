﻿namespace Api.Data.Api.Responses.FileController
{
    public class GetAllFilesInFolderResponse
    {
        public string? FolderName { get; set; }
        public List<FileDtoWithUrl>? Files { get; set; }
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }

        public GetAllFilesInFolderResponse(string? folderName, List<FileDtoWithUrl>? files, string? message, bool isSuccess)
        {
            FolderName = folderName;
            Files = files;
            Message = message;
            IsSuccess = isSuccess;
        }

        public GetAllFilesInFolderResponse(string? message) : this(null,null, message, false) { }

        public GetAllFilesInFolderResponse(string? folderName, List<FileDtoWithUrl>? files, string? message) : this(folderName, files, message, false) { }


        public static List<FileDtoWithUrl> ConvertToFileDtoWithUrlList(Google.Protobuf.Collections.RepeatedField<FileService.FileDTO> fileDtos)
        {
            var convertedList = new List<FileDtoWithUrl>();

            foreach (var fileDto in fileDtos)
            {
                var convertedFileDto = new FileDtoWithUrl()
                {
                    Id = fileDto.Id,
                    FolderId = fileDto.FolderId,
                    Name = fileDto.Filename,
                    Url = fileDto.Url
                };

                convertedList.Add(convertedFileDto);
            }

            return convertedList;
        }
    }

    public struct FileDtoWithUrl
    {
        /// <summary>
        /// Gets or sets the ID of the file.
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the folder containing the file.
        /// </summary>
        public Int64 FolderId { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the file url.
        /// </summary>
        public string? Url { get; set; }
    }
}
