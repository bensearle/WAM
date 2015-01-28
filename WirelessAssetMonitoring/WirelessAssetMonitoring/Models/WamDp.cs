using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProductsApp.Helpers;

namespace ProductsApp.Models
{
    public class WamDp
    {
        public Guid AssetGuid { get; set; }
        public string Name { get; set; }
        public double Time { get; set; }
        public bool Digit { get; set; }
        public double Analogue { get; set; }
        public double LocLat { get; set; }
        public double LocLong { get; set; }

        public WamDp()
        {
            AssetGuid = Guid.NewGuid();
            Name = "Default";
            Time = UnixTime.Now();
            Digit = false;
            Analogue = 4.0;
            LocLong = 45.6;
            LocLat = 67.8;  
        }

        public override string ToString()
        {
            return "Guid: " + AssetGuid + " Name: " + Name + " Time: " + Time + " DI: " + Digit + " AI: " + Analogue + " Lat : " + LocLat + " Long: " + LocLong;
        }
    }
}