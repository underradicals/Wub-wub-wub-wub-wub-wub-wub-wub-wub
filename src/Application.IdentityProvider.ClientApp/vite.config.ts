/// <reference types="vitest" />
import { defineConfig } from "vite";
import { dirname, resolve } from 'node:path';
import { fileURLToPath } from 'node:url';
import react from '@vitejs/plugin-react-swc';
import type { UserConfig, ServerOptions, BuildEnvironmentOptions, ResolveOptions, AliasOptions } from "vite";

const __dirname = dirname(fileURLToPath(import.meta.url));

const ServerSettings: ServerOptions = {
  port: 3000,
  host: '0.0.0.0',
  strictPort: true,
  https: {
    key: resolve("D:\\certs\\cert.key"),
    cert: resolve("D:\\certs\\cert.crt")
  }
} satisfies ServerOptions;

const BuildSettings: BuildEnvironmentOptions = {
  outDir: 'dist',
  emptyOutDir: true,
  minify: 'terser',
  terserOptions: {
    format: {
      comments: true,
    },
  },
  rollupOptions: {
    input: {
      main: resolve(__dirname, "index.html"),
      login: resolve(__dirname, 'login/index.html')
    },
    output: {
      banner: "router-view"
    }
  }
} satisfies BuildEnvironmentOptions;

const CSS_Settings: UserConfig["css"] = {
  postcss: {
    plugins: []
  },
  modules: {
    localsConvention: "camelCaseOnly"
  }
} satisfies UserConfig["css"]

const TestSettings: UserConfig["test"] = {
  browser: {
    enabled: true,
    provider: 'playwright',
    instances: [
      { browser: 'chromium' },
    ],
  },
} satisfies UserConfig["test"]

const ResolveSettings: ResolveOptions & {
  alias?: AliasOptions;
} = {
  alias: {
    '@': resolve(__dirname, 'src')
  }
}

const ViteConfiguration: UserConfig = {
  plugins: [react()],
  server: ServerSettings,
  build: BuildSettings,
  css: CSS_Settings,
  test: TestSettings,
  resolve: ResolveSettings
} satisfies UserConfig;

export default defineConfig(ViteConfiguration);
