<script setup>
import { onMounted, onUnmounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import axios from 'axios'

import DrawerHead from '@/components/cart/DrawerHead.vue'
import UserReviews from '@/components/users/UserReviews.vue'

const item = ref({
  id: 0,
  title: '',
  price: 0,
  imageUrl: '',
  description: {
    info1: '',
    info2: '',
    material: '',
    brand: '',
    season: '',
    color: '',
    country: '',
    article: ''
  },
  rating: 4.7,
  reviews: [
    {
      estimation: 4,
      id: 1,
      reviewer: 'pisun',
      text: 'ну пойдет'
    }
  ]
})

const route = useRoute()
const router = useRouter()
const id = route.params.id

const getData = async () => {
  const { data } = await axios.get(`https://localhost:7228/api/Items/info/${id}`)
  item.value = data
}


const isFavorite = ref(false)

const addToFavorite = async (id) => {
  try {
    if (!isFavorite.value) {
      const obj = {
        item_id: id
      }
      await axios.post(`https://4c860bad2146c5b3.mokky.dev/favorites`, obj)
      return
    }
    if (isFavorite.value) {
      await axios.delete(`https://4c860bad2146c5b3.mokky.dev/favorites/${item.value.id}`)
      return
    }
  } catch (err) {
    console.error(err)
  }
}

const goBack = () => {
  router.push('/')
}

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
  currentPosition.value.x += (targetPosition.value.x - currentPosition.value.x) * 0.04
  currentPosition.value.y += (targetPosition.value.y - currentPosition.value.y) * 0.04

  glowStyle.value.left = `${currentPosition.value.x - 250}px`
  glowStyle.value.top = `${currentPosition.value.y - 250}px`

  animationFrameId = requestAnimationFrame(updateGlowPosition)
}

const isOpened = ref(false)

onMounted(() => {
  getData()
  console.log(id)
  animationFrameId = requestAnimationFrame(updateGlowPosition)
})

onUnmounted(() => {
  cancelAnimationFrame(animationFrameId)
})
</script>

<template>
  <div class="py-5">
    <DrawerHead :back-to-func="goBack" :title="item.title" />
    <div class="flex lg:flex-row flex-col mb-5">
      <div
        class="flex flex-col lg:w-2/3 w-full gap-5 bg-color-soft inner-shadow px-4 transition glow-area"
        @mousemove="handleMouseMove"
        @mouseenter="handleMouseEnter"
        @mouseleave="handleMouseLeave"
      >
        <img class="z-10 transition card-lightning" :src="item.imageUrl" alt="Sneaker" />
        <div class="glow" :style="glowStyle"></div>
      </div>
      <div class="lg:w-1/3 w-full p-5">
        <div class="flex lg:text-2xl gap-2">
          <span>Цена:</span>
          <b class="c-accent">{{ item.price }} руб.</b>
        </div>
        <p class="mt-2">
          {{ item.description.info1 }}
        </p>
        <div class="flex w-full justify-stretch gap-2 mt-4">
          <button
            @click="null"
            class="font-bold w-full max-h-12 rounded-xl py-3 hover-accent-shadow-box active:text-white disabled:opacity-50 disabled:shadow-none disabled:cursor-not-allowed transition cursor-pointer"
          >
            В корзину
          </button>
          <img
            @click="addToFavorite(item.id)"
            :src="isFavorite ? '/like-2.svg' : '/like-1.svg'"
            alt="likeButton"
            class="cursor-pointer h-12"
          />
        </div>
      </div>
    </div>
    <div @click="isOpened = !isOpened" class="w-full flex justify-center">
      <div :class="isOpened ? 'rotate' : 'rotate-back'">
        <div class="bounce"></div>
      </div>
    </div>
    <div :class="isOpened ? 'open' : 'close'" class="flex flex-col">
      <p class="mt-5">
        {{ item.description.info2 }}
      </p>
      <div class="flex lg:flex-row flex-col gap-10 mt-7">
        <div class="flex flex-col gap-5 mb-6 lg:w-1/3 w-full">
          <div class="flex gap-2">
            <span>Материал: </span>
            <div class="flex-1 border-zinc-400 border-b border-dashed"></div>
            <b>{{ item.description.material }}</b>
          </div>
          <div class="flex gap-2">
            <span>Бренд: </span>
            <div class="flex-1 border-zinc-400 border-b border-dashed"></div>
            <b>{{ item.description.brand }}</b>
          </div>
          <div class="flex gap-2">
            <span>Сезон: </span>
            <div class="flex-1 border-zinc-400 border-b border-dashed"></div>
            <b>{{ item.description.season }}</b>
          </div>
          <div class="flex gap-2">
            <span>Цвет: </span>
            <div class="flex-1 border-zinc-400 border-b border-dashed"></div>
            <b>{{ item.description.color }}</b>
          </div>
          <div class="flex gap-2">
            <span>Страна производства: </span>
            <div class="flex-1 border-zinc-400 border-b border-dashed"></div>
            <b>{{ item.description.country }}</b>
          </div>
          <div class="flex gap-2">
            <span>Артикул: </span>
            <div class="flex-1 border-zinc-400 border-b border-dashed"></div>
            <b>{{ item.description.article }}</b>
          </div>
        </div>
        <UserReviews :rating="item.rating" :reviews="item.reviews" />
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

.bounce {
  width: 50px;
  height: 50px;
  background: url('/arrow-down-short.svg') no-repeat;
  background-size: contain;
  position: relative;
  animation: bounce 1.5s infinite;
}

@keyframes bounce {
  0%,
  20%,
  50%,
  80%,
  100% {
    transform: translateY(0);
  }
  40% {
    transform: translateY(-10px);
  }
  60% {
    transform: translateY(-5px);
  }
}

.rotate {
  transition: transform 0.5s ease-in-out;
  transform: rotate(180deg);
}

.rotate-back {
  transition: transform 0.5s ease-in-out;
  transform: rotate(0deg);
}

.close {
  max-height: 0px;
  overflow: hidden;
  transition:
    max-height 0.5s ease,
    opacity 0.5s ease;
  opacity: 0;
}

.open {
  opacity: 1;
  max-height: 1000px;
  transition:
    max-height 0.5s ease,
    opacity 0.5s ease;
}
</style>
