import { defineConfig, loadEnv } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd());
  const apiUrl = env.VITE_TASKMANAGER_API_URL;

  console.log("API URL in Vite config:", apiUrl);  // בדוק שה־API URL נכון

  return {
    plugins: [react()],
    server: {
      proxy: {
        '/api': {
          target: apiUrl,  // ה־API URL שנשלף
          changeOrigin: true,  // משנה את ה-Origin של הבקשה כך שיתאים לשרת ה־API
          secure: false,  // אם ה-API לא משתמש ב-HTTPS, שים secure=false
        },
      },
    },
  };
});
