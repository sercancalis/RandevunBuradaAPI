using System.Globalization;
using Application.Services.Notifications;
using Application.Services.Repositories;
using Application.Services.User;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR; 

public class CreateAppointmentCommand : IRequest<bool>, ICacheRemoverRequest
{
    public int BusinessId { get; set; }  
    public string UserId { get; set; }
    public string PersonelId { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public string Services { get; set; }  
    public string UserName { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetListAppointments";

    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, bool>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly BusinessRules _businessRules;
        private readonly INotificationService _notificationService; 
        public CreateAppointmentCommandHandler(IMapper mapper, BusinessRules businessRules, IAppointmentRepository appointmentRepository, INotificationService notificationService)
        {
            _mapper = mapper;
            _businessRules = businessRules;
            _appointmentRepository = appointmentRepository;
            _notificationService = notificationService; 
        }

        public async Task<bool> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = _mapper.Map<Appointment>(request);
            var res = await _appointmentRepository.AddAsync(appointment);
            if (res != null)
            {  
                await _notificationService.SendNotification(new Notification
                {
                    Title = "Randevu Onay",
                    Body = $"{request.UserName} isimli kişi {request.Date.ToString("dd MMMM yyyy dddd", new CultureInfo("tr-TR"))} Saat {request.Time} için randevu talep etmektedir.",
                    NotificationType = Domain.Enums.NotificationType.RequestAppointment,
                    SenderId = request.UserId,
                    ReceiverId = request.PersonelId,
                    Action = null,
                    ActionId = res.Id
                });

                return true;
            }
            return false;
        }
    }
}