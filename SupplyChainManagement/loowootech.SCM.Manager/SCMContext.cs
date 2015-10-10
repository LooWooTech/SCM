
using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class SCMContext : DbContext
    {
        public SCMContext() : base("name=SCM") { }
        public SCMContext(string connectionString) : base(connectionString) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Remittance> Remittances { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<AddressList> AddressLists { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Inventory> Inventorys { get; set; }
        public DbSet<Express> Expresses { get; set; }
        public DbSet<Contract> Contracts { get; set; }
    }
}
