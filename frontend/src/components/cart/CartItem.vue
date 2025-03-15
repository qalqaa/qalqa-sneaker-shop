<script setup>
const emit = defineEmits(['onClickRemove'])

defineProps({
  id: Number,
  title: String,
  imageUrl: String,
  price: Number
})
</script>

<template>
  <div class="flex items-center bg-color-soft inner-shadow rounded-xl p-4 gap-4">
    <img class="w-20 h-15" :src="imageUrl" :alt="title" />

    <div class="flex flex-col flex-1">
      <p>{{ title }}</p>

      <div class="flex justify-between">
        <b class="flex-1 c-accent">{{ price }} руб.</b>
        <img
          @click="emit('onClickRemove')"
          class="opacity-30 cursor-pointer transition hover:opacity-100"
          src="/src/assets/imgs/close.svg"
          alt="close"
        />
      </div>
    </div>
  </div>
</template>
