namespace Todozor.Shared
{
	public class TodoItem
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Text { get; set; }

		public bool Completed { get; set; }

		public override string ToString()
		{
			return $"TodoItem {Id}, Title: {Title}, Text: {Text}, Completed: {Completed}";
		}
	}
}
