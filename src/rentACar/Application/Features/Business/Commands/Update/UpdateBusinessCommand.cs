using System;
using Application.Features.Business.Commands.Create;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static Application.Features.Business.Constants.BusinessOperationClaims;

namespace Application.Features.Business.Commands.Update;

public class UpdateBusinessCommand : IRequest<UpdateBusinessResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Address { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetListBusiness";

    public string[] Roles => new[] { Admin, Write, Add };

    public class UpdateBusinessCommandHandler : IRequestHandler<UpdateBusinessCommand, UpdateBusinessResponse>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;
        private readonly BusinessRules _businessRules;

        public UpdateBusinessCommandHandler(IMapper mapper, BusinessRules businessRules, IBusinessRepository businessRepository)
        {
            _mapper = mapper;
            _businessRepository = businessRepository;
            _businessRules = businessRules;
        }

        public async Task<UpdateBusinessResponse> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.CheckBusiness(request.Id);

            var business = await _businessRepository.GetAsync(x => x.Id == request.Id);
            _mapper.Map(request, business); 
            var res = await _businessRepository.UpdateAsync(business);

            return _mapper.Map<UpdateBusinessResponse>(res);
        }
    }
}