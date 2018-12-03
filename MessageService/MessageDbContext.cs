using MessageService.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageService
{
    public class MessageDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
    }
}
