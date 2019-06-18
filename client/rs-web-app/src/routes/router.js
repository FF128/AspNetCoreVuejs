import Vue from "vue";
import store from "../store/store"
import Router from "vue-router";
import AuthService from "../_services/auth";
import publicPages from "../_helpers/publicPages"

//let authService = new AuthService();
// Create new Instance to use vuex in vue router
let vue = new Vue({
  store
})

Vue.use(Router);

import routes from "./index"
const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

//router.beforeEach((to, from, next) => {
// redirect to login page if not logged in and trying to access a restricted page
//const publicPages = ["/login", "/register", "/home-job"];
//const authRequired = !publicPages.includes(to.name);
// const loggedIn = localStorage.getItem('_u');
// const loggedIn = authService.isLoggedIn();
// const appLoggedIn = authService.applicantIsLoggeIn();

// Mutate Active Route
// vue.$store.commit("changeRoute", {
//   activeRoute: to.meta.activeRoute,
//   activeRouteName: to.meta.activeRouteName
// })

// if (to.meta.mod == "app") {
//   if (authRequired && !appLoggedIn) {
//     return next("/home-job");
//   }
// } else {
//   if (authRequired && !loggedIn) {
//     return next("/login");
//   }
// }

// next();
//});

export default router;