<template>
    <div class="section">
        <h1 class="title is-4">{{title}}</h1>
        <div class="columns">
            <div class="column is-4">
                <b-field label="Code" custom-class="is-small">
                    <b-input size="is-small" v-model="jobLevel.code"></b-input>
                </b-field>
            </div>
            <div class="column">
                <b-field label="Description" custom-class="is-small">
                    <b-input size="is-small" v-model="jobLevel.description"></b-input>
                </b-field>
            </div>
        </div>
        <div class="field is-grouped">
            <div class="control" v-if="!onEdit">
                <button class="button is-success" @click="save" :class="isSaving | buttonLoading">Save</button>
            </div>
            <div class="control" v-if="onEdit">
                <button class="button is-success" @click.prevent="update" :class="isUpdating | buttonLoading">Update</button>
            </div>
            <div class="control" v-if="onEdit">
                <button class="button is-default" @click.prevent="cancel">Cancel</button>
            </div>
        </div>

        <b-table
            :data="isEmpty ? [] : jobLevelData"
            :bordered="isBordered"
            :striped="isStriped"
            :narrowed="isNarrowed"
            :hoverable="isHoverable"
            :loading="isLoading"
            :focusable="isFocusable"
            :mobile-cards="hasMobileCards">

            <template slot-scope="props">
                <b-table-column field="code" label="Code">
                    {{ props.row.code }}
                </b-table-column>

                <b-table-column field="description" label="Description">
                    {{ props.row.description }}
                </b-table-column>
                <b-table-column label="">
                    <button class="button is-default" @click.prevent="edit(props.row)">
                        <b-icon
                            icon="pencil"
                            size="is-small">
                        </b-icon>
                    </button>
                    &nbsp;
                    <button class="button is-danger" @click.prevent="deleteInfo(props.row)">
                        <b-icon
                            icon="delete"
                            size="is-small">

                        </b-icon>
                    </button>
                </b-table-column>
            </template>

            <template slot="empty">
                <section class="section">
                    <div class="content has-text-grey has-text-centered">
                        <p>
                            <b-icon
                                icon="emoticon-sad"
                                size="is-large">
                            </b-icon>
                        </p>
                        <p>Nothing here.</p>
                    </div>
                </section>
            </template>
        </b-table>
        
        <div class="modal" :class="deleteModal | modalStatusFilter"> <!-- Delete Confirmation -->
            <div class="modal-background"></div>
            <div class="modal-card">
                <header class="modal-card-head">
                    <p class="modal-card-title">Confirmation</p>
                    <button class="delete" aria-label="close" @click="closeDeleteModal"></button>
                </header>
                <section class="modal-card-body">
                    <!-- Content ... -->
                    <h1 class="title is-5">Do you want to delete this information (Code: {{selectedJobLevel.code}})?</h1>
                </section>
                <footer class="modal-card-foot" style="justify-content: flex-end;">
                    <button class="button is-danger" @click="deleteConfirmed(selectedJobLevel)" :class="isDeleting | buttonLoading">Yes</button>
                    <button class="button" @click="closeDeleteModal">No</button>
                </footer>
            </div>
        </div>
    </div>
    
    
</template>
<script>
import { mapActions, mapState } from "vuex"
import AppToast from "@/project-modules/appToast"
//import AppToast from "../../../..//project-modules/appToast";

let appToast = new AppToast();

let apiEnpoint = "api/employee-status-file"

export default {
    data() {
        return {
            title: "Job Level",
            jobLevel: {},
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
            deleteModal: false,
            selectedJobLevel: {}
        }
    },
    methods: {
        ...mapActions('jobLevel', [
            'getAllJobLevels'
        ]),
        save() {
            this.isSaving = true;
            this.$axios.post(apiEnpoint, this.jobLevel)
                .then(response => {
                    this.isSaving = false;

                    appToast.success(`Successfully Added`);
                    // Update List
                    this.getAllJobLevels();
                })
                .catch(err => {
                    this.isSaving = false;
                    appToast.danger("Something went wrong!");
                });
        },
        edit(item){
            this.onEdit = true;
            this.jobLevel = item;
        },
        update() {
            this.isUpdating = true
            this.$axios.put(apiEnpoint, this.jobLevel)
                .then(response => {
                    this.isUpdating = false;
                    this.onEdit = false;
                    appToast.success("Successfully Updated");

                    this.getAllJobLevels();
                })
                .catch(err => {
                    this.isUpdating = false;
                    appToast.danger("Error");
                })
        },
        deleteInfo(item) {
            this.deleteModal = true;
            this.selectedJobLevel = item;
        },
        deleteConfirmed(item) {
            this.isDeleting = true;
            this.$axios.delete(`${apiEnpoint}/${item.id}`)
                .then(response => {
                    this.getAllEmployeeStatusFiles();

                    appToast.success(`Code: ${item.code} has been deleted`);

                    this.isDeleting = false;
                    this.deleteModal = false;
                })
                .catch(err => {
                    appToast.danger("Error deleting")

                    this.isDeleting = false;
                })
        },
        closeDeleteModal() {
            this.deleteModal = false;
        },
        cancel() {
            this.onEdit = false;
            this.jobLevel = {}

            this.getAllJobLevels();
        }
    },
    computed: {
        ...mapState('jobLevel', {
            jobLevelData: state => state.jobLevels,
            isLoading: state => state.loading
        })
    },
    created() {
        this.getAllJobLevels();
    }
}
</script>
