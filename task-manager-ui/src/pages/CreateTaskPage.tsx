import { TaskForm } from '../features/tasks';

const CreateTaskPage = () => {
  return (
    <div className="max-w-xl mx-auto mt-6">
      <h2 className="text-2xl font-bold mb-4">Create New Task</h2>
      <TaskForm />
    </div>
  );
};

export default CreateTaskPage;
