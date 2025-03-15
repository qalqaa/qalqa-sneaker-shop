<script setup>
import { onMounted, ref } from 'vue'

defineProps({
  state: String,
  message: String
})

const isVisible = ref(true)
const duration = 5000

onMounted(() => {
  setTimeout(() => {
    isVisible.value = false
  }, duration)
})
</script>

<template>
  <div
    v-if="isVisible"
    class="fixed z-10 right-5 bottom-5 flex flex-col p-5 rounded-xl transition opacity-100"
    :class="[state === 'error' ? 'error' : 'done']"
  >
    <div class="flex gap-2">
      <img
        class="w-5"
        :src="state === 'error' ? '../src/assets/imgs/error.svg' : '../src/assets/imgs/done.svg'"
        alt=""
      />
      <h3 class="text-xl font-bold">{{ state === 'error' ? 'Ошибка!' : 'Успешно!' }}</h3>
    </div>
    <p class="overflow-auto">
      {{ message }}
    </p>
  </div>
</template>

<style scoped>
.error {
  color: #e46962;
  background: #e4696244;
}

.done {
  color: #1cdb63;
  background: #1cdb6244;
}

.transition {
  opacity: 0;
  animation: fadeInOut 5s forwards;
}

@keyframes fadeInOut {
  0% {
    opacity: 0;
  }
  5% {
    opacity: 1;
  }
  95% {
    opacity: 1;
  }
  100% {
    opacity: 0;
  }
}
</style>
