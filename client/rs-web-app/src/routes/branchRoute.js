let Layout = () => import("../layouts/VuetifyLayout.vue");

let Branch = () => 
    import("../views/setup/standard/employment-information/organizational-level/branch/Branch.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "branch",
      component: Branch
    }
  ]
}