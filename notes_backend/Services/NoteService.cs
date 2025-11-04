using NotesBackend.DTOs;
using NotesBackend.Models;
using NotesBackend.Repositories;

namespace NotesBackend.Services
{
    /// <summary>
    /// Default note service with simple business rules.
    /// </summary>
    public class NoteService : INoteService
    {
        private readonly INoteRepository _repository;

        public NoteService(INoteRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Note> ListNotes()
        {
            return _repository.GetAll();
        }

        public Note? GetNote(Guid id)
        {
            return _repository.GetById(id);
        }

        public Note CreateNote(CreateNoteRequest request)
        {
            var now = DateTime.UtcNow;
            var note = new Note
            {
                Id = Guid.NewGuid(),
                Title = request.Title.Trim(),
                Content = request.Content,
                CreatedAt = now,
                UpdatedAt = now
            };

            _repository.Add(note);
            return note;
        }

        public bool UpdateNote(Guid id, UpdateNoteRequest request)
        {
            var existing = _repository.GetById(id);
            if (existing == null) return false;

            existing.Title = request.Title.Trim();
            existing.Content = request.Content;
            existing.UpdatedAt = DateTime.UtcNow;

            return _repository.Update(existing);
        }

        public bool DeleteNote(Guid id)
        {
            return _repository.Delete(id);
        }
    }
}
