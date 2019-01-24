import axios from "axios";
import { Toast } from "buefy/dist/components/toast";

const state = {
  grades: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllGrades(state) {
    state.loading = true;
    axios
      .get("api/grade")
      .then(response => {
        state.grades = response.data;
        state.loading = false;
      })
      .catch(err => {
        Toast.open("Error!");
        state.loading = false;
      });
  }
};

const actions = {
  getAllGrades({ commit }) {
    commit("getAllGrades");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
