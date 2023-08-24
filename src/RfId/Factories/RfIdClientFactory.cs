using System;
using RfId.Core.Enums;
using RfId.Core.Exceptions;
using RfId.Core.Models.ConnectionInfo;
using RfId.Core.RfIdClients;

namespace RdId.Core.Factories
{
    public static class RfIdClientFactory
    {
        public static BaseRfIdClient Create(RfIdType type)
        {
            switch (type)
            {
                case RfIdType.ComPort:
                    return new ComPortRfIdClient(new ComPortInfo());
                case RfIdType.RaspiPort:
                    return new RaspiPortRfIdClient(new RaspiPortInfo());
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static BaseRfIdClient Create(BaseRfIdInfo baseRfIdData)
        {
            if (baseRfIdData.GetType() == typeof(ComPortInfo))
            {
                return new ComPortRfIdClient(baseRfIdData);
            }
            if (baseRfIdData.GetType() == typeof(RaspiPortInfo))
            {
                return new RaspiPortRfIdClient(baseRfIdData);
            }

            //if (baseGpsData.GetType() == typeof(WindowsLocationApiInfo))
            //{
            //    return new WindowsLocationApiGpsClient(baseGpsData);
            //}
            throw new UnknownTypeException(baseRfIdData.GetType());
        }
    }
}
