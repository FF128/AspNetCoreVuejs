import axios from "axios";

const API_ENDPOINT = "api/lang";

const state = {
  languages: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllLanguages(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.languages = response.data;
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
  getAllLanguages({ commit }) {
    commit("getAllLanguages");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
