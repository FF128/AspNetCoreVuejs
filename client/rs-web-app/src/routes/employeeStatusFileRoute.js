let Layout = () => import("../layouts/VuetifyLayout.vue");

let EmployeeStatusFile = () => 
    import("../views/setup/standard/employment-information/employee-level/employee-status-file/EmployeeStatusFile");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "employee-status-file",
      component: EmployeeStatusFile
    }
  ]
}