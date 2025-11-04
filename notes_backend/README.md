# Notes Backend (Simple Notes Application)

A .NET 8 Web API providing CRUD operations for notes using a clean architecture (Controller -> Service -> Repository) and in-memory storage.

- Swagger UI: GET /docs
- OpenAPI JSON: GET /openapi.json
- Health: GET /

## Endpoints

- POST /api/notes
- GET /api/notes
- GET /api/notes/{id}
- PUT /api/notes/{id}
- DELETE /api/notes/{id}

### Models

Note:
- id: Guid
- title: string (required, non-empty)
- content: string
- createdAt: DateTime (UTC)
- updatedAt: DateTime (UTC)

### Example Requests

Create:
```bash
curl -s -X POST http://localhost:3001/api/notes \
  -H "Content-Type: application/json" \
  -d '{"title":"First note", "content":"Hello world"}'
```

List:
```bash
curl -s http://localhost:3001/api/notes
```

Get by id:
```bash
curl -s http://localhost:3001/api/notes/{id}
```

Update:
```bash
curl -s -X PUT http://localhost:3001/api/notes/{id} \
  -H "Content-Type: application/json" \
  -d '{"title":"Updated title", "content":"Updated content"}' -i
```

Delete:
```bash
curl -s -X DELETE http://localhost:3001/api/notes/{id} -i
```

## Architecture

- Controllers: HTTP layer and response codes
- Services: Business logic
- Repositories: Data access (in-memory, thread-safe)

CORS is enabled for all origins to support local previews.

## Run

This project is configured to run on http://localhost:3001 (Development profile).

Use the workspace's configured process to run. Once running, visit:
- API docs: http://localhost:3001/docs
- OpenAPI: http://localhost:3001/openapi.json
