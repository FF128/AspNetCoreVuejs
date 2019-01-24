import axios from "axios";

const API_ENDPOINT = "api/overall-ratings";

const state = {
  ratings: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllRatings(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.ratings = response.data;
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
  getAllRatings({ commit }) {
    commit("getAllRatings");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
