const state = {
    activeRoute: 'STD'
}

const getters = {

}

const mutations = {
    changeRoute(state, payload) {
        if(typeof payload == "undefined"){
            payload = state.activeRoute
        }
        state.activeRoute = payload;
    }
}

const actions = {
    changeRoute({ commit }, payload){
        commit("changeRoute", payload)
    }
}

export default {
    state,
    getters,
    mutations,
    actions,
    namespaced: true
}