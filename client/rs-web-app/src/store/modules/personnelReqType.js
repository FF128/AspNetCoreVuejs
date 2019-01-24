import axios from "axios";

const API_ENDPOINT = "api/prt";

const state = {
  prtData: [],
  loading: false
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
        // console.log(err.response.status)
        // if(err.response.status === 401){
        //     alert("Unauthorized")
        // }
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
