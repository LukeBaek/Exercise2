using AddressbookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressbookAPI.Data
{
    public class AddressBookAPIDbcontext : DbContext
    {
        public AddressBookAPIDbcontext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AddressBook> Addressbooks { get; set; }
    }

}
