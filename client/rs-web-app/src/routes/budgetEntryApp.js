let BudgetEntryApprovalMain = () =>
  import("../views/transactions/budget-entry/BudgetEntryApprovalMain.vue");

let BudgetEntryApprovalList = () =>
  import("../views/transactions/budget-entry/BudgetEntryApprovalList.vue");

let BudgetEntryApproval = () =>
  import("../views/transactions/budget-entry/BudgetEntryApproval.vue");
export default {
  path: "/budget-entry-approval",
  component: BudgetEntryApprovalMain,
  children: [
      {
          path: '',
          component: BudgetEntryApprovalList
      },
      {
          path: ':transNo',
          component: BudgetEntryApproval
      }
  ]
};
