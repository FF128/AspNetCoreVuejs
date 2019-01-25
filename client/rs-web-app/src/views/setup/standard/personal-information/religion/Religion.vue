<template>
    <div>
        <v-form v-model="valid" ref="form">
            <v-container>
                <h1>{{title}}</h1>
                <v-layout row wrap>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            label="Code"
                            v-model="religion.code"
                            :readonly="onEdit"
                            :rules="codeRules">

                        </v-text-field>
                    </v-flex>
                    
                    <v-flex xs12 sm6 md8>
                        <v-text-field
                            label="Description"
                            v-model="religion.description">

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
                :items="religionData"
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
                    Do you want to delete this Code: {{ selectedRel.code }}?
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
      valid: false,
      title: "Religion",
      religion: {},
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
      selectedRel: {},
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
      apiEndpoint: "api/religion"
    };
  },
  methods: {
    ...mapActions("religion", ["getAllReligions"]),
    save() {
      this.isSaving = true;
      this.$axios
        .post(this.apiEndpoint, this.religion)
        .then(response => {
          this.isSaving = false;
          // Toast custom message
          toast.show(response.data);
          // Update List
          this.getAllReligions();
          this.religion = {};
        })
        .catch(({ response }) => {
          this.isSaving = false;
          toast.show(response.data);
        });
    },
    edit(item) {
      this.onEdit = true;
      this.religion = item;
    },
    update() {
      this.isUpdating = true;
      this.$axios
        .put(this.apiEndpoint, this.religion)
        .then(response => {
          this.isUpdating = false;
          this.onEdit = false;
          // Toast custom message
          toast.show(response.data);
          this.getAllReligions();
        })
        .catch(({ response }) => {
          this.isUpdating = false;
          toast.show(response.data);
        });
    },
    deleteInfo(item) {
      this.deleteDialog = true;
      this.selectedRel = item;
    },
    deleteConfirmed() {
      this.isDeleting = true;
      this.$axios
        .delete(`${this.apiEndpoint}/${this.selectedRel.id}`)
        .then(response => {
          this.getAllReligions();
          // Toast custom message
          toast.show(response.data);
          this.isDeleting = false;
          this.deleteDialog = false;
        })
        .catch(({ response }) => {
          toast.show(response.data);
          this.isDeleting = false;
        });
    },
    closeDeleteDialog() {
      this.deleteDialog = false;
    },
    cancel() {
      this.onEdit = false;
      this.religion = {};

      this.getAllReligions();
    }
  },
  computed: {
    ...mapState("religion", {
      religionData: state => state.religions,
      isLoading: state => state.loading
    })
  },
  created() {
    this.getAllReligions();
  }
};
</script>
