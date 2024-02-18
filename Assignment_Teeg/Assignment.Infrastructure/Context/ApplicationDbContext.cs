using System;
using Assignment.Application.Interface;
using Assignment.Domain;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Infrastructure.Context
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName: "GuestDb");
		}
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<Guest> Guest { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) 
		{
			modelBuilder.Entity<List<string>>().HasNoKey();
		}

        public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}
    }
}

