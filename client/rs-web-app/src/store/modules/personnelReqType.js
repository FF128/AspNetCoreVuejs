import axios from "axios";

const API_ENDPOINT = "api/prt";

const state = {
  prtData: [],
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
  getAllPRTData(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.prtData = response.data;
        state.loading = false;
      })
      .catch(err => {
        state.loading = false;
      });
  }
};

const actions = {
  getAllPRTData({ commit }) {
    commit("getAllPRTData");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
