let Layout = () => import("../layouts/VuetifyLayout.vue");

let Duties = () =>
  import("../views/setup/standard/employment-other-info/others/duties-responsibilities/DutiesResponsibilities.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "duties-responsibilities",
      component: Duties
    }
  ]
};
