using System;
using Application.Features.Employees.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using static Application.Features.Employees.Constants.EmployeeOperationClaims;
namespace Application.Features.Employees.Commands.Create;

public class CreateEmployeeCommand : IRequest<CreateEmployeeResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public string UserId { get; set; }
    public int BusinessId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetEmployees";

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly EmployeeRules _employeeRules;
        public CreateEmployeeCommandHandler(IMapper mapper, IEmployeeRepository employeeRepository, EmployeeRules employeeRules)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _employeeRules = employeeRules;
        }

        public async Task<CreateEmployeeResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeRules.EmployeeExist(request.UserId);
            await _employeeRules.CheckBussinessEmployee(request.UserId,request.BusinessId);

            var mappedData = _mapper.Map<Employee>(request);
            var res = await _employeeRepository.AddAsync(mappedData);

            return _mapper.Map<CreateEmployeeResponse>(res);
        }
    }
}
