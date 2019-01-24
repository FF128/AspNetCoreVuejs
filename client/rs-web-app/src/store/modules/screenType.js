import axios from "axios";

const API_ENDPOINT = "api/screentype";

const state = {
  stData: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllTypes(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.stData = response.data;
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
  getAllTypes({ commit }) {
    commit("getAllTypes");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
