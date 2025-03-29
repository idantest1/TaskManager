import { Routes, Route, Navigate } from 'react-router-dom';
import { TaskList } from './features/tasks';
import CreateTaskPage from './pages/CreateTaskPage';
import EditTaskPage from './pages/EditTaskPage';
import { ToastContainer } from 'react-toastify';

function App() {
  return (
    <>
      <div className="min-h-screen bg-gray-50 text-gray-900 p-6">
        <Routes>
          <Route path="/tasks" element={<TaskList />} />
          <Route path="/tasks/create" element={<CreateTaskPage />} />
          <Route path="/tasks/edit/:taskId" element={<EditTaskPage />} />
          <Route path="*" element={<Navigate to="/tasks" replace />} />
        </Routes>
      </div>

      <ToastContainer position="top-right" autoClose={3000} />
    </>
  );
}

export default App;
