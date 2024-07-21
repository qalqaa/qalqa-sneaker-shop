<script setup>
import { onMounted, ref, inject } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'

import List from '../components/home/List.vue'
import DrawerHead from '@/components/cart/DrawerHead.vue'
import NotAuthPlate from '@/components/ui/ux/NotAuthPlate.vue'

const favorites = ref([])
const router = useRouter()

onMounted(async () => {
  try {
    const { data } = await axios.get(
      'https://4c860bad2146c5b3.mokky.dev/favorites?_relations=items'
    )

    favorites.value = data.map((obj) => obj.item)
  } catch (error) {
    console.error(error)
  }
})

const goBack = () => {
  router.push('/')
}

const isAuth = inject('isAuth')

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
