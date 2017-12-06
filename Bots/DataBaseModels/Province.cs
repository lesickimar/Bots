using System;
using System.Collections.Generic;

namespace Bots.DataBaseModels
{
    public partial class Province
    {
        public Province()
        {
            Town = new HashSet<Town>();
        }

        public int NumberId { get; set; }
        public string Name { get; set; }

        public ICollection<Town> Town { get; set; }
    }
}
