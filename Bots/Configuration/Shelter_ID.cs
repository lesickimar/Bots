using System;
using System.Collections.Generic;
using System.Text;

namespace Bots.Configuration
{
    class Shelter_ID
    {
        public static id na_paluchu = new id(1, "Na Paluchu");





        public class id
        {
            public int ID;
            public string Name;

            public id(int _id, string _name)
            {
                ID = _id;
                Name = _name;
            }
        }
    }
}
