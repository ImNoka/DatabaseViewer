using System;
using System.Collections.Generic;

namespace DataBaseManager.Model
{
    public partial class PipingFluid
    {
        public string Oid { get; set; } = null!;
        public string? FluidCode { get; set; }
        public double? PressureRating { get; set; }
        public double? Temp { get; set; }
    }
}
