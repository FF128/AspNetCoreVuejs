import Vue from "vue";
import moment from "moment";
import numeral from "numeral";

Vue.filter("dateFormat", value => moment(value).format("MM/DD/YYYY"));

Vue.filter("modalStatusFilter", value => (value ? "is-active" : ""));

Vue.filter("currencyFormat", value => numeral(value).format("0,0.00"));

Vue.filter("buttonLoading", value => (value ? "is-loading" : ""));
