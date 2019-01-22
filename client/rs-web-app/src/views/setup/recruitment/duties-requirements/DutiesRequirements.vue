<template>
  <div>
    <v-form>
      <v-container>
        <h1>{{title}}</h1>
        <v-layout row wrap>
          <v-text-field
              label="Designation"
              v-model="selectedDesignation"
              :append-icon="'mdi-account-search'"
              @click:append="searchDesignation"
              readonly>
          </v-text-field>
        </v-layout>
      </v-container>
    </v-form>
     <!-- Designation Dialog -->
    <v-dialog
        v-model="designationDialog"
        max-width="600">
        <v-card>
            <v-card-title class="headline">List of Designations</v-card-title>

            <v-card-text>
                <v-layout>
                      <v-data-table
                        :headers="designationHeaders"
                        :items="designationFileData"
                        class="elevation-1">

                        <template slot="items" slot-scope="props">
                            <td>{{ props.item.code }}</td>
                            <td class="text-xs-left">{{ props.item.description }}</td>
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


    <v-container>
      <h3>Duties and Responsibilities</h3>
      <v-data-table
        :items="duties"
        class="elevation-1"
        hide-actions
        select-all
        v-model="selectedDuties"
        item-key="dutiesResponsibilitiesCode"
        :headers="dutiesHeaders"
      >
        <template slot="items" slot-scope="props">
          <td>
            <v-checkbox
              v-model="props.selected"
              primary
              hide-details
            ></v-checkbox>
          </td>
          <td>{{ props.item.dutiesResponsibilitiesCode }}</td>
          <td class="text-xs-left">{{ props.item.dutiesResponsibilitiesDesc }}</td>
        </template>
      </v-data-table>
    </v-container>
    <!-- Job Requirements -->
    <v-container>
      <h3>Job Requirements</h3>
      <v-data-table
        :items="jobReqData"
        class="elevation-1"
        v-model="selectedJobReq"      
        :headers="jobReqHeaders"
        hide-actions
        select-all
        item-key="jobReqCode"
      >
        <template slot="items" slot-scope="props">
          <td>
            <v-checkbox
              v-model="props.selected"
              primary
              hide-details
            ></v-checkbox>
          </td>
          <td>{{ props.item.jobReqCode }}</td>
          <td class="text-xs">{{ props.item.jobReqDesc }}</td>
        </template>
      </v-data-table>
    </v-container>
    <v-container>
       <v-flex xs12 sm12 md12>
        <v-btn @click.prevent="save" :loading="isSaving" color="success" v-if="!onEdit">
          Save
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
    </v-container>

    <!-- Main table -->
    <v-container>
        <v-data-table
            :headers="designationDutiesReqHeaders"
            :items="dutiesReqData"
            class="elevation-1">

            <template slot="items" slot-scope="props">
                <td class="text-xs-left">{{ props.item.designationCode }}</td>
                <td class="text-xs-left">
                   <!-- <v-list dense expand>
                    <template>
                      <v-subheader>
                        • Test asd sd kljadkf a asfasdfdf asd?
                      </v-subheader>
                    </template>
                    <v-divider></v-divider>
                  </v-list> -->
                  <template v-for="(item, key) in props.item.dutiesRes">
                     <p v-if="item" v-bind:key="key">• {{ item }}</p>
                  </template>
                 
                </td>
                <td class="text-xs-left">
                    <template v-for="(item, key) in props.item.jobReq">
                      <p v-if="item" v-bind:key="key">• {{ item }}</p>
                    </template>
                </td>
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
                    Do you want to delete this Code: {{ selectedData.designationCode }}?
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
  import { mapState, mapActions } from "vuex"
  let toast = new Toast();
  export default {
    data () {
      return {
        title: 'Designation Duties and Requirements',
        designation: {},
        designationDutiesReqData: [],
        selectedDesignation: '',
        selectedDuties: [],
        selectedJobReq: [],
        onEdit: false,
        isSaving: false,
        isUpdating: false,
        dutiesHeaders: [
          { text: 'Code', value: 'dutiesResponsibilitiesCode'},
          { text: 'Description', value: 'dutiesResponsibilitiesDesc' }
        ],
        jobReqHeaders: [
          { text: 'Code', value: 'jobReqCode'},
          { text: 'Description', value: 'jobReqDesc'} 
        ],
        designationHeaders: [
          {
              text: 'Code',
              align: 'left',
              sortable: false,
              value: 'code'
          },
          { text: 'Description', value: 'description', align: 'left' },
          { text: '', value: 'actions' }
        ],
        designationDialog: false,
        designationDutiesReqHeaders: [
          { text: 'Designation', value: 'designation', width: '2px' },
          { text: 'Duties and Responsibilities', value :'dutiesAndRes', sortable: false },
          { text: 'Job Requirements', value: 'jobReq', sortable: false },
          { text: '', value: 'actions', sortable: false}
        ],
        deleteDialog: false,
        selectedData: {},
        apiEndpoint: 'api/desig-duties-req'
      }
      
    },
    methods: {
      ...mapActions('dutiesReq',[
        'getAllDutiesReqData'
      ]),
      ...mapActions('designationFile', [
        'getAllDesignationFiles'
      ]),
      ...mapActions('duties', [
        'getAllDuties'
      ]),
      ...mapActions('jobReq', [
        'getAllJobReqData'
      ]),
      searchDesignation() {
        this.designationDialog = true;
        this.getAllDesignationFiles();
      },
      selectDesignation(item) {
        this.selectedDesignation = item.description;
        this.designation.code = item.code;
        this.designationDialog = false;
      },
      save() {
        this.isSaving = true;
        this.$axios.post(this.apiEndpoint, {
          designationCode: this.designation.code,
          designationDutiesResponsibilities: this.selectedDuties,
          designationDutiesJobReqs: this.selectedJobReq
        })
        .then(({ data }) => {
          this.isSaving = false;
          let { message, hasError } = data;
          this.getAllDutiesReqData();
          toast.show(message, hasError)
        })
        .catch(({response }) => {
          this.isSaving = false;
          let { message, hasError } = response.data;
          toast.show(message, hasError);
        })
      },
      edit(item) {
        this.selectedDuties = item.dutiesAndResponsibilities;
        this.selectedJobReq = item.jobReqs;
        this.designation.code = item.designationCode;
        this.onEdit = true;
      },
      update() {
        this.isUpdating = true;
        this.$axios.put(this.apiEndpoint, {
          designationCode: this.designation.code,
          designationDutiesResponsibilities: this.selectedDuties,
          designationDutiesJobReqs: this.selectedJobReq
        })
        .then(({ data }) => {
          this.isUpdating = false;
          let { message, hasError } = data;
          this.getAllDutiesReqData();
          toast.show(message, hasError)
        })
        .catch(({response }) => {
          this.isSaving = false;
          let { message, hasError } = response.data;
          toast.show(message, hasError);
        })
      },
      deleteInfo(item) {
        this.deleteDialog = true;
        this.selectedData = item;
      },
      deleteConfirmed() {
        this.$axios.delete(`${this.apiEndpoint}/${this.selectedData.designationCode}`)
          .then(({ data }) => {
            this.deleteDialog = false;
            let { message, hasError} = data;
            this.getAllDutiesReqData();
            toast.show(message, hasError);
          })
          .catch(( { response }) => {
            let { message, hasError} = response.data;
            toast.show(message, hasError);

            this.deleteDialog = false;
          })
      },
      cancel() {
        this.getAllDutiesReqData();
        this.onEdit = false;

        this.selectedDuties =[]
        this.selectedJobReq =[]
      }
    },
    computed: {
      ...mapState('dutiesReq', {
        dutiesReqData: state => state.dutiesReqData
      }),
      ...mapState('designationFile', {
          designationFileData: state => state.designationFiles,
          isLoading: state => state.loading
      }),
      ...mapState('duties', {
        duties: state => state.duties
      }),
      ...mapState('jobReq', {
        jobReqData: state => state.jobReqData
      })
    },
    created() {
      this.getAllDuties();
      this.getAllJobReqData();
      this.getAllDutiesReqData();
    }
  }
</script>