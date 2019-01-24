let Layout = () => import("../layouts/VuetifyLayout.vue");

// let School = () =>
//     import("../views/setup/recruitment/screening-type/ScreenType.vue")
let ScreenDetails = resolve =>
  require([
    "../views/setup/recruitment/screening-details/ScreeningDetails.vue"
  ], resolve);
let ScreenDetailsSub = resolve =>
  require([
    "../views/setup/recruitment/screening-details/ScreenDetailsSub.vue"
  ], resolve);
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "screen-details",
      component: ScreenDetails,
    },
    {
      path: "screen-details/fill-up/:code/:id",
      component: ScreenDetailsSub
    }
  ]
};
