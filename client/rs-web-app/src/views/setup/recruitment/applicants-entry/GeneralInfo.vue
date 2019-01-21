<template>
    <div>
        <v-form>
            <v-container>
                <v-layout row wrap>
                    <v-flex xs12 sm6 md4>
                        <v-switch
                            label="Is Active"
                            v-model="genInfo.active"
                        ></v-switch>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                        <v-switch
                            label="Show [Yes] Option"
                            v-model="genInfo.showYes"
                        ></v-switch>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                        <v-switch
                            label="Show [No] Option"
                            v-model="genInfo.showNo"
                        ></v-switch>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                        <v-switch
                            label="Show [None] Option"
                            v-model="genInfo.showNone"
                        ></v-switch>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                        <v-switch
                            label="Show [Why] Field"
                            v-model="genInfo.showWhy"
                        ></v-switch>
                    </v-flex>
                    
                    <v-flex xs12 sm6 md6>
                        <v-text-field
                            label="Show [Why Text] Field"
                            v-model="genInfo.showWhyText">

                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md6>
                        <v-text-field
                            label="Question"
                            v-model="genInfo.question">

                        </v-text-field>
                    </v-flex>
                    <v-flex xs12 sm12 md12>
                        <v-btn
                            @click.prevent="save"
                            v-if="!onEdit"
                            color="success"
                            :loading="isSaving">
                            Add
                        </v-btn>
                        <div v-if="onEdit">
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
                    </v-flex>
                </v-layout>
                <v-data-table
                    :items="genInfoData"
                    class="elevation-1"
                    hide-actions
                    :headers="headers"
                >
                    <template slot="items" slot-scope="props">
                    <td>{{ props.item.active }}</td>
                    <td class="text-xs-right">{{ props.item.showYes }}</td>
                    <td class="text-xs-right">{{ props.item.showNo }}</td>
                    <td class="text-xs-right">{{ props.item.showNone }}</td>
                    <td class="text-xs-right">{{ props.item.showWhy }}</td>
                    <td class="text-xs-right">{{ props.item.showWhyText }}</td>
                    <td class="text-xs-right">{{ props.item.question }}</td>
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
        </v-form>
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
import { mapState, mapActions } from 'vuex';

let toast = new Toast();
export default {
    data() {
        return {
            genInfo: {},
            onEdit: false,
            isSaving: false,
            isUpdating: false,
            headers: [
                {
                    text: 'Active',
                    align: 'left',
                    sortable: false,
                    value: 'active'
                },
                { text: 'Show Yes', value: 'showYes' },
                { text: 'Show No', value: 'showNo' },
                { text: 'Show None', value: 'showNone' },
                { text: 'Show Why', value: 'showWhy' },
                { text: 'Show Why Text', value: 'showWhyText' },
                { text: 'Question', value: 'question' },
                { text: '', value: 'actions'}
            ],
            selectedInfo: {},
            deleteDialog: false,
            apiEndpoint: "api/applicants-entry/gen"
        }
    },
    methods: {
        ...mapActions('appEntry',[
            'getAllGenInfoData'
        ]),
        save() {
            this.isSaving = true
            this.$axios.post(this.apiEndpoint, this.genInfo)
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
            this.genInfo = item;

            this.onEdit = true;
        },
        cancel() {
            this.onEdit = false;
            this.genInfo = {}
            this.getAllGenInfoData();
        },
        update() {
            this.isUpdating = false;
            this.$axios.put(`${this.apiEndpoint}`, this.genInfo)
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
            genInfoData: state => state.genInfoData
        })
    },
    created() {
        this.getAllGenInfoData();
    }
}
</script>