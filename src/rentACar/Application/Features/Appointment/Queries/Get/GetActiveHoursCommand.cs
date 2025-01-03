using Application.Services.Repositories; 
using Core.Application.Pipelines.Caching; 
using Domain.Enums;
using MediatR; 

namespace Application.Features.Appointment.Queries.Get;

public class GetActiveHoursCommand : IRequest<List<ActiveHoursResponse>>, ICachableRequest
{
    public int BusinessId { get; set; }
    public string PersonelId { get; set; }
    public DateTime Date { get; set; }

    public bool BypassCache { get; }
    public string CacheKey => $"GetActiveHours({BusinessId}-{PersonelId}-{Date})";
    public string CacheGroupKey => "GetListAppointments";
    public TimeSpan? SlidingExpiration { get; }

    public class GetActiveHoursCommandHandler : IRequestHandler<GetActiveHoursCommand, List<ActiveHoursResponse>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IWorkingHourRepository _workingHourRepository; 
        public GetActiveHoursCommandHandler(IAppointmentRepository appointmentRepository, IWorkingHourRepository workingHourRepository)
        {
            _appointmentRepository = appointmentRepository;
            _workingHourRepository = workingHourRepository;
        }

        public async Task<List<ActiveHoursResponse>> Handle(GetActiveHoursCommand request, CancellationToken cancellationToken)
        {
            var result = new List<ActiveHoursResponse>();
            var workingHours = await _workingHourRepository.GetListAsync(x => x.BusinessId == request.BusinessId);
            var selectedDayOfWeek = ((int)request.Date.DayOfWeek + 6) % 7; // 0: Sunday, 6: Saturday
            var todaysWorkingHours = workingHours.Items.FirstOrDefault(x => x.WorkingDay == (WorkingDays)selectedDayOfWeek);

            if (todaysWorkingHours == null)
                return result;

            var workingHoursParts = todaysWorkingHours.Value.Split('–');
            var startTime = TimeSpan.Parse(workingHoursParts[0]);
            var endTime = TimeSpan.Parse(workingHoursParts[1]);

            var appointments = await _appointmentRepository.GetListAsync(
                index:0,
                size:20,
                predicate: x => x.BusinessId == request.BusinessId &&
                   x.PersonelId == request.PersonelId &&
                   x.Date.Date == request.Date.Date && (x.IsConfirmed == null || x.IsConfirmed == false));

            // Generate time slots in 30-minute intervals
            var currentTime = startTime;
            while (currentTime < endTime)
            {
                var endTimeSlot = currentTime.Add(TimeSpan.FromMinutes(30));
                var isSlotAvailable = !appointments.Items.Any(a =>
                {
                    var timeParts = a.Time.Split('-');
                    var appointmentStartTime = TimeSpan.Parse(timeParts[0]);
                    var appointmentEndTime = TimeSpan.Parse(timeParts[1]);

                    return appointmentStartTime < endTimeSlot && appointmentEndTime > currentTime;
                });

                result.Add(new ActiveHoursResponse
                {
                    Hour = $"{currentTime:hh\\:mm}-{endTimeSlot:hh\\:mm}",
                    IsAvailable = isSlotAvailable
                });

                currentTime = endTimeSlot;
            }

            return result;
        }
    }
}
