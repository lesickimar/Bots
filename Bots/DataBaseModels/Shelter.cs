using System;
using System.Collections.Generic;

namespace Bots.DataBaseModels
{
    public partial class Shelter
    {
        public int NumberId { get; set; }
        public string Name { get; set; }
        public int? TownId { get; set; }

        public Town Town { get; set; }
    }
}
