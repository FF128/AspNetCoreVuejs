let PRFExtendMain = () =>
    import( /* webpackChunkName: 'pr-exnted' */ "../views/transactions/personnel-requisition/extend/PRFExtendMain.vue")
let PRFExtendList = () =>
    import( /* webpackChunkName: 'pr-exnted' */ "../views/transactions/personnel-requisition/extend/PRFExtendList.vue")
let PRFExtend = () =>
    import( /* webpackChunkName: 'pr-exnted' */ "../views/transactions/personnel-requisition/extend/PRFExtend.vue")
export default {
    path: "/prf-extend",
    component: PRFExtendMain,
    children: [{
            path: "",
            component: PRFExtendList
        },
        {
            path: ":prfNo",
            component: PRFExtend
        }
    ]
};