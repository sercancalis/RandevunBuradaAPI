using System;
using Application.Features.Employees.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Features.Employees.Rules;

public class EmployeeRules: BaseBusinessRules
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeRules(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task CheckBussinessEmployee(string userId, int businessId)
    {
        var result = await _employeeRepository.GetAsync(x => x.UserId == userId && x.BusinessId == businessId && x.IsActive);
        if (result != null)
            throw new BusinessException(EmployeeMessages.EmployeeExist);
    }

    public async Task EmployeeExist(string userId)
    {
        var result = await _employeeRepository.GetAsync(x => x.UserId == userId && x.IsActive);
        if (result != null)
            throw new BusinessException(EmployeeMessages.EmployeeExist);
    }

    public async Task CheckEmployee(int id)
    {
        var result = await _employeeRepository.GetAsync(x => x.Id == id);
        if (result == null)
            throw new BusinessException(EmployeeMessages.CheckEmployee);
    }
}

