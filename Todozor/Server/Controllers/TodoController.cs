using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Todozor.Server.Model;
using Todozor.Shared;

namespace Todozor.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TodoController : ControllerBase
	{
		private readonly ILogger<TodoController> logger;

		public TodoController(ILogger<TodoController> logger)
		{
			this.logger = logger;
		}

		[HttpPost]
		public async Task PostAsync(TodoItem todoItem, CancellationToken ct)
		{
			using var db = new TodoContext();

			logger.LogInformation($"Adding {todoItem} to database.");

			await db.TodoItems.AddAsync(todoItem);

			await db.SaveChangesAsync(ct);
		}

		[HttpGet]
		public async Task<IEnumerable<TodoItem>> GetAsync(CancellationToken ct)
		{
			using var db = new TodoContext();
			await db.Database.EnsureCreatedAsync();

			var todoItemsCount = await db.TodoItems.CountAsync();
			if (todoItemsCount == 0)
			{
				await db.TodoItems.AddAsync(new TodoItem { Id = 1, Title = "Test", Text = "Texttest", Completed = false });

				await db.SaveChangesAsync(ct);
			}

			var todos = await db.TodoItems.OrderBy(t => t.Id).ToListAsync();
			
			return todos ?? Enumerable.Empty<TodoItem>();
		}
	}
}
