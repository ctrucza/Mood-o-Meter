using Microsoft.AspNet.SignalR;

namespace Mood_o_Meter.Hubs
{
    public class MooodHub: Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}