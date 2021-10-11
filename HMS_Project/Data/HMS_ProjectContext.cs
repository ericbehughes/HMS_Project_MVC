using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HMS_Project.Models;

namespace HMS_Project.Data
{
    public class HMS_ProjectContext : DbContext
    {
        public HMS_ProjectContext (DbContextOptions<HMS_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<HMS_Project.Models.Request> Request { get; set; }

    }
}
