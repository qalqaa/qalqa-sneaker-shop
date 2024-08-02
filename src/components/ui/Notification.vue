<script setup>
import { ref } from 'vue'

const props = defineProps({
  state: String,
  message: String
})

const title = ref('title')

switch (props.state) {
  case 'done':
    title.value = 'Успешно!'
    break
  case 'error':
    title.value = 'Ошибка!'
    break
}
</script>

<template>
  <div class="bg flex flex-col p-5" :class="state === 'error' ? 'error' : 'done'">
    <div class="flex gap-2">
      <img
        class="w-5 test"
        :src="state === 'error' ? '../../../public/error.svg' : '../../../public/done.svg'"
        alt=""
      />
      <h3 class="text-xl font-bold">{{ state === 'error' ? 'Ошибка!' : 'Успешно!' }}</h3>
    </div>
    <p class="overflow-auto">
      Lorem ipsum dolor sit amet consectetur, adipisicing elit. Dolores, Lorem ipsum dolor sit amet
      consdddddddddddddddddd
    </p>
  </div>
</template>

<style scoped>
.bg {
  position: fixed;
  z-index: 9999;
  right: 20px;
  bottom: 20px;
  border-radius: 10px;
  width: 500px;
  height: 120px;
}

.error {
  color: #e46962;
  background: #e4696244;
}

.done {
  color: #1cdb63;
  background: #1cdb6244;
}
</style>
