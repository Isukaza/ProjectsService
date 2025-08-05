# ProjectsService

This service manages projects and their related charts and indicators. 
It provides REST API endpoints for CRUD operations on projects and supports analytical queries such as retrieving the top indicators used by users filtered by subscription type.

## Build Docker Image
```bash
  docker build -t projects_service:test -f ProjectsService/Dockerfile .