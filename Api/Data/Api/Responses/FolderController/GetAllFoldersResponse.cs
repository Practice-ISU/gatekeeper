using Api.Data.Api.Responses.FileController;

namespace Api.Data.Api.Responses.FolderController
{
    /// <summary>
    /// Represents a response object for getting all folders.
    /// </summary>
    public class GetAllFoldersResponse
    {
        /// <summary>
        /// Gets or sets the list of folders.
        /// </summary>
        public List<FolderDto>? Folders { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the response.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllFoldersResponse"/> class.
        /// </summary>
        /// <param name="folders">The list of folders.</param>
        /// <param name="message">The message associated with the response.</param>
        /// <param name="isSuccess">A value indicating whether the operation was successful.</param>
        public GetAllFoldersResponse(List<FolderDto>? folders, string? message, bool isSuccess)
        {
            Folders = folders;
            Message = message;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllFoldersResponse"/> class with only a message.
        /// </summary>
        /// <param name="message">The message associated with the response.</param>
        public GetAllFoldersResponse(string? message) : this(null, message, false)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllFoldersResponse"/> class with folders and message.
        /// </summary>
        /// <param name="folders">The list of folders.</param>
        /// <param name="message">The message associated with the response.</param>
        public GetAllFoldersResponse(List<FolderDto>? folders, string? message) : this(folders, message, true)
        {

        }

        /// <summary>
        /// Converts a list of <see cref="Folderservice.FolderDTO"/> to a list of <see cref="FolderDto"/>.
        /// </summary>
        /// <param name="folderDtos">The list of folder DTOs to convert.</param>
        /// <returns>The converted list of <see cref="FolderDto"/>.</returns>
        public static List<FolderDto> ConvertToFolderDtoList(Google.Protobuf.Collections.RepeatedField<Folderservice.FolderDTO> folderDtos)
        {
            var convertedList = new List<FolderDto>();

            foreach (var folderDto in folderDtos)
            {
                var convertedFolderDto = new FolderDto
                {
                    Id = folderDto.Id,
                    Name = folderDto.FolderName
                };

                convertedList.Add(convertedFolderDto);
            }

            return convertedList;
        }
    }

    /// <summary>
    /// Represents a data transfer object for a folder.
    /// </summary>
    public struct FolderDto
    {
        /// <summary>
        /// Gets or sets the ID of the folder.
        /// </summary>
        public Int64? Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the folder.
        /// </summary>
        public string? Name { get; set; }

        public FolderDto(Int64? folderId, string? folderName)
        {
            Id = folderId;
            Name = folderName;
        }
    }
}
