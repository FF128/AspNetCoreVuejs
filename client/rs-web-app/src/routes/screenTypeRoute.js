let Layout = () => import("../layouts/VuetifyLayout.vue");

// let School = () => 
//     import("../views/setup/recruitment/screening-type/ScreenType.vue")
let ScreenType =  resolve => require(["../views/setup/recruitment/screening-type/ScreenType.vue"], resolve);
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "screen-type",
      component: ScreenType
    }
  ]
}