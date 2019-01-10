let Layout = () => import("../layouts/VuetifyLayout.vue");

let Step = () => 
    import("../views/setup/standard/employment-information/employee-level/step/Step.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "step",
      component: Step
    }
  ]
}