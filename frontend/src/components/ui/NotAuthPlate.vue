<script setup>
defineProps({
  goToAuth: Function
})
</script>

<template>
  <div class="h-96 flex flex-col text-center justify-center items-center">
    <img height="50" width="50" src="/src/assets/imgs/emoji-1.png" alt="Sad emoji" />
    <p class="text-xl mb-5 mt-3">
      Увы, данная страница доступна только для авторизованных пользователей, чтобы получить к ней
      доступ нажмите кнопку ниже!
    </p>
    <button
      @click="goToAuth"
      class="mt-4 w-1/2 font-bold rounded-xl p-3 hover-accent-shadow-box active:text-white disabled:opacity-50 disabled:shadow-none disabled:cursor-not-allowed transition cursor-pointer"
    >
      Войти
    </button>
  </div>
</template>

<style scoped></style>
