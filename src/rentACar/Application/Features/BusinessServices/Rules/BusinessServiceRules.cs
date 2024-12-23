using System;
using Application.Features.Business.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Features.BusinessServices.Rules;

public class BusinessServiceRules : BaseBusinessRules
{
    private readonly IBusinessServiceRepository _businessServiceRepository;

    public BusinessServiceRules(IBusinessServiceRepository businessServiceRepository)
    { 
        _businessServiceRepository = businessServiceRepository;
    }

    public async Task NameExist(int businessId, string name)
    {
        var result = await _businessServiceRepository.GetAsync(x => x.Name == name && x.BusinessId == businessId);
        if (result != null)
            throw new BusinessException(BusinessMessages.CheckBusiness);
    }
}
