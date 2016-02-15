using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.Entity;
namespace Demo.Models
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext()
        {
            this.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public DbSet<T_Notice> T_Notice { get; set; }
        public DbSet<T_NoticeType> T_NoticeType { get; set; }
    }
}