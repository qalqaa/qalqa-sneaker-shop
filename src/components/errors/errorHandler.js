import { inject, provide, ref } from 'vue'

const ErrorHandlerSymbol = Symbol('ErrorHandler')

export function useErrorHandlerProvider() {
  const error = ref(null)

  function handleError(err) {
    if (err.response) {
      switch (err.response.status) {
        case 400:
          error.value = 'Ошибка 400: Неверный запрос'
          break
        case 401:
          error.value = 'Ошибка 401: Неавторизован'
          break
        case 404:
          error.value = 'Ошибка 404: Не найдено'
          break
        case 500:
          error.value = 'Ошибка 500: Внутренняя ошибка сервера'
          break
        default:
          error.value = `Ошибка ${err.response.status}: ${err.response.data.message}`
      }
    } else if (err.request) {
      error.value = 'Ошибка: Сервер не ответил'
    } else {
      error.value = `Ошибка: ${err.message}`
    }
    console.error(error.value)
  }

  provide(ErrorHandlerSymbol, { error, handleError })

  return { error, handleError }
}

export function useErrorHandler() {
  const errorHandler = inject(ErrorHandlerSymbol)
  if (!errorHandler) {
    throw new Error('useErrorHandler must be used within a ErrorHandlerProvider')
  }
  return errorHandler
}
