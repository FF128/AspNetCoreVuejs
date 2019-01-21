let Layout = () => import("../layouts/VuetifyLayout.vue");

// let School = () => 
//     import("../views/setup/recruitment/screening-type/ScreenType.vue")
let OverallRatings =  resolve => require(["../views/setup/recruitment/overall-ratings/OverallRatings.vue"], resolve);
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "overall-ratings",
      component: OverallRatings
    }
  ]
}