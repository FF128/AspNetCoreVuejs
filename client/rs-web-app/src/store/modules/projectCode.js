import axios from "axios";

const API_ENDPOINT = "api/project-code";

const state = {
  projectCodeData: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllProjectCode(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.projectCodeData = response.data;
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
  getAllProjectCode({ commit }) {
    commit("getAllProjectCode");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
