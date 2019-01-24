import axios from "axios";
import { Toast } from "buefy/dist/components/toast";

const state = {
  jobLevels: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllJobLevels(state) {
    state.loading = true;
    axios
      .get("api/job-level")
      .then(response => {
        state.jobLevels = response.data;
        state.loading = false;
      })
      .catch(err => {
        Toast.open("Error!");
        state.loading = false;
      });
  }
};

const actions = {
  getAllJobLevels({ commit }) {
    commit("getAllJobLevels");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
