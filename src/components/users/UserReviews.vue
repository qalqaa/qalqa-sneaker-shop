<script setup>
import Rating from '@/components/ui/Raiting.vue'
</script>
<template>
  <div class="w-2/3">
    <h2 class="text-2xl font-semibold mb-3">Отзывы</h2>
    <div class="flex justify-between mb-3"> 
      <div class="flex gap-3">
        <h3 class="text-xl">4.8</h3>
        <Rating />
      </div>
      <button
        class="mb-2 w-1/3 font-bold rounded-xl px-2 py-1 hover-accent-shadow-box active:text-white disabled:opacity-50 disabled:shadow-none disabled:cursor-not-allowed transition cursor-pointer"
      >
        Оставить отзыв
      </button>
    </div>
    <div class="flex flex-col gap-3">
      <div class="bg-color-soft p-4 rounded-xl inner-shadow">
        <span class="text-lg font-semibold c-accent">Иван</span>
        <p>Lorem ipsum dolor sit</p>
      </div>
      <div class="bg-color-soft p-4 rounded-xl inner-shadow">
        <span class="text-lg font-semibold c-accent">Еблан</span>
        <p>Lorem ipsum dolor sit</p>
      </div>
      <div class="bg-color-soft p-4 rounded-xl inner-shadow">
        <span class="text-lg font-semibold c-accent">Хуета</span>
        <p>Lorem ipsum dolor sit</p>
      </div>
    </div>
  </div>
</template>

<style scoped></style>
