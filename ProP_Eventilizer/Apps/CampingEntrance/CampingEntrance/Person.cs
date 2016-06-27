using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampingEntrance
{
    class Person
    {
        public int Id
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public int HostId
        {
            get;
            set;
        }

        public Person(int id,string firstname,string lastname,int hostid)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastname;
            HostId = hostid;
        }

    }
}
