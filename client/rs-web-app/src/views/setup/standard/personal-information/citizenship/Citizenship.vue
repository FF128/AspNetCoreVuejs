<template>
    <div>
        <v-form v-model="valid" ref="form">
            <v-container>
                <h1>{{title}}</h1>
                <v-layout row wrap>
                    <v-flex xs12 sm6 md4>
                        <v-text-field
                            label="Code"
                            v-model="cit.code"
                            :readonly="onEdit"
                            :rules="codeRules">

                        </v-text-field>
                    </v-flex>
                    
                    <v-flex xs12 sm6 md8>
                        <v-text-field
                            label="Description"
                            v-model="cit.description">

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
                :items="citData"
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
                    Do you want to delete this Code: {{ selectedCit.code }}?
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
import codeRules from "@/rules/codeRules"
import Toast from "@/project-modules/toast"

let toast = new Toast();

export default {
    data() {
        return {
            valid: false,
            title: "Citizenship",
            cit: {},
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
            selectedCit: {},
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
            apiEndpoint: "api/citizenship"
        }
    },
    methods: {
        ...mapActions('citizenship', [
            'getAllCitizenship'
        ]),
        ...mapActions('user', [
            'decodeToken'
        ]),
        save() {
            this.isSaving = true;
            this.$axios.post(this.apiEndpoint, this.cit)
                .then(response => {
                    this.isSaving = false;
                    let { message, hasError } = response.data;

                    // Toast custom message
                    if(hasError) {
                        toast.error(message)
                    }else{
                        toast.success(message)
                    }
                    // Update List
                    this.getAllCitizenship();
                    this.cit = {}
                })
                .catch(err => {
                    this.isSaving = false;
                });
        },
        edit(item){
            this.onEdit = true;
            this.cit = item;
        },
        update() {
            this.isUpdating = true
            this.$axios.put(this.apiEndpoint, this.cit)
                .then(response => {
                    this.isUpdating = false;
                    this.onEdit = false;

                    let { message, hasError } = response.data;

                    if(hasError) {
                        toast.error(message)
                    }else{
                        toast.success(message)
                    }

                    this.getAllCitizenship();

                    this.cit = {}
                })
                .catch(err => {
                    this.isUpdating = false;
                })
        },
        deleteInfo(item) {
            this.deleteDialog = true;
            this.selectedCit = item;
        },
        deleteConfirmed() {
            this.isDeleting = true;
            this.$axios.delete(`${this.apiEndpoint}/${this.selectedCit.id}`)
                .then(response => {
                    this.getAllCitizenship();

                    let { message, hasError } = response.data;

                    if(hasError) {
                        toast.error(message)
                    }else{
                        toast.success(message)
                    }

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
            this.cit = {}

            this.getAllCitizenship();
        }
        // getCompanyCode() {
        //     var user = getUserDetails();
        //     var decodedToken = this.$jwt.decode(user.token);
            
        //     this.companyCode = decodedToken.CompanyCode;
        // }
    },
    computed: {
        ...mapState('citizenship', {
            citData: state => state.cit,
            isLoading: state => state.loading
        }),
        ...mapState('user', {
            companyCode: state => state.companyCode
        })
    },
    created() {
        // decode token and company code
        //this.getCompanyCode();
        this.decodeToken();
        // get all citizenship
        this.getAllCitizenship();
    }
}
</script>
