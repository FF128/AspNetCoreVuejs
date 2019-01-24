import axios from "axios";

const API_ENDPOINT = "api/multi-company";

const state = {
  multiCompData: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllMultiCompData(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.multiCompData = response.data;
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
    getAllMultiCompData({ commit }) {
        commit("getAllMultiCompData");
    }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
