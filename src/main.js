import './assets/main.css'

import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import { autoAnimatePlugin } from '@formkit/auto-animate/vue'
import App from './App.vue'

import Home from './pages/Home.vue'
import Favorites from './pages/Favorites.vue'
import Profile from './pages/Profile.vue'
import Auth from './pages/Auth.vue'
import Register from './pages/Register.vue'

const routes = [
  { path: '/', name: 'Home', component: Home },
  { path: '/favorites', name: 'Favorites', component: Favorites },
  { path: '/profile', name: 'Profile', component: Profile },
  { path: '/register', name: 'Register', component: Register },
  { path: '/auth', name: 'Auth', component: Auth }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

const app = createApp(App)

app.use(autoAnimatePlugin)

app.use(router)

app.mount('#app')
