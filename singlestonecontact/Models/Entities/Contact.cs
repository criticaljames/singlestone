using singlestone.Models.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace singlestone.Models.Entities
{
    public class Name
    {
        public Name()
        {
        }

        public Name( string firstName, string middleName, string lastName )
        {
            this.First  = firstName;
            this.Middle = middleName;
            this.Last   = lastName;
        }

        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
    }

    public class Address
    {
        public Address()
        {
        }

        public Address( string street, string city, string state, string postalCode )
        {
            this.Street = street;
            this.City   = city;
            this.State  = state;
            this.Zip    = postalCode;
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class Phone
    {
        public Phone()
        {
        }

        public Phone( string number, string type )
        {
            this.Number = number;
            this.Type   = type;
        }

        public string Number { get; set; }
        public string Type { get; set; }
    }

    public class Contact
    {
        public Contact()
        {
        }

        public Contact( ContactDto contact )
        {
            if ( contact != null )
            {
                this.Name = new Name( contact?.Name.First, contact?.Name.Middle, contact?.Name.Last );
                this.Address = new Address( contact?.Address.Street, contact?.Address.City, contact?.Address.State, contact?.Address.Zip );
                if ( contact.Phone != null )
                {
                    this.Phone = new List<Phone>();
                    foreach ( var phone in contact.Phone )
                    {
                        this.Phone.Add( new Phone( phone?.Number, phone?.Type ) );
                    }
                }
                this.Email = contact.Email;
            }
        }

        public int Id { get; set; }
        public Name Name { get; set; }
        public Address Address { get; set; }
        public List<Phone> Phone { get; set; }
        public string Email { get; set; }
    }

    
}

