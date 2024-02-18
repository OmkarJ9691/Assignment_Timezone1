using System;
using Assignment.Domain;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Application.Interface
{
	public interface IApplicationDbContext
	{
        DbSet<Guest> Guest { get; set; }
        Task<int> SaveChangesAsync();
	}
}

