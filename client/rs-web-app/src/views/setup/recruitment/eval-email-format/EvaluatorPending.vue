<template>
    <div>
        <v-container>
            <v-layout row wrap>
                <v-form>
                    <v-flex xs12 sm12 md12>
                        <v-text-field
                            label="Subject"
                            v-model="evalEmailFormat.subject">

                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm12 md12>
                        <quill-editor v-model="evalEmailFormat.message"
                            :options="editorOption"></quill-editor>
                    </v-flex>
                    <v-divider></v-divider>
                    <v-card>
                        <v-container>
                            <p> These are the following fields that you can use to trigger to the evaluator/recruiter the results of evaluators screening and pending screening per applicant.  To use these field Input this with curly brackets eg. <b>{ApplicantName}</b></p>
                            <v-divider></v-divider>
                            <v-flex xs12 sm12 md12>
                                <p>Applicant Name: <b>{ApplicantName}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Transaction No.(For All Options except Recruiter-Result of Screening): <b>{TransactionNo}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Transaction Description (For All Options except Recruiter-Result of Screening): <b>{TransactionDesc}</b></p>
                            </v-flex>
                             <v-flex xs12 sm12 md12>
                                <p>Designation: <b>{Designation}</b></p>
                            </v-flex>
                             <v-flex xs12 sm12 md12>
                                <p>Evaluator: <b>{Evaluator}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Screening Comment (For Recruiter-Result of Screening Option): <b>{ScreeningComment}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Screening Date (For Recruiter-Result of Screening Option): <b>{ScreeningDate}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Screening Ratings (For Recruiter-Result of Screening Option): <b>{ScreeningRatings}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Screening Time (For Recruiter-Result of Screening Option): <b>{ScreeningTime}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Screening Type: <b>{ScreeningType}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Screening Order (For All Options except Recruiter-Result of Screening): <b>{ScreenOrder}</b></p>
                            </v-flex>
                            <v-flex xs12 sm12 md12>
                                <p>Recruiter: <b>{Recruiter}</b></p>
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
                                    v-html="evalEmailFormat.message">
                                    {{evalEmailFormat.message}}
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
import Toast from "@/project-modules/toast";
import "quill/dist/quill.core.css";
import "quill/dist/quill.snow.css";
import "quill/dist/quill.bubble.css";

import { quillEditor } from "vue-quill-editor";
import { mapActions } from "vuex";

let toast = new Toast();
export default {
  inherit: true,
  components: {
    quillEditor
  },
  data() {
    return {
      title: "Email Format Setup",
      evalEmailFormat: {
        transType: "EvalPending"
      },
      isSaving: false,
      editorOption: {
        //   theme: "snow",
        //   modules: {
        //     toolbar: false
        //   }
      },
      apiEndpoint: "api/eval-email-format"
    };
  },
  methods: {
    ...mapActions("evalEmailFormat", {
      saveEvalEmailFormat: "saveEvalEmailFormat"
    }),
    save() {
      this.saveEvalEmailFormat(this.evalEmailFormat);
    },
    getEvalEmailFormat() {
      this.$axios
        .get(`${this.apiEndpoint}/${this.evalEmailFormat.transType}`)
        .then(({ data }) => {
          if (data) {
            this.evalEmailFormat = data;
            return;
          }
          this.evalEmailFormat = {
            transType: "EvalPending"
          };
        })
        .catch(({ response }) => {
          let { message, hasError } = response.data;
          toast.show(message, hasError);
        });
    }
  },
  created() {
    this.getEvalEmailFormat();
  }
};
</script>
