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
    class="fixed z-10 right-5 bottom-5 flex flex-col p-5 rounded-xl"
    :class="state === 'error' ? 'error' : 'done'"
  >
    <div class="flex gap-2">
      <img
        class="w-5"
        :src="state === 'error' ? '../../../public/error.svg' : '../../../public/done.svg'"
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
</style>
