import axios from "axios";

const API_ENDPOINT = "api/license";

const state = {
  licenses: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllLicenses(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.licenses = response.data;
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
  getAllLicenses({ commit }) {
    commit("getAllLicenses");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
