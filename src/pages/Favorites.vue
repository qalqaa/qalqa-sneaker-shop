<script setup>
import { onMounted, ref } from 'vue'
import axios from 'axios'

import List from '../components/List.vue'
import DrawerHead from '@/components/DrawerHead.vue'
import { useRouter } from 'vue-router'

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
</script>


<template>
  <DrawerHead :back-to-func="goBack" :title="'Избранное'" />
  <List :items="favorites" is-favorites />
</template>