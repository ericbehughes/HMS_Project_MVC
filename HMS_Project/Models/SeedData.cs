using HMS_Project.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_Project.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new HMS_ProjectContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<HMS_ProjectContext>>()))
            {
                // Look for any movies.
                if (context.Rooms.Any())
                {
                    return;   // DB has been seeded
                }

                context.Rooms.AddRange(
                    new Room
                    {
                        Capacity = 2,
                        IsActive = false,
                        Number = "111"
                    },

                    new Room
                    {
                        Capacity = 3,
                        IsActive = false,
                        Number = "222"
                    },
                    new Room
                    {
                        Capacity = 4,
                        IsActive = false,
                        Number = "444"
                    },
                    new Room
                    {
                        Capacity = 5,
                        IsActive = false,
                        Number = "555"
                    },
                    new Room
                    {
                        Capacity = 6,
                        IsActive = false,
                        Number = "666"
                    },
                    new Room
                    {
                        Capacity = 7,
                        IsActive = false,
                        Number = "777
                    },
                    new Room
                    {
                        Capacity = 8,
                        IsActive = false,
                        Number = "888
                    }


                );
                context.SaveChanges();
            }
        }
    }
}
