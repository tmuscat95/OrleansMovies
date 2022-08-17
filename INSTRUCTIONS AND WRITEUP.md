# Instructions
- Ensure that Movies.Server and Movies.ReactUI are set as the startup projects.
- Ensure Docker Desktop is configured to use Linux containers prior to running in container.
- App backend is accessible on `http://localhost:6600` (NOT https), both when running on host and when running in Docker container, with `/api/graphql` being the API endpoint. 
- Frontend is accessible on `http://localhost:3000` (note: configured to launch browser on `http://localhost:3000` rather than https; nevertheless some browsers ignore this. Check address bar if frontend is not working).
- JSON Data is loaded automatically on startup (see ApiStartup.cs)

## Summary Of Work:
Requirements + "Extra Credit" implemented as per specification (to the best of my understanding). 

UI, implemented in React, is rudimentary and does not feature GraphQL subscriptions, or more complex form controls such as updating or creating a new movie (seeing as this is a backend position I'm applying for, I'm guessing you're not too concerned with my React skills).

App was configured to use http rather than https owing to issues with self-signed certificate verification on macOS; I would obviously not do it this way in production. 

Orleans configured to use Memory storage provider.

Testing implemented using NUnit and Orleans.TestingHost.
