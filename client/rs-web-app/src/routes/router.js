import Vue from "vue";
import Router from "vue-router";
Vue.use(Router);

// Default Layout 
let Layout = () => import("../layouts/VuetifyLayout.vue");
// About
let Home  = () => import("../views/Home.vue");
let About = () => import(/* webpackChunkName: "about" */ "../views/About.vue");

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
export default new Router({
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
      path: "/",
      name: "about",
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: Layout,
      children: [
        {
          component: About,
          path: "about"
        }
      ]
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
