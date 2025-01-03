using System;
using Application.Features.Employees.Rules;
using Application.Services.Notifications;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using static Application.Features.Employees.Constants.EmployeeOperationClaims;
namespace Application.Features.Employees.Commands.Create;

public class CreateEmployeeCommand : IRequest<CreateEmployeeResponse>, ICacheRemoverRequest
{
    public string UserId { get; set; } 
    public int BusinessId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetEmployees";

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly EmployeeRules _employeeRules;
        private readonly INotificationService _notificationService;
        private readonly IBusinessRepository _businessRepository;
        public CreateEmployeeCommandHandler(IMapper mapper, IEmployeeRepository employeeRepository, EmployeeRules employeeRules, INotificationService notificationService, IBusinessRepository businessRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _employeeRules = employeeRules;
            _notificationService = notificationService;
            _businessRepository = businessRepository;
        }

        public async Task<CreateEmployeeResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeRules.EmployeeExist(request.UserId);
            await _employeeRules.CheckBussinessEmployee(request.UserId,request.BusinessId);

            var mappedData = _mapper.Map<Employee>(request);
            var res = await _employeeRepository.AddAsync(mappedData);

            if (res != null)
            {
                var business = await _businessRepository.GetAsync(x => x.Id == request.BusinessId);
                var bossId = business!.UserId;
                await _notificationService.SendNotification(new Notification
                {
                    Title = "Personel Kayıt",
                    Body = $"{business.Name} isimli işletme'de personel olarak çalışıyor musunuz?",
                    NotificationType = Domain.Enums.NotificationType.SaveBusiness,
                    SenderId = bossId,
                    ReceiverId = request.UserId,
                    Action = null,
                    ActionId = res.Id
                });
            }
            return _mapper.Map<CreateEmployeeResponse>(res);
        }
    }
}
