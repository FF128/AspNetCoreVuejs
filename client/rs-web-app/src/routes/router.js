import Vue from "vue";
import Router from "vue-router";
import AuthService from "../_services/auth";

let authService = new AuthService();
Vue.use(Router);

import routes from "./index"
const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});
router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ["/login", "/register"];
  const authRequired = !publicPages.includes(to.path);
  // const loggedIn = localStorage.getItem('_u');
  const loggedIn = authService.isLoggedIn();

  if (authRequired && !loggedIn) {
    return next("/login");
  }

  next();
});

export default router;
