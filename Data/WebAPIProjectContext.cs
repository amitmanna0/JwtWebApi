using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPIProject.Models;

namespace WebAPIProject.Data
{
    public class WebAPIProjectContext : DbContext
    {
        public WebAPIProjectContext (DbContextOptions<WebAPIProjectContext> options)
            : base(options)
        {
        }

        public DbSet<WebAPIProject.Models.UserCredential> UserCredential { get; set; } = default!;
    }
}
