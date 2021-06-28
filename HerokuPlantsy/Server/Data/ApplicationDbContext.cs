using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Plantsy.Server.Models;
using HerokuPlantsy.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantsy.Server.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Plant> Plants { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Change> Changes { get; set; }
		public DbSet<Water> Water { get; set; }
	}
}

