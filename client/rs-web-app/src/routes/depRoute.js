let Layout = () => import("../layouts/VuetifyLayout.vue");

let Department = () => 
    import("../views/setup/standard/employment-information/organizational-level/department/Department.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "department",
      component: Department
    }
  ]
}