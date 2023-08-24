using System;
using RfId.Core.Enums;
using RfId.Core.Models.ConnectionInfo;

namespace RfID.Core.Factories
{
    public static class GpsDataFactory
    {
        public static BaseRfIdInfo Create(RfIdType type)
        {
            var dataType = GetDataType(type);
            return (BaseRfIdInfo)Activator.CreateInstance(dataType);
        }

        public static Type GetDataType(RfIdType type)
        {
            switch (type)
            {
                case RfIdType.ComPort:
                    return typeof(ComPortInfo);
                case RfIdType.RaspiPort:
                    return typeof(RaspiPortInfo);
                //case GpsType.WindowsLocationApi:
                //    return typeof(WindowsLocationApiInfo);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
