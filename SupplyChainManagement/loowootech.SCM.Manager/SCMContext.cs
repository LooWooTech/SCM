
using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace loowoo.SCM.Manager
{
    public class SCMContext : DbContext
    {
        public SCMContext() : base("name=SCM") { }
        public SCMContext(string connectionString) : base(connectionString) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Remittance> Remittances { get; set; }
        public DbSet<Components> Components { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
    }
}
