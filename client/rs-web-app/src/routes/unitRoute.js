let Layout = () => import("../layouts/VuetifyLayout.vue");

let Unit = () => 
    import("../views/setup/standard/employment-information/organizational-level/unit/Unit.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "unit",
      component: Unit
    }
  ]
}