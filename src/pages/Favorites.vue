<script setup>
import axios from 'axios'
import { inject, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'

import DrawerHead from '@/components/cart/DrawerHead.vue'
import NotAuthPlate from '@/components/ui/NotAuthPlate.vue'
import List from '../components/home/List.vue'

const favorites = ref([])
const router = useRouter()

const isAuth = inject('isAuth')
const token = ref(inject('token'))

onMounted(async () => {
  token.value = localStorage.getItem('token')
  console.log(token.value)
  if (isAuth.value) {
    try {
      const { data } = await axios.get('https://localhost:7228/api/Items/favorites', {
        headers: {
          Authorization: `Bearer ${token.value}`
        }
      })

      favorites.value = data.map((obj) => obj)
    } catch (error) {
      console.error(error)
    }
  }
  return
})

const goBack = () => {
  router.push('/')
}

const goToAuth = () => {
  router.push('/auth')
}
</script>

<template>
  <div v-if="isAuth">
    <DrawerHead :back-to-func="goBack" :title="'Избранное'" />
    <List :items="favorites" is-favorites />
  </div>
  <div v-else>
    <NotAuthPlate :go-to-auth="goToAuth" />
  </div>
</template>
