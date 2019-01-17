let Layout = () => import("../layouts/VuetifyLayout.vue");

let PersonnelReqType = () => 
    import("../views/setup/recruitment/personnel-req-type/PersonnelReqType");
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "personnel-req-type",
      component: PersonnelReqType
    }
  ]
}