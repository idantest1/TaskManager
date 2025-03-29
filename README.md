# Task Manager

This project is a task management application built with **React** for the frontend and **.NET** for the backend. The application allows users to create, update, view, and delete tasks. It also demonstrates the integration of frontend and backend using **REST APIs** and **Entity Framework Core** for database operations.

## Prerequisites

1. **.NET 8.0+**
2. **Node.js (v16.0.0+)**
3. **npm (v7.0.0+)**
4. **SQL Server** (or any database compatible with EF Core)

---

## Setup Instructions

#### Clone the repository
```bash
git clone https://github.com/idantest1/TaskManager.git
cd task-manager
```
### 1. Backend (.NET - TaskManagerBackend)

#### Step 1: Install Backend dependencies

Navigate to the `TaskManagerBackend` directory and restore the necessary packages:

```bash
cd TaskManagerBackend
dotnet restore
```


#### Step 2: Configure Database Connection and CORS Settings

In the `appsettings.json` file, configure both the connection string to your SQL Server database and the UI URL to avoid CORS issues. Here's an example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TaskManagerDb;User Id=sa;Password=your_password;"
  },
  "UIUrl": "http://localhost:5173"
}
```

- **Database Connection**: Replace the placeholders in the `DefaultConnection` string with your actual database details. If you're using a local database, the format provided should work. Ensure the database is accessible with the correct credentials.
  
- **CORS Configuration**: The `"UIUrl"` property is used to specify the URL of your React front-end application. This is essential for configuring CORS correctly on the backend and ensuring smooth communication between the client and the API.

Make sure to update both values with the actual details corresponding to your environment.

Make sure to replace the placeholders with your actual database connection details.

#### Step 3: Create and Apply Migrations

Use the following commands to create and apply database migrations:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

This will create the necessary tables for the application.

#### Step 4: Run the Backend

Now you can run the backend API using the following command:

```bash
dotnet run
```

By default, the backend will be running at `http://localhost:5023`.

---

### 2. Frontend (React - TaskManagerUI)

#### Step 1: Navigate to the frontend folder

Change directory to the `TaskManagerUI` folder:

```bash
cd ../task-manager-ui
```

#### Step 2: Install Frontend dependencies

Run the following command to install the necessary npm packages:

```bash
npm install
```

#### Step 3: Configure Environment Variables

Create a `.env` file in the root of the `task-manager-ui` project with the following content:

```bash
VITE_TASKMANAGER_API_URL=http://localhost:5023
```

This will allow the frontend to communicate with the backend API.

#### Step 4: Run the Frontend

Now, you can run the frontend by using the following command:

```bash
npm run dev
```

By default, the frontend will be running at `http://localhost:5173`.

---

## Important Configuration

### CORS Configuration in Backend

In the `.NET` backend, make sure you allow requests from the frontend's URL (`http://localhost:5173` by default). This is done by setting up CORS (Cross-Origin Resource Sharing).

In the `Program.cs` file, add the following configuration:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

app.UseCors("AllowReactApp");
```

This will allow the frontend to make requests to the backend.

---

## API Endpoints

### Task Management

The following endpoints are available for managing tasks:

#### Create a Task

```http
POST /api/tasks
```

Request body:

```json
{
  "title": "Task Title",
  "description": "Task Description",
  "dueDate": "2025-03-29",
  "priority": "High",
  "fullName": "John Doe",
  "telephone": "1234567890",
  "email": "john.doe@example.com"
}
```

#### Get All Tasks

```http
GET /api/tasks
```

#### Get a Task by ID

```http
GET /api/tasks/{id}
```

#### Update a Task

```http
PUT /api/tasks/{id}
```

Request body:

```json
{
  "title": "Updated Title",
  "description": "Updated Description",
  "dueDate": "2025-03-30",
  "priority": "Low",
  "fullName": "Jane Doe",
  "telephone": "9876543210",
  "email": "jane.doe@example.com"
}
```

#### Delete a Task

```http
DELETE /api/tasks/{id}
```

---

## Testing

### Frontend Testing

Frontend testing is done using [Jest](https://jestjs.io/) along with [React Testing Library](https://testing-library.com/docs/react-testing-library/intro/). You can run tests using:

```bash
npm test
```

### Backend Testing

Backend testing is done using [xUnit](https://xunit.net/) and [Moq](https://github.com/moq/moq4). To run the tests:

```bash
dotnet test
```

---

## Additional Notes

- Ensure that both the backend and frontend are running for full functionality.
- The API is set up to allow CORS requests from the frontend running at `http://localhost:5173`.
- If you're deploying to a production environment, make sure to configure the proper environment variables and adjust the CORS settings accordingly.

---
