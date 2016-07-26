using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Dal;
using Microsoft.Azure.Documents;
using Newtonsoft.Json.Linq;

namespace Lunchorder.Api.Infrastructure.Services
{
    public class SeedService
    {
        private readonly IDocumentStore _documentStore;

        public SeedService(IDocumentStore documentStore)
        {
            if (documentStore == null) throw new ArgumentNullException(nameof(documentStore));
            _documentStore = documentStore;
        }

        /// <summary>
        /// On startup, this will make sure basic data and stored procedures are updated
        /// </summary>
        public async Task SeedStoredProcedures()
        {
            var assembly = Assembly.GetAssembly(typeof(DocumentDbRepository));
            var storedProcedures = assembly.GetStoredProcedures(".Seed.Stored_Procedures", ".js");

            foreach (var storedProcedure in storedProcedures)
            {
                await _documentStore.CreateStoredProcedure(storedProcedure, false, true);
            }
        }

        /// <summary>
        /// On startup, this will make sure documents are seeded but not updated
        /// </summary>
        public async Task SeedDocuments()
        {
            var assembly = Assembly.GetAssembly(typeof(DocumentDbRepository));
            var documents = assembly.GetDocuments(".Seed.Documents", ".json");

            foreach (var document in documents)
            {
                JObject json = JObject.Parse(document);
                await _documentStore.UpsertDocumentIfNotExists(json["id"].ToString(), document);
            }
        }

        public async Task SetIndexes()
        {
            await _documentStore.SetIndexMode(IndexingMode.Lazy);
            await _documentStore.CreateIndex(new IncludedPath
            {
                Path = "/*/id",
                Indexes = new Collection<Index>
                {
                    new HashIndex(DataType.String),
                    new RangeIndex(DataType.String)
                }
            });
        }
    }

    public static class SeedHelper
    {
        /// <summary>
        /// Retrieves all resource files from a folder
        /// </summary>
        /// <param name="folderPath">Path of the folder (eg: ".Resources.Folder")</param>
        /// <param name="fileExtension">Extension of the files to scan (eg: ".js")</param>
        /// <returns></returns>
        public static string[] GetResourceFiles(this Assembly assembly, string folderPath, string fileExtension)
        {
            string folderName = $"{assembly.GetName().Name}{folderPath}";

            return assembly
                .GetManifestResourceNames()
                .Where(r => r.StartsWith(folderName) && r.EndsWith(fileExtension))
                .Select(x => x)
                .ToArray();
        }

        public static IEnumerable<string> GetDocuments(this Assembly assemblyToSearch, string folderPath, string extension)
        {
            var filePaths = assemblyToSearch.GetResourceFiles(folderPath, extension);

            var storedProcedures = new List<string>();

            foreach (var filePath in filePaths)
            {
                using (Stream stream = assemblyToSearch.GetManifestResourceStream(filePath))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                    storedProcedures.Add(result);
                }
            }
            return storedProcedures;
        }

        public static IEnumerable<StoredProcedure> GetStoredProcedures(this Assembly assemblyToSearch, string folderPath, string extension)
        {
            var filePaths = assemblyToSearch.GetResourceFiles(folderPath, extension);

            var storedProcedures = new List<StoredProcedure>();

            foreach (var filePath in filePaths)
            {
                var splittedFilePath = filePath.Split('.');
                var segments = splittedFilePath.Count() - 1;
                string fileName = $"{splittedFilePath[segments - 1]}.{splittedFilePath[segments]}";

                using (Stream stream = assemblyToSearch.GetManifestResourceStream(filePath))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                    storedProcedures.Add(new StoredProcedure { Id = fileName.Split('.')[0], Body = result });
                }
            }
            return storedProcedures;
        }
    }
}