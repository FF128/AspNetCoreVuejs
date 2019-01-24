import axios from "axios";
import { Toast } from "buefy/dist/components/toast";

const state = {
  employeeStatusFiles: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllEmployeeStatusFiles(state) {
    state.loading = true;
    axios
      .get("api/employee-status-file")
      .then(response => {
        state.employeeStatusFiles = response.data;
        state.loading = false;
      })
      .catch(err => {
        Toast.open("Error!");
        state.loading = false;
      });
  }
};

const actions = {
  getAllEmployeeStatusFiles({ commit }) {
    commit("getAllEmployeeStatusFiles");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
