import '@testing-library/jest-dom';

import { TextEncoder, TextDecoder } from 'util';
Object.assign(globalThis, { TextEncoder, TextDecoder });

// Mock fetch for RTK Query
import 'whatwg-fetch'; // ✅ מוסיף fetch ל-Jest