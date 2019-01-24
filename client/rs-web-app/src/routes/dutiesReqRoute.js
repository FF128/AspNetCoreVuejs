let Layout = () => import("../layouts/VuetifyLayout.vue");

let DutiesReq = () =>
  import("../views/setup/recruitment/duties-requirements/DutiesRequirements");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "designation-duties-requirements",
      component: DutiesReq
    }
  ]
};