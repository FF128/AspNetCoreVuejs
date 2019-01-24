<template>
    <div class="section">
        <h1 class="title is-4">{{title}}</h1>
        <form id="company-info-form" @submit.prevent="processForm">
            <div class="columns">
                <div class="column is-4">
                    <b-field label="Code" custom-class="is-small">
                        <b-input size="is-small" v-model="ci.code"></b-input>
                    </b-field>
                </div>
            </div>
            <div class="columns">
                <div class="column is-4">
                    <b-field label="Description" custom-class="is-small">
                        <b-input size="is-small" v-model="ci.description"></b-input>
                    </b-field>
                </div>
            </div>
            <div class="columns">
                <div class="column">
                    <b-field label="Address" custom-class="is-small">
                        <b-input size="is-small" v-model="ci.address"></b-input>
                    </b-field>
                </div>
                
                <div class="column">
                    <b-field label="Province" custom-class="is-small">
                        <b-input size="is-small" v-model="ci.province"></b-input>
                    </b-field>
                </div>
            </div>
            
            <div class="columns">
                <div class="column is-3">
                    <b-field label="City" custom-class="is-small">
                        <b-input size="is-small" v-model="ci.city"></b-input>
                    </b-field>
                </div>
                <div class="column is-3">
                    <b-field label="Zip Code" custom-class="is-small">
                        <b-input size="is-small" v-model="ci.zipCode"></b-input>
                    </b-field>
                </div>
                <div class="column">
                    <b-field label="Tel No." custom-class="is-small">
                        <b-input size="is-small" v-model="ci.telNum"></b-input>
                    </b-field>
                </div>
                <div class="column">
                    <b-field label="Email" custom-class="is-small">
                        <b-input size="is-small" v-model="ci.email"></b-input>
                    </b-field>
                </div>
            </div>
            <div class="columns">
                <div class="column">
                    <b-field label="TIN No." custom-class="is-small">
                        <b-input size="is-small" v-model="ci.tin"></b-input>
                    </b-field>
                </div>
                <div class="column">
                    <b-field label="SSS No." custom-class="is-small">
                        <b-input size="is-small" v-model="ci.sss"></b-input>
                    </b-field>
                </div>
                <div class="column">
                    <b-field label="Philhealth No." custom-class="is-small">
                        <b-input size="is-small" v-model="ci.philhealth"></b-input>
                    </b-field>
                </div>
                <div class="column">
                    <b-field label="Pag-ibig No." custom-class="is-small">
                        <b-input size="is-small" v-model="ci.pagibig"></b-input>
                    </b-field>
                </div>
                <div class="column is-2">
                    <b-field label="BIR Branch Code" custom-class="is-small">
                        <b-input size="is-small" v-model="ci.birBranchCode"></b-input>
                    </b-field>
                </div>
                
            </div>
            <div class="column is-4">
                <b-field class="file">
                    <b-upload v-model="ci.logoForReports">
                        <a class="button is-primary">
                            <b-icon icon="upload"></b-icon>
                            <span>Logo for Reports</span>
                        </a>
                    </b-upload>
                    <span class="file-name" v-if="ci.logoForReports">
                        {{ ci.logoForReports.name }}
                    </span>
                </b-field>
            </div>
            <div class="column is-4">
                <b-field class="file">
                    <b-upload v-model="ci.logoForSite">
                        <a class="button is-primary">
                            <b-icon icon="upload"></b-icon>
                            <span>Logo for Site</span>
                        </a>
                    </b-upload>
                    <span class="file-name" v-if="ci.logoForSite">
                        {{ ci.logoForSite.name }}
                    </span>
                </b-field>
            </div>
            <div class="column is-4">
                <b-field class="file">
                    <b-upload v-model="ci.contentForSite">
                        <a class="button is-primary">
                            <b-icon icon="upload"></b-icon>
                            <span>Content for Site</span>
                        </a>
                    </b-upload>
                    <span class="file-name" v-if="ci.contentForSite">
                        {{ ci.contentForSite.name }}
                    </span>
                </b-field>
            </div>
            <div class="field is-grouped">
                <div class="control">
                    <button class="button is-success" type="submit" :class="isSaving | buttonLoading">Save</button>
                </div>
            </div>
        </form>
    </div>
</template>
<script>
let imageContent = {
  imageName: "",
  imageUrl: "",
  imageFile: ""
};
export default {
  data() {
    return {
      title: "Company Information",
      ci: {
        logoForReports: null
      },
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
      logoForSite: imageContent,
      contentForSite: imageContent,
      file: null
    };
  },
  methods: {
    save() {
      this.$axios
        .post("api/companyInfo", this.ci)
        .then(response => {
          console.log(response.data);
        })
        .catch(err => console.log(err));
    },
    onFilePicked(e) {
      const files = e.target.files;
      if (files[0] !== undefined) {
        this.logoForReports.imageName = files[0].name;
        if (this.logoForReports.imageName.lastIndexOf(".") <= 0) {
          return;
        }
        const fr = new FileReader();
        fr.readAsDataURL(files[0]);
        fr.addEventListener("load", () => {
          this.logoForReports.imageUrl = fr.result;
          this.logoForReports.imageFile = files[0]; // this is an image file that can be sent to server...
        });
      } else {
        this.logoForReports.imageName = "";
        this.logoForReports.imageFile = "";
        this.logoForReports.imageUrl = "";
      }
    },
    pickFile() {
      this.$refs.image.click();
    },
    processForm() {
      var bodyFormData = new FormData();
      bodyFormData.set("code", this.ci.code);
      bodyFormData.set("address", this.ci.address);
      bodyFormData.set("province", this.ci.province);
      bodyFormData.set("city", this.ci.city);
      bodyFormData.set("zipCode", this.ci.zipCode);
      bodyFormData.set("telNum", this.ci.telNum);
      bodyFormData.set("email", this.ci.email);
      bodyFormData.set("TIN", this.ci.tin);
      bodyFormData.set("SSS", this.ci.sss);
      bodyFormData.set("pagibig", this.ci.pagibig);
      bodyFormData.set("philhealth", this.ci.philhealth);
      bodyFormData.set("birBranchCode", this.ci.birBranchCode);

      bodyFormData.append("LogoForReportsFile", this.ci.logoForReports);
      bodyFormData.append("LogoForSiteFile", this.ci.logoForSite);
      bodyFormData.append("ContentForSiteFile", this.ci.contentForSite);

      this.$axios({
        method: "post",
        url: "api/companyInfo",
        data: bodyFormData,
        config: { headers: { "Content-Type": "multipart/form-data" } }
      })
        .then(function(response) {
          //handle success
          console.log(response);
        })
        .catch(function(response) {
          //handle error
          console.log(response);
        });
    }
  }
};
</script>
