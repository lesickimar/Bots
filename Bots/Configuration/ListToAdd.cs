using System;
using System.Collections.Generic;
using System.Text;

namespace Bots.Configuration
{
    class ListToAdd
    {
        public int Number_ID { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Size { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Weight { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int ChipNumber { get; set; }
        public byte[] Photo { get; set; }
        public int Shelter_ID { get; set; }

        public ListToAdd(int number_id, string name, string breed, string size, string age, string gender, string weight, string description,
            DateTime datestart, DateTime dateend, int chipnumber, byte[] photo, int shelter_id)
        {
            Number_ID = number_id;
            Name = name;
            Breed = breed;
            Size = size;
            Age = age;
            Gender = gender;
            Weight = weight;
            Description = description;
            DateStart = datestart;
            DateEnd = dateend;
            ChipNumber = chipnumber;
            Photo = photo;
            Shelter_ID = shelter_id;
        }
        
    }
}
