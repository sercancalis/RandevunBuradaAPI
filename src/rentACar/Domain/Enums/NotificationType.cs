using System;
using System.ComponentModel;

namespace Domain.Enums;

public enum NotificationType
{
    [Description("Tümü")]
    All = 0,
    [Description("İşletme Kaydet")]
    SaveBusiness = 1,
    [Description("Çalışan Kaydet")]
    SaveEmployee = 2,
    [Description("Randevu Talebi")]
    RequestAppointment = 3,
    [Description("Randevu Onay")]
    ConfirmAppointment = 4,
    [Description("Randevu Ret")]
    RejectAppointment = 5,
    [Description("Randevu Revize")]
    RevisedAppointment = 6,
}

