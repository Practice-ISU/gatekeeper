namespace Api.Data.Api.Responses.FileController
{
    /// <summary>
    /// Represents a response object containing a list of file information in Base64 format for a specific folder.
    /// </summary>
    public class GetAllFilesInFolderBase64Response
    {
        /// <summary>
        /// Gets or sets the list of file DTOs.
        /// </summary>
        public List<FileDto>? Files { get; set; }

        /// <summary>
        /// Gets or sets the response message.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllFilesInFolderBase64Response"/> class.
        /// </summary>
        /// <param name="files">The list of file DTOs.</param>
        /// <param name="message">The response message.</param>
        /// <param name="isSuccess">A value indicating whether the operation was successful.</param>
        public GetAllFilesInFolderBase64Response(List<FileDto>? files, string? message, bool isSuccess)
        {
            Files = files;
            Message = message;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllFilesInFolderBase64Response"/> class with only a response message.
        /// </summary>
        /// <param name="message">The response message.</param>
        public GetAllFilesInFolderBase64Response(string? message) : this(null, message, false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllFilesInFolderBase64Response"/> class with specified file DTOs and a response message indicating success.
        /// </summary>
        /// <param name="files">The list of file DTOs.</param>
        /// <param name="message">The response message.</param>
        public GetAllFilesInFolderBase64Response(List<FileDto>? files, string? message) : this(files, message, false) { }

        /// <summary>
        /// Converts a list of Protobuf FileBase64 objects to a list of FileDto objects.
        /// </summary>
        /// <param name="fileDtos">The list of Protobuf FileBase64 objects.</param>
        /// <returns>The converted list of FileDto objects.</returns>
        public static List<FileDto> ConvertToFileDtoList(Google.Protobuf.Collections.RepeatedField<FileService.FileBase64> fileDtos)
        {
            var convertedList = new List<FileDto>();

            foreach (var fileDto in fileDtos)
            {
                var convertedFileDto = new FileDto()
                {
                    Id = fileDto.Id,
                    FolderId = fileDto.FolderId,
                    Name = fileDto.FileName,
                    Base64 = fileDto.Base64
                };

                convertedList.Add(convertedFileDto);
            }

            return convertedList;
        }
    }

    /// <summary>
    /// Represents a Data Transfer Object (DTO) for file information.
    /// </summary>
    public struct FileDto
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
        /// Gets or sets the file content in Base64 format.
        /// </summary>
        public string? Base64 { get; set; }
    }
}
