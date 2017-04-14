using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lunchorder.Api.Infrastructure.Services;
using Lunchorder.Common.Interfaces;
using Microsoft.Azure.Documents;
using Newtonsoft.Json.Linq;

namespace Lunchorder.Test.Integration.Helpers.Base {

    public class DocumentDbBase
    {
        private readonly SeedService _seedService;

        public DocumentDbBase(IDocumentStore documentStore, SeedService seedService)
        {
            if (documentStore == null) throw new ArgumentNullException(nameof(documentStore));
            if (seedService == null) throw new ArgumentNullException(nameof(seedService));
            DocumentStore = documentStore;
            _seedService = seedService;
        }

        public IDocumentStore DocumentStore { get; set; }

        public void Init()
        {
            CreateDatabase().Wait();
            CreateCollection().Wait();
            SeedStoredProcedures().Wait();
            CleanDatabase().Wait();
            SeedDocuments().Wait();
            //seedService.SeedDocuments().Wait();
            _seedService.SeedStoredProcedures().Wait();
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public async Task SeedDocuments()
        {
            var documentDirectory = Path.Combine(AssemblyDirectory, Path.Combine($"{TestConstants.SeedPathPrefix}Documents"));
            string[] documents = Directory.GetFiles(documentDirectory, "*.json");

            foreach (var document in documents)
            {
                string text = File.ReadAllText(document, Encoding.UTF8);
                JObject json = JObject.Parse(text);
                await DocumentStore.UpsertDocument(json);
            }
        }
       
        public async Task CleanDatabase()
        {
            await DocumentStore.ExecuteStoredProcedure<object>("deleteAllDocuments", "SELECT * FROM c");
        }

        private async Task CreateCollection()
        {
            //await DocumentStore.CreateCollection(true);
        }

        private async Task CreateDatabase()
        {
            await DocumentStore.CreateDatabase();
        }

        private async Task SeedStoredProcedures()
        {
            var documentDirectory = Path.Combine(AssemblyDirectory, Path.Combine($"{TestConstants.SeedPathPrefix}Stored Procedures"));
            string[] fileNames = Directory.GetFiles(documentDirectory, "*.js");

            foreach (var fileName in fileNames)
            {
                string result = File.ReadAllText(fileName, Encoding.UTF8);
                var storedProcedure = new StoredProcedure { Id = Path.GetFileNameWithoutExtension(fileName), Body = result };
                await DocumentStore.CreateStoredProcedure(storedProcedure, false, true);
            }
        }
    }
}