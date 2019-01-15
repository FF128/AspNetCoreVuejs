let Layout = () => import("../layouts/VuetifyLayout.vue");

let ResidenceType = () => 
    import("../views/setup/standard/employment-other-info/others/residence-type/ResidenceType.vue")
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "residence-type",
      component: ResidenceType
    }
  ]
}