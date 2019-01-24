<template>
    <v-form>
        <v-container>
            <h1>{{title}}</h1>
            <v-layout wrap row>
                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.code"
                        :counter="10"
                        label="Code"
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md4>
                    <v-text-field
                        v-model="ci.description"
                        :counter="50"
                        label="Description"
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md5>
                    <v-text-field
                        v-model="ci.address"
                        label="Address"
                        required
                    ></v-text-field>
                </v-flex>

                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.zipCode"
                        :counter="10"
                        label="Zip Code"
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md4>
                    <v-text-field
                        v-model="ci.city"
                        :counter="50"
                        label="City"
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md5>
                    <v-text-field
                        v-model="ci.province"
                        label="Province"
                        required
                    ></v-text-field>
                </v-flex>

                <v-flex 
                    xs12 sm6 md4>
                    <v-text-field
                        v-model="ci.telNum"
                        :counter="10"
                        label="Tel No."
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md4>
                    <v-text-field
                        v-model="ci.email"
                        :counter="50"
                        :rules="emailRules"
                        label="Email"
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md4>
                    <v-text-field
                        v-model="ci.tin"
                        label="TIN No."
                        required
                    ></v-text-field>
                </v-flex>

                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.sss"
                        label="SSS No."
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.philhealth"
                        label="Philhealth No."
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.pagibig"
                        label="Pag-ibig No."
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.birBranchCode"
                        label="BIR Branch Code"
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex xs12 sm12 md12>
                    <h4>System Integration</h4>
                </v-flex>             

                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.tksdb"
                        label="128 Timekeeping System"
                        required
                    ></v-text-field>
                     <v-checkbox
                        v-model="ci.tksFlag"
                        type="checkbox"
                        required
                        >
                    </v-checkbox>
                </v-flex>
                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.payrollDB"
                        label="128 Payroll System"
                        required
                    ></v-text-field>
                     <v-checkbox
                        v-model="ci.payrollFlag"
                        type="checkbox"
                        required
                        >
                    </v-checkbox>
                </v-flex>
                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.onlineDB"
                        label="128 Employee Online System"
                        required
                    ></v-text-field>
                     <v-checkbox
                        v-model="ci.empOnlineFlag"
                        type="checkbox"
                        required
                        >
                    </v-checkbox>
                </v-flex>
                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.hrisdb"
                        label="128 Human Resource Information System"
                        required
                    ></v-text-field>
                     <v-checkbox
                        v-model="ci.hrisFlag"
                        type="checkbox"
                        required
                        >
                    </v-checkbox>
                </v-flex>

                <v-flex md12>
                    <!-- <img v-bind:src="'data:image/jpeg;base64,'+ci.logoForReports" class="img-responsive" v-if="ci.logoForReports"/> -->
                    <img :src="logoForReports.imageUrl" height="100" width="100%" v-if="logoForReports.imageUrl"/>
                    <!-- <v-text-field label="Logo for Reports" @click='pickFile' v-model='logoForReports.imageName' prepend-icon='attach_file'></v-text-field>
                    <input
						type="file"
						style="display: none"
						ref="image"
						accept="image/*"
						@change="onLogoForReportsPicked()"
					> -->
                    <h4>Logo for Reports</h4>
                    <input type="file" id="logoForReports" ref="logoForReports" class="custom-file-input" 
                        @change="logoForReportsChanged" accept="image/*" >
                </v-flex>

                
                <v-flex xs12 sm12 md12>
                    <!-- <img v-bind:src="'data:image/jpeg;base64,'+ci.logoForSite" class="img-responsive" v-if="ci.logoForSite"/> -->
                    <img :src="logoForSite.imageUrl" height="100" width="100%" v-if="logoForSite.imageUrl"/>
                    <!-- <v-text-field label="Logo for Site" @click='pickFileForSite' v-model='logoForSite.imageName' prepend-icon='attach_file'></v-text-field>
                    <input
						type="file"
						style="display: none"
						ref="image2"
						accept="image/*"
						@change="onLogoForSitePicked()"
					> -->
                    <h4>Logo for Site</h4>
                    <input type="file" id="logoForSite" ref="logoForSite" class="custom-file-input" 
                        @change="logoForSiteChanged" accept="image/*" >
                </v-flex>

                <v-flex xs12 sm12 md12>
                    <!-- <img v-bind:src="'data:image/jpeg;base64,'+ci.contentForSite" class="img-responsive" v-if="ci.contentForSite"/> -->
                    <img :src="contentForSite.imageUrl" height="100" width="100%" v-if="contentForSite.imageUrl"/>
                    <!-- <v-text-field label="Content for Site" @click='pickFile' v-model='contentForSite.imageName' prepend-icon='attach_file'></v-text-field>
                    <input
						type="file"
						style="display: none"
						ref="image3"
						accept="image/*"
						@change="onLogoForReportsPicked($event)"
					> -->
                    <h4>Content for Site</h4>
                    <input type="file" id="contentForSite" ref="contentForSite" class="custom-file-input" 
                        @change="contentForSiteChanged" accept="image/*"/>
                </v-flex>
                <v-flex xs12 sm12 md12>
                    <v-btn
                        color="success"
                        @click.prevent="processForm"
                        :loading="isSaving">
                        Save
                    </v-btn>
                </v-flex>
                
            </v-layout>
        </v-container>
    </v-form>
