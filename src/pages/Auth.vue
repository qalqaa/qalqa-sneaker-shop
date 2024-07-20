<script setup>
import { useRouter } from 'vue-router'

import AuthRegHeader from '@/components/AuthRegHeader.vue'
import axios from 'axios'
import { reactive } from 'vue'

const user = reactive({
  email: '',
  password: ''
})

const router = useRouter()

const login = async () => {
  try {
    const response = await axios.post('https://localhost:7228/login', {
      email: user.email,
      password: user.password
    })
    console.log(response.data)
  } catch (error) {
    console.error('Error:', error)
  }
}

const isState = () => router.currentRoute.value.path === '/auth'
</script>

<template>
  <AuthRegHeader :state="isState()" />
  <div class="flex mt-7 test">
    <div class="w-full">
      <form @submit.prevent="login">
        <ul class="w-full flex flex-col gap-5 justify-center">
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
              placeholder="email@example.com"
              v-model="user.email"
            />
          </li>
          <li class="relative">
            <label for="password">Пароль</label>
            <input
              id="password"
              class="p-2 bg-color-soft inner-shadow px-4 transition outline-none rounded-md w-full mt-2"
              type="password"
              placeholder="Введите пароль"
              v-model="user.password"
            />
          </li>
        </ul>
        <button
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
</style>
