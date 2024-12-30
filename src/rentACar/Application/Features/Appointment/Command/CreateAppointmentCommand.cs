using Application.Services.Repositories; 
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

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetListAppointments";

    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, bool>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly BusinessRules _businessRules;
        public CreateAppointmentCommandHandler(IMapper mapper, BusinessRules businessRules, IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _businessRules = businessRules;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<bool> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = _mapper.Map<Appointment>(request);
            var res = await _appointmentRepository.AddAsync(appointment);
            return res != null;
        }
    }
}