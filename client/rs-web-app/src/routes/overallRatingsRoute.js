// let Layout = () => import("../layouts/VuetifyLayout.vue");
let OverallRatings = resolve =>
  require([
    "../views/setup/recruitment/overall-ratings/OverallRatings.vue"
  ], resolve);
export default {
  path: "/overall-ratings",
  component: OverallRatings
};
