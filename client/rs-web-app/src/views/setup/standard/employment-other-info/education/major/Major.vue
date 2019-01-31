<template>
    <div>
        <v-form v-model="valid">
            <v-container>
                <h1>{{title}}</h1>
                <v-layout row wrap>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            label="Code"
                            v-model="major.majorCode"
                            :readonly="onEdit"
                            :rules="codeRules">

                        </v-text-field>
                    </v-flex>
                    
                    <v-flex xs12 sm6 md8>
                        <v-text-field
                            label="Description"
                            v-model="major.majorDesc">

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
                :items="majorData"
                class="elevation-1">

                <template slot="items" slot-scope="props">
                    <td>{{ props.item.majorCode }}</td>
                    <td class="text-xs-left">{{ props.item.majorDesc }}</td>
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
                    Do you want to delete this Code: {{ selectedMajor.majorCode }}?
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
      title: "Major",
      major: {},
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
      selectedMajor: {},
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
      apiEndpoint: "api/major"
    };
  },
  methods: {
    ...mapActions("major", ["getAllMajors"]),
    save() {
      this.isSaving = true;
      this.$axios
        .post(this.apiEndpoint, this.major)
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
      this.major = item;
    },
    update() {
      this.isUpdating = true;
      this.$axios
        .put(this.apiEndpoint, this.major)
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
      this.selectedMajor = item;
    },
    deleteConfirmed() {
      this.isDeleting = true;
      this.$axios
        .delete(`${this.apiEndpoint}/${this.selectedMajor.majorCode}`)
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
      this.major = {};

      this.getAllMajors();
    }
  },
  computed: {
    ...mapState("major", {
      majorData: state => state.majors,
      isLoading: state => state.loading
    })
  },
  created() {
    this.getAllMajors();
  }
};
</script>
