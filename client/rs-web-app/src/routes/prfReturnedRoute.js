let ReturnPRFEntryMain = () =>
    import( /* webpackChunkName: 'pr-return' */ "../views/transactions/personnel-requisition/returned/ReturnPRFEntryMain.vue")
let ReturnPRFEntryList = () =>
    import( /* webpackChunkName: 'pr-return' */ "../views/transactions/personnel-requisition/returned/ReturnPRFEntryList.vue")
let ReturnPRFEntry = () =>
    import( /* webpackChunkName: 'pr-return' */ "../views/transactions/personnel-requisition/returned/ReturnPRFEntry.vue")
export default {
    path: "/returned-pr-entry",
    component: ReturnPRFEntryMain,
    children: [{
            path: "",
            component: ReturnPRFEntryList
        },
        {
            path: ":prfNo",
            component: ReturnPRFEntry
        }
    ]
};