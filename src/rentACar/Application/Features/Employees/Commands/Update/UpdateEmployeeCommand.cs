using System;
using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using static Application.Features.Employees.Constants.EmployeeOperationClaims;

namespace Application.Features.Employees.Commands.Update;

  
public class UpdateEmployeeCommand : IRequest<UpdateEmployeeResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int BusinessId { get; set; }
    public bool IsConfirmed { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetEmployees";

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeResponse>
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

        public async Task<UpdateEmployeeResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeRules.CheckBussinessEmployee(request.UserId, request.BusinessId);

            var employee = await _employeeRepository.GetAsync(x => x.UserId == request.UserId && x.IsActive);
            await _employeeRules.CheckEmployee(employee?.Id ?? 0);

            _mapper.Map(request, employee); 
            var res = await _employeeRepository.UpdateAsync(employee);
            return _mapper.Map<UpdateEmployeeResponse>(res);
        }
    }
}

