let ReturnedBudgetEntryMain = () =>
  import("../views/transactions/budget-entry/returned-budget-entry/ReturnedBudgetEntryMain.vue");
let ReturnedBudgetEntryList = () =>
  import("../views/transactions/budget-entry/returned-budget-entry/ReturnedBudgetEntryList.vue");
  let ReturnedBudgetEntry = () =>
  import("../views/transactions/budget-entry/returned-budget-entry/ReturnedBudgetEntry.vue");
export default {
  path: "/returned-budget-entry",
  component: ReturnedBudgetEntryMain,
  children: [
      {
          path: "",
          component: ReturnedBudgetEntryList
      },
      {
          path: ":transNo",
          component: ReturnedBudgetEntry
      }
  ]
};
