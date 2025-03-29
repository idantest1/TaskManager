using System.ComponentModel.DataAnnotations;

namespace TaskManager.Data
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public required string Priority { get; set; }

        [Required]
        public required string FullName { get; set; }

        [Required]
        [Phone]
        public required string Telephone { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }

}
