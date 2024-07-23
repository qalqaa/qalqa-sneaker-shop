import { createStore } from 'vuex'

const store = createStore({
  state() {
    return {
      data: null
    }
  },
  mutations: {
    setData(state, payload) {
      state.data = payload
    }
  }
})

export default store
