<script setup>
import axios from 'axios'
import debounce from 'lodash.debounce'
import { inject, onMounted, reactive, ref, watch } from 'vue'

import List from '../components/home/List.vue'

const { cart } = inject('cart')

const items = ref([])
const isEmpty = ref(false)
const isAuth = inject('isAuth')
const token = ref(inject('token'))

const filters = reactive({
  sortOrder: 'title_desc',
  searchString: ''
})

const onChangeSelect = (event) => {
  filters.sortOrder = event.target.value
}

const onChangeSearchInput = debounce((event) => {
  filters.searchString = event.target.value
}, 500)

const fetchFavorites = async () => {
  if (isAuth.value) {
    try {
      const { data: favorites } = await axios.get('https://localhost:8081/api/Items/favorites', {
        headers: {
          Authorization: `Bearer ${token.value}`
        }
      })

      items.value = items.value.map((item) => {
        const favorite = favorites.find((favorite) => favorite.id === item.id)

        if (!favorite) {
          return item
        }

        return {
          ...item,
          isFavorite: true,
          favoriteId: favorite.id
        }
      })
    } catch (error) {
      console.error(error)
    }
  }
  return
}

const fetchItems = async () => {
  try {
    const params = {
      sortOrder: filters.sortOrder
    }

    if (filters.searchString) {
      params.searchString = `${filters.searchString}`
    }

    const { data } = await axios.get(`https://localhost:8081/api/items/sorted`, { params })

    if (data.length === 0) {
      isEmpty.value = true
    } else {
      isEmpty.value = false
    }

    items.value = data.map((obj) => ({
      ...obj,
      isFavorite: false,
      isAdded: false
    }))
  } catch (error) {
    console.error(error)
  }
}

onMounted(async () => {
  const localCart = [localStorage.getItem('cart')]
  if (localCart[0] !== null) {
    cart.value = localCart ? JSON.parse(localCart) : []
  }
  await fetchItems()
  await fetchFavorites()

  items.value = items.value.map((item) => ({
    ...item,
    isAdded: cart.value.some((cartItem) => cartItem.id === item.id)
  }))
})

watch(cart, () => {
  items.value = items.value.map((item) => ({
    ...item,
    isAdded: false
  }))
})

watch(filters, fetchItems)
</script>

<template>
  <div class="flex justify-between items-center flex-wrap">
    <h2 class="text-3xl font-bold md:mb-8 mb-2">Все кроссовки</h2>
    <div class="flex lg:w-1/2 md:w-3/5 w-full py-2 gap-2">
      <select
        @change="onChangeSelect"
        class="w-1/2 px-4 outline-none bg-color-soft inner-shadow rounded-md"
      >
        <option value="title_desc">По названию</option>
        <option value="price">По цене (возрастанию)</option>
        <option value="price_desc">По цене (убыванию)</option>
      </select>
      <div class="w-1/2 relative">
        <img class="absolute top-3 left-4" src="/search.svg" alt="search" />
        <input
          @input="onChangeSearchInput"
          class="p-2 bg-color-soft inner-shadow pl-11 pr-4 transition outline-none rounded-md w-full"
          type="text"
          placeholder="Поиск..."
        />
      </div>
    </div>
  </div>
  <div v-if="!isEmpty">
    <List :items="items" />
  </div>
  <div class="flex flex-col justify-center items-center" v-else>
    <img height="50" width="50" src="/emoji-1.png" alt="Sad emoji" />
    <p class="text-xl mt-3">По вашему запросу ничего не найдено</p>
  </div>
</template>
