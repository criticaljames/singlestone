using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace singlestone.Models.DataTransfer
{
     public class Name
    {
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class Phone
    {
        public string Number { get; set; }
        public string Type { get; set; }
    }

    public class ContactDto
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public Address Address { get; set; }
        public List<Phone> Phone { get; set; }
        public string Email { get; set; }
    }
}
