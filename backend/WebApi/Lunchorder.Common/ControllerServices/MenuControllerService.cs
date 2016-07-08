using System;
using System.Threading.Tasks;
using AutoMapper;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos;
using Ploeh.AutoFixture;

namespace Lunchorder.Common.ControllerServices
{
    public class MenuControllerService : IMenuControllerService
    {
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public MenuControllerService(IMapper mapper)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper;
            _fixture = new Fixture();
        }

        public async Task<bool> SendEmail()
        {
            return await Task.FromResult(_fixture.Create<bool>());
        }

        public async Task<Domain.Dtos.Menu> Get()
        {
            var docDbMenu = _fixture.Create<Domain.Entities.DocumentDb.Menu>();
            var menuDto = _mapper.Map<Domain.Entities.DocumentDb.Menu, Domain.Dtos.Menu>(docDbMenu);
            return await Task.FromResult(menuDto);
        }

        public Task Add(Menu menu)
        {
            return null;
        }

        public Task Update(Menu menu)
        {
            return null;
        }

        public Task Delete(Guid menuId)
        {
            return null;
        }
    }
}