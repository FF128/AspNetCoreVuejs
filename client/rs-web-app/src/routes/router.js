import Vue from "vue";
import Router from "vue-router";
Vue.use(Router);

// Default Layout 
let Layout = () => import("../layouts/VuetifyLayout.vue");
// About
let Home  = () => import("../views/Home.vue");
// Login Component
let Login = () => import("../views/user/Login.vue")
// Company Info
let CompanyInfo = () => import("../views/setup/standard/general-setup/company-info/CompanyInfo.vue")
let companyInfoRoute = {
  path: "/",
  component: Layout,
  children: [
    {
      path: "company-info",
      component: CompanyInfo
    }
  ]
}
// End Company info

//Citizenship 
let Citizenship = () => import("../views/setup/standard/personal-information/citizenship/Citizenship.vue");
let citizenshipRoute = {
  path: "/",
  component: Layout,
  children: [
    {
      path: "citizenship",
      component: Citizenship
    }
  ]
}
// End Citizenship

// Religion 
let Religion = () => import("../views/setup/standard/personal-information/religion/Religion.vue")
let religionRoute = {
  path: "/",
  component: Layout,
  children: [
    {
      path: "religion",
      component: Religion
    }
  ]
}
// End Religion

import employeeStatusFileRoute from "./employeeStatusFileRoute"
import jobLevelRoute from "./jobLevelRoute"
import gradeRoute from "./gradeRoute"
import stepRoute from "./stepRoute"
import designationFileRoute from "./designationFileRoute"
const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      component: Layout,
      children: [
        {
          component: Home,
          name: "Home",
          path: ""
        }
      ]
    },
    {
      path: "/login",
      component: Login
    },
    companyInfoRoute,
    citizenshipRoute,
    religionRoute,
    employeeStatusFileRoute,
    jobLevelRoute,
    gradeRoute,
    stepRoute,
    designationFileRoute
  ]
});

// router.beforeEach((to, from, next) => {
//   // redirect to login page if not logged in and trying to access a restricted page
//   const publicPages = ['/login', '/register'];
//   const authRequired = !publicPages.includes(to.path);
//   const loggedIn = localStorage.getItem('_u');

//   if (authRequired && !loggedIn) {
//     return next('/login');
//   }

//   next();
// })

export default router;