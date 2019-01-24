<template>
    <div>
        <v-container>
            <v-layout row wrap>
                <v-form>
                    <v-flex xs12 sm12 md12>
                        <v-text-field
                            label="Subject"
                            v-model="smsFormat.subject">

                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm12 md12>
                        <quill-editor v-model="smsFormat.message"></quill-editor>
                    </v-flex>
                    <v-divider></v-divider>
                    <v-card>
                        <v-container>
                        <p> These are the following fields that you can use for the Email.  To use these field Input this with curly brackets eg. <b>{ApplicantName}</b></p>

                             <v-flex xs12 sm12 md12>
                                <p>Applicant Name: <b>{ApplicantName}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Designation: <b>{Designation}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Evaluator: <b>{Evaluator}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Screening Date: <b>{ScreeningDate}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Screening Time: <b>{ScreeningTime}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Screening Type: <b>{ScreeningType}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Company Name: <b>{CompanyName}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>CompanyAddress: <b>{CompanyAddress}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <v-btn color="success"
                                    @click.prevent="save"
                                    :loading="isSaving">
                                    Save
                                </v-btn>
                            </v-flex>
                        </v-container>
                    </v-card>
                    <v-divider></v-divider>
                    <v-card>
                        <v-container>
                            <h3>Preview</h3>
                            <v-flex xs12>
                                <div contenteditable="false" 
                                    class="ql-editor" 
                                    v-html="smsFormat.message">
                                    {{smsFormat.message}}
                                </div>
                            </v-flex>
                        </v-container>
                    </v-card>
                    <br/>
                </v-form>
            </v-layout>
        </v-container>
        
    </div>
</template>
<script>
import { mapActions } from "vuex";
import "quill/dist/quill.core.css";
import "quill/dist/quill.snow.css";
import "quill/dist/quill.bubble.css";

import { quillEditor } from "vue-quill-editor";

export default {
  components: {
    quillEditor
  },
  data() {
    return {
      title: "Email Format Setup",
      smsFormat: {
        transType: "ScheduleScreening"
      },
      isSaving: false,
      apiEndpoint: "api/sms-format"
    };
  },
  methods: {
    ...mapActions("smsFormat", {
      saveSMSFormat: "saveSMSFormat"
    }),
    save() {
      this.saveSMSFormat(this.smsFormat);
    },
    getSMSFormat() {
      this.$axios
        .get(`${this.apiEndpoint}/${this.smsFormat.transType}`)
        .then(({ data }) => {
          if (data) {
            this.smsFormat = data;
            return;
          }
          this.smsFormat = {
            transType: "ScheduleScreening"
          };
        })
        .catch(({ response }) => {
          let { message, hasError } = response.data;
          toast.show(message, hasError);
        });
    }
  },
  created() {
    this.getSMSFormat();
  }
};
</script>
