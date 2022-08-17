using IMS.Areas.Pos.Models;
using IMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleClaim> RoleClaim { get; set; }

        //Accountstables
        public DbSet<AccountHead> AccountHead { get; set; }
        public DbSet<Firstlevel> Firstlevel { get; set; }
        public DbSet<Secondlevel> Secondlevel { get; set; }
        public DbSet<ThirdLevel> ThirdLevel { get; set; }
        public DbSet<TranscationDetails> TranscationDetails { get; set; }
        //Customers
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerGurantor> CustomerGurantor { get; set; }
        public DbSet<Gurantor> Gurantor { get; set; }
        //Supplier cargo
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<ModelNo> ModelNo { get; set; }
        //Purchase
        public DbSet<PurchaseMaster> PurchaseMaster { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetail { get; set; }
        //Vouchers
        public DbSet<VoucherMaster> VoucherMaster { get; set; }
        public DbSet<VoucherDetail> VoucherDetail { get; set; }
        //Pos MetaData
        public DbSet<Product> Product { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
