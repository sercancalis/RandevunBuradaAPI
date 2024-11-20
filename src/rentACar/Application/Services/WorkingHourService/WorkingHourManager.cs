using System;
using Application.Services.Repositories; 
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services.WorkingHourService
{
    public class WorkingHourManager: IWorkingHourService
    {
        private readonly IWorkingHourRepository _workingHourRepository; 
        public WorkingHourManager(IWorkingHourRepository workingHourRepository)
        {
            _workingHourRepository = workingHourRepository; 
        }

        public async Task SaveWorkingHours(List<WorkingHour> workingHours)
        {
            var businessId = workingHours.FirstOrDefault()?.BusinessId;
            if (businessId == null)
                throw new BusinessException("İşletme Bilgisi bulunamadı");

            var existingHours = await _workingHourRepository.GetListAsync(x => x.BusinessId == businessId);

            foreach (WorkingDays day in Enum.GetValues(typeof(WorkingDays)))
            {
                if (!workingHours.Any(x => x.WorkingDay == day))
                {
                    workingHours.Add(new WorkingHour
                    {
                        BusinessId = businessId.Value,
                        WorkingDay = day,
                        Value = "09:00 - 17:00"
                    });
                }
            }

            if (existingHours == null || !existingHours.Items.Any())
            {
                await _workingHourRepository.AddRangeAsync(workingHours);
            }
            else
            {
                // Mevcut saatleri güncelle
                foreach (var existingHour in existingHours.Items)
                {
                    var updatedHour = workingHours.FirstOrDefault(x => x.WorkingDay == existingHour.WorkingDay);
                    if (updatedHour != null)
                    {
                        existingHour.Value = updatedHour.Value;
                    }
                }

                // Eksik olan günleri mevcut listeye ekle
                var missingDays = workingHours.Where(x => !existingHours.Items.Any(y => y.WorkingDay == x.WorkingDay)).ToList();
                if (missingDays.Any())
                {
                    await _workingHourRepository.AddRangeAsync(missingDays);
                }

                await _workingHourRepository.UpdateRangeAsync(existingHours.Items);
            }
        }
    }
}

