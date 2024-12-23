using System;
using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class UserDevice : Entity
    {
        public string UserId { get; set; }
        public string DeviceToken { get; set; }
        public string Brand { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string ModelName { get; set; }
        public string OsVersion { get; set; }

        public UserDevice()
        {
        }

        public UserDevice(string userId, string deviceToken, string brand, string deviceName, string deviceType, string modelName, string osVersion)
        {
            UserId = userId;
            DeviceToken = deviceToken;
            Brand = brand;
            DeviceName = deviceName;
            DeviceType = deviceType;
            ModelName = modelName;
            OsVersion = osVersion;
        }
    }
}

