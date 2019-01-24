import axios from "axios";

const API_ENDPOINT = "api/screen-details";

const state = {
  screenDetails: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllDetails(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.screenDetails = response.data;
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
    getAllDetails({ commit }) {
    commit("getAllDetails");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};