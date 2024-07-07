<script setup>
import axios from 'axios'
import { inject, ref, computed } from 'vue'

import DrawerHead from './DrawerHead.vue'
import CartItemList from './CartItemList.vue'
import infoBlock from './infoBlock.vue'

const props = defineProps({
  totalPrice: Number,
  vatPrice: Number
})

const { cart, closeCart } = inject('cart')

const isLoading = ref(false)
const orderId = ref(null)

const createOrder = async () => {
  try {
    isLoading.value = true
    const { data } = await axios.post(`https://4c860bad2146c5b3.mokky.dev/orders`, {
      items: cart.value,
      totalPrice: props.totalPrice.value
    })
    cart.value = []
    orderId.value = data.id
    return data
  } catch (error) {
    console.error(error)
  } finally {
    isLoading.value = false
  }
}

const cartIsEmpty = computed(() => cart.value.length === 0)
const buttonDisabled = computed(() => isLoading.value || cartIsEmpty.value)
</script>

<template>
  <div @click="closeCart" class="fixed top-0 left-0 w-full h-full bg-black/70 z-20"></div>
  <div class="bg-color-mute w-96 h-full fixed right-0 top-0 z-30 p-10">
    <DrawerHead :backToFunc="closeCart" title="Корзина" />

    <div v-if="!totalPrice || orderId" class="flex h-full items-center">
      <infoBlock
        v-if="!totalPrice && !orderId"
        title="Корзина пуста"
        descr="Добавьте хотя бы одну пару кроссовок, чтобы оформить
    заказ."
        imgUrl="/package-icon.png"
      />

      <infoBlock
        v-if="orderId"
        title="Заказ оформлен"
        :descr="`Ваш заказ #${orderId} скоро будет передан курьерской службе.`"
        imgUrl="/order-success-icon.png"
      />
    </div>

    <div v-else>
      <CartItemList />
      <div class="flex flex-col gap-5 mt-7 mb-6">
        <div class="flex gap-2">
          <span>Итого: </span>
          <div class="flex-1 border-zinc-400 border-b border-dashed"></div>
          <b>{{ totalPrice }} ₽</b>
        </div>
        <div class="flex gap-2">
          <span>НДС 20%: </span>
          <div class="flex-1 border-zinc-400 border-b border-dashed"></div>
          <b>{{ vatPrice }} ₽</b>
        </div>
        <button
          class="mt-4 w-full font-bold rounded-xl p-3 hover-accent-shadow-box active:text-white disabled:opacity-50 disabled:shadow-none disabled:cursor-not-allowed transition cursor-pointer"
          @click="createOrder"
          :disabled="buttonDisabled"
        >
          Оформить заказ
        </button>
      </div>
    </div>
  </div>
</template>