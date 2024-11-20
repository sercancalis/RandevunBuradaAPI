using Application.Services.Repositories;
using Application.Services.WorkingHourService;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using static Application.Features.Business.Constants.BusinessOperationClaims;

namespace Application.Features.Business.Commands.Create;

public class CreateBusinessCommand : IRequest<CreateBusinessResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public string Name { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Address { get; set; }

    public List<WorkingHour> WorkingHours { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetListBusiness";

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, CreateBusinessResponse>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;
        private readonly BusinessRules _businessRules;
        private readonly IWorkingHourService _workingHourService;

        public CreateBusinessCommandHandler(IMapper mapper, BusinessRules businessRules, IBusinessRepository businessRepository, IWorkingHourService workingHourService)
        {
            _mapper = mapper;
            _businessRepository = businessRepository;
            _businessRules = businessRules;
            _workingHourService = workingHourService;
        }

        public async Task<CreateBusinessResponse> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
        { 
            var mappedData = _mapper.Map<Domain.Entities.Business>(request);
            var res = await _businessRepository.AddAsync(mappedData);

            await _workingHourService.SaveWorkingHours(request.WorkingHours);

            return _mapper.Map<CreateBusinessResponse>(res);
        }
    }
}