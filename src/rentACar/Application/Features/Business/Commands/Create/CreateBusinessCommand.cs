using Application.Features.Business.Models;
using Application.Services.ImageService;
using Application.Services.Repositories;
using Application.Services.WorkingHourService;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Business.Commands.Create;

public class CreateBusinessCommand : IRequest<bool>, ICacheRemoverRequest
{
    public string Category { get; set; }
    public string Name { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Address { get; set; }
    public List<CreateWorkingHour> WorkingHours { get; set; }
    public List<IFormFile> Images { get; set; } 

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetListBusiness"; 

    public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, bool>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;
        private readonly BusinessRules _businessRules;
        private readonly IWorkingHourService _workingHourService;
        private readonly ImageServiceBase _imageServiceBase;
        private readonly IBusinessImageRepository _businessImageRepository;

        public CreateBusinessCommandHandler(IMapper mapper, BusinessRules businessRules, IBusinessRepository businessRepository, IWorkingHourService workingHourService, ImageServiceBase imageServiceBase, IBusinessImageRepository businessImageRepository)
        {
            _mapper = mapper;
            _businessRepository = businessRepository;
            _businessRules = businessRules;
            _workingHourService = workingHourService;
            _imageServiceBase = imageServiceBase;
            _businessImageRepository = businessImageRepository;
        }

        public async Task<bool> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
        {
            var business = new Domain.Entities.Business
            {
                Category = request.Category,
                Name = request.Name,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                PhoneNumber = request.PhoneNumber,
                City = request.City,
                District = request.District,
                Address = request.Address,
                IsConfirmed = false,
            };
            var res = await _businessRepository.AddAsync(business);

            var workingHours = new List<WorkingHour>();
            foreach (var item in request.WorkingHours)
            {
                workingHours.Add(new WorkingHour
                {
                    BusinessId = res.Id,
                    WorkingDay = item.WorkingDay,
                    Value = item.Value
                }); 
            }
            await _workingHourService.SaveWorkingHours(workingHours);

            var images = new List<BusinessImage>();
            foreach (var item in request.Images)
            {
                var imageRes = await _imageServiceBase.UploadAsync(item);
                if(imageRes != null)
                    images.Add(new BusinessImage
                    {
                        BusinessId = res.Id,
                        ImageUrl = imageRes
                    });
            }

            _businessImageRepository.AddRange(images);
            
            return res != null;
        }
    }
}