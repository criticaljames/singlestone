using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using singlestone.Models.DataTransfer;
using singlestone.Models.Entities;

namespace singlestonecontact.Services
{
    public class ContactService : IContactService
    {
        public async Task<Contact> Find( int Id )
        {
            Contact result = null;
            using ( var db = new LiteDatabase( @"singlestone.db" ) )
            {
                LiteCollection<Contact> contacts = db.GetCollection<Contact>( "contacts" );
                result = contacts.Find( x => x.Id == Id ).FirstOrDefault();
            }

            return await Task.FromResult( result );
        }


        public async Task<IEnumerable<Contact>> List()
        {
            IEnumerable<Contact> result;
            using ( var db = new LiteDatabase( @"singlestone.db" ) )
            {
                LiteCollection<Contact> contacts = db.GetCollection<Contact>( "contacts" );
                result = contacts.FindAll();
            }

            return await Task.FromResult( result );
        }


        public async Task<int> Insert( ContactDto contactDto )
        {
            using ( var db = new LiteDatabase( @"singlestone.db" ) )
            {
                var contacts = db.GetCollection<Contact>( "contacts" );

                contacts.EnsureIndex( x => x.Id, true );
                var contact = new Contact( contactDto );
                contacts.Insert( contact );

                return await Task.FromResult( contact.Id );
            }
        }


        public async Task<bool> Delete( int Id )
        {
            bool result = false;
            using ( var db = new LiteDatabase( @"singlestone.db" ) )
            {
                LiteCollection<Contact> contacts = db.GetCollection<Contact>( "contacts" );

                result = contacts.Delete( Id );
            }

            return await Task.FromResult( result );        
        }


        public async Task<bool> Update( int Id, ContactDto contactDto )
        {
            using ( var db = new LiteDatabase( @"singlestone.db" ) )
            {
                LiteCollection<Contact> contacts = db.GetCollection<Contact>( "contacts" );
                Contact contact = contacts.Find( x => x.Id == Id ).FirstOrDefault();

                if ( contact != null )
                {
                    var updateContact = new Contact( contactDto );
                    updateContact.Id = Id;

                    contacts.Update( Id, updateContact );
                }
            }

            return await Task.FromResult( true );
        }
    }
}
