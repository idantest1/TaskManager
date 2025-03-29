import { useDeleteTaskMutation } from '../api/taskApi';
import { toast, ToastContainer } from 'react-toastify';
import { useLoadingHandler } from '../../../hooks/useLoadingHandler';
import { useNavigate } from 'react-router-dom';

type Props = {
  id: number;
};

const DeleteButton = ({ id }: Props) => {
  const [deleteTask, { isLoading }] = useDeleteTaskMutation();
  const navigate = useNavigate();
  const isBusy = useLoadingHandler(isLoading);

  const handleDelete = async () => {
    const confirmed = confirm('Are you sure you want to delete this task?');
    if (!confirmed) return;

    try {
      await deleteTask(id).unwrap();
      toast.success('Task deleted successfully');
      setTimeout(() => {
        navigate('/');
      }, 500);
    } catch (err) {
      toast.error('Failed to delete task');
    }
  };

  return (
    <><button
      onClick={handleDelete}
      disabled={isBusy}
      className="btn btn-error text-white"
    >
      {isBusy ? 'Deleting…' : 'Delete'}
    </button><ToastContainer /></>
  );
};

export default DeleteButton;
