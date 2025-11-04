using Microsoft.AspNetCore.Mvc;
using NotesBackend.DTOs;
using NotesBackend.Models;
using NotesBackend.Services;

namespace NotesBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _service;

        public NotesController(INoteService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create a new note.
        /// </summary>
        /// <param name="request">Create note payload.</param>
        /// <returns>Created note.</returns>
        /// <response code="201">Returns the newly created note.</response>
        /// <response code="400">If the request is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Note), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CreateNoteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var created = _service.CreateNote(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Get all notes.
        /// </summary>
        /// <returns>List of notes.</returns>
        /// <response code="200">Returns the list of notes.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Note>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var notes = _service.ListNotes();
            return Ok(notes);
        }

        /// <summary>
        /// Get a note by id.
        /// </summary>
        /// <param name="id">Note identifier (GUID).</param>
        /// <returns>Note if found.</returns>
        /// <response code="200">Returns the note.</response>
        /// <response code="404">If the note is not found.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Note), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var note = _service.GetNote(id);
            if (note == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Note not found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"Note with id '{id}' was not found.",
                    Instance = HttpContext.Request.Path
                });
            }

            return Ok(note);
        }

        /// <summary>
        /// Update a note by id.
        /// </summary>
        /// <param name="id">Note identifier (GUID).</param>
        /// <param name="request">Update note payload.</param>
        /// <returns>No content on success.</returns>
        /// <response code="204">Note updated successfully.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="404">If the note is not found.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateNoteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var exists = _service.GetNote(id);
            if (exists == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Note not found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"Note with id '{id}' was not found.",
                    Instance = HttpContext.Request.Path
                });
            }

            var updated = _service.UpdateNote(id, request);
            if (!updated)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "Failed to update note");
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a note by id.
        /// </summary>
        /// <param name="id">Note identifier (GUID).</param>
        /// <returns>No content on success.</returns>
        /// <response code="204">Note deleted successfully.</response>
        /// <response code="404">If the note is not found.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var deleted = _service.DeleteNote(id);
            if (!deleted)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Note not found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"Note with id '{id}' was not found.",
                    Instance = HttpContext.Request.Path
                });
            }

            return NoContent();
        }
    }
}
