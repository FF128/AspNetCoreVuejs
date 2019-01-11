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
    user
  }
});
