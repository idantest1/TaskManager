כמובן! הנה קובץ ה-`README` המעדכן בלי הצעד על התקנת Entity Framework Core:

```markdown
# Task Manager Application

This project consists of two parts:
- **Backend**: .NET Core (TaskManagerBackend)
- **Frontend**: React (task-manager-ui)

## Prerequisites

Before starting the project, ensure you have the following installed:

- [Node.js](https://nodejs.org/) (for frontend)
- [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) with the .NET SDK (for backend)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) or any other database you use for the backend

## Getting Started

### Step 1: Clone the repository

Clone the repository to your local machine:

```bash
git clone https://github.com/idantest1/TaskManager
cd TaskManager
```

### Step 2: Setup Backend (TaskManagerBackend)

1. Navigate to the `TaskManagerBackend` folder:
   ```bash
   cd TaskManagerBackend
   ```

2. Restore the backend dependencies:
   ```bash
   dotnet restore
   ```

3. Apply the database migrations (make sure your database server is running):
   ```bash
   dotnet ef database update
   ```

4. Run the backend server:
   ```bash
   dotnet run
   ```

   The backend should now be running at `http://localhost:5023`.

### Step 3: Setup Frontend (task-manager-ui)

1. Navigate to the `task-manager-ui` folder:
   ```bash
   cd task-manager-ui
   ```

2. Install frontend dependencies:
   ```bash
   npm install
   ```

3. Run the frontend server:
   ```bash
   npm run dev
   ```

   The frontend should now be running at `http://localhost:5173`.

### Step 4: Configure Environment Variables

#### Backend

- Make sure to configure the API URL in `appsettings.json` (located in the `TaskManagerBackend` folder). Set the backend URL as needed (typically `http://localhost:5023`):

```json
{
  "UIUrl": "http://localhost:5173"
}
```

#### Frontend

- Make sure to configure the API URL in `.env` (located in the `task-manager-ui` folder). Set the backend API URL:

```bash
VITE_TASKMANAGER_API_URL=http://localhost:5023
```

### Step 5: Running the Application

- The backend is accessible via `http://localhost:5023`.
- The frontend is accessible via `http://localhost:5173`.

You should be able to interact with the task manager application, where you can create, view, edit, and delete tasks.

## Features

- **Create Task**: Add new tasks with title, description, due date, priority, full name, phone, and email.
- **Edit Task**: Modify existing tasks.
- **Delete Task**: Remove tasks from the system.
- **Task List**: View all tasks with a list view.

## Notes

- The backend uses Entity Framework Core to manage the data.
- The frontend is built using React and Redux for state management.
- You may need to configure CORS in the backend to allow communication from the frontend if they are on different ports.

---

Feel free to reach out if you encounter any issues or need further assistance. Enjoy using the Task Manager Application!
```
"# TaskManager" 
