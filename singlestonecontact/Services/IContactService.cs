using singlestone.Models.DataTransfer;
using singlestone.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace singlestonecontact.Services
{
    public interface IContactService
    {
        Task<Contact> Find( int Id );

        Task<IEnumerable<Contact>> List();

        Task<int> Insert( ContactDto contactDto );

        Task<bool> Delete( int Id );

        Task<bool> Update( int Id, ContactDto contactDto );
    }
}
