using Application.Features.UserDevices.Command.Save;
using AutoMapper; 
using Domain.Entities;

namespace Application.Features.UserDevices.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserDevice, SaveUserDeviceCommand>().ReverseMap();
    }
}

