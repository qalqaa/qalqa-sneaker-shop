<script setup>
import { useRouter } from 'vue-router'

defineProps({
  state: Boolean
})

const router = useRouter()

const changeRegAuth = (event) => {
  router.push(event.target.id)
}

</script>

<template>
  <div class="flex items-center gap-5 mb-5 justify-center">
    <h2
      id="auth"
      @click="changeRegAuth"
      :class="{ active: state, inactive: !state }"
      class="align-middle text-xl font-bold"
    >
      Авторизация
    </h2>
    <h2
      id="register"
      @click="changeRegAuth"
      :class="{ active: !state, inactive: state }"
      class="align-middle text-xl font-bold"
    >
      Регистация
    </h2>
  </div>
</template>

<style scoped>
.active {
  color: var(--color-accent);
}

.inactive {
  opacity: 0.5;
  color: var(--vt-c-text-dark-2);
}
</style>
