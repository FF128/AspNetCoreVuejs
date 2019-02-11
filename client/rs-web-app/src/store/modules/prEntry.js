import axios from "axios";

const API_ENDPOINT = "api/pr";

const state = {
  prEntryData: [],
  returnedEntryData: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllPREntryData(state) {
    state.loading = true;
    axios
      .get(`${API_ENDPOINT}/waiting`)
      .then(response => {
        state.prEntryData = response.data;
        state.loading = false;
      })
      .catch(err => {
        state.loading = false;
      });
  },
  getAllReturnedPREntryData(state) {
    state.loading = true;
    axios
      .get(`${API_ENDPOINT}/returned`)
      .then(response => {
        state.returnedEntryData = response.data;
        state.loading = false;
      })
      .catch(err => {
        state.loading = false;
      });
  }
};

const actions = {
  getAllPREntryData({ commit }) {
    commit("getAllPREntryData");
  },
  getAllReturnedPREntryData({ commit }) {
    commit("getAllReturnedPREntryData");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
