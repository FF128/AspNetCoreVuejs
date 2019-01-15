import Vue from "vue";
import Vuex from "vuex";
// Import Modules
import citizenship from "./modules/citizenship"
import religion from "./modules/religion"
import employeeStatusFile from './modules/employeeStatusFile'
import jobLevel from "./modules/jobLevel"
import grade from "./modules/grade"
import step from "./modules/step"
import designationFile from "./modules/designationFile"
import user from "./modules/user"
import area from "./modules/area"
import branch from "./modules/branch"
import department from "./modules/department"
import division from "./modules/division"
import location from "./modules/location"
import unit from "./modules/unit"
import section from "./modules/section"
import rank from "./modules/rank"
import projectCode from "./modules/projectCode"
import payHouse from "./modules/payHouse"
import region from "./modules/region"
import loe from "./modules/loe"

Vue.use(Vuex);

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  modules: {
    citizenship,
    religion,
    employeeStatusFile,
    jobLevel,
    grade,
    step,
    designationFile,
    user,
    area,
    branch,
    department,
    division,
    location,
    unit,
    section,
    rank,
    projectCode,
    payHouse,
    region,
    loe
  }
});
