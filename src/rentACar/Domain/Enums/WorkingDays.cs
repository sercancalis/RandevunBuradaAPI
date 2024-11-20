using System;
using System.ComponentModel;

namespace Domain.Enums
{
    public enum WorkingDays
    {
        [Description("Pazartesi")]
        Mon = 0,
        [Description("Salı")]
        Tue = 1,
        [Description("Çarşamba")]
        Wed = 2,
        [Description("Perşembe")]
        Thu = 3,
        [Description("Cuma")]
        Fri = 4,
        [Description("Cumartesi")]
        Sat = 5,
        [Description("Pazar")]
        Sun = 6
    }
}

