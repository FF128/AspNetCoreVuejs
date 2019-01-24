import "babel-polyfill";
import Vue from "vue";
import "./plugins/vuetify";
import "./plugins/axios";
import App from "./App.vue";
import router from "./routes/router";
import store from "./store/store";
//import "./registerServiceWorker";
// import Buefy from 'buefy'
// import 'buefy/dist/buefy.css'
import "./filters/filter";
import VueJWT from "vuejs-jwt";
import Toasted from "vue-toasted";
// import VueMaterial from 'vue-material'
// import 'vue-material/dist/vue-material.min.css'

// Vue.use(VueMaterial)

Vue.use(Toasted);

Vue.use(VueJWT);

// Vue.use(Buefy)

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
