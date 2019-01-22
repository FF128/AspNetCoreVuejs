<template>
    <div>
        <v-form v-model="valid">
            <v-container>
                <v-layout row wrap>
                    <v-flex xs12 sm6 md3>
                        <v-text-field
                            label="Code"
                            v-model="source.code"
                            :rules="codeRules"
                            :readonly="onEdit">

                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md6>
                        <v-text-field
                            label="Source of Information"
                            v-model="source.desc">

                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md3>
                        <v-switch
                            label="Is Active"
                            v-model="source.active"
                        ></v-switch>
                    </v-flex>
                    <v-flex xs12 sm12 md12>
                        <v-btn
                            @click.prevent="save"
                            v-if="!onEdit"
                            color="success"
                            :loading="isSaving"
                            :disabled="!valid">
                            Add
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
        <v-container>
            <v-data-table
                :items="sources"
                class="elevation-1"
                hide-actions
                :headers="headers"
            >
                <template slot="items" slot-scope="props">
                <td>{{ props.item.active }}</td>
                <td class="text-xs-left">{{ props.item.code }}</td>
                <td class="text-xs-left">{{ props.item.desc }}</td>
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
            max-width="300">
            <v-card>
                <v-card-title class="headline">Confirmation</v-card-title>

                <v-card-text>
                    Do you want to delete this record?
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
    </div>
</template>
<script>
import Toast from "@/project-modules/toast"
import codeRules from "@/rules/codeRules"
import { mapState, mapActions } from 'vuex';

let toast = new Toast();
export default {
    data() {
        return {
            source: {},
            onEdit: false,
            isSaving: false,
            isUpdating: false,
            codeRules,
            valid:false,
            headers: [
                {
                    text: 'Active',
                    align: 'left',
                    sortable: false,
                    value: 'active'
                },
                { text: 'Code', value: 'code' },
                { text: 'Description', value: 'desc'},
                { text: '', value: 'actions'}
            ],
            selectedInfo: {},
            deleteDialog: false,
            apiEndpoint: "api/applicants-entry/source"
        }
    },
     methods: {
        ...mapActions('appEntry',[
            'getAllSourceOfInfo'
        ]),
        save() {
            this.isSaving = true
            this.$axios.post(this.apiEndpoint, this.source)
                .then(({ data }) => {
                    let { message, hasError } = data;
                    toast.show(message, hasError);

                    this.isSaving = false;
                    this.cancel();
                })
                .catch(( { response }) => {
                    let { message, hasError } = response.data;
                    toast.show(message, hasError);
                    this.isSaving = false;
                });
        },
        edit(item) {
            this.source = item;

            this.onEdit = true;
        },
        cancel() {
            this.onEdit = false;
            this.source = {}
            this.getAllSourceOfInfo();
        },
        update() {
            this.isUpdating = false;
            this.$axios.put(`${this.apiEndpoint}`, this.source)
                .then(({ data }) => {
                    let { message, hasError } = data;
                    toast.show(message, hasError);

                    this.isUpdating = false;
                    this.cancel();
                })
                .catch(({ response }) => {
                    let { message, hasError } = response.data;
                    toast.show(message, hasError);

                    this.isUpdating = false;
                });
        },
        deleteInfo(item) {
            this.selectedInfo = item;
            this.deleteDialog = true;
        },
        deleteConfirmed() {
            this.$axios.delete(`${this.apiEndpoint}/${this.selectedInfo.id}`)
                .then(({ data }) => {
                    this.deleteDialog = false;
                    let { message, hasError } = data;
                    toast.show(message, hasError);
                    this.cancel();
                })
                .catch(({ response }) => {
                    let { message, hasError } = response.data;
                    toast.show(message, hasError);
                });
        }
    },
    computed: {
        ...mapState('appEntry', {
            sources: state => state.sources
        })
    },
    created() {
        this.getAllSourceOfInfo();
    }
}
</script>
