using System;
using Application.Features.Business.Models;
using Application.Features.BusinessServices.Rules;
using Application.Services.ImageService;
using Application.Services.Repositories;
using Application.Services.User;
using Application.Services.WorkingHourService;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.BusinessServices.Command.Create;

public class CreateServiceCommand : IRequest<CreateServiceResponse>, ICacheRemoverRequest
{
    public int BusinessId { get; set; }
    public string Name { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetListServices";

    public class CreateBusinessCommandHandler : IRequestHandler<CreateServiceCommand, CreateServiceResponse>
    {
        private readonly IBusinessServiceRepository _businessServiceRepository;
        private readonly IMapper _mapper;
        private readonly BusinessServiceRules _businessServiceRules;

        public CreateBusinessCommandHandler(IMapper mapper, IBusinessServiceRepository businessServiceRepository, BusinessServiceRules businessServiceRules)
        {
            _mapper = mapper; 
            _businessServiceRepository = businessServiceRepository;
            _businessServiceRules = businessServiceRules;
        }

        public async Task<CreateServiceResponse> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            await _businessServiceRules.NameExist(request.BusinessId, request.Name);
            var data = _mapper.Map<BusinessService>(request);
            var res = await _businessServiceRepository.AddAsync(data);
            return _mapper.Map<CreateServiceResponse>(res);
        }
    }
}