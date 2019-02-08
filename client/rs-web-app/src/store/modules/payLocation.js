import axios from "axios";

const API_ENDPOINT = "api/pay-location";

const state = {
  payLocations: [],
  loading: false,
  headers: [
    {
      text: "Code",
      align: "left",
      sortable: false,
      value: "code"
    },
    { text: "Description", value: "description", align: "left" },
    { text: "", value: "actions" }
  ]
};

const getters = {};

const mutations = {
  getAllPayLocations(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.payLocations = response.data;
        state.loading = false;
      })
      .catch(err => {
        state.loading = false;
      });
  }
};

const actions = {
  getAllPayLocations({ commit }) {
    commit("getAllPayLocations");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
