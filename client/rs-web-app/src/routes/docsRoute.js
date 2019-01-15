let Layout = () => import("../layouts/VuetifyLayout.vue");

let DocSubmitted = () => 
    import("../views/setup/standard/employment-other-info/others/doc-submitted/DocSubmitted.vue")
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "documents",
      component: DocSubmitted
    }
  ]
}