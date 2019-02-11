let PRFApprovalMain = () =>
  import("../views/transactions/personnel-requisition/approval/PRFApprovalMain.vue");

let PRFApprovalList = () =>
  import("../views/transactions/personnel-requisition/approval/PRFApprovalList.vue");

let PRFApproval = () =>
  import("../views/transactions/personnel-requisition/approval/PRFApproval.vue");
export default {
  path: "/pr-approval",
  component: PRFApprovalMain,
  children: [
      {
          path: '',
          component: PRFApprovalList
      },
      {
          path: ':prfNo',
          component: PRFApproval
      }
  ]
};
