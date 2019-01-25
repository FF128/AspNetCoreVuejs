<template>
    <div>
        <v-form v-model="valid">
            <v-container>
                <h1>{{title}}</h1>
                <v-layout row wrap>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            label="Job Group"
                            v-model="selectedJobGroup"
                            :append-icon="'mdi-account-search'"
                            @click:append="searchJobGroup"
                            readonly>
                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            label="Code"
                            v-model="jobCat.jobCatCode"
                            :readonly="onEdit"
                            :rules="codeRules">

                        </v-text-field>
                    </v-flex>
                    
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            label="Description"
                            v-model="jobCat.jobCatDesc">

                        </v-text-field>
                    </v-flex>
                </v-layout>
            </v-container>
        </v-form>
            <!-- Position table -->
        <v-container>
            <v-data-table
                :items="designationFileData"
                class="elevation-1"
                hide-actions
                select-all
                v-model="selectedDesignation"
                item-key="designationFileCode"
                :headers="designationFileHeaders">
                <template slot="items" slot-scope="props">
                    <td>
                        <v-checkbox
                            v-model="props.selected"
                            primary
                            hide-details>
                        </v-checkbox>
                    </td>
                    <td>{{ props.item.designationFileCode }}</td>
                    <td class="text-xs-left">{{ props.item.designationFileDesc }}</td>
                </template>
            </v-data-table>
            <!-- Button Add Update -->
            <v-layout row wrap>
                <v-flex xs12 sm12 md12>
                    <v-btn
                        color="success"
                        @click.prevent="save"
                        v-if="!onEdit"
                        :loading="isSaving"
                        :disabled="!valid">
                        Save
                    </v-btn>
                    <div v-if="onEdit">
                        <v-btn
                            color="success"
                            @click.prevent="update"
                            :loading="isUpdating"
                            :disabled="!valid">
                            Update
                        </v-btn>
                        <v-btn
                            @click.prevent="cancel">
                            Cancel
                        </v-btn>
                    </div>
                </v-flex>
            </v-layout>
            
            <!-- Main Table -->
            <v-data-table
                :headers="jobCategoryHeaders"
                :items="jobCategories"
                class="elevation-1">

                <template slot="items" slot-scope="props">
                    <td class="text-xs-left">{{ props.item.jobGroupDesc }}</td>
                    <td class="text-xs-left">{{ props.item.jobCatCode }}</td>
                    <td class="text-xs-left">{{ props.item.jobCatDesc }}</td>
                    <td class="text-xs-left">
                        <template v-for="(item, key) in props.item.designationFiles">
                            <p v-if="item" v-bind:key="key">• {{ item.designationFileDesc }}</p>
                        </template>
                    </td>
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

            <!-- Job Group Dialog -->
            <v-dialog
                v-model="jobGroupDialog"
                max-width="600">
                <v-card>
                    <v-card-title class="headline">List of Job Groups</v-card-title>
                    <v-card-text>
                        <v-layout>
                            <v-data-table
                                :headers="jobGroupHeaders"
                                :items="jobGroupData"
                                class="elevation-1">

                                <template slot="items" slot-scope="props">
                                    <td>{{ props.item.jobGroupCode }}</td>
                                    <td class="text-xs-left">{{ props.item.jobGroupDesc }}</td>
                                    <td>
                                        <v-btn icon
                                            @click.prevent="selectJobGroup(props.item)">
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
                            @click.prevent="jobGroupDialog = false">
                            Close
                        </v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
            <!-- Delete Dialog -->
            <v-dialog
                v-model="deleteDialog"
                max-width="400">
                <v-card>
                    <v-card-title class="headline">Confirmation</v-card-title>

                    <v-card-text>
                        Do you want to delete this Code: {{ selectedJobCat.jobCatCode }}?
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
        </v-container>
    </div>
</template>
<script>
import { mapActions, mapState } from "vuex";
import codeRules from "@/rules/codeRules";
import Toast from "@/project-modules/toast";

