// let Layout = () => import("../layouts/VuetifyLayout.vue");

// let School = () =>
//     import("../views/setup/recruitment/screening-type/ScreenType.vue")
let ScreenDetailsMain = resolve =>
  require([
    "../views/setup/recruitment/screening-details/ScreenDetailsMain.vue"
  ], resolve);
let ScreenDetails = resolve =>
  require([
    "../views/setup/recruitment/screening-details/ScreeningDetails.vue"
  ], resolve);
let ScreenDetailsSub = resolve =>
  require([
    "../views/setup/recruitment/screening-details/ScreenDetailsSub.vue"
  ], resolve);
export default {
  path: "/screen-details",
  component: ScreenDetailsMain,
  children: [
    {
      path: "",
      component: ScreenDetails
    },
    {
      path: "fill-up/:code/:id",
      component: ScreenDetailsSub
    }
  ]
};
