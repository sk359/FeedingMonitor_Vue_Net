import './assets/main.css'
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"


import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

/* import the fontawesome core */
import { library } from '@fortawesome/fontawesome-svg-core'

/* import font awesome icon component */
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

/* import specific icons */
import { faTrash } from '@fortawesome/free-solid-svg-icons'

/* add icons to the library */
library.add(faTrash)

const app = createApp(App)

app.use(router)

app.config.errorHandler = (err, instance, info) => {
  console.log("ERROR", err, info)
}

app.component('font-awesome-icon', FontAwesomeIcon).mount('#app')
