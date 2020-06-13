using Microsoft.EntityFrameworkCore;
using System.Linq;
using Todozor.Shared;

namespace Todozor.Server.Model
{
	public class TodoContext : DbContext
	{
		public DbSet<TodoItem> TodoItems { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=todos.db");
		}
	}
}
