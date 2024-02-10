import vue from '@vitejs/plugin-vue'
import { defineConfig } from 'vite'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  assetsInclude: ['**/*.md'],
  build: {
    outDir: 'docs',
    sourcemap: false
  },
  server: {
    port: 8080
  }
})
