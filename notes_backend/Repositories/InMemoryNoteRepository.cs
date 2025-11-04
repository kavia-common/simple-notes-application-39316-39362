using System.Collections.Concurrent;
using NotesBackend.Models;

namespace NotesBackend.Repositories
{
    /// <summary>
    /// Thread-safe in-memory storage for notes.
    /// Persists for the lifetime of the application.
    /// </summary>
    public class InMemoryNoteRepository : INoteRepository
    {
        private readonly ConcurrentDictionary<Guid, Note> _notes = new();

        public IEnumerable<Note> GetAll()
        {
            return _notes.Values.OrderByDescending(n => n.UpdatedAt);
        }

        public Note? GetById(Guid id)
        {
            _notes.TryGetValue(id, out var note);
            return note;
        }

        public void Add(Note note)
        {
            _notes[note.Id] = note;
        }

        public bool Update(Note note)
        {
            if (!_notes.ContainsKey(note.Id)) return false;
            _notes[note.Id] = note;
            return true;
        }

        public bool Delete(Guid id)
        {
            return _notes.TryRemove(id, out _);
        }
    }
}
