import axios from "axios";

const API_ENDPOINT = "api/jobgroup";

const state = {
  jobGroups: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllJobGroups(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.jobGroups = response.data;
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
  getAllJobGroups({ commit }) {
    commit("getAllJobGroups");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
