import camelCase from "lodash/camelCase"
const requireModule = require.context(".", false, /\.js$/)
//const requireModuleApp = require.context("./applicant-routes", false, /\.js$/)
const routes = []

requireModule.keys().forEach(fileName => {
    if (fileName == "./index.js" || fileName == "./router.js") return;

    const routeName = camelCase(fileName.replace(/(\.\/|\.js)/g, ""));
    //routes[routeName] = requireModule(fileName).default;
    routes.push(requireModule(fileName).default)
})
// requireModuleApp.keys().forEach(fileName => {
//     if (fileName == "./index.js" || fileName == "./router.js") return;

//     const routeName = camelCase(fileName.replace(/(\.\/|\.js)/g, ""));
//     //routes[routeName] = requireModule(fileName).default;
//     routes.push(requireModuleApp(fileName).default)
// })
export default routes