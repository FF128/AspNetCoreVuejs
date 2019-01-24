let Layout = () => import("../layouts/VuetifyLayout.vue");

// let School = () =>
//     import("../views/setup/recruitment/screening-type/ScreenType.vue")
let Ratings = resolve =>
  require(["../views/setup/recruitment/ratings/Ratings.vue"], resolve);
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "ratings",
      component: Ratings
    }
  ]
};
