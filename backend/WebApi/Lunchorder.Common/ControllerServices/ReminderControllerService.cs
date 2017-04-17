using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos.Requests;
using Lunchorder.Domain.Entities.DocumentDb;
using Microsoft.AspNet.Identity;
using Reminder = Lunchorder.Domain.Dtos.Reminder;

namespace Lunchorder.Common.ControllerServices
{
    public class ReminderControllerService : IReminderControllerService
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ReminderControllerService(IDatabaseRepository databaseRepository, IMapper mapper)
        {
            if (databaseRepository == null) throw new ArgumentNullException(nameof(databaseRepository));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _databaseRepository = databaseRepository;
            _mapper = mapper;
        }

        public async Task Register(string token, ClaimsIdentity claimsIdentity)
        {
            await _databaseRepository.SavePushToken(token, claimsIdentity.GetUserId());
        }

        public async Task SetReminder(Reminder reminder, ClaimsIdentity claimsIdentity)
        {
            var dbReminder = _mapper.Map<Reminder, Domain.Entities.DocumentDb.Reminder>(reminder);
            dbReminder.Action = ActionType.AddOrUpdate;
            await _databaseRepository.SetReminder(dbReminder, claimsIdentity.GetUserId());
        }

        public async Task DeleteReminder(int type, ClaimsIdentity claimsIdentity)
        {
            var dbReminder = new Domain.Entities.DocumentDb.Reminder {Action = ActionType.Delete, Type = (ReminderType)Enum.Parse(typeof(ReminderType), type.ToString()) };
            await _databaseRepository.SetReminder(dbReminder, claimsIdentity.GetUserId());
        }
    }
}