using System.ComponentModel.DataAnnotations;

namespace NotesBackend.Models
{
    /// <summary>
    /// Represents a Note entity stored by the API.
    /// </summary>
    public class Note
    {
        /// <summary>
        /// The unique identifier for the note.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The title of the note.
        /// </summary>
        [Required]
        [MinLength(1, ErrorMessage = "Title cannot be empty.")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The content/body of the note.
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// Creation timestamp (UTC).
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Last update timestamp (UTC).
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
