<script setup>
import { useRouter } from 'vue-router'

import AuthRegHeader from '@/components/ui/AuthRegHeader.vue'
import { computed, inject, reactive } from 'vue'

import { useDoneHandler } from '@/components/errors/doneHandler'
import { useErrorHandler } from '@/components/errors/errorHandler'

const { handleDone } = useDoneHandler()
const { handleError } = useErrorHandler()

import api from '../interceptors/AuthIterseptor'

const isAuth = inject('isAuth')

const user = reactive({
  email: '',
  password: ''
})

const errors = reactive({
  email: '',
  password: ''
})

const router = useRouter()

const validateEmailPattern = (email) => {
  const pattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/
  return pattern.test(email)
}

const validateField = (field) => {
  if (!user[field]) {
    errors[field] = `${field.charAt(0).toUpperCase() + field.slice(1)} не может быть пустым`
  } else {
    if (field === 'email' && !validateEmailPattern(user.email)) {
      errors.email = 'Email должен быть в формате email@example.com'
    } else {
      errors[field] = ''
    }
  }
}

const login = async () => {
  validateField('email')
  validateField('password')
  if (errors.email || errors.password) {
    return
  }
  try {
    const response = await api.post('/login', {
      email: user.email,
      password: user.password
    })
    const token = response.data.token
    localStorage.setItem('token', token)
    isAuth.value = true
    handleDone(response.data.message)
    router.push('/')
  } catch (error) {
    handleError(error)
    console.error('Error:', error)
  }
}

const isState = () => router.currentRoute.value.path === '/auth'

const isFormValid = computed(() => user.email && user.password && !errors.email && !errors.password)
</script>

<template>
  <AuthRegHeader :state="isState()" />
  <div class="flex mt-7 test">
    <div class="w-full">
      <form @submit.prevent="login">
        <ul class="w-full flex flex-col mb-2 gap-5 justify-center">
          <!-- <li class="relative">
            <label for="login">Логин</label>
            <input
              id="login"
              class="p-2 bg-color-soft inner-shadow px-4 transition outline-none rounded-md w-full mt-2"
              type="text"
              placeholder="Введите логин"
            />
          </li> -->
          <li class="relative">
            <label for="email">Email</label>
            <input
              id="email"
              class="p-2 bg-color-soft inner-shadow px-4 transition outline-none rounded-md w-full mt-2"
              type="email"
              :class="{ error: errors.email }"
              placeholder="email@example.com"
              v-model="user.email"
              @blur="validateField('email')"
            />
            <span v-if="errors.email" class="error__text">{{ errors.email }}</span>
          </li>

          <li class="relative">
            <label for="password">Пароль</label>
            <input
              id="password"
              class="p-2 bg-color-soft inner-shadow px-4 transition outline-none rounded-md w-full mt-2"
              type="password"
              :class="{ error: errors.password }"
              placeholder="Введите пароль"
              v-model="user.password"
              @blur="validateField('password')"
            />
            <span v-if="errors.password" class="error__text">{{ errors.password }}</span>
          </li>
        </ul>
        <a class="recover__password font-bold transition cursor-pointer"> Забыли пароль? </a>
        <button
          :disabled="!isFormValid"
          type="submit"
          class="mt-4 w-full font-bold rounded-xl p-3 hover-accent-shadow-box active:text-white disabled:opacity-50 disabled:shadow-none disabled:cursor-not-allowed transition cursor-pointer"
        >
          Сохранить
        </button>
      </form>
    </div>
  </div>
</template>

<style scoped>
label {
  position: absolute;
  top: -5px;
  left: 10px;
  padding: 0 5px;
}

.error {
  box-shadow: inset 0 0 10px #ff252570;
}

.error__text {
  color: #ff252570;
  font-size: 0.8em;
  position: absolute;
  top: 40%;
  right: 10px;
}

.recover__password {
  color: var(--color-accent);
  font-size: 0.8em;
}
</style>
