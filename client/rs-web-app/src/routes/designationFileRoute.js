let Layout = () => import("../layouts/VuetifyLayout.vue");

let DesignationFile = () => 
    import("../views/setup/standard/employment-information/employee-level/designation-file/DesignationFile");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "designation-file",
      component: DesignationFile
    }
  ]
}