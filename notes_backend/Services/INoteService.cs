using NotesBackend.DTOs;
using NotesBackend.Models;

namespace NotesBackend.Services
{
    /// <summary>
    /// Service layer to encapsulate business logic for Notes.
    /// </summary>
    public interface INoteService
    {
        // PUBLIC_INTERFACE
        IEnumerable<Note> ListNotes();

        // PUBLIC_INTERFACE
        Note? GetNote(Guid id);

        // PUBLIC_INTERFACE
        Note CreateNote(CreateNoteRequest request);

        // PUBLIC_INTERFACE
        bool UpdateNote(Guid id, UpdateNoteRequest request);

        // PUBLIC_INTERFACE
        bool DeleteNote(Guid id);
    }
}
