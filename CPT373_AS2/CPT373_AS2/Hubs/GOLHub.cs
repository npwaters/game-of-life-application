﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace CPT373_AS2
{
    public class GOLHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}