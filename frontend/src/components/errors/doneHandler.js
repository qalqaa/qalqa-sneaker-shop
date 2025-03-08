import { inject, provide, ref } from 'vue'

const DoneHandlerSymbol = Symbol('DoneHandler')

export function useDoneHandlerProvider() {
  const done = ref(null)
  function handleDone(message) {
    let doneMessage = message
    done.value = doneMessage
    setTimeout(() => {
      done.value = null
    }, 5000)
  }
  provide(DoneHandlerSymbol, { done, handleDone })
  return { done, handleDone }
}

export function useDoneHandler() {
  const doneHandler = inject(DoneHandlerSymbol)
  return doneHandler
}