</template>
<script>
import { mapActions, mapState } from "vuex";
import Toast from "@/project-modules/toast";
import codeRules from "@/rules/codeRules";

let toast = new Toast();
export default {
  data() {
    return {
      title: "Company Information",
      ci: {},
      isSaving: false,
      emailRules: [
        v => !!v || "E-mail is required",
        v => /.+@.+/.test(v) || "E-mail must be valid"
      ],
      logoForReports: {
        imageName: "",
        imageUrl: "",
        imageFile: ""
      },
      logoForSite: {
        imageName: "",
        imageUrl: "",
        imageFile: ""
      },
      contentForSite: {
        imageName: "",
        imageUrl: "",
        imageFile: ""
      },
      files: null,
      apiEndpoint: "api/companyInfo"
    };
  },
  methods: {
    ...mapActions("user", ["decodeToken"]),
    save() {
      this.$axios
        .post("api/companyInfo", this.ci)
        .then(response => {
          let { message, hasError } = response.data;

          // Toast custom message
          toast.show(message, hasError);
        })
        .catch(err => console.log(err));
    },
    logoForSiteChanged() {
      let files = this.$refs.logoForSite.files;

      this.logoForSite.imageName = files[0].name;
      const fr = new FileReader();
      fr.readAsDataURL(files[0]);
      fr.addEventListener("load", () => {
        this.logoForSite.imageUrl = fr.result;
        this.logoForSite.imageFile = files[0]; // this is an image file that can be sent to server...
      });
      this.ci.logoForSite = files[0];
    },
    logoForReportsChanged() {
      let files = this.$refs.logoForReports.files;
      this.logoForReports.imageName = files[0].name;
      const fr = new FileReader();
      fr.readAsDataURL(files[0]);
      fr.addEventListener("load", () => {
        this.logoForReports.imageUrl = fr.result;
        this.logoForReports.imageFile = files[0]; // this is an image file that can be sent to server...
      });
      this.ci.logoForReports = files[0];
    },
    contentForSiteChanged() {
      let files = this.$refs.contentForSite.files;
      this.contentForSite.imageName = files[0].name;
      const fr = new FileReader();
      fr.readAsDataURL(files[0]);
      fr.addEventListener("load", () => {
        this.contentForSite.imageUrl = fr.result;
        this.contentForSite.imageFile = files[0]; // this is an image file that can be sent to server...
      });
      this.ci.contentForSite = files[0];
    },
    processForm() {
      this.isSaving = true;
      var bodyFormData = new FormData();
      bodyFormData.set("Code", this.ci.code);
      bodyFormData.set("Description", this.ci.description);
      bodyFormData.set("Address", this.ci.address);
      bodyFormData.set("Province", this.ci.province);
      bodyFormData.set("City", this.ci.city);
      bodyFormData.set("ZipCode", this.ci.zipCode);
      bodyFormData.set("TelNum", this.ci.telNum);
      bodyFormData.set("Email", this.ci.email);
      bodyFormData.set("TIN", this.ci.tin);
      bodyFormData.set("SSS", this.ci.sss);
      bodyFormData.set("Pagibig", this.ci.pagibig);
      bodyFormData.set("Philhealth", this.ci.philhealth);
      bodyFormData.set("BirBranchCode", this.ci.birBranchCode);
      bodyFormData.set("TKSFlag", this.ci.tksFlag);
      bodyFormData.set("PayrollFlag", this.ci.payrollFlag);
      bodyFormData.set("HRISFlag", this.ci.hrisFlag);
      bodyFormData.set("EmpOnlineFlag", this.ci.empOnlineFlag);
      bodyFormData.set("TKSDB", this.ci.tksdb);
      bodyFormData.set("PayrollDB", this.ci.payrollDB);
      bodyFormData.set("HRISDB", this.ci.hrisdb);
      bodyFormData.set("OnlineDB", this.ci.onlineDB);

      // bodyFormData.append('LogoForReports',null);
      // bodyFormData.append('LogoForSite', null);
      // bodyFormData.append('ContentForSite', null);

      bodyFormData.append("LogoForReportsFile", this.ci.logoForReports);
      bodyFormData.append("LogoForSiteFile", this.ci.logoForSite);
      bodyFormData.append("ContentForSiteFile", this.ci.contentForSite);
      let self = this;
      this.$axios({
        method: "POST",
        url: this.apiEndpoint,
        data: bodyFormData,
        config: { headers: { "Content-Type": "multipart/form-data" } }
      })
        .then(function(response) {
          //handle success
          let { message, hasError } = response.data;

          // Toast custom message
          toast.show(message, hasError);
          self.isSaving = false;
          self.getCompanyInfoByCode();
        })
        .catch(function(err) {
          //handle error
          let { message, hasError } = err.response.data;

          // Toast custom message
          toast.show(message, hasError);

          self.isSaving = false;
        });
    },
    getCompanyInfoByCode() {
      this.$axios
        .get(`${this.apiEndpoint}/company-code/${this.companyCode}`)
        .then(response => {
          this.ci = response.data;
        })
        .catch(err => {});
    }
    // onLogoForReportsPicked (e) {
    // 	const files = e.target.files
    // 	if(files[0] !== undefined) {
    //         console.log('reports')
    // 		this.logoForReports.imageName = files[0].name
    // 		if(this.logoForReports.imageName.lastIndexOf('.') <= 0) {
    // 			return
    // 		}
    // 		const fr = new FileReader ()
    // 		fr.readAsDataURL(files[0])
    // 		fr.addEventListener('load', () => {
    // 			this.logoForReports.imageUrl = fr.result
    // 			this.logoForReports.imageFile = files[0] // this is an image file that can be sent to server...
    // 		})
    // 	} else {
    // 		this.logoForReports.imageName = ''
    // 		this.logoForReports.imageFile = ''
    // 		this.logoForReports.imageUrl = ''
    // 	}
    // },
    // onLogoForSitePicked (e) {
    // 	const files = e.target.files
    // 	if(files[0] !== undefined) {
    //         console.log('site')
    // 		this.imageName = files[0].name
    // 		if(this.imageName.lastIndexOf('.') <= 0) {
    // 			return
    // 		}
    // 		const fr = new FileReader ()
    // 		fr.readAsDataURL(files[0])
    // 		fr.addEventListener('load', () => {
    // 			this.imageUrl = fr.result
    // 			this.imageFile = files[0] // this is an image file that can be sent to server...
    // 		})
    // 	} else {
    // 		this.imageName = ''
    // 		this.imageFile = ''
    // 		this.imageUrl = ''
    // 	}
    // },
    // pickFile () {
    //     this.$refs.image.click()
    // },
    // pickFileForSite() {
    //     this.$refs.image.click()
    // }
  },
  computed: {
    ...mapState("user", {
      companyCode: state => state.companyCode
    })
  },
  created() {
    this.decodeToken();
    this.getCompanyInfoByCode();
  }
};
</script>

<style>
.img-responsive {
  max-width: 100%;
  height: auto;
  display: block;
}
</style>
