<template>
    <div>
        <v-container>
            <v-layout row wrap>
                <v-textarea
                    name="input-7-1"
                    label=""
                    v-model="text"
                    placeHolder="Type Here"
                    solo
                    :readonly="!onEdit">
                </v-textarea>
                <v-flex xs12 sm12 md12>
                    <v-btn v-if="!onEdit"
                        @click.prevent="onEdit = true">
                        Edit
                    </v-btn>
                    <div v-if="onEdit">
                        <v-btn @click.prevent="update">
                            Update
                        </v-btn>
                        <v-btn
                            @click.prevent="onEdit = false">
                            Cancel
                        </v-btn>
                    </div>
                </v-flex>
            </v-layout>
        </v-container>
    </div>
</template>
<script>
import Toast from "@/project-modules/toast";
let toast = new Toast();
export default {
  data() {
    return {
      text: "",
      onEdit: false,
      apiEndpoint: "api/applicants-entry/text"
    };
  },
  methods: {
    update() {
      this.$axios
        .post(`${this.apiEndpoint}`, { textCertification: this.text })
        .then(({ data }) => {
          toast.show(data);
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    },
    getTextCertificate() {
      this.$axios
        .get(`${this.apiEndpoint}`)
        .then(({ data }) => {
          this.text = data.textCertification;
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    }
  },
  created() {
    this.getTextCertificate();
  }
};
</script>
