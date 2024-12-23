using System;
using Application.Features.BusinessServices.Command.Update;
using Application.Features.BusinessServices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;

namespace Application.Features.BusinessServices.Command.Update;

public class UpdateServiceCommand : IRequest<UpdateServiceResponse>, ICacheRemoverRequest
{
    public int BusinessId { get; set; }
    public string Name { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetListServices";

    public class CreateBusinessCommandHandler : IRequestHandler<UpdateServiceCommand, UpdateServiceResponse>
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

        public async Task<UpdateServiceResponse> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            await _businessServiceRules.NameExist(request.BusinessId, request.Name);
            var data = _mapper.Map<BusinessService>(request);
            var res = await _businessServiceRepository.UpdateAsync(data);
            return _mapper.Map<UpdateServiceResponse>(res);
        }
    }
}