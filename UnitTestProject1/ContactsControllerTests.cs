using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using singlestone.Models.DataTransfer;
using singlestone.Models.Entities;
using singlestonecontact.Controllers;
using singlestonecontact.Services;



namespace UnitTestProject1
{
    [TestClass]
    public class ContactsControllerTests
    {
        public IEnumerable<Contact> contactList;
        public Contact contact1;
        public Contact contact2;
        public ContactDto contactDto;


        [TestInitialize]
        public void Initialize()
        {
            
            contact1 = new Contact() { Id = 1, Name = new singlestone.Models.Entities.Name( "contact", "", "one" ) };
            contact2 = new Contact() { Id = 2, Name = new singlestone.Models.Entities.Name( "contact", "", "two" ) };

            contactList = new List<Contact>() { contact1, contact2 };

            contactDto = new ContactDto();
        }

        [TestMethod]
        public void GetContacts()
        {
            var mockContactService = new Mock<IContactService>();
            mockContactService.Setup( x => x.List() ).Returns( Task.FromResult<IEnumerable<Contact>>( contactList ) );

            ContactsController controller = new ContactsController( mockContactService.Object );

            var result = controller.Get().Result as OkObjectResult;
            
            Assert.IsNotNull( result );
            Assert.AreEqual( 200, result.StatusCode );

            var listResult = result.Value as IEnumerable<Contact>;

            Assert.AreEqual( 2, listResult.Count() );
        }


        [TestMethod]
        public void GetContact_Success()
        {
            var mockContactService = new Mock<IContactService>();
            mockContactService.Setup( x => x.Find( It.IsAny<int>() ) ).Returns( Task.FromResult<Contact>( contact1 ) );

            ContactsController controller = new ContactsController( mockContactService.Object );

            var result = controller.Get( 1 ).Result as OkObjectResult;

            Assert.IsNotNull( result );
            Assert.AreEqual( 200, result.StatusCode );

            var contactResult = result.Value as Contact;

            Assert.AreEqual( contact1, contactResult );
        }


        [TestMethod]
        public void GetContact_NotFound()
        {
            var mockContactService = new Mock<IContactService>();
            mockContactService.Setup( x => x.Find( It.IsAny<int>() ) ).Returns( Task.FromResult<Contact>( null ) );

            ContactsController controller = new ContactsController( mockContactService.Object );

            var result = controller.Get( 1 ).Result as NotFoundResult;

            Assert.IsNotNull( result );
            Assert.AreEqual( 404, result.StatusCode );
        }


        [TestMethod]
        public void PostContact_Success()
        {
            var mockContactService = new Mock<IContactService>();
            mockContactService.Setup( x => x.Insert( It.IsAny<ContactDto>() ) ).Returns( Task.FromResult<int>( 1 ) );

            ContactsController controller = new ContactsController( mockContactService.Object );

            var result = controller.Post( contactDto ).Result as OkObjectResult;

            Assert.IsNotNull( result );
            Assert.AreEqual( 200, result.StatusCode );
            var postResult = (int?) result.Value;
            Assert.AreEqual( 1, postResult.GetValueOrDefault() );
        }


        [TestMethod]
        public void PostContact_BadRequest()
        {
            var mockContactService = new Mock<IContactService>();
            mockContactService.Setup( x => x.Insert( It.IsAny<ContactDto>() ) ).Returns( Task.FromResult<int>( 1 ) );

            ContactsController controller = new ContactsController( mockContactService.Object );

            var result = controller.Post( null ).Result as BadRequestObjectResult;

            Assert.IsNotNull( result );
            Assert.AreEqual( 400, result.StatusCode );
        }


        [TestMethod]
        public void PutContact_Success()
        {
            var mockContactService = new Mock<IContactService>();
            mockContactService.Setup( x => x.Update( 1, It.IsAny<ContactDto>() ) ).Returns( Task.FromResult<bool>( true ) );

            ContactsController controller = new ContactsController( mockContactService.Object );

            var result = controller.Put( 1, contactDto ).Result as OkResult;

            Assert.IsNotNull( result );
            Assert.AreEqual( 200, result.StatusCode );
        }


        [TestMethod]
        public void PutContact_BadRequest()
        {
            var mockContactService = new Mock<IContactService>();
            mockContactService.Setup( x => x.Update( 1, It.IsAny<ContactDto>() ) ).Returns( Task.FromResult<bool>( true ) );

            ContactsController controller = new ContactsController( mockContactService.Object );

            var result = controller.Put( 1, null ).Result as BadRequestResult;

            Assert.IsNotNull( result );
            Assert.AreEqual( 400, result.StatusCode );
        }


        [TestMethod]
        public void DeleteContact_Success()
        {
            var mockContactService = new Mock<IContactService>();
            mockContactService.Setup( x => x.Delete( 1 ) ).Returns( Task.FromResult<bool>( true ) );

            ContactsController controller = new ContactsController( mockContactService.Object );

            var result = controller.Delete( 1 ).Result as OkResult;

            Assert.IsNotNull( result );
            Assert.AreEqual( 200, result.StatusCode );
        }


        [TestMethod]
        public void DeleteContact_NotFound()
        {
            var mockContactService = new Mock<IContactService>();
            mockContactService.Setup( x => x.Delete( 1 ) ).Returns( Task.FromResult<bool>( false ) );

            ContactsController controller = new ContactsController( mockContactService.Object );

            var result = controller.Delete( 1 ).Result as NotFoundResult;

            Assert.IsNotNull( result );
            Assert.AreEqual( 404, result.StatusCode );
        }
    }
}
