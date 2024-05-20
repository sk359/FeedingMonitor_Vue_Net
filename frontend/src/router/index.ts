import { createRouter, createWebHistory } from 'vue-router'
import BewegdatenView from '../views/BewegdatenView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'bewegdaten',
      component: BewegdatenView
    },
    {
      path: '/stammdaten',
      name: 'stammdaten',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/StammdatenView.vue')
    }
  ]
})

export default router
