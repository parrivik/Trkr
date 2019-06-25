using System;
namespace Trkr.RestServices.DataConstruct
{
    public class PersonLoader
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }

        public override string ToString()
        {
            return Name + "  " + Surname;
        }
    }
}
