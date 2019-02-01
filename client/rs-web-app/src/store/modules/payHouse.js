import axios from "axios";

const API_ENDPOINT = "api/pay-house";

const state = {
  payHouseData: [],
  loading: false,
  headers: [
    {
      text: "Code",
      align: "left",
      sortable: false,
      value: "code"
    },
    { text: "Description", value: "description", align: "left" },
    { text: "", value: "actions" }
  ]
};

const getters = {};

const mutations = {
  getAllPayHouseData(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.payHouseData = response.data;
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
  getAllPayHouseData({ commit }) {
    commit("getAllPayHouseData");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
