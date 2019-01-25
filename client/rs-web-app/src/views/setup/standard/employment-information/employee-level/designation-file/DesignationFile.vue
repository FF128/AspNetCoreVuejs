<template>
    <div>
        <v-form v-model="valid">
            <v-container>
                <h1>{{title}}</h1>
                <v-layout row wrap>
                    <v-flex xs12 sm4 md3>
                        <v-text-field
                            label="Code"
                            v-model="designationFile.designationFileCode"
                            :readonly="onEdit"
                            :rules="codeRules">

                        </v-text-field>
                    </v-flex>
                    
                    <v-flex xs12 sm4 md6>
                        <v-text-field
                            label="Description"
                            v-model="designationFile.designationFileDesc">

                        </v-text-field>
                    </v-flex>

                    <v-flex xs12 sm4 md3>
                        <v-text-field
                            label="Job Level"
                            v-model="designationFile.jobLevelCode"
                            :append-icon="'mdi-account-search'"
                            @click:append="searchJobLevelCode"
                            readonly>

                        </v-text-field>
                    </v-flex>
                    <v-btn
                        color="success"
                        @click.prevent="save"
                        :loading="isSaving"
                        v-if="!onEdit"
                        :disabled="!valid">
                        Save
                    </v-btn>
                    <div v-if="onEdit && valid">
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
                </v-layout>
            </v-container>
        </v-form>
        <v-container>
            <v-data-table
                :headers="headers"
                :items="designationFileData"
                class="elevation-1">

                <template slot="items" slot-scope="props">
                    <td>{{ props.item.designationFileCode }}</td>
                    <td class="text-xs-left">{{ props.item.designationFileDesc }}</td>
                    <td class="text-xs-left">{{ props.item.jobLevelCode }}</td>
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
        <!-- Delete Confirmation Dialog -->
        <v-dialog
            v-model="deleteDialog"
            max-width="400">
            <v-card>
                <v-card-title class="headline">Confirmation</v-card-title>

                <v-card-text>
                    Do you want to delete this Code: {{ selectedDesignationFile.designationFileCode }}?
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
                        @click.prevent="closeDeleteDialog">
                        No
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>

        <!-- Job Level Dialog -->
        <v-dialog
            v-model="jobLevelDialog"
            max-width="850">
            <v-card>
                <v-card-title class="headline">List of Job Level</v-card-title>

                <v-card-text>
                    <v-layout>
                         <v-data-table
                            :headers="jobLevelHeaders"
                            :items="jobLevelData"
                            class="elevation-1">

                            <template slot="items" slot-scope="props">
                                <td>{{ props.item.code }}</td>
                                <td class="text-xs-left">{{ props.item.description }}</td>
                                <td class="text-xs-left">{{ props.item.gradeCode }}</td>
                                <td class="text-xs-left">{{ props.item.stepCode }}</td>
                                <td class="text-xs-left">{{ props.item.minimumSalary }}</td>
                                <td class="text-xs-left">{{ props.item.maximumSalary }}</td>
                                <td>
                                    <v-btn icon
                                        @click.prevent="selectJobLevel(props.item)">
                                        <v-icon>mdi-magnify-plus</v-icon>
                                    </v-btn>
                                </td>
                            </template>
                        </v-data-table>
                    </v-layout>
                </v-card-text>

                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn
                        flat="flat"
                        @click.prevent="jobLevelDialog = false">
                        Close
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>
</template>
<script>
import { mapActions, mapState } from "vuex";
import Toast from "@/project-modules/toast";
import codeRules from "@/rules/codeRules";

let toast = new Toast();
export default {
  data() {
    return {
      title: "Designation File",
      designationFile: {},
      codeRules,
      valid: false,
      isSaving: false,
      isUpdating: false,
      isDeleting: false,
      onEdit: false,
      deleteDialog: false,
      selectedDesignationFile: {},
      headers: [
        {
          text: "Code",
          align: "left",
          sortable: false,
          value: "code"
        },
        { text: "Description", value: "description", align: "left" },
        { text: "Job Level", value: "jobLevelCode", align: "left" },
        { text: "", value: "actions" }
      ],
      jobLevelHeaders: [
        {
          text: "Code",
          align: "left",
          sortable: false,
          value: "code"
        },
        { text: "Description", value: "description", align: "left" },
        { text: "Grade", value: "gradeDescription", align: "left" },
        { text: "Step", value: "stepDescription", align: "left" },
        { text: "Minimum Salary", value: "minimumSalary", align: "left" },
        { text: "Maximum Salary", value: "maximumSalary", align: "left" },
        { text: "", value: "actions" }
      ],
      jobLevelDialog: false,
      apiEndpoint: "api/designation-file"
    };
  },
  methods: {
    ...mapActions("jobLevel", ["getAllJobLevels"]),
    ...mapActions("designationFile", ["getAllDesignationFiles"]),
    save() {
      this.isSaving = true;
      this.$axios
        .post(this.apiEndpoint, this.designationFile)
        .then(response => {
          this.isSaving = false;
          toast.show(response.data);
          // Update List
          this.cancel();
        })
        .catch(({response}) => {
          toast.show(response.data);
          this.isSaving = false;
        });
    },
    edit(item) {
      this.onEdit = true;
      this.designationFile = item;
    },
    update() {
      this.isUpdating = true;
      this.$axios
        .put(this.apiEndpoint, this.designationFile)
        .then(response => {
          this.isUpdating = false;
          this.onEdit = false;
          toast.show(response.data);
          this.cancel();
        })
        .catch(({response}) => {
          toast.show(response.data);
          this.isUpdating = false;
        });
    },
    deleteInfo(item) {
      this.deleteDialog = true;
      this.selectedDesignationFile = item;
    },
    deleteConfirmed() {
      this.isDeleting = true;
      this.$axios
        .delete(`${this.apiEndpoint}/${this.selectedDesignationFile.id}`)
        .then(response => {
          toast.show(response.data);
          this.cancel();

          this.isDeleting = false;
          this.deleteDialog = false;
        })
        .catch(({response}) => {
          toast.show(response.data);
          this.isDeleting = false;
        });
    },
    closeDeleteDialog() {
      this.deleteDialog = false;
    },
    cancel() {
      this.onEdit = false;
      this.designationFile = {};

      this.getAllDesignationFiles();
    },
    searchJobLevelCode() {
      this.jobLevelDialog = true;

      this.getAllJobLevels();
    },
    selectJobLevel(item) {
      this.designationFile.jobLevelCode = item.code;

      this.jobLevelDialog = false;
    }
  },
  computed: {
    ...mapState("jobLevel", {
      jobLevelData: state => state.jobLevels
    }),
    ...mapState("designationFile", {
      designationFileData: state => state.designationFiles,
      isLoading: state => state.loading
    })
  },
  created() {
    this.getAllDesignationFiles();
  }
};
</script>
