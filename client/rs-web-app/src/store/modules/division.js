import axios from "axios";

const API_ENDPOINT = "api/div";

const state = {
  divisions: [],
  headers: [
    {
      text: "Code",
      align: "left",
      sortable: false,
      value: "code"
    },
    { text: "Description", value: "description", align: "left" },
    { text: "", value: "actions" }
  ],
  loading: false
};

const getters = {};

const mutations = {
  getAllDivisions(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.divisions = response.data;
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
  getAllDivisions({ commit }) {
    commit("getAllDivisions");
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
