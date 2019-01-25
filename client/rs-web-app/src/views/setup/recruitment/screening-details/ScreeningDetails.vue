<template>
    <div>
        <v-form v-model="valid">
            <v-container>
                <h1>{{title}}</h1>
                <v-layout row wrap>
                    <!-- <v-flex xs12 sm6 md4>
                        <v-text-field
                            label="Code"
                            v-model="screenDetail.screenDetailsCode"
                            :readonly="onEdit"
                            :rules="codeRules">

                        </v-text-field>
                    </v-flex> -->
                    <v-flex xs12 sm4 md3>
                        <v-text-field
                            label="Code"
                            v-model="screenDetail.screenDetailsCode"
                            :append-icon="'mdi-account-search'"
                            @click:append="searchDesignation"
                            readonly
                            :rules="codeRules">
                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md8>
                        <v-text-field
                            label="Description"
                            v-model="screenDetail.screenDetailsDesc">

                        </v-text-field>
                    </v-flex>
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
                </v-layout>
            </v-container>
        </v-form>
        <v-container>
            <v-data-table
                :headers="headers"
                :items="screenDetailData"
                class="elevation-1">

                <template slot="items" slot-scope="props">
                    <td>
                        <v-btn flat icon 
                            @click.prevent="$router.push(`screen-details/fill-up/${props.item.screenDetailsCode}/${props.item.id}`)">
                            <v-icon>arrow_right_alt</v-icon>
                        </v-btn>
                    </td>
                    <td>{{ props.item.screenDetailsCode }}</td>
                    <td class="text-xs-left">{{ props.item.screenDetailsDesc }}</td>
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
                    Do you want to delete this Code: {{ selectedDetail.screenDetailsCode }}?
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
        <!-- Designation Dialog -->
        <v-dialog
            v-model="designationDialog"
            max-width="600"
            >
            <v-card v-if="designationDialog">
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
      title: "Screening Details",
      screenDetail: {},
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
      selectedDetail: {},
      headers: [
        { text: "Fill up", value: "fillUp", sortable: false},
        {
          text: "Code",
          align: "left",
          sortable: false,
          value: "screenDetailsCode"
        },
        { text: "Description", value: "screenDetailsDesc", align: "left" },
        { text: "", value: "actions" }
      ],
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
      apiEndpoint: "api/screen-details"
    };
  },
  methods: {
    ...mapActions("screenDetails", ["getAllDetails"]),
    ...mapActions("designationFile", ["getAllDesignationFiles"]),
    save() {
      this.isSaving = true;
      this.$axios
        .post(this.apiEndpoint, this.screenDetail)
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
      this.screenDetail = item;
    },
    update() {
      this.isUpdating = true;
      this.$axios
        .put(this.apiEndpoint, this.screenDetail)
        .then(response => {
          this.isUpdating = false;
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
      this.selectedDetail = item;
    },
    deleteConfirmed() {
      this.isDeleting = true;
      this.$axios
        .delete(`${this.apiEndpoint}/${this.selectedDetail.id}`)
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
      this.screenDetail = {};

      this.getAllDetails();
    },
    searchDesignation() {
      this.designationDialog = true;
      this.getAllDesignationFiles();
    },
    selectDesignation(item) {
      this.screenDetail = {
          screenDetailsCode: item.designationFileCode,
          screenDetailsDesc: item.designationFileDesc
      }
      this.designationDialog = false;
    },
    fillUp() {

    }
  },
  computed: {
    ...mapState("screenDetails", {
      screenDetailData: state => state.screenDetails,
      isLoading: state => state.loading
    }),
    ...mapState("designationFile", {
      designationFileData: state => state.designationFiles,
      isLoading: state => state.loading
    })
  },
  created() {
    this.getAllDetails();
  }
};
</script>
