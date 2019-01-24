let Layout = () => import("../layouts/VuetifyLayout.vue");

// let School = () =>
//     import("../views/setup/recruitment/screening-type/ScreenType.vue")
let MultiCompanyDatabase = resolve =>
  require([
    "../views/setup/recruitment/multi-company/MultiCompanyDatabase.vue"
  ], resolve);
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "multi-company",
      component: MultiCompanyDatabase
    }
  ]
};
