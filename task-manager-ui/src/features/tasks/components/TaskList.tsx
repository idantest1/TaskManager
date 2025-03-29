import { useState, useEffect } from 'react';
import { useGetTasksQuery, useDeleteTaskMutation } from '../api/taskApi';
import { Task } from '../types/task';
import { Link } from 'react-router-dom';
import DeleteButton from './DeleteButton';

const TaskList = () => {
  const { data: tasks, isLoading, error } = useGetTasksQuery(undefined, {
    refetchOnMountOrArgChange: true,
  });
  const [taskList, setTaskList] = useState<Task[]>([]);
  useEffect(() => {
    if (tasks) {
      setTaskList(tasks);
    }
  }, [tasks]);


  if (isLoading) return <div>Loading...</div>;
  if (error) return <div>Error loading tasks.</div>;

  return (
    <div className="space-y-4">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-semibold">Task List</h1>
        <Link to="/tasks/create" className="btn btn-primary">
          Add Task
        </Link>
      </div>

      <ul>
        {taskList.map((task) => (
          <li key={task.id} className="p-4 border-b">
            <div className="flex justify-between">
              <div>
                <h3 className="font-semibold">{task.title}</h3>
                <p>{task.description}</p>
                <p className="text-sm text-gray-500">Due: {new Date(task.dueDate).toLocaleDateString()}</p>
                <p className="text-sm text-gray-500">Priority: {task.priority}</p>
                <p className="text-sm text-gray-500">Full Name: {task.fullName}</p>
                <p className="text-sm text-gray-500">Phone: {task.telephone}</p>
                <p className="text-sm text-gray-500">Email: {task.email}</p>
              </div>
              <div className="flex flex-col items-end">
                <Link
                  to={`/tasks/edit/${task.id}`}
                  className="btn btn-sm btn-secondary mb-2"
                >
                  Edit
                </Link>
                <Link
                  to={`/tasks/${task.id}`}
                  className="btn btn-sm btn-primary mb-2"
                >
                  View
                </Link>
                  <DeleteButton id={task.id} />          
              </div>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default TaskList;
