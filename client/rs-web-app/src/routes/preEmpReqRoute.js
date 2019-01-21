let Layout = () => import("../layouts/VuetifyLayout.vue");

// let School = () => 
//     import("../views/setup/recruitment/screening-type/ScreenType.vue")
let PreEmpReq =  resolve => require(["../views/setup/recruitment/pre-employment/PreEmpReq.vue"], resolve);
export default {
  path: "/",
  component: Layout,
  children: [
    {
      path: "pre-emp-req",
      component: PreEmpReq
    }
  ]
}