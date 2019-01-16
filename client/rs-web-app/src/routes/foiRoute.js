let Layout = () => import("../layouts/VuetifyLayout.vue");

let FOI = () => 
    import("../views/setup/standard/employment-other-info/others/field-of-interest/FieldOfInterest.vue")
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "field-interest",
      component: FOI
    }
  ]
}