namespace TaskManager.Logic.Dto
{
    public class TaskDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime DueDate { get; set; }
        public required string Priority { get; set; }
        public required string FullName { get; set; }
        public required string Telephone { get; set; }
        public required string Email { get; set; }
    }
}
