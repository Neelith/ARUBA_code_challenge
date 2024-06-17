# ProcedureManagement

Hi, this is my take-home test for Aruba. It is a simple CRUD app made with CQRS and mediator.

## How to Run

### Prerequisites:
- Docker
- Git & Git Bash
- Any web browser

### How to Run:
Launch these three commands in your Git Bash:
- `git clone https://github.com/Neelith/ARUBA_code_challenge.git`
- `cd ARUBA_code_challenge/ProcedureManagement`
- `docker-compose -f DockerCompose.yaml up --build`

Then, open your web browser and navigate to `http://localhost:8080/health`.

## Project Structure
The project consists of two parts:

1. **The Web API Project:**
    - This project exposes the `../api/v1/procedures` endpoint.
    - It exposes the following endpoints:
        - `GET ../api/v1/procedures` => Retrieves the task with the specified ID from the database
        - `POST ../api/v1/procedures` => Creates a new task in the database
        - `PUT ../api/v1/procedures` => Updates a task in the database
        - `DELETE ../api/v1/procedures` => Deletes a task from the database
    - The APIs are configured to listen on port 8080. The complete URL should look like this: `http://localhost:8080/api/v1/procedures`.

3. **The Database:**
    - The database used is SQL Server 2022.

## Underlying System Architecture
For this project, I used clean architecture, dividing the application into layers.

There are four layers in the project:
- **Domain Layer:** Contains all the business logic, including validation logic. I borrowed some principles from Domain Driven Design.
- **Infrastructure Layer:** Contains all the infrastructure services implementations.
- **Application Layer:** Coordinates the Domain and Infrastructure layers.
- **Presentation Layer:** For the web API, this is the WebApi project. This layer is responsible for presentation.

## How to test the APIs with Postman
import the ProcedureManagementAPIs.postman_collection.json file in PostMan, you probably need to bring your own pdf file to upload