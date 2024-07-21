<script setup>
defineProps({
  imgUrl: String,
  title: String,
  descr: String
})
</script>

<template>
  <div class="flex flex-col gap-3 items-center text-center w-72 mx-auto back-shadow">
    <img height="120" width="120" :src="imgUrl" alt="Info image" />
    <h2 class="text-2xl font-medium">{{ title }}</h2>
    <span>{{ descr }}</span>
  </div>
</template>

<style scoped>
.back-shadow {
  background: radial-gradient(var(--color-accent-mute) 0%, transparent 70%);
}
</style>