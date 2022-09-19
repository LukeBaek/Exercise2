using AddressbookAPI.Data;
using AddressbookAPI.Models;
using AddressBookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AddressBookAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressBooksController : Controller
    {
        private readonly AddressBookAPIDbcontext dbContext;
        public AddressBooksController(AddressBookAPIDbcontext dbContext)

        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAddressBooks()
        {
            return Ok(await dbContext.Addressbooks.ToListAsync());

        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAddressBook([FromRoute] Guid id)
        {
            var address = await dbContext.Addressbooks.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }
        [HttpPost]
        public async Task<IActionResult> AddAddressBook(AddAddressRequest addAddressRequest)
        {
            var addressBook = new AddressBook()
            {
                Id = Guid.NewGuid(),
                Address = addAddressRequest.Address,
                FullName = addAddressRequest.FullName
            };
            await dbContext.Addressbooks.AddAsync(addressBook);
            await dbContext.SaveChangesAsync();
            return Ok(addressBook);

        }
     
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAddressBook([FromRoute] Guid id, UpdateAddressBookRequest updateAddressBookRequest)
        {
            var address = await dbContext.Addressbooks.FindAsync(id);
            if (address != null)
            {
                address.FullName = updateAddressBookRequest.FullName;
                address.Address = updateAddressBookRequest.Address;

                await dbContext.SaveChangesAsync();
                return Ok(address);
            }
            return NotFound();

        }
      
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] Guid id)
        {
            var address = await dbContext.Addressbooks.FindAsync(id);
            if (address != null)
            {
                dbContext.Remove(address);
                await dbContext.SaveChangesAsync();
                return Ok(address);
            }
            return NotFound();
        }
    }

}
