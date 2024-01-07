# soccer-time
This is a portfolio project, still a work in progress.

Is an app for a ficticiuous company that rents artificial grass soccer fields (a big business in Latin America)

The app will post the calendar for each soccer field in a web app, so the clients can reserve slots.

# Domain Driven Design
See the domain project, that one is almost finished.

# Technologies
- Net 8 (Asp Net Api)
- Ef Core
- MediatR
- Postgres Db
- Docker

# Demo Features
In this project, I added several ideas I experimented with during the years.

1. Use a database running in docker for integration testing, this database is dropped and recreated before each test.
2. Use a switch to turn on/off the integration tests.
3. Use value objects to validate arguments
4. Make the Repository expose the Domain Aggregate as an interface
5. Use MediatR requests and vertical slice architecture.

   And much more to come...
