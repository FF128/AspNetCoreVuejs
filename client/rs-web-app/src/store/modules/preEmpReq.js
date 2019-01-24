import axios from "axios";

const API_ENDPOINT = "api/pre-emp-req";

const state = {
  preEmpReqData: [],
  loading: false
};

const getters = {};

const mutations = {
  getAllPreEmpReqData(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.preEmpReqData = response.data;
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
  getAllPreEmpReqData({ commit }) {
    commit("getAllPreEmpReqData");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
