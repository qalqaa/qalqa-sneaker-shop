<script setup>
import { computed, onMounted, onUnmounted, provide, ref, watch } from 'vue'

import Drawer from './components/cart/Drawer.vue'
import Header from './components/home/Header.vue'

const isAuth = ref(false)
provide('isAuth', isAuth)

const token = ref(localStorage.getItem('token'))
provide('token', token.value)
console.log(token.value)

onMounted(() => {})

onUnmounted(() => {
  localStorage.removeItem('token')
})

/*Корзина*/
const cart = ref([])

const cartOpen = ref(false)

const totalPrice = computed(() => cart.value.reduce((acc, item) => acc + item.price, 0))
const vatPrice = computed(() => totalPrice.value * 0.2)

const closeCart = () => {
  cartOpen.value = false
}
const openCart = () => {
  cartOpen.value = true
}

function addToCart(item) {
  cart.value.push(item)
  item.isAdded = true
}

function removeFromCart(item) {
  cart.value.splice(cart.value.indexOf(item), 1)
  item.isAdded = false
}

watch(
  cart,
  () => {
    localStorage.setItem('cart', JSON.stringify(cart.value))
  },
  { deep: true }
)

provide('cart', { cart, closeCart, openCart, addToCart, removeFromCart })

/*Корзина*/
</script>

<template>
  <Drawer v-if="cartOpen" :total-price="totalPrice" :vat-price="vatPrice" />
  <div
    class="bg-color-mute m-auto rounded-none shadow-xl md:rounded-xl md:w-4/5 w-full md:my-10 my-0"
  >
    <Header :total-price="totalPrice" @open-cart="openCart" />
    <div class="p-5 sm:p-10">
      <RouterView />
    </div>
  </div>
</template>
