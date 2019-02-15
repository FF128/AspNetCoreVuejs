import axios from "axios";
import Vue from "vue"

const API_ENDPOINT = "api/affiliations";

const state = {
  affData: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllAffiliations(state) {
    state.loading = true;
    Vue.axios
      .get(API_ENDPOINT)
      .then(response => {
        state.affData = response.data;
        state.loading = false;
      })
      .catch(err => {
        state.loading = false;
      });
  }
};

const actions = {
  getAllAffiliations({
    commit
  }) {
    commit("getAllAffiliations");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};