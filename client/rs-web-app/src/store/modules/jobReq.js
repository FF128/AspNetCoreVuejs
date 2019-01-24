import axios from "axios";

const API_ENDPOINT = "api/job-req";

const state = {
  jobReqData: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllJobReqData(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.jobReqData = response.data;
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
  getAllJobReqData({ commit }) {
    commit("getAllJobReqData");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
