using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace AkalTrucking.Models
{
    public class WeeklyLoadFuel
    {
        public IList<Load> LoadsList { get; set; }
        public IList<Fuel> FuelList { get; set; }
    }
}