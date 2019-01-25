import Vue from "vue";
import Router from "vue-router";
import AuthService from "../_services/auth";

let authService = new AuthService();
Vue.use(Router);

// Default Layout
// let Layout = () => import("../layouts/VuetifyLayout.vue");
// About
let Home = () => import("../views/Home.vue");
// Login Component
let Login = () => import("../views/user/Login.vue");
// Company Info
let CompanyInfo = () =>
  import("../views/setup/standard/general-setup/company-info/CompanyInfo.vue");
let companyInfoRoute = {
  path: "/company-info",
  component: CompanyInfo
};
// End Company info

//Citizenship
const Citizenship = resolve =>
  require([
    "../views/setup/standard/personal-information/citizenship/Citizenship.vue"
  ], resolve);
//let Citizenship = () => import("../views/setup/standard/personal-information/citizenship/Citizenship.vue");
let citizenshipRoute = {
  path: "/citizenship",
  component: Citizenship
};
// End Citizenship

// Religion
let Religion = resolve =>
  require([
    "../views/setup/standard/personal-information/religion/Religion.vue"
  ], resolve);
let religionRoute = {
  path: "/religion",
  component: Religion
};
// End Religion
import redirectionRoute from "./redirectionRoute";

import employeeStatusFileRoute from "./employeeStatusFileRoute";
import jobLevelRoute from "./jobLevelRoute";
import gradeRoute from "./gradeRoute";
import stepRoute from "./stepRoute";
import designationFileRoute from "./designationFileRoute";
import areaRoute from "./areaRoute";
import branchRoute from "./branchRoute";
import depRoute from "./depRoute";
import divRoute from "./divRoute";
import locationRoute from "./locationRoute";
import unitRoute from "./unitRoute";
import sectionRoute from "./sectionRoute";
import rankRoute from "./rankRoute";
import projectCodeRoute from "./projectCodeRoute";
import payHouseRoute from "./payHouseRoute";
import regionRoute from "./regionRoute";
import loeRoute from "./loeRoute";
import courseRoute from "./courseRoute";
import majorRoute from "./majorRoute";
import schoolRoute from "./schoolRoute";
import dutiesRoute from "./drRoute";
import foiRoute from "./foiRoute";
import govExamsRoute from "./govExamsRoute";
import jobReqRoute from "./jobReqRoute";
import langRoute from "./languageRoute";
import licenseRoute from "./licenseRoute";
import residenceTypeRoute from "./residenceTypeRoute";
import skillsRoute from "./skillsRoute";
import docsRoute from "./docsRoute";
import affRoute from "./affRoute";
import prtRoute from "./prtRoute";
import screenTypeRoute from "./screenTypeRoute";
import ratingsRoute from "./ratingsRoute";
import overallRatingsRoute from "./overallRatingsRoute";
import preEmpReqRoute from "./preEmpReqRoute";
import dutiesReqRoute from "./dutiesReqRoute";
import appEntryRoute from "./appEntryRoute";
import jobGroupRoute from "./jobGroupRoute";
import jobCategoryRoute from "./jobCategoryRoute";
import emailFormatRoute from "./emailFormat";
import screenDetailsRoute from "./screenDetailsRoute"
import multiCompanyRoute from "./multiCompanyRoute"
import smsFormatRoute from "./smsFormatRoute"
import evalEmailFormatRoute from "./evalEmailFormatRoute"

const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      component: Home
    },
    {
      path: "/login",
      component: Login
    },
    redirectionRoute,
    companyInfoRoute,
    citizenshipRoute,
    religionRoute,
    employeeStatusFileRoute,
    jobLevelRoute,
    gradeRoute,
    stepRoute,
    designationFileRoute,
    areaRoute,
    branchRoute,
    depRoute,
    divRoute,
    locationRoute,
    unitRoute,
    sectionRoute,
    rankRoute,
    projectCodeRoute,
    payHouseRoute,
    regionRoute,
    loeRoute,
    courseRoute,
    majorRoute,
    schoolRoute,
    dutiesRoute,
    foiRoute,
    govExamsRoute,
    jobReqRoute,
    langRoute,
    licenseRoute,
    residenceTypeRoute,
    skillsRoute,
    docsRoute,
    affRoute,
    prtRoute,
    screenTypeRoute,
    ratingsRoute,
    overallRatingsRoute,
    preEmpReqRoute,
    dutiesReqRoute,
    appEntryRoute,
    jobGroupRoute,
    jobCategoryRoute,
    emailFormatRoute,
    screenDetailsRoute,
    multiCompanyRoute,
    smsFormatRoute,
    evalEmailFormatRoute
  ]
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
