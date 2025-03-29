import { useEffect, useRef } from 'react';
import { toast } from 'react-toastify';

export function useSuccessHandler(success: boolean, message = 'Success') {
  const shown = useRef(false);

  useEffect(() => {
    if (success && !shown.current) {
      toast.success(message);
      shown.current = true;
    }

    if (!success) {
      shown.current = false;
    }
  }, [success, message]);
}
