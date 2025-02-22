using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models;

public class TodoModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Title is required")]
    [StringLength(50, ErrorMessage = "Description cannot exceed 50 characters")]
    public string Title { get; set; } = string.Empty;
    [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
    public string? Description { get; set; } = string.Empty;
}