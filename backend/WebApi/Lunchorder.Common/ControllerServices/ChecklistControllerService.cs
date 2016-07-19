using System;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Entities.DocumentDb;

namespace Lunchorder.Common.ControllerServices
{
    public class ChecklistControllerService : IChecklistControllerService
    {
        private readonly IDocumentStore _documentStore;

        public ChecklistControllerService(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public async Task SeedUserData()
        {
            await _documentStore.UpsertDocument(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "User"
            });
        }
    }
}
