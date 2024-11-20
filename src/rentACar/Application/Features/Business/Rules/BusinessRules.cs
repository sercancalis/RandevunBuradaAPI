using System;
using Application.Features.Business.Constants;
using Application.Features.Employees.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;

public class BusinessRules : BaseBusinessRules
{
    private readonly IBusinessRepository _businessRepository;

    public BusinessRules(IBusinessRepository businessRepository)
    {
        _businessRepository = businessRepository;
    }
     
    public async Task CheckBusiness(int id)
    {
        var result = await _businessRepository.GetAsync(x => x.Id == id);
        if (result == null)
            throw new BusinessException(BusinessMessages.CheckBusiness);
    }
}
