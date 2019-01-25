import axios from "axios";
import Toast from "@/project-modules/toast";
let toast = new Toast();
const API_ENDPOINT = "api/eval-email-format";

const state = {};

const getters = {};
const mutations = {
  saveEvalEmailFormat(state, payload) {
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
  saveEvalEmailFormat({ commit }, payload) {
    commit("saveEvalEmailFormat", payload);
  }
};

export default {
  state,
  getters,
  mutations,
  actions,
  namespaced: true
};
