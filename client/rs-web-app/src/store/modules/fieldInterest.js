import axios from "axios";

const API_ENDPOINT = "api/foi";

const state = {
  foiData: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllFOIData(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.foiData = response.data;
        state.loading = false;
      })
      .catch(err => {
        // console.log(err.response.status)
        // if(err.response.status === 401){
        //     alert("Unauthorized")
        // }
        state.loading = false;
      });
  }
};

const actions = {
  getAllFOIData({ commit }) {
    commit("getAllFOIData");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
