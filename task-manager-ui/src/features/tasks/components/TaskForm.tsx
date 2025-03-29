import { useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { Task } from '../types/task';
import {
  useCreateTaskMutation,
  useUpdateTaskMutation,
  useGetTaskQuery,
} from '../api/taskApi';
import { useNavigate } from 'react-router-dom';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

type Props = {
  taskId?: number;
};

const TaskForm = ({ taskId }: Props) => {
  const navigate = useNavigate();
  const isEditMode = !!taskId;

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
  } = useForm<Task>();

  const { data: task, isLoading: isLoadingTask, error: taskError } = useGetTaskQuery(taskId!, {
    skip: !taskId,
    refetchOnMountOrArgChange: true,
  });

  const [createTask, { isSuccess: createSuccess, error: createError }] = useCreateTaskMutation();
  const [updateTask, { isSuccess: updateSuccess, error: updateError }] = useUpdateTaskMutation();

  useEffect(() => {
    if (task && !isLoadingTask) {
      const formattedTask = {
        ...task,
        dueDate: new Date(task.dueDate).toISOString().split('T')[0],
      };
      reset(formattedTask);
    }
  }, [task, isLoadingTask, reset]);
  
  useEffect(() => {
    if (createSuccess || updateSuccess) {
      toast.success(updateSuccess ? 'Task updated successfully' : 'Task created successfully');
      setTimeout(() => {
        navigate('/');
        reset();
      }, 500);
    }
  }, [createSuccess, updateSuccess, navigate]);

  const onSubmit = async (data: Task) => {
    try {
      if (isEditMode && taskId) {
        await updateTask({ id: taskId, task: data }).unwrap();
        reset(data);
      } else {
        await createTask(data).unwrap();
      }
    } catch {
      toast.error('Error saving task');
    }
  };

  const goBack = () => {
    navigate('/');
  };

  if (isEditMode && isLoadingTask) return <p>Loading...</p>;
  if (taskError)
    return (
      <p>
        Error:{' '}
        {typeof taskError === 'object' && taskError && 'message' in taskError
          ? (taskError as any).message
          : 'Something went wrong'}
      </p>
    );

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4 bg-white p-6 rounded shadow">
      <div>
        <label htmlFor="title" className="block font-semibold">Title</label>
        <input
          id="title"
          type="text"
          {...register('title', { required: 'Title is required' })}
          className="input input-bordered w-full"
        />
        {errors.title && <p className="text-red-500 text-sm">{errors.title.message}</p>}
      </div>

      <div>
        <label htmlFor="description" className="block font-semibold">Description</label>
        <textarea
          id="description"
          {...register('description', { required: 'Description is required' })}
          className="input input-bordered w-full"
        />
        {errors.description && <p className="text-red-500 text-sm">{errors.description.message}</p>}
      </div>

      <div>
        <label htmlFor="dueDate" className="block font-semibold">Due Date</label>
        <input
          id="dueDate"
          type="date"
          {...register('dueDate', { required: 'Due date is required' })}
          className="input input-bordered w-full"
        />
        {errors.dueDate && <p className="text-red-500 text-sm">{errors.dueDate.message}</p>}
      </div>

      <div>
        <label htmlFor="priority" className="block font-semibold">Priority</label>
        <input
          id="priority"
          type="text"
          {...register('priority', { required: 'Priority is required' })}
          className="input input-bordered w-full"
        />
        {errors.priority && <p className="text-red-500 text-sm">{errors.priority.message}</p>}
      </div>

      <div>
        <label htmlFor="fullName" className="block font-semibold">Full Name</label>
        <input
          id="fullName"
          type="text"
          {...register('fullName', { required: 'Full Name is required' })}
          className="input input-bordered w-full"
        />
        {errors.fullName && <p className="text-red-500 text-sm">{errors.fullName.message}</p>}
      </div>

      <div>
        <label htmlFor="telephone" className="block font-semibold">Phone</label>
        <input
          id="telephone"
          type="tel"
          {...register('telephone', { required: 'Phone is required' })}
          className="input input-bordered w-full"
        />
        {errors.telephone && <p className="text-red-500 text-sm">{errors.telephone.message}</p>}
      </div>

      <div>
        <label htmlFor="email" className="block font-semibold">Email</label>
        <input
          id="email"
          type="email"
          {...register('email', { required: 'Email is required' })}
          className="input input-bordered w-full"
        />
        {errors.email && <p className="text-red-500 text-sm">{errors.email.message}</p>}
      </div>

      <button type="submit" disabled={isSubmitting} className="btn btn-primary w-full">
        {isSubmitting ? 'Saving...' : 'Save Task'}
      </button>

      <button type="button" onClick={goBack} className="btn btn-secondary w-full mt-4">
        Go Back
      </button>

      <ToastContainer />
    </form>
  );
};

export default TaskForm;