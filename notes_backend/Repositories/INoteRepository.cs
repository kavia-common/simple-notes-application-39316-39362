using NotesBackend.Models;

namespace NotesBackend.Repositories
{
    /// <summary>
    /// Abstraction for data access for Notes.
    /// </summary>
    public interface INoteRepository
    {
        // PUBLIC_INTERFACE
        IEnumerable<Note> GetAll();

        // PUBLIC_INTERFACE
        Note? GetById(Guid id);

        // PUBLIC_INTERFACE
        void Add(Note note);

        // PUBLIC_INTERFACE
        bool Update(Note note);

        // PUBLIC_INTERFACE
        bool Delete(Guid id);
    }
}
