<template>
    <div>
        <v-container>
            <v-layout row wrap>
                <v-form>
                    <v-flex xs12 sm12 md12>
                        <v-text-field
                            label="Subject"
                            v-model="emailFormat.subject">

                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm12 md12>
                        <quill-editor v-model="emailFormat.message"
                            :options="editorOption"></quill-editor>
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
                                    v-html="emailFormat.message">
                                    {{emailFormat.message}}
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
      emailFormat: {
        transType: "Hired"
      },
      isSaving: false,
      editorOption: {
        //   theme: "snow",
        //   modules: {
        //     toolbar: false
        //   }
      },
      apiEndpoint: "api/email-format"
    };
  },
  methods: {
    ...mapActions("emailFormat", {
      saveEmailFormat: "saveEmailFormat"
    }),
    save() {
      this.saveEmailFormat(this.emailFormat);
    },
    getEmailFormat() {
      this.$axios
        .get(`${this.apiEndpoint}/${this.emailFormat.transType}`)
        .then(({ data }) => {
          if (data) {
            this.emailFormat = data;
            return;
          }
          this.emailFormat = {
            transType: "Hired"
          };
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    }
  },
  created() {
    this.getEmailFormat();
  }
};
</script>
