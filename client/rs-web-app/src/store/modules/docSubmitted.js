import axios from "axios";

const API_ENDPOINT = "api/docs";

const state = {
  docs: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllDocs(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.docs = response.data;
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
  getAllDocs({ commit }) {
    commit("getAllDocs");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
