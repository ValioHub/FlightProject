using System;
using System.Data.Entity;
using Domain;

namespace Data
{
    public class Entities : DbContext
    {
        public DbSet<Flight> Flights => Set<Flight>();
    }
}
