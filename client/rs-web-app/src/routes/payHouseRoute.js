let Layout = () => import("../layouts/VuetifyLayout.vue");

let PayHouse = () => 
    import("../views/setup/standard/employment-information/organizational-level/pay-house/PayHouse.vue");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "pay-house",
      component: PayHouse
    }
  ]
}