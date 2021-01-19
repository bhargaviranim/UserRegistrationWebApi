using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistrationAPI.Model
{
    public class URDbcontext : DbContext
    {
        public DbSet<UserRegistrationUser> users { get; set; }

        //private const string CONN = @"Server=(LocalDB)\MSSQLLocalDB;
        //                              Database= UserRegistration;
        //                              MultipleActiveResultSets=True; 
        //                              Trusted_Connection=True";

        //private const string CONN = @" Data Source = (local)\SQLEXPRESS;
        //                               Initial Catalog =UserRegistration; 
        //                               Integrated Security = SSPI";
        private const string CONN = @"Server=(localdb)\MSSQLLocalDB;
        Database=UserRegistration;
        AttachDbFilename=C:\Users\bhargavim\source\repos\UserRegistration\UserRegistrationAPI\Dbfiles\UserRegistration.mdf;
        MultipleActiveResultSets=true";
        protected override void OnConfiguring(
                    DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONN);
        }
    }
}
