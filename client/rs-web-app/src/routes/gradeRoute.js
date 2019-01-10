let Layout = () => import("../layouts/VuetifyLayout.vue");

let Grade = () => 
    import("../views/setup/standard/employment-information/employee-level/grade/Grade.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "grade",
      component: Grade
    }
  ]
}