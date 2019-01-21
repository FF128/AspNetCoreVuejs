import Vue from "vue";
import Vuex from "vuex";
// Import Modules
import citizenship from "./modules/citizenship"
import religion from "./modules/religion"
import employeeStatusFile from './modules/employeeStatusFile'
import jobLevel from "./modules/jobLevel"
import grade from "./modules/grade"
import step from "./modules/step"
import designationFile from "./modules/designationFile"
import user from "./modules/user"
import area from "./modules/area"
import branch from "./modules/branch"
import department from "./modules/department"
import division from "./modules/division"
import location from "./modules/location"
import unit from "./modules/unit"
import section from "./modules/section"
import rank from "./modules/rank"
import projectCode from "./modules/projectCode"
import payHouse from "./modules/payHouse"
import region from "./modules/region"
import loe from "./modules/loe"
import courseDegree from "./modules/courseDegree"
import major from "./modules/major"
import school from "./modules/school"
import duties from "./modules/duties"
import fieldInterest from "./modules/fieldInterest"
import government from "./modules/government"
import jobReq from "./modules/jobReq"
import language from "./modules/language"
import license from "./modules/license"
import residenceType from "./modules/residenceType"
import skills from "./modules/skills"
import docSubmitted from "./modules/docSubmitted"
import affiliations from "./modules/affiliation"
import personnelReqType from "./modules/personnelReqType"
import screenType from "./modules/screenType"
import ratings from "./modules/ratings"
import overallRatings from "./modules/overallRatings"
import preEmpReq from "./modules/preEmpReq"
import appEntry from "./modules/appEntry"

Vue.use(Vuex);

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  modules: {
    citizenship,
    religion,
    employeeStatusFile,
    jobLevel,
    grade,
    step,
    designationFile,
    user,
    area,
    branch,
    department,
    division,
    location,
    unit,
    section,
    rank,
    projectCode,
    payHouse,
    region,
    loe,
    courseDegree,
    major,
    school,
    duties,
    fieldInterest,
    government,
    jobReq,
    language,
    license,
    residenceType,
    skills,
    docSubmitted,
    affiliations,
    personnelReqType,
    screenType,
    ratings,
    overallRatings,
    preEmpReq,
    appEntry
  }
});
