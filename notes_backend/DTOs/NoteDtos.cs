using System.ComponentModel.DataAnnotations;

namespace NotesBackend.DTOs
{
    /// <summary>
    /// Payload for creating a note.
    /// </summary>
    public class CreateNoteRequest
    {
        [Required]
        [MinLength(1, ErrorMessage = "Title cannot be empty.")]
        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }
    }

    /// <summary>
    /// Payload for updating a note.
    /// </summary>
    public class UpdateNoteRequest
    {
        [Required]
        [MinLength(1, ErrorMessage = "Title cannot be empty.")]
        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }
    }
}
