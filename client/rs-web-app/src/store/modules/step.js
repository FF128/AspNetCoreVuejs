import axios from "axios";

const state = {
  steps: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllSteps(state) {
    state.loading = true;
    axios
      .get("api/step")
      .then(response => {
        state.steps = response.data;
        state.loading = false;
      })
      .catch(err => {
        state.loading = false;
      });
  }
};

const actions = {
  getAllSteps({ commit }) {
    commit("getAllSteps");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
