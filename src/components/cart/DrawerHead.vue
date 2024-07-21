<script setup>
defineProps({
  title: String,
  backToFunc: Function
})
</script>

<template>
  <div class="flex items-center gap-5 mb-5">
    <svg
      @click="backToFunc"
      class="opacity-50 cursor-pointer rotate-180 transition hover:opacity-100 hover:-translate-x-1"
      width="16"
      height="14"
      viewBox="0 0 16 14"
      fill="none"
    >
      <path
        d="M1 7H14.7143"
        stroke="white"
        stroke-width="2"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
      <path
        d="M8.71436 1L14.7144 7L8.71436 13"
        stroke="white"
        stroke-width="2"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
    </svg>

    <h2 class="align-middle text-3xl font-bold">{{ title }}</h2>
  </div>
</template>