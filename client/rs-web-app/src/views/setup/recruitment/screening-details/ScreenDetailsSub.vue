<template>
    <div>
        <v-container>
            <h1>{{title}}</h1>
            <v-layout row wrap>
                <v-flex xs12 sm12 md12>
                    <v-btn icon @click.prevent="$router.back()">
                        <v-icon>keyboard_backspace</v-icon>
                    </v-btn>    
                </v-flex>
                <v-flex>
                    <v-btn flat @click="addRow">
                        <v-icon>add</v-icon>
                        Add row
                    </v-btn>    
                </v-flex>
            </v-layout>
            <v-data-table
                :items="screenDetailsSubItem"
                class="elevation-1"
                hide-actions
                :headers="headers">
                <template slot="items" slot-scope="props">
                    <td>{{ props.item.screenOrder }}</td>
                    <td class="text-xs-left">
                        {{ props.item.screenTypeDesc }}
                        <v-btn icon @click="selectScreenOrder(props.item.screenOrder)">
                            <v-icon >search</v-icon>
                        </v-btn>
                    </td>
                    <td>
                        <v-btn icon
                            @click.prevent="deleteRow">
                            <v-icon color="red">delete</v-icon>
                        </v-btn>
                    </td>
                </template>
            </v-data-table>
            <v-btn color="success"
                @click.prevent="save">
                Save
            </v-btn>
            <!-- Designation Dialog -->
            <v-dialog
                v-model="screenTypeDialog"
                max-width="550"
                >
                <v-card v-if="screenTypeDialog">
                    <v-card-title class="headline">Screening Type</v-card-title>

                    <v-card-text>
                        <v-layout>
                            <v-data-table
                                :headers="screenTypeHeaders"
                                :items="screenTypeData"
                                class="elevation-1">

                                <template slot="items" slot-scope="props">
                                    <td>{{ props.item.screenTypeCode }}</td>
                                    <td class="text-xs-left">{{ props.item.screenTypeDesc }}</td>
                                    <td>
                                        <v-btn icon
                                            @click.prevent="selectScreenType(props.item)">
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
                            @click.prevent="screenTypeDialog = false">
                            Close
                        </v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-container>
        
    </div>
</template>
<script>
import _ from 'lodash'
import { mapState, mapActions } from 'vuex'
import Toast from "@/project-modules/toast"
let toast = new Toast();
export default {
    data() {
        return {
            title: 'Screening Details - Fill up',
            screenDetail: {},
            headers: [
                { text: 'Order', value: 'screenOrder', sortable: false },
                { text: 'Screening Type', value: 'screenOrderTypeCode', sortable: false },
                { text: '', value: 'actions', sortable: false}

            ],
            screenTypeHeaders: [
                { text: "Code", value: "screenTypeCode" },
                { text: "Description", value: "screenTypeDesc" },
                { text: '', value: 'actions'}
            ],
            selectedOrder: {},
            screenDetailsSubItem: [],
            defaultCount: 1,
            screenTypeDialog: false,
            apiEndpoint: 'api/screen-details'
        }
    },
    methods: {
        ...mapActions('screenType', [
            'getAllTypes'
        ]),
        getScreenDetail(id) {
            this.$axios.get(`${this.apiEndpoint}/${id}`)
                .then(response => {
                    this.screenDetail = response.data;
                    this.getScreenDetailSub(this.screenDetail.id);
                    
                })
                .catch(err => {
                    
                })
        },
        getScreenDetailSub(screenDetailId) {
            this.$axios.get(`${this.apiEndpoint}/sub/${screenDetailId}`)
                .then(response => {
                    this.screenDetailsSubItem = response.data;
                    this.defaultCount = response.data.length + 1;
                })
                .catch(err => {

                });
        },
        addRow() {
            //this.defaultCount++;
            // this.screenDetailsSubItem = []
             this.screenDetailsSubItem.push({ screenOrder: this.defaultCount++, screenTypeCode: '', screenTypeDesc: '' });
        },
        deleteRow() {
            this.screenDetailsSubItem.splice(this.defaultCount-2,1);
            this.defaultCount--;
        },
        selectScreenOrder(screenOrder) {
           this.selectedOrder = _.find(this.screenDetailsSubItem, ['screenOrder', screenOrder]);
           this.screenTypeDialog = true;
           this.getAllTypes();
        },
        selectScreenType(item) {
            // Condition
            // var isExists = _.find(this.screenDetailsSubItem, ['screenTypeCode', item.screenTypeCode]);
            // if(isExists) {
            //     alert('Exists!')
            // }
            this.selectedOrder.screenTypeDesc = item.screenTypeDesc;
            this.selectedOrder.screenTypeCode= item.screenTypeCode;
            this.screenTypeDialog = false;
        },
        save() {
            this.$axios.post(`${this.apiEndpoint}/sub`,{
                id: this.screenDetail.id,
                screenDetailsSubs: this.screenDetailsSubItem
            })
            .then(({data}) =>{
                let { message, hasError } = data;
                toast.show(message, hasError);
            })
            .catch(({response}) =>{
                let { message, hasError } = response.data;
                toast.show(message, hasError);
            })
        }
    },
    created() {
        this.getScreenDetail(this.$route.params.id);
    },
    computed: {
        ...mapState('screenType', {
            screenTypeData: state => state.stData
        })
    }
}
</script>
