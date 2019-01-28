import axios from "axios";
import Toast from "@/project-modules/toast";
let toast = new Toast();
const API_ENDPOINT = "api/sms-format";

const state = {};

const getters = {};
const mutations = {
  getAllJobCategories(state) {
    state.loading = true;
    axios
      .get(API_ENDPOINT)
      .then(response => {
        state.jobCategories = response.data;
        state.loading = false;
      })
      .catch(err => {
        // console.log(err.response.status)
        // if(err.response.status === 401){
        //     alert("Unauthorized")
        // }
        state.loading = false;
      });
  },
  saveSMSFormat(state, payload) {
    axios
      .post(API_ENDPOINT, payload)
      .then(({ data }) => {
        toast.show(data);
      })
      .catch(({ response }) => {
        toast.show(response.data);
      });
  }
};

const actions = {
  getAllJobCategories({ commit }) {
    commit("getAllJobCategories");
  },
  saveSMSFormat({ commit }, payload) {
    commit("saveSMSFormat", payload);
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};