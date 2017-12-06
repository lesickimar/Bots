using System;
using System.Collections.Generic;

namespace Bots.DataBaseModels
{
    public partial class Town
    {
        public Town()
        {
            Shelter = new HashSet<Shelter>();
        }

        public int NumberId { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }

        public Province Province { get; set; }
        public ICollection<Shelter> Shelter { get; set; }
    }
}
