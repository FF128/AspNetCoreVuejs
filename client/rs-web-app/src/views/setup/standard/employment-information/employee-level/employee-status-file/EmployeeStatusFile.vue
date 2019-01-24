<template>
    <div>
        <v-form v-model="valid">
            <v-container>
                <h1>{{title}}</h1>
                <v-layout row wrap>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            label="Code"
                            v-model="employeeStatusFile.code"
                            :readonly="onEdit"
                            :rules="codeRules">

                        </v-text-field>
                    </v-flex>
                    
                    <v-flex xs12 sm6 md8>
                        <v-text-field
                            label="Description"
                            v-model="employeeStatusFile.description">

                        </v-text-field>
                    </v-flex>
                    <v-btn
                        color="success"
                        @click.prevent="save"
                        v-if="!onEdit && valid">
                        Save
                    </v-btn>
                    <div v-if="onEdit && valid">
                        <v-btn
                            color="success"
                            @click.prevent="update">
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
                :items="empStatusFileData"
                class="elevation-1">

                <template slot="items" slot-scope="props">
                    <td>{{ props.item.code }}</td>
                    <td class="text-xs-left">{{ props.item.description }}</td>
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
                    Do you want to delete this Code: {{ selectedEmpStatusFile.code }}?
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
      title: "Employee Status File",
      employeeStatusFile: {},
      valid: false,
      codeRules,
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
      selectedEmpStatusFile: {},
      headers: [
        {
          text: "Code",
          align: "left",
          sortable: false,
          value: "code"
        },
        { text: "Description", value: "description", align: "left" },
        { text: "", value: "actions" }
      ],
      apiEndpoint: "api/employee-status-file"
    };
  },
  methods: {
    ...mapActions("employeeStatusFile", ["getAllEmployeeStatusFiles"]),
    save() {
      this.isSaving = true;
      this.$axios
        .post(this.apiEndpoint, this.employeeStatusFile)
        .then(response => {
          this.isSaving = false;

          let { message, hasError } = response.data;

          // Toast custom message
          if (hasError) {
            toast.error(message);
          } else {
            toast.success(message);
          }
          // Update List
          this.cancel();
        })
        .catch(err => {
          this.isSaving = false;
        });
    },
    edit(item) {
      this.onEdit = true;
      this.employeeStatusFile = item;
    },
    update() {
      this.isUpdating = true;
      this.$axios
        .put(this.apiEndpoint, this.employeeStatusFile)
        .then(response => {
          this.isUpdating = false;
          // this.onEdit = false;

          let { message, hasError } = response.data;

          // Toast custom message
          if (hasError) {
            toast.error(message);
          } else {
            toast.success(message);
          }

          this.cancel();
        })
        .catch(err => {
          this.isUpdating = false;
        });
    },
    deleteInfo(item) {
      this.deleteDialog = true;
      this.selectedEmpStatusFile = item;
    },
    deleteConfirmed() {
      this.isDeleting = true;
      this.$axios
        .delete(`${this.apiEndpoint}/${this.selectedEmpStatusFile.id}`)
        .then(response => {
          this.getAllEmployeeStatusFiles();

          let { message, hasError } = response.data;

          // Toast custom message
          if (hasError) {
            toast.error(message);
          } else {
            toast.success(message);
          }

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
      this.employeeStatusFile = {};

      this.getAllEmployeeStatusFiles();
    }
  },
  computed: {
    ...mapState("employeeStatusFile", {
      empStatusFileData: state => state.employeeStatusFiles,
      isLoading: state => state.loading
    })
  },
  created() {
    this.getAllEmployeeStatusFiles();
  }
};
</script>
