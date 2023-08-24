using CommunityToolkit.Mvvm.Messaging.Messages;

namespace IvisMaui.Models
{
    public class SelectBusMessage : ValueChangedMessage<Bus>
    {
        public SelectBusMessage(Bus bus) : base(bus)
        {

        }
    }
}
