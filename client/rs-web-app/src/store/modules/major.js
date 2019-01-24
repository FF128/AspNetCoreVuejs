import axios from "axios";

const API_ENDPOINT = "api/major";

const state = {
  majors: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllMajors(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.majors = response.data;
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
  getAllMajors({ commit }) {
    commit("getAllMajors");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
