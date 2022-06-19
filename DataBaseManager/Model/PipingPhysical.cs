using System;
using System.Collections.Generic;

namespace DataBaseManager.Model
{
    public partial class PipingPhysical
    {
        public string Oid { get; set; } = null!;
        public double? RunLength { get; set; }
        public double? LineWeight { get; set; }
        public double? RunDiam { get; set; }
        public double? WallThickness { get; set; }
    }
}
