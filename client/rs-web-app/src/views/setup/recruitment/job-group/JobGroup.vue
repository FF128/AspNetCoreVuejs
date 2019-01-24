<template>
    <vuetify-layout-default>
        <v-form v-model="valid">
            <v-container>
                <h1>{{title}}</h1>
                <v-layout row wrap>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            label="Code"
                            v-model="jobGroup.jobGroupCode"
                            :readonly="onEdit"
                            :rules="codeRules">

                        </v-text-field>
                    </v-flex>
                    
                    <v-flex xs12 sm6 md8>
                        <v-text-field
                            label="Description"
                            v-model="jobGroup.jobGroupDesc">

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
                :items="jobGroupData"
                class="elevation-1">

                <template slot="items" slot-scope="props">
                    <td>{{ props.item.jobGroupCode }}</td>
                    <td class="text-xs-left">{{ props.item.jobGroupDesc }}</td>
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
                    Do you want to delete this Code: {{ selectedJobGroup.jobGroupCode }}?
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
    </vuetify-layout-default>
</template>
<script>
import VuetifyLayoutDefault from "@/layouts/VuetifyLayoutDefault";
import { mapActions, mapState } from "vuex";
import Toast from "@/project-modules/toast";
import codeRules from "@/rules/codeRules";

let toast = new Toast();
export default {
  components: {
    VuetifyLayoutDefault
  },
  data() {
    return {
      title: "Job Group",
      jobGroup: {},
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
      selectedJobGroup: {},
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
      apiEndpoint: "api/jobgroup"
    };
  },
  methods: {
    ...mapActions("jobGroup", ["getAllJobGroups"]),
    save() {
      this.isSaving = true;
      this.$axios
        .post(this.apiEndpoint, this.jobGroup)
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
      this.jobGroup = item;
    },
    update() {
      this.isUpdating = true;
      this.$axios
        .put(this.apiEndpoint, this.jobGroup)
        .then(response => {
          this.isUpdating = false;
          let { message, hasError } = response.data;

          // Toast custom message
          toast.show(message, hasError);
          this.cancel();
        })
        .catch(err => {
          let { message, hasError } = err.response.data;
          toast.show(message, hasError);
          this.isUpdating = false;
        });
    },
    deleteInfo(item) {
      this.deleteDialog = true;
      this.selectedJobGroup = item;
    },
    deleteConfirmed() {
      this.isDeleting = true;
      this.$axios
        .delete(`${this.apiEndpoint}/${this.selectedJobGroup.id}`)
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
      this.jobGroup = {};

      this.getAllJobGroups();
    }
  },
  computed: {
    ...mapState("jobGroup", {
      jobGroupData: state => state.jobGroups,
      isLoading: state => state.loading
    })
  },
  created() {
    this.getAllJobGroups();
  }
};
</script>
