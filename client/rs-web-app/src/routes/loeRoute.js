let Layout = () => import("../layouts/VuetifyLayout.vue");

let LOE = () => 
    import("../views/setup/standard/employment-information/organizational-level/levels-of-employee/LevelsOfEmployee.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "levels-of-employee",
      component: LOE
    }
  ]
}