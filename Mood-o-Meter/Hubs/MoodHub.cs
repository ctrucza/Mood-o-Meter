﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Mood_o_Meter.Hubs
{
    public class MoodHub: Hub
    {
        public void Hello()
        {
            Clients.All.hello2();
        }
    }
}