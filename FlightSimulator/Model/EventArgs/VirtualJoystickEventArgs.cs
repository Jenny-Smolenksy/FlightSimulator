﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.EventArgs
{
    /// <summary>
    /// arguments for joystick event
    /// </summary>
    public class VirtualJoystickEventArgs
    {
        public double Aileron { get; set; }
        public double Elevator { get; set; }
    }
}
