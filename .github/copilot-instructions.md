# Copilot Instructions

## Project Guidelines
- For TutorConnect admin UI, use the existing primary gradient theme rather than a black navbar, and avoid duplicating the signed-in identity and user-site link in the global admin navigation when they are already shown on the dashboard.
- For TutorConnect code, prefer simplified syntax and follow the existing project style in controllers and services instead of introducing more complex patterns or abstractions. Prefer simple, explicit C# code that follows existing repository/controller style; avoid helper methods and abstractions when their behavior is hard to understand, and use direct inline checks where practical.
- When legacy database values have been corrected manually, preserve strict IsActive == "Active" filtering rather than treating NULL as active; retain nullable entity properties when the database column allows NULL.