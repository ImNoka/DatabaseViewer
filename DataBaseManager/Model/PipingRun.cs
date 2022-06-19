using System;
using System.Collections.Generic;

namespace DataBaseManager.Model
{
    public partial class PipingRun
    {
        public string Oid { get; set; } = null!;
        public string RunName { get; set; } = null!;
        public string? ItemTag { get; set; }
        public long Npd { get; set; }
    }
}
