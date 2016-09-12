using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Lunchorder.Common.ControllerServices
{
    public class UploadControllerService : IUploadControllerService
    {
        private readonly IConfigurationService _configurationService;
        private readonly CloudBlobClient _blobClient;

        public UploadControllerService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            if (configurationService == null) throw new ArgumentNullException(nameof(configurationService));

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(configurationService.AzureStorage.ConnectionString);
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task<string> UploadImage(Collection<HttpContent> contents)
        {
            string typeValue = null;
            Stream buffer = null;
            string fileName = "";
            string fileType = "";

            foreach (var file in contents)
            {
                var contentType = file.Headers.ContentDisposition.Name.Trim('\"').ToLowerInvariant();
                if (contentType == "type")
                {
                    typeValue = (await file.ReadAsStringAsync());
                }
                else if (contentType == "file")
                {
                    fileName = file.Headers.ContentDisposition.FileName.Trim('\"');
                    fileType = file.Headers.ContentType.MediaType;
                    buffer = await file.ReadAsStreamAsync();
                }
            }

            if (buffer == null)
            {
                throw new BadRequestException("File is missing");
            }

            var maxSizeInBytes = 100000;
            if (buffer.Length > maxSizeInBytes)
            {
                throw new BadRequestException("Filesize is too large, maximum of 100Kb");
            }

            List<string> allowedFileNames = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };
            var fileNameExtension = Path.GetExtension(fileName).ToLowerInvariant();
            if (!allowedFileNames.Contains(fileNameExtension))
            {
                throw new BadRequestException($"Incorrect file extension, allowed extensions are {string.Join(",", allowedFileNames)}");
            }

            var fileNameBody = Path.GetFileNameWithoutExtension(fileName);

            const int maxLength = 10;
            if (fileNameBody.Length > maxLength)
            {
                fileNameBody = fileNameBody.Substring(0, maxLength);
            }

            fileName = $"{fileNameBody}-{Guid.NewGuid()}{fileNameExtension}";
            var url = await UploadBlobAsync(buffer, fileName, fileType, _configurationService.AzureStorage.ImageContainerName);
            return url.ToString();
        }

        /// <summary>
        /// Uploads a blob to an azure storage blob container
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="fileName"></param>
        /// <param name="fileType"></param>
        /// <param name="containerName"></param>
        /// <param name="folderPath"></param>
        /// <returns>The url of the uploaded file</returns>
        private async Task<Uri> UploadBlobAsync(Stream fileStream, string fileName, string fileType, string containerName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            blockBlob.Properties.ContentType = fileType;
            await blockBlob.UploadFromStreamAsync(fileStream);
            return blockBlob.Uri;
        }

        private string Combine(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return $"{uri1}/{uri2}";
        }
    }
}