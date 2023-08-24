using System;
using NMEAParser.Extensions;
using NMEAParser.NMEAMessages.Base;


namespace NMEAParser
{
    public class NmeaParser
    {
        /// <summary>
        /// Parses a string to the NmeaMessage class.
        /// </summary>
        /// <param name="message">The nmea string that need to be parsed.</param>
        /// <returns>Returns an NmeaMessage class. If it cannot parse it will return null.</returns>
        public NmeaMessage Parse(string message)
        {
            //Console.WriteLine($"msg = {message}");
            try
            {
                if (!message.StartsWith("$"))
                {
                    return null;
                }

                var messageParts = message.RemoveAfter("*").Split(',');
                var classType = NmeaConstants.GetClassType(messageParts[0].TrimStart('$'));
                if (classType == null)
                {
                    return null;
                }
                var newInstance = (NmeaMessage)Activator.CreateInstance(classType);
                newInstance.Parse(messageParts);
                return newInstance;
            }
            catch (Exception )
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
