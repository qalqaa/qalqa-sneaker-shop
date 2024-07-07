<script setup>
import { useRouter } from 'vue-router'
import DrawerHead from '@/components/DrawerHead.vue'

const router = useRouter()

const goBack = () => {
  router.push('/')
}
</script>

<template>
  <DrawerHead :back-to-func="goBack" :title="'Мой Профиль'" />
  <div class="flex mt-7">
    <div class="w-1/5">
      <!-- TODO:Добавить промежуточный роутинг -->
      <ul class="flex flex-col gap-5">
        <li>
          <span class="text-xl font-semibold c-accent">Личные данные</span>
        </li>
        <!-- Сделать страницу на которую будут загружаться orders c нужным id -->
        <li>
          <span class="text-xl font-semibold">Мои заказы</span>
        </li>
      </ul>
    </div>
    <!-- TODO:Добавить авторизацию и возможность предзаполнения этих полей, 
     в ином случаее они должны быть скрыты заглушкой "Войти" -->
    <div class="w-4/5">
      <div>
        <h2 class="text-2xl font-semibold mb-4">Персональные данные</h2>
        <form action="">
          <ul class="grid grid-cols-2 w-full gap-5 justify-center">
            <li class="relative">
              <label for="fio">ФИО</label>
              <input
                id="fio"
                class="p-2 bg-color-soft inner-shadow px-4 transition outline-none rounded-md w-full mt-2"
                type="text"
                placeholder="Иванов Иван Иванович"
              />
            </li>
            <li class="relative">
              <label for="telephone">Телефон</label>
              <input
                id="telephone"
                class="p-2 bg-color-soft inner-shadow px-4 transition outline-none rounded-md w-full mt-2"
                type="tel"
                placeholder="+7 (987) 123-45-67"
              />
            </li>
            <li class="relative">
              <label for="email">Email</label>
              <input
                id="email"
                class="p-2 bg-color-soft inner-shadow px-4 transition outline-none rounded-md w-full mt-2"
                type="email"
                placeholder="email@example.com"
              />
            </li>
            <li class="relative">
              <label for="sex">Пол</label>
              <select
                id="sex"
                class="p-2 bg-color-soft inner-shadow px-4 transition outline-none rounded-md w-full mt-2"
              >
                <!-- TODO:ТУТ СДЕЛАТЬ РОФЛ НАДА -->
                <option value="ns">Не указан</option>
                <option value="men">Мужской</option>
                <option value="women">Женский</option>
                <option value="pidor">Иное</option>
              </select>
            </li>
          </ul>
          <button
            class="mt-4 w-full font-bold rounded-xl p-3 hover-accent-shadow-box active:text-white disabled:opacity-50 disabled:shadow-none disabled:cursor-not-allowed transition cursor-pointer"
          >
            Сохранить
          </button>
        </form>
      </div>

      <div>
        <h2 class="text-2xl font-semibold mb-4 mt-8">Обновить пароль</h2>
        <form action="">
          <ul class="flex flex-col gap-5">
            <li class="relative">
              <label>Старый пароль</label
              ><input
                class="p-2 bg-color-soft inner-shadow px-4 transition outline-none rounded-md w-full mt-2"
                type="text"
              />
            </li>
            <li class="relative">
              <label>Новый пароль</label
              ><input
                class="p-2 bg-color-soft inner-shadow px-4 transition outline-none rounded-md w-full mt-2"
                type="text"
              />
            </li>
            <li class="relative">
              <label>Подтвердите новый пароль</label
              ><input
                class="p-2 bg-color-soft inner-shadow px-4 transition outline-none rounded-md w-full mt-2"
                type="text"
              />
            </li>
          </ul>
          <button
            class="mt-4 w-full font-bold rounded-xl p-3 hover-accent-shadow-box active:text-white disabled:opacity-50 disabled:shadow-none disabled:cursor-not-allowed transition cursor-pointer"
          >
            Сохранить
          </button>
        </form>
      </div>
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