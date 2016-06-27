using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampingEntrance
{
    class Group
    {
      
        public int TentSpotId { get; private set; }
        public Person Host { get; private set; }

        public List<Person> Guests{ get; private set; }


        public Group(int tentSpotId, Person host)
        {
            this.TentSpotId = tentSpotId;
            this.Host = host;
            this.Guests = new List<Person>();
        }

        public void InsertIntoGuests(Person guest)
        {
            Guests.Add(guest);
        }

    }
}
