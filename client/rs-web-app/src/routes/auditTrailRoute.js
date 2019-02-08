//let Layout = () => import("../layouts/VuetifyLayout.vue");

let AuditTrail = () =>
  import("../views/admin/audit-trail/AuditTrail.vue");
export default {
  meta: {
    module: 'REC'
  },
  path: "/audit-trail",
  component: AuditTrail
};