let toast = new Toast();
export default {
  data() {
    return {
      title: "Job Category",
      jobCat: {},
      selectedJobCat: {},
      selectedJobGroup: "",
      jobGroupDialog: false,
      onEdit: false,
      isSaving: false,
      isUpdating: false,
      valid: false,
      codeRules,
      deleteDialog: false,
      selectedDesignation: [],
      jobGroupHeaders: [
        {
          text: "Code",
          align: "left",
          sortable: false,
          value: "code"
        },
        { text: "Description", value: "description", align: "left" },
        { text: "", value: "actions" }
      ],
      designationFileHeaders: [
        {
          text: "Code",
          align: "left",
          sortable: false,
          value: "code"
        },
        { text: "Description", value: "description", align: "left" }
      ],
      jobCategoryHeaders: [
        {
          text: "Job Group",
          align: "left",
          sortable: false,
          value: "jobGroup"
        },
        { text: "Code", value: "code", align: "left" },
        { text: "Description", value: "description", align: "left" },
        { text: "Designation", value: "designation", align: "left" },
        { text: "", value: "actions" }
      ],
      apiEndpoint: "api/job-cat"
    };
  },
  methods: {
    ...mapActions("jobCategory", ["getAllJobCategories"]),
    ...mapActions("jobGroup", ["getAllJobGroups"]),
    ...mapActions("designationFile", ["getAllDesignationFiles"]),
    searchJobGroup() {
      this.getAllJobGroups();

      this.jobGroupDialog = true;
    },
    selectJobGroup(item) {
      this.selectedJobGroup = item.jobGroupDesc;
      this.jobCat.jobGroupCode = item.jobGroupCode;
      this.jobGroupDialog = false;
    },
    save() {
      this.isSaving = true;
      this.$axios
        .post(this.apiEndpoint, {
          jobCatCode: this.jobCat.jobCatCode,
          jobCatDesc: this.jobCat.jobCatDesc,
          jobGroupCode: this.jobCat.jobGroupCode,
          jobCategoryDetails: this.selectedDesignation
        })
        .then(({ data }) => {
          this.isSaving = false;
          this.cancel();
          toast.show(data);
        })
        .catch(({ response }) => {
          this.isSaving = false;
          toast.show(response.data);
        });
    },
    edit(item) {
      this.selectedDesignation = item.designationFiles;
      this.jobCat = {
        jobCatCode: item.jobCatCode,
        jobCatDesc: item.jobCatDesc,
        jobGroupCode: item.jobGroupCode
      };
      this.selectedJobGroup = item.jobGroupDesc;
      this.onEdit = true;
    },
    update() {
      this.isUpdating = true;
      this.$axios
        .put(this.apiEndpoint, {
          jobCatCode: this.jobCat.jobCatCode,
          jobCatDesc: this.jobCat.jobCatDesc,
          jobGroupCode: this.jobCat.jobGroupCode,
          jobCategoryDetails: this.selectedDesignation
        })
        .then(({ data }) => {
          this.isUpdating = false;
          this.cancel();
          toast.show(data);
        })
        .catch(({ response }) => {
          this.isUpdating = false;
          toast.show(response.data);
        });
    },
    deleteInfo(item) {
      this.deleteDialog = true;
      this.selectedJobCat = item;
    },
    deleteConfirmed() {
      this.isDeleting = true;
      this.$axios
        .delete(`${this.apiEndpoint}/${this.selectedJobCat.jobCatCode}`)
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
    cancel() {
      this.getAllJobCategories();
      this.onEdit = false;
      this.jobCat = {};
      this.selectedDesignation = [];
      this.selectedJobGroup = "";
    }
  },
  computed: {
    ...mapState("jobCategory", {
      jobCategories: state => state.jobCategories,
      isLoading: state => state.loading
    }),
    ...mapState("jobGroup", {
      jobGroupData: state => state.jobGroups,
      isLoading: state => state.loading
    }),
    ...mapState("designationFile", {
      designationFileData: state => state.designationFiles,
      isLoading: state => state.loading
    })
  },
  created() {
    this.getAllJobCategories();

    this.getAllDesignationFiles();
  }
};
</script>
