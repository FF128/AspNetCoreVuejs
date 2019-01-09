import Vue from "vue";
import Vuex from "vuex";
// Import Modules
import citizenship from "./modules/citizenship"
import religion from "./modules/religion"
import employeeStatusFile from './modules/employeeStatusFile'
import jobLevel from "./modules/jobLevel"

Vue.use(Vuex);

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  modules: {
    citizenship,
    religion,
    employeeStatusFile,
    jobLevel
  }
});
