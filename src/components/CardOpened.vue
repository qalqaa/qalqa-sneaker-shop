<script setup>
import { onMounted, onUnmounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import axios from 'axios'

import DrawerHead from '@/components/DrawerHead.vue'

defineProps({})

const item = ref({})

const route = useRoute()
const router = useRouter()
const id = route.params.id

const getData = async () => {
  const { data } = await axios.get(`https://4c860bad2146c5b3.mokky.dev/items/${id}`)
  item.value = data
}

const goBack = () => {
  router.push('/')
}

onMounted(() => {
  getData()
  animationFrameId = requestAnimationFrame(updateGlowPosition)
})

const glowStyle = ref({
  left: '0px',
  top: '0px',
  opacity: '0'
})

const targetPosition = ref({ x: 0, y: 0 })
const currentPosition = ref({ x: 0, y: 0 })
let animationFrameId

const handleMouseMove = (e) => {
  const rect = e.target.getBoundingClientRect()
  targetPosition.value.x = e.clientX - rect.left
  targetPosition.value.y = e.clientY - rect.top
}

const handleMouseEnter = () => {
  glowStyle.value.opacity = '1'
}

const handleMouseLeave = () => {
  glowStyle.value.opacity = '0'
}

const updateGlowPosition = () => {
  currentPosition.value.x += (targetPosition.value.x - currentPosition.value.x) * 0.1
  currentPosition.value.y += (targetPosition.value.y - currentPosition.value.y) * 0.1

  glowStyle.value.left = `${currentPosition.value.x - 250}px`
  glowStyle.value.top = `${currentPosition.value.y - 250}px`

  animationFrameId = requestAnimationFrame(updateGlowPosition)
}

onUnmounted(() => {
  cancelAnimationFrame(animationFrameId)
})
</script>

<template>
  <div>
    <DrawerHead :back-to-func="goBack" :title="item.title" />
    <div class="flex">
      <div
        class="flex flex-col w-2/3 gap-5 bg-color-soft inner-shadow px-4 transition glow-area"
        @mousemove="handleMouseMove"
        @mouseenter="handleMouseEnter"
        @mouseleave="handleMouseLeave"
      >
        <img class="test transition card-lightning" :src="item.imageUrl" alt="Sneaker" />
        <div class="glow" :style="glowStyle"></div>
      </div>
      <div class="w-1/3 p-5">
        <div class="flex text-2xl gap-2">
          <span>Цена:</span>
          <b class="c-accent">{{ item.price }} руб.</b>
        </div>
        <div class="flex gap-2 mt-4">
          <button
            class="w-full font-bold rounded-xl p-3 hover-accent-shadow-box active:text-white disabled:opacity-50 disabled:shadow-none disabled:cursor-not-allowed transition cursor-pointer"
          >
            В корзину
          </button>
          <img :src="'/like-2.svg'" alt="likeButton" class="cursor-pointer w-1/5" />
        </div>
        <p class="mt-4">
          Lorem ipsum, dolor sit amet consectetur adipisicing elit. Illo sed inventore, consectetur
          autem ab totam, veritatis eos sit doloribus rerum, quaerat maiores alias neque non
          adipisci. Nihil fugit eligendi cum?
        </p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.glow-area {
  border-radius: 20px;
  position: relative;
  overflow: hidden;
}

.test {
  z-index: 10;
}

.glow {
  position: absolute;
  z-index: 0;
  width: 500px;
  height: 500px;
  border-radius: 50%;
  background: radial-gradient(circle at center, var(--color-accent) 0%, rgba(129, 255, 125, 0) 70%);
  pointer-events: none;
  transition:
    opacity 0.2s ease,
    transform 0.1s ease;
  mix-blend-mode: overlay;
  filter: blur(100px);
}
</style>
