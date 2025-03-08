import './assets/main.css'

import { autoAnimatePlugin } from '@formkit/auto-animate/vue'
import { createApp, h } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import App from './App.vue'
import store from './storage/storage'

import CardOpened from './components/card/CardOpened.vue'
import { useDoneHandlerProvider } from './components/errors/doneHandler'
import { useErrorHandlerProvider } from './components/errors/errorHandler'
import Auth from './pages/Auth.vue'
import Favorites from './pages/Favorites.vue'
import Home from './pages/Home.vue'
import Profile from './pages/Profile.vue'
import Register from './pages/Register.vue'

const routes = [
  { path: '/', name: 'Home', component: Home },
  { path: '/favorites', name: 'Favorites', component: Favorites },
  { path: '/profile', name: 'Profile', component: Profile },
  { path: '/register', name: 'Register', component: Register },
  { path: '/auth', name: 'Auth', component: Auth },
  {
    path: '/product/:id',
    name: 'CardOpened',
    component: CardOpened,
    props: true
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

const app = createApp({
  setup() {
    useErrorHandlerProvider(), useDoneHandlerProvider()
  },
  render: () => h(App)
})

app.use(autoAnimatePlugin)

app.use(router)
app.use(store)

app.mount('#app')
