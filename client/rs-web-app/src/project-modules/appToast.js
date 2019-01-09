import Vue from "vue";
import Buefy from "buefy";

Vue.use(Buefy);
let vue = new Vue();
class AppToast {
    success(msg) {
        return vue.$toast.open({
            message: msg,
            position: "is-bottom-right",
            type: "is-success",
            duration: 5000
        });
    }

    danger(msg) {
        return vue.$toast.open({
            message: msg,
            position: "is-bottom-right",
            type: "is-danger",
            duration: 5000
        });
    }

    info(msg) {
        return vue.$toast.open({
            message: msg,
            position: "is-bottom-right",
            type: "is-info",
            duration: 5000
        });
    }
}

export default AppToast;