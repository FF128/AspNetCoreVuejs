<template>
    <div>
        <v-form>
            <v-container>
                <v-layout row wrap>
                    <v-flex xs12 sm6 md8>
                        <v-text-field
                            label="Essay Question"
                            v-model="essay.question">

                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                        <v-switch
                            label="Is Active"
                            v-model="essay.active"
                        ></v-switch>
                    </v-flex>
                    <v-flex xs12 sm12 md12>
                        <v-btn
                            @click.prevent="save"
                            v-if="!onEdit"
                            color="success"
                            :loading="isSaving">
                            Add
                        </v-btn>
                        <div v-if="onEdit">
                            <v-btn
                                color="success"
                                @click.prevent="update"
                                :loading="isUpdating">
                                Update
                            </v-btn>
                            <v-btn
                                @click.prevent="cancel">
                                Cancel
                            </v-btn>
                        </div>
                    </v-flex>
                </v-layout>
            </v-container>
        </v-form>
        <v-container>
            <v-data-table
                :items="essayQuestions"
                class="elevation-1"
                hide-actions
                :headers="headers"
            >
                <template slot="items" slot-scope="props">
                <td>{{ props.item.active }}</td>
                <td class="text-xs-left">{{ props.item.question }}</td>
                <td>
                    <v-btn icon
                        @click.prevent="edit(props.item)">
                        <v-icon>edit</v-icon>
                    </v-btn>
                    <v-btn icon
                        @click.prevent="deleteInfo(props.item)">
                        <v-icon color="red">delete</v-icon>
                    </v-btn>
                </td>
                </template>
            </v-data-table>
        </v-container>
        <v-dialog
            v-model="deleteDialog"
            max-width="300">
            <v-card>
                <v-card-title class="headline">Confirmation</v-card-title>

                <v-card-text>
                    Do you want to delete this record?
                </v-card-text>

                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn
                        color="red"
                        flat="flat"
                        @click.prevent="deleteConfirmed">
                        Yes
                    </v-btn>

                    <v-btn
                        flat="flat"
                        @click.prevent="deleteDialog = false">
                        No
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>
</template>
<script>
import Toast from "@/project-modules/toast";
import { mapState, mapActions } from "vuex";

let toast = new Toast();
export default {
  data() {
    return {
      essay: {},
      onEdit: false,
      isSaving: false,
      isUpdating: false,
      headers: [
        {
          text: "Active",
          align: "left",
          sortable: false,
          value: "active"
        },
        { text: "Essay Question", value: "question" },
        { text: "", value: "actions" }
      ],
      selectedQuestion: {},
      deleteDialog: false,
      apiEndpoint: "api/applicants-entry/essay"
    };
  },
  methods: {
    ...mapActions("appEntry", ["getAllQuestions"]),
    save() {
      this.isSaving = true;
      this.$axios
        .post(this.apiEndpoint, this.essay)
        .then(({ data }) => {
          toast.show(data);

          this.isSaving = false;
          this.cancel();
        })
        .catch(({ response }) => {
         toast.show(response.data);
          this.isSaving = false;
        });
    },
    edit(item) {
      this.essay = item;

      this.onEdit = true;
    },
    cancel() {
      this.onEdit = false;
      this.essay = {};
      this.getAllQuestions();
    },
    update() {
      this.isUpdating = false;
      this.$axios
        .put(`${this.apiEndpoint}`, this.essay)
        .then(({ data }) => {
          toast.show(data);

          this.isUpdating = false;
          this.cancel();
        })
        .catch(({ response }) => {
          toast.show(response.data);

          this.isUpdating = false;
        });
    },
    deleteInfo(item) {
      this.selectedInfo = item;
      this.deleteDialog = true;
    },
    deleteConfirmed() {
      this.$axios
        .delete(`${this.apiEndpoint}/${this.selectedInfo.id}`)
        .then(({ data }) => {
          this.deleteDialog = false;
          toast.show(data);
          this.cancel();
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    }
  },
  computed: {
    ...mapState("appEntry", {
      essayQuestions: state => state.essayQuestions
    })
  },
  created() {
    this.getAllQuestions();
  }
};
</script>
