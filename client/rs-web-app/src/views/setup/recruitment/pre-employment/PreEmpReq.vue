<template>
    <div>
        <v-form v-model="valid">
            <v-container>
                <h1>{{title}}</h1>
                <v-layout row wrap>
                    <v-flex xs12 sm6 md2>
                        <v-text-field
                            label="Code"
                            v-model="preEmpReq.preEmpReqCode"
                            :readonly="onEdit"
                            :rules="codeRules">

                        </v-text-field>
                    </v-flex>
                    
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            label="Description"
                            v-model="preEmpReq.preEmpReqDesc">

                        </v-text-field>
                    </v-flex>

                    <v-flex xs12 sm4 md3>
                        <v-text-field
                            label="Employee Status"
                            v-model="preEmpReq.employmentStatusCode"
                            :append-icon="'mdi-account-search'"
                            @click:append="searchEmployeeStatus"
                            readonly>
                        </v-text-field>
                    </v-flex>

                     <v-flex xs12 sm4 md3>
                        <v-text-field
                            label="Designation"
                            v-model="preEmpReq.designationCode"
                            :append-icon="'mdi-account-search'"
                            @click:append="searchDesignation"
                            readonly>
                        </v-text-field>
                    </v-flex>
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
            </v-container>
        </v-form>
        <!-- Employee Status -->
        <v-dialog
            v-model="empStatusDialog"
            max-width="600">
            <v-card>
                <v-card-title class="headline">List of Employee Status</v-card-title>

                <v-card-text>
                    <v-layout>
                         <v-data-table
                            :headers="employeeStatusHeaders"
                            :items="empStatusFileData"
                            class="elevation-1">

                            <template slot="items" slot-scope="props">
                                <td>{{ props.item.code }}</td>
                                <td class="text-xs-left">{{ props.item.description }}</td>
                                <td>
                                    <v-btn icon
                                        @click.prevent="selectEmpStatus(props.item)">
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
                        @click.prevent="empStatusDialog = false">
                        Close
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <!-- Designation Dialog -->
        <v-dialog
            v-model="designationDialog"
            max-width="600">
            <v-card>
                <v-card-title class="headline">List of Designations</v-card-title>

                <v-card-text>
                    <v-layout>
                         <v-data-table
                            :headers="designationHeaders"
                            :items="designationFileData"
                            class="elevation-1">

                            <template slot="items" slot-scope="props">
                                <td>{{ props.item.designationFileCode }}</td>
                                <td class="text-xs-left">{{ props.item.designationFileDesc }}</td>
                                <td>
                                    <v-btn icon
                                        @click.prevent="selectDesignation(props.item)">
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
                        @click.prevent="designationDialog = false">
                        Close
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>

        <!-- Main table -->
        <v-container>
            <v-data-table
                :headers="headers"
                :items="preEmpReqData"
                class="elevation-1">

                <template slot="items" slot-scope="props">
                    <td>{{ props.item.preEmpReqCode }}</td>
                    <td class="text-xs-left">{{ props.item.preEmpReqDesc }}</td>
                    <td class="text-xs-left">{{ props.item.employmentStatus }}</td>
                    <td class="text-xs-left">{{ props.item.designation }}</td>
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
            max-width="400">
            <v-card>
                <v-card-title class="headline">Confirmation</v-card-title>

                <v-card-text>
                    Do you want to delete this Code: {{ selectedRate.preEmpReqCode }}?
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
      title: "Pre Employment Requirements",
      preEmpReq: {},
      codeRules,
      valid: false,
      isSaving: false,
      isUpdating: false,
      isDeleting: false,
      onEdit: false,
      data: [],
      isEmpty: false,
      isBordered: false,
      isStriped: false,
      isNarrowed: false,
      isHoverable: false,
      isFocusable: false,
      hasMobileCards: true,
      deleteDialog: false,
      selectedRate: {},
      headers: [
        {
          text: "Code",
          align: "left",
          sortable: false,
          value: "code"
        },
        { text: "Description", value: "description", align: "left" },
        { text: "Employee Status", value: "empStatus", align: "left" },
        { text: "Designation", value: "designation", align: "left" },
        { text: "", value: "actions" }
      ],
      employeeStatusHeaders: [
        {
          text: "Code",
          align: "left",
          sortable: false,
          value: "code"
        },
        { text: "Description", value: "description", align: "left" },
        { text: "", value: "actions" }
      ],
      empStatusDialog: false,
      designationHeaders: [
        {
          text: "Code",
          align: "left",
          sortable: false,
          value: "code"
        },
        { text: "Description", value: "description", align: "left" },
        { text: "", value: "actions" }
      ],
      designationDialog: false,
      apiEndpoint: "api/pre-emp-req"
    };
  },
  methods: {
    ...mapActions("employeeStatusFile", ["getAllEmployeeStatusFiles"]),
    ...mapActions("designationFile", ["getAllDesignationFiles"]),
    ...mapActions("preEmpReq", ["getAllPreEmpReqData"]),
    save() {
      this.isSaving = true;
      this.$axios
        .post(this.apiEndpoint, this.preEmpReq)
        .then(response => {
          this.isSaving = false;
          let { message, hasError } = response.data;

          // Toast custom message
          toast.show(message, hasError);
          // Update List
          this.cancel();
        })
        .catch(err => {
          this.isSaving = false;
        });
    },
    edit(item) {
      this.onEdit = true;
      this.preEmpReq = item;
    },
    update() {
      this.isUpdating = true;
      this.$axios
        .put(this.apiEndpoint, this.preEmpReq)
        .then(response => {
          this.isUpdating = false;
          let { message, hasError } = response.data;

          // Toast custom message
          toast.show(message, hasError);
          this.cancel();
        })
        .catch(err => {
          this.isUpdating = false;
        });
    },
    deleteInfo(item) {
      this.deleteDialog = true;
      this.selectedRate = item;
    },
    deleteConfirmed() {
      this.isDeleting = true;
      this.$axios
        .delete(`${this.apiEndpoint}/${this.selectedRate.id}`)
        .then(response => {
          let { message, hasError } = response.data;

          // Toast custom message
          toast.show(message, hasError);

          this.cancel();
          this.isDeleting = false;
          this.deleteDialog = false;
        })
        .catch(err => {
          this.isDeleting = false;
        });
    },
    closeDeleteDialog() {
      this.deleteDialog = false;
    },
    cancel() {
      this.onEdit = false;
      this.preEmpReq = {};

      this.getAllPreEmpReqData();
    },
    searchEmployeeStatus() {
      this.empStatusDialog = true;
      this.getAllEmployeeStatusFiles();
    },
    selectEmpStatus(item) {
      this.preEmpReq.employmentStatusCode = item.code;

      this.empStatusDialog = false;
    },
    searchDesignation() {
      this.designationDialog = true;
      this.getAllDesignationFiles();
    },
    selectDesignation(item) {
      this.preEmpReq.designationCode = item.designationFileCode;
      this.designationDialog = false;
    }
  },
  computed: {
    ...mapState("employeeStatusFile", {
      empStatusFileData: state => state.employeeStatusFiles,
      isLoading: state => state.loading
    }),
    ...mapState("designationFile", {
      designationFileData: state => state.designationFiles,
      isLoading: state => state.loading
    }),
    ...mapState("preEmpReq", {
      preEmpReqData: state => state.preEmpReqData,
      isLoading: state => state.loading
    })
  },
  created() {
    this.getAllPreEmpReqData();
  }
};
</script>
