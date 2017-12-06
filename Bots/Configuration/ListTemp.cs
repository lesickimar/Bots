using System;
using System.Collections.Generic;

namespace Bots.Configuration
{
    public class ListTemp
    {
        public string name { get; set; }
        public string breed { get; set; }
        public string gender { get; set; }
        public string age { get; set; }
        public string weight { get; set; }
        public string description { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public string chipNumber { get; set; }
        public int shelter_ID { get; set; }
        public int size_ID { get; set; }
        public List<byte[]> photo = new List<byte[]>();
    }
}