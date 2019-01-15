<template>
    <div>
        <v-form>
            <v-container>
                <h1>{{title}}</h1>
                <v-layout row wrap>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            label="Code"
                            v-model="rank.rankCode">

                        </v-text-field>
                    </v-flex>
                    
                    <v-flex xs12 sm6 md8>
                        <v-text-field
                            label="Description"
                            v-model="rank.rankDesc">

                        </v-text-field>
                    </v-flex>
                    <v-btn
                        color="success"
                        @click.prevent="save"
                        v-if="!onEdit">
                        Save
                    </v-btn>
                    <div v-if="onEdit">
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
                :items="rankData"
                class="elevation-1">

                <template slot="items" slot-scope="props">
                    <td>{{ props.item.rankCode }}</td>
                    <td class="text-xs-left">{{ props.item.rankDesc }}</td>
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
                    Do you want to delete this Code: {{ selectedRank.code }}?
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
import { mapActions, mapState } from "vuex"
import AppToast from "@/project-modules/appToast"

let appToast = new AppToast();
export default {
    data() {
        return {
            title: "Rank",
            rank: {},
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
            selectedRank: {},
            headers: [
                {
                    text: 'Code',
                    align: 'left',
                    sortable: false,
                    value: 'code'
                },
                { text: 'Description', value: 'description', align: 'left' },
                { text: '', value: 'actions' }
            ],
            apiEndpoint: "api/rank"
        }
    },
    methods: {
        ...mapActions('rank', [
            'getAllRanks'
        ]),
        save() {
            this.isSaving = true;
            this.$axios.post(this.apiEndpoint, this.rank)
                .then(response => {
                    this.isSaving = false;

                    // Update List
                    this.getAllRanks();
                    this.rank = {}
                })
                .catch(err => {
                    this.isSaving = false;
                });
        },
        edit(item){
            this.onEdit = true;
            this.rank = item;
        },
        update() {
            this.isUpdating = true
            this.$axios.put(this.apiEndpoint, this.rank)
                .then(response => {
                    this.isUpdating = false;
                    
                    this.cancel();
                })
                .catch(err => {
                    this.isUpdating = false;
                })
        },
        deleteInfo(item) {
            this.deleteDialog = true;
            this.selectedRank = item;
        },
        deleteConfirmed() {
            this.isDeleting = true;
            this.$axios.delete(`${this.apiEndpoint}/${this.selectedRank.id}`)
                .then(response => {
                    this.getAllRanks();
                    
                    this.isDeleting = false;
                    this.deleteDialog = false;
                })
                .catch(err => {
                    this.isDeleting = false;
                })
        },
        closeDeleteDialog() {
            this.deleteDialog = false;
        },
        cancel() {
            this.onEdit = false;
            this.rank = {}

            this.getAllRanks();
        }
    },
    computed: {
        ...mapState('rank', {
            rankData: state => state.ranks,
            isLoading: state => state.loading
        })
    },
    created() {
        this.getAllRanks();
    }
}
</script>
