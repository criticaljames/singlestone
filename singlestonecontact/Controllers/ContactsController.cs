using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using singlestone.Models.DataTransfer;
using singlestonecontact.Services;

namespace singlestonecontact.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController( IContactService contactService )
        {
            _contactService = contactService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _contactService.List();

            return Ok( result );
        }

    
        [HttpGet( "{id}" )]
        public async Task<IActionResult> Get( int id )
        {
            var contact = await _contactService.Find( id );

            if ( contact == null )
            {
                return NotFound();
            }
            return Ok( contact );
        }


        [HttpPost]
        public async Task<IActionResult> Post( [FromBody] ContactDto contact )
        {
            if ( contact == null )
            {
                return BadRequest( "No data received" );
            }
            var id = await _contactService.Insert( contact );

            return Ok( id );
        }


        [HttpPut( "{id}" )]
        public async Task<IActionResult> Put( int id, [FromBody] ContactDto contact )
        {
            if ( contact != null && id > 0 )
            {
                await _contactService.Update( id, contact );
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpDelete( "{id}" )]
        public async Task<IActionResult> Delete( int id )
        {
            var result = await _contactService.Delete( id );

            if ( result )
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
