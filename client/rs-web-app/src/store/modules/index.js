import camelCase from "lodash/camelCase"
const requireModule = require.context(".", false, /\.js$/)
//const requireModuleApplicant = require.context("./applicant", false, /\.js$/)
const modules = {}

requireModule.keys().forEach(fileName => {
    if (fileName == "./index.js") return;

    const moduleName = camelCase(fileName.replace(/(\.\/|\.js)/g, ""));
    modules[moduleName] = requireModule(fileName).default;
});

// requireModuleApplicant.keys().forEach(fileName => {
//     if (fileName == "./index.js") return;

//     const moduleName = camelCase(fileName.replace(/(\.\/|\.js)/g, ""));
//     modules[moduleName] = requireModuleApplicant(fileName).default;
// });

export default modules;