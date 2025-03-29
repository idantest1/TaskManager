import { useParams } from 'react-router-dom';
import { TaskForm } from '../features/tasks';

const EditTaskPage = () => {
  const { taskId } = useParams();
  console.log("taskId from URL:", taskId);
  if (!taskId) {
    return <div>Error: taskId is missing from the URL.</div>;
  }

  return (
    <div className="max-w-xl mx-auto mt-6">
      <h2 className="text-2xl font-bold mb-4">Edit Task</h2>
      <TaskForm taskId={parseInt(taskId)} />
    </div>
  );
};

export default EditTaskPage;
