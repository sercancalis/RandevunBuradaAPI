using System;
using Application.Features.BusinessServices.Command.Create;
using Application.Features.BusinessServices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserDevices.Command.Save;

public class SaveUserDeviceCommand : IRequest<bool>
{
    public string UserId { get; set; }
    public string DeviceToken { get; set; }
    public string Brand { get; set; }
    public string DeviceName { get; set; }
    public string DeviceType { get; set; }
    public string ModelName { get; set; }
    public string OsVersion { get; set; }
     
    public class SaveUserDeviceCommandHandler : IRequestHandler<SaveUserDeviceCommand, bool>
    {
        private readonly IUserDeviceRepository _userDeviceRepository;
        private readonly IMapper _mapper;

        public SaveUserDeviceCommandHandler(IMapper mapper, IUserDeviceRepository userDeviceRepository)
        {
            _mapper = mapper; 
            _userDeviceRepository = userDeviceRepository;
        }

        public async Task<bool> Handle(SaveUserDeviceCommand request, CancellationToken cancellationToken)
        {
            var findUserDevice = await _userDeviceRepository.GetAsync(x => x.UserId == request.UserId);
            if (findUserDevice == null)
            {
                var mappedData = _mapper.Map<UserDevice>(request);
                var res = await _userDeviceRepository.AddAsync(mappedData);
                return res != null;
            }
            else
            {
                _mapper.Map(request, findUserDevice);
                var res = await _userDeviceRepository.UpdateAsync(findUserDevice);
                return res != null;
            }
        }
    }
}