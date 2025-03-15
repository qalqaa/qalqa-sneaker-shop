<script setup>
import { useDoneHandler } from '@/components/errors/doneHandler'
import { useErrorHandler } from '@/components/errors/errorHandler'
import AuthRegHeader from '@/components/ui/AuthRegHeader.vue'
import axios from 'axios'
import { computed, reactive } from 'vue'
import { useRouter } from 'vue-router'

const { handleDone } = useDoneHandler()
const { handleError } = useErrorHandler()

const router = useRouter()

const user = reactive({
  username: '',
  email: '',
  password: ''
})

const errors = reactive({
  username: '',
  email: '',
  password: ''
})

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

const register = async () => {
  validateField('username')
  validateField('email')
  validateField('password')

  try {
    const response = await axios.post('https://qalqa.tw1.ru/api/register', {
      email: user.email,
      password: user.password,
      userName: user.username
    })
    handleDone(response.data.message)
    router.push('/auth')
  } catch (error) {
    handleError(error)
  }
}

const isState = () => router.currentRoute.value.path === '/auth'

const isFormValid = computed(
  () =>
    user.username &&
    user.email &&
    user.password &&
    !errors.username &&
    !errors.email &&
    !errors.password
)
</script>

<template>
  <AuthRegHeader :state="isState()" />
  <div class="flex mt-7 test">
    <div class="w-full">
      <form @submit.prevent="register">
        <ul class="w-full flex flex-col gap-5 justify-center">
          <li class="relative">
            <label for="userName">Логин</label>
            <input
              id="userName"
              class="p-2 bg-color-soft inner-shadow px-4 transition outline-none rounded-md w-full mt-2"
              :class="{ error: errors.username }"
              type="text"
              placeholder="Введите логин"
              v-model="user.username"
              @blur="validateField('username')"
            />
            <span v-if="errors.username" class="error__text">{{ errors.username }}</span>
          </li>
          <li class="relative">
            <label for="email">Email</label>
            <input
              id="email"
              class="p-2 bg-color-soft inner-shadow px-4 transition outline-none rounded-md w-full mt-2"
              :class="{ error: errors.email }"
              type="email"
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
              :class="{ error: errors.password }"
              type="password"
              placeholder="Введите пароль"
              v-model="user.password"
              @blur="validateField('password')"
            />
            <span v-if="errors.password" class="error__text">{{ errors.password }}</span>
          </li>
        </ul>
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
</style>
