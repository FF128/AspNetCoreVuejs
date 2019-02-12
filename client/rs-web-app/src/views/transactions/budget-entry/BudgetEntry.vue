<template>
  <div>
    <v-container>
      <h1>{{title}}</h1>
      <v-expansion-panel popout expand v-model="panel">
        <v-expansion-panel-content>
          <div slot="header">Transaction Header</div>
          <!-- <general-info></general-info> -->
          <v-form v-model="valid">
            <v-container>
              <v-layout row wrap>
                <v-flex xs12 sm6 md3>
                  <v-text-field label="Description" v-model="budgetEntry.description"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md3>
                  <v-text-field
                    label="Date Required"
                    type="date"
                    :rules="requiredRules"
                    v-model="budgetEntry.dateRequired"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md3>
                  <v-text-field
                    label="Duration From"
                    type="date"
                    :rules="requiredRules"
                    v-model="budgetEntry.durationFrom"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md3>
                  <v-text-field
                    label="Duration To"
                    type="date"
                    :rules="requiredRules"
                    v-model="budgetEntry.durationTo"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md3>
                  <v-text-field
                    v-model="budgetEntry.empStatus"
                    label="Employment Status"
                    :append-icon="'search'"
                    @click:append="searchEmploymentCode"
                    readonly
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md3>
                  <v-text-field
                    v-model="budgetEntry.reason"
                    label="Personnel Request Type"
                    :append-icon="'search'"
                    @click:append="searchPRTCode"
                    readonly
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-container>
          </v-form>
        </v-expansion-panel-content>
        <v-expansion-panel-content>
          <div slot="header">Transaction Details</div>
          <v-form v-model="valid">
            <v-container>
              <v-layout wrap>
                <v-flex xs12 sm12 md12>
                  <v-text-field
                    v-model="budgetEntry.database"
                    label="Company"
                    :append-icon="'search'"
                    @click:append="searchCompany"
                    readonly
                  ></v-text-field>
                </v-flex>
                <!-- Payroll Location -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.locId"
                    label="Payroll Location"
                    :append-icon="'search'"
                    @click:append="searchPayLoc"
                    readonly
                    :rules="requiredRules"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.locName" label="Description"></v-text-field>
                </v-flex>
                <!-- Department -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.depCode"
                    label="Department"
                    :append-icon="'search'"
                    @click:append="searchDepartment"
                    readonly
                    :rules="requiredRules"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.department" label="Description"></v-text-field>
                </v-flex>
                <!-- Location -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.locationCode"
                    label="Location"
                    :append-icon="'search'"
                    @click:append="searchLocation"
                    readonly
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.location" label="Description"></v-text-field>
                </v-flex>
                <!-- Group Code -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.grpCode"
                    label="Group Code"
                    :append-icon="'search'"
                    @click:append="searchEmploymentCode"
                    readonly
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.group" label="Description"></v-text-field>
                </v-flex>
                <!-- Division/Office -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.divisionCode"
                    label="Division/Office"
                    :append-icon="'search'"
                    @click:append="searchDivision"
                    readonly
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.division" label="Description"></v-text-field>
                </v-flex>

                <!-- Area -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.areaCode"
                    label="Area"
                    :append-icon="'search'"
                    @click:append="searchArea"
                    readonly
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.area" label="Description"></v-text-field>
                </v-flex>
                <!-- Branch -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.braCode"
                    label="Branch"
                    :append-icon="'search'"
                    @click:append="searchBranch"
                    readonly
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.branch" label="Description"></v-text-field>
                </v-flex>

                <!-- Unit -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.unitCode"
                    label="Unit"
                    :append-icon="'search'"
                    @click:append="searchUnit"
                    readonly
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.unit" label="Description"></v-text-field>
                </v-flex>
                <!-- Section -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.sectionCode"
                    label="Section"
                    :append-icon="'search'"
                    @click:append="searchSection"
                    readonly
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.section" label="Description"></v-text-field>
                </v-flex>
                <!-- Rank -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.rankCode"
                    label="Rank"
                    :append-icon="'search'"
                    @click:append="searchRank"
                    readonly
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.rank" label="Description"></v-text-field>
                </v-flex>

                <!-- Pay House -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.payHouseCode"
                    label="Pay House"
                    :append-icon="'search'"
                    @click:append="searchPayHouse"
                    readonly
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.payHouse" label="Description"></v-text-field>
                </v-flex>

                <!-- Region -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.regionCode"
                    label="Region"
                    :append-icon="'search'"
                    @click:append="searchRegion"
                    readonly
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.region" label="Description"></v-text-field>
                </v-flex>

                <!-- Levels of Employee -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.loeCode"
                    label="Levels of Employee"
                    :append-icon="'search'"
                    @click:append="searchLOE"
                    readonly
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.loe" label="Description"></v-text-field>
                </v-flex>

                <!-- Project Code -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.projectCode"
                    label="Project Code"
                    :append-icon="'search'"
                    @click:append="searchProjectCode"
                    readonly
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.project" label="Description"></v-text-field>
                </v-flex>

                <!-- Designation -->
                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.designationCode"
                    label="Designation"
                    :append-icon="'search'"
                    @click:append="searchDesignation"
                    readonly
                    :rules="requiredRules"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md6>
                  <v-text-field v-model="budgetEntry.designation" label="Description"></v-text-field>
                </v-flex>

                <v-flex xs12 sm6 md6>
                  <v-text-field
                    v-model="budgetEntry.agreedSalary"
                    label="Budgeted Salary"
                    :rules="requiredRules"
                  ></v-text-field>
                  <v-text-field
                    v-model="budgetEntry.quantity"
                    label="Quantity"
                    :rules="requiredRules"
                  ></v-text-field>
                  <v-text-field v-model="budgetEntry.remarks" label="Remarks"></v-text-field>
                </v-flex>
                <v-flex xs12 sm12 md12>
                  <v-text-field
                    label="New attachment"
                    @click="pickFile"
                    v-model="fileName"
                    prepend-icon="attach_file"
                  ></v-text-field>
                  <input type="file" style="display: none" ref="file" @change="onFilePicked">
                  <v-btn @click.prevent="add">New</v-btn>
                  <v-data-table hide-actions class="elevation-1" :items="attachments">
                    <template slot="items" slot-scope="props">
                      <td>{{ props.item.name }}</td>
                      <td>
                        <v-btn icon @click.prevent="attachments.splice(props.index, 1)">
                          <v-icon color="red">delete</v-icon>
                        </v-btn>
                      </td>
                    </template>
                  </v-data-table>
                </v-flex>
              </v-layout>
            </v-container>
          </v-form>
        </v-expansion-panel-content>
      </v-expansion-panel>
      <v-layout>
        <!-- <v-btn
                    color="success"
                    :disabled="!valid"
                    :loading="isSaving"
                    @click.prevent="save">
                    Save
        </v-btn>-->
        <v-btn @click.prevent="addDetails" :disabled="!valid">Add</v-btn>
      </v-layout>
      <v-data-table
        :headers="bedHeaders"
        :items="budgetEntryDetails"
        class="elevation-1"
        hide-actions
        v-if="budgetEntryDetails[0] != null"
      >
        <template slot="items" slot-scope="props">
          <td>
            <v-btn icon @click.prevent="budgetEntryDetails.splice(props.index, 1)">
              <v-icon color="red">delete</v-icon>
            </v-btn>
          </td>
          <td>{{ props.item.locName }}</td>
          <td>{{ props.item.department }}</td>
          <td>{{ props.item.location }}</td>
          <td>{{ props.item.group }}</td>
          <td>{{ props.item.division }}</td>
          <td>{{ props.item.branch }}</td>
          <td>{{ props.item.unit }}</td>
          <td>{{ props.item.section }}</td>
          <td>{{ props.item.designation }}</td>
          <td>{{ props.item.agreedSalary }}</td>
          <td>{{ props.item.quantity }}</td>
        </template>
      </v-data-table>
      <v-layout>
        <!-- <v-btn
                    color="success"
                    v-if="budgetEntryDetails.length !== 0"
                    :loading="isSaving"
                    @click.prevent="save">
                    Submit
        </v-btn>-->
        <v-btn
          @click="processForm"
          color="success"
          v-if="budgetEntryDetails.length !== 0"
          :loading="isSaving"
        >Submit</v-btn>
      </v-layout>
    </v-container>

    <!-- Employee Status Dialog -->
    <look-up-dialog
      :dialog="employmentStatusDialog"
      :title="'Employee Status'"
      v-if="employmentStatusDialog"
    >
      <template slot="table">
        <v-data-table :headers="empStatusHeaders" :items="employeeStatusFiles" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.code }}</td>
            <td class="text-xs-left">{{ props.item.description }}</td>
            <td>
              <v-btn icon @click.prevent="selectEmpStatus(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="employmentStatusDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Personnel Req Type -->
    <look-up-dialog :dialog="prtDialog" :title="'Personnel Request Type'" v-if="prtDialog">
      <template slot="table">
        <v-data-table :headers="prtHeaders" :items="prtData" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.personnelReqTypeCode }}</td>
            <td class="text-xs-left">{{ props.item.personnelReqTypeDesc }}</td>
            <td>
              <v-btn icon @click.prevent="selectPRT(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="prtDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Pay Location -->
    <look-up-dialog :dialog="payLocDialog" :title="'Payroll Location'" v-if="payLocDialog">
      <template slot="table">
        <v-data-table :headers="payLocHeaders" :items="payLocations" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.locId }}</td>
            <td class="text-xs-left">{{ props.item.locName }}</td>
            <td>
              <v-btn icon @click.prevent="selectLoc(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="payLocDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Department -->
    <look-up-dialog :dialog="depDialog" :title="'Departments'" v-if="depDialog">
      <template slot="table">
        <v-data-table :headers="depHeaders" :items="departments" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.departmentCode }}</td>
            <td class="text-xs-left">{{ props.item.departmentDesc }}</td>
            <td>
              <v-btn icon @click.prevent="selectDepartment(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="depDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Location -->
    <look-up-dialog :dialog="locDialog" :title="'Location'" v-if="locDialog">
      <template slot="table">
        <v-data-table :headers="locHeaders" :items="locations" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.locationCode }}</td>
            <td class="text-xs-left">{{ props.item.locationDesc }}</td>
            <td>
              <v-btn icon @click.prevent="selectLocation(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="locDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Division -->
    <look-up-dialog :dialog="divDialog" :title="'Division'" v-if="divDialog">
      <template slot="table">
        <v-data-table :headers="divHeaders" :items="divisions" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.divisionCode }}</td>
            <td class="text-xs-left">{{ props.item.divisionDesc }}</td>
            <td>
              <v-btn icon @click.prevent="selectDivision(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="divDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Area -->
    <look-up-dialog :dialog="areaDialog" :title="'Area'" v-if="areaDialog">
      <template slot="table">
        <v-data-table :headers="areaHeaders" :items="areas" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.areaCode }}</td>
            <td class="text-xs-left">{{ props.item.areaDesc }}</td>
            <td>
              <v-btn icon @click.prevent="selectArea(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="areaDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Branch -->
    <look-up-dialog :dialog="branchDialog" :title="'Branch'" v-if="branchDialog">
      <template slot="table">
        <v-data-table :headers="branchHeaders" :items="branches" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.branchCode }}</td>
            <td class="text-xs-left">{{ props.item.branchDesc }}</td>
            <td>
              <v-btn icon @click.prevent="selectBranch(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="branchDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Unit -->
    <look-up-dialog :dialog="unitDialog" :title="'Unit'" v-if="unitDialog">
      <template slot="table">
        <v-data-table :headers="unitHeaders" :items="units" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.unitCode }}</td>
            <td class="text-xs-left">{{ props.item.unitDesc }}</td>
            <td>
              <v-btn icon @click.prevent="selectUnit(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="unitDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Section -->
    <look-up-dialog :dialog="sectionDialog" :title="'Section'" v-if="sectionDialog">
      <template slot="table">
        <v-data-table :headers="sectionHeaders" :items="sections" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.sectionCode }}</td>
            <td class="text-xs-left">{{ props.item.sectionDesc }}</td>
            <td>
              <v-btn icon @click.prevent="selectSection(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="sectionDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Rank -->
    <look-up-dialog :dialog="rankDialog" :title="'Rank'" v-if="rankDialog">
      <template slot="table">
        <v-data-table :headers="rankHeaders" :items="ranks" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.rankCode }}</td>
            <td class="text-xs-left">{{ props.item.rankDesc }}</td>
            <td>
              <v-btn icon @click.prevent="selectRank(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="rankDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Pay House -->
    <look-up-dialog :dialog="phDialog" :title="'Pay House'" v-if="phDialog">
      <template slot="table">
        <v-data-table :headers="phHeaders" :items="phData" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.payHouseCode }}</td>
            <td class="text-xs-left">{{ props.item.payHouseDesc }}</td>
            <td>
              <v-btn icon @click.prevent="selectPayHouse(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="phDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Region -->
    <look-up-dialog :dialog="regionDialog" :title="'Region'" v-if="regionDialog">
      <template slot="table">
        <v-data-table :headers="regionHeaders" :items="regions" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.regionCode }}</td>
            <td class="text-xs-left">{{ props.item.regionDesc }}</td>
            <td>
              <v-btn icon @click.prevent="selectRegion(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="regionDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Levels of Employee -->
    <look-up-dialog :dialog="loeDialog" :title="'Levels of Employee'" v-if="loeDialog">
      <template slot="table">
        <v-data-table :headers="loeHeaders" :items="loeData" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.loeCode }}</td>
            <td class="text-xs-left">{{ props.item.loeDesc }}</td>
            <td>
              <v-btn icon @click.prevent="selectLOE(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="loeDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Project Code -->
    <look-up-dialog :dialog="projDialog" :title="'Project Code'" v-if="projDialog">
      <template slot="table">
        <v-data-table :headers="projHeaders" :items="projData" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.projectCode }}</td>
            <td class="text-xs-left">{{ props.item.projectDesc }}</td>
            <td>
              <v-btn icon @click.prevent="selectProjectCode(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="projDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Designation -->
    <look-up-dialog :dialog="desigDialog" :title="'Designation'" v-if="desigDialog">
      <template slot="table">
        <v-data-table :headers="desigHeaders" :items="designationFiles" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.designationFileCode }}</td>
            <td class="text-xs-left">{{ props.item.designationFileDesc }}</td>
            <td>
              <v-btn icon @click.prevent="selectDesignation(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="desigDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
    <!-- Multi Company -->
    <look-up-dialog :dialog="compDialog" :title="'Company'" v-if="compDialog">
      <template slot="table">
        <v-data-table :headers="compHeaders" :items="compData" class="elevation-1">
          <template slot="items" slot-scope="props">
            <td v-if="props.item.isActive">{{ props.item.databaseCode }}</td>
            <td class="text-xs-left" v-if="props.item.isActive">{{ props.item.databaseDesc }}</td>
            <td v-if="props.item.isActive">
              <v-btn icon @click.prevent="selectCompany(props.item)">
                <v-icon>mdi-magnify-plus</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <template slot="card-actions">
        <v-btn flat="flat" @click.prevent="compDialog = false">Close</v-btn>
      </template>
    </look-up-dialog>
  </div>
</template>
<script>
import { mapState, mapActions } from "vuex";
import LookUpDialog from "@/components/LookUpDialog";
import requiredRules from "@/rules/requiredRules";
import Toast from "@/project-modules/toast";
import _ from "lodash";
import moment from "moment";

let toast = new Toast();
export default {
  components: {
    LookUpDialog
  },
  data() {
    return {
      title: "Budget Entry",
      panel: [true, true],
      employmentStatusDialog: false,
      requiredRules,
      valid: false,
      prtDialog: false,
      payLocDialog: false,
      depDialog: false,
      locDialog: false,
      divDialog: false,
      areaDialog: false,
      branchDialog: false,
      unitDialog: false,
      sectionDialog: false,
      rankDialog: false,
      phDialog: false,
      regionDialog: false,
      loeDialog: false,
      projDialog: false,
      desigDialog: false,
      compDialog: false,
      budgetEntry: this.newBudgetEntry(),
      budgetEntryDetails: [],
      fileName: "",
      fileUrl: "",
      file: {},
      isSaving: false,
      attachments: [],
      bedHeaders: [
        { text: "", value: "actions" },
        {
          text: "Payroll Location",
          align: "left",
          sortable: false,
          value: "locName"
        },
        {
          text: "Department",
          align: "left",
          sortable: false,
          value: "department"
        },
        {
          text: "Location",
          align: "left",
          sortable: false,
          value: "location"
        },
        {
          text: "Group",
          align: "left",
          sortable: false,
          value: "group"
        },
        {
          text: "Division",
          align: "left",
          sortable: false,
          value: "division"
        },
        {
          text: "Branch",
          align: "left",
          sortable: false,
          value: "branch"
        },
        {
          text: "Unit",
          align: "left",
          sortable: false,
          value: "unit"
        },
        {
          text: "Section",
          align: "left",
          sortable: false,
          value: "section"
        },
        {
          text: "Designation",
          align: "left",
          sortable: false,
          value: "designation"
        },
        {
          text: "Agreed Salary",
          align: "left",
          sortable: false,
          value: "agreedSalary"
        },
        {
          text: "Quantity",
          align: "left",
          sortable: false,
          value: "quantity"
        }
      ],
      apiEndpoint: "api/budget-entry"
    };
  },
  computed: {
    ...mapState("employeeStatusFile", {
      employeeStatusFiles: state => state.employeeStatusFiles,
      empStatusHeaders: state => state.headers
    }),
    ...mapState("personnelReqType", {
      prtData: state => state.prtData,
      prtHeaders: state => state.headers
    }),
    ...mapState("payLocation", {
      payLocations: state => state.payLocations,
      payLocHeaders: state => state.headers
    }),
    ...mapState("department", {
      departments: state => state.departments,
      depHeaders: state => state.headers
    }),
    ...mapState("location", {
      locations: state => state.locations,
      locHeaders: state => state.headers
    }),
    ...mapState("division", {
      divisions: state => state.divisions,
      divHeaders: state => state.headers
    }),
    ...mapState("area", {
      areas: state => state.areas,
      areaHeaders: state => state.headers
    }),
    ...mapState("branch", {
      branches: state => state.branches,
      branchHeaders: state => state.headers
    }),
    ...mapState("unit", {
      units: state => state.units,
      unitHeaders: state => state.headers
    }),
    ...mapState("section", {
      sections: state => state.sections,
      sectionHeaders: state => state.headers
    }),
    ...mapState("rank", {
      ranks: state => state.ranks,
      rankHeaders: state => state.headers
    }),
    ...mapState("payHouse", {
      phData: state => state.payHouseData,
      phHeaders: state => state.headers
    }),
    ...mapState("region", {
      regions: state => state.regions,
      regionHeaders: state => state.headers
    }),
    ...mapState("loe", {
      loeData: state => state.loeData,
      loeHeaders: state => state.headers
    }),
    ...mapState("projectCode", {
      projData: state => state.projectCodeData,
      projHeaders: state => state.headers
    }),
    ...mapState("designationFile", {
      designationFiles: state => state.designationFiles,
      desigHeaders: state => state.headers
    }),
    ...mapState("multiCompany", {
      compData: state => state.multiCompData,
      compHeaders: state => state.headers
    })
  },
  methods: {
    pickFile() {
      this.$refs.file.click();
    },
    onFilePicked(e) {
      const files = e.target.files;
      if (files[0] !== undefined) {
        this.fileName = files[0].name;
        if (this.fileName.lastIndexOf(".") <= 0) {
          return;
        }
        const fr = new FileReader();
        fr.readAsDataURL(files[0]);
        fr.addEventListener("load", () => {
          this.fileUrl = fr.result;
          this.file = files[0]; // this is an image file that can be sent to server...
          const newArray = Object.assign({}, this.file, this.file);
          this.attachments.push(this.file);
        });
      } else {
        this.fileName = "";
        this.file = "";
        this.fileUrl = "";
      }
    },
    ...mapActions("employeeStatusFile", ["getAllEmployeeStatusFiles"]),
    ...mapActions("personnelReqType", ["getAllPRTData"]),
    ...mapActions("payLocation", ["getAllPayLocations"]),
    ...mapActions("department", ["getAllDepartments"]),
    ...mapActions("location", ["getAllLocations"]),
    ...mapActions("division", ["getAllDivisions"]),
    ...mapActions("area", ["getAllAreas"]),
    ...mapActions("branch", ["getAllBranch"]),
    ...mapActions("unit", ["getAllUnits"]),
    ...mapActions("section", ["getAllSections"]),
    ...mapActions("rank", ["getAllRanks"]),
    ...mapActions("payHouse", ["getAllPayHouseData"]),
    ...mapActions("region", ["getAllRegions"]),
    ...mapActions("loe", ["getAllLOEData"]),
    ...mapActions("projectCode", ["getAllProjectCode"]),
    ...mapActions("designationFile", ["getAllDesignationFiles"]),
    ...mapActions("multiCompany", ["getAllMultiCompData"]),
    searchEmploymentCode() {
      this.employmentStatusDialog = true;
      this.getAllEmployeeStatusFiles();
    },
    selectEmpStatus(item) {
      this.budgetEntry.empStatus = item.code;
      this.employmentStatusDialog = false;
    },
    searchPRTCode() {
      this.prtDialog = true;
      this.getAllPRTData();
    },
    selectPRT(item) {
      this.budgetEntry.reason = item.personnelReqTypeCode;
      this.prtDialog = false;
    },
    searchPayLoc() {
      this.payLocDialog = true;
      this.getAllPayLocations();
    },
    selectLoc(item) {
      this.budgetEntry.locId = item.locId;
      this.budgetEntry.locName = item.locName;
      this.payLocDialog = false;
    },
    searchDepartment() {
      this.depDialog = true;
      this.getAllDepartments();
    },
    selectDepartment(item) {
      this.budgetEntry.depCode = item.departmentCode;
      this.budgetEntry.department = item.departmentDesc;
      this.depDialog = false;
    },
    searchLocation() {
      this.locDialog = true;
      this.getAllLocations();
    },
    selectLocation(item) {
      this.budgetEntry.locationCode = item.locationCode;
      this.budgetEntry.location = item.locationDesc;
      this.locDialog = false;
    },
    searchDivision() {
      this.divDialog = true;
      this.getAllDivisions();
    },
    selectDivision(item) {
      this.budgetEntry.divisionCode = item.divisionCode;
      this.budgetEntry.division = item.divisionDesc;
      this.divDialog = false;
    },
    searchArea() {
      this.areaDialog = true;
      this.getAllAreas();
    },
    selectArea(item) {
      this.budgetEntry.areaCode = item.areaCode;
      this.budgetEntry.area = item.areaDesc;
      this.areaDialog = false;
    },
    searchBranch() {
      this.branchDialog = true;
      this.getAllBranch();
    },
    selectBranch(item) {
      this.budgetEntry.braCode = item.branchCode;
      this.budgetEntry.branch = item.branchDesc;
      this.branchDialog = false;
    },
    searchUnit() {
      this.unitDialog = true;
      this.getAllUnits();
    },
    selectUnit(item) {
      this.budgetEntry.unitCode = item.unitCode;
      this.budgetEntry.unit = item.unitDesc;
      this.unitDialog = false;
    },
    searchSection() {
      this.sectionDialog = true;
      this.getAllSections();
    },
    selectSection(item) {
      this.budgetEntry.sectionCode = item.sectionCode;
      this.budgetEntry.section = item.sectionDesc;
      this.sectionDialog = false;
    },
    searchRank() {
      this.rankDialog = true;
      this.getAllRanks();
    },
    selectRank(item) {
      this.budgetEntry.rankCode = item.rankCode;
      this.budgetEntry.rank = item.rankDesc;
      this.rankDialog = false;
    },
    searchPayHouse() {
      this.phDialog = true;
      this.getAllPayHouseData();
    },
    selectPayHouse(item) {
      this.budgetEntry.payHouseCode = item.payHouseCode;
      this.budgetEntry.payHouse = item.payHouseDesc;
      this.phDialog = false;
    },
    searchRegion() {
      this.regionDialog = true;
      this.getAllRegions();
    },
    selectRegion(item) {
      this.budgetEntry.regionCode = item.regionCode;
      this.budgetEntry.region = item.regionDesc;
      this.regionDialog = false;
    },
    searchLOE() {
      this.loeDialog = true;
      this.getAllLOEData();
    },
    selectLOE(item) {
      this.budgetEntry.loeCode = item.loeCode;
      this.budgetEntry.loe = item.loeDesc;
      this.loeDialog = false;
    },
    searchProjectCode() {
      this.projDialog = true;
      this.getAllProjectCode();
    },
    selectProjectCode(item) {
      this.budgetEntry.projectCode = item.projectCode;
      this.budgetEntry.project = item.projectDesc;
      this.projDialog = false;
    },
    searchDesignation() {
      this.desigDialog = true;
      this.getAllDesignationFiles();
    },
    selectDesignation(item) {
      this.budgetEntry.designationCode = item.designationFileCode;
      this.budgetEntry.designation = item.designationFileDesc;
      this.desigDialog = false;
    },
    searchCompany() {
      this.compDialog = true;
      this.getAllMultiCompData();
    },
    selectCompany(item) {
      this.budgetEntry.databaseCode = item.databaseCode;
      this.budgetEntry.database = item.databaseDesc;
      this.compDialog = false;
    },
    add() {
      this.fileName = "";
      this.fileUrl = "";
      this.file = "";
    },
    addDetails() {
      var item = _.find(this.budgetEntryDetails, {
        designationCode: this.budgetEntry.designationCode,
        locId: this.budgetEntry.locId,
        depCode: this.budgetEntry.depCode,
        divisionCode: this.budgetEntry.divisionCode,
        braCode: this.budgetEntry.braCode,
        unitCode: this.budgetEntry.unitCode,
        sectionCode: this.budgetEntry.sectionCode
      });
      var divCode = _.find(this.budgetEntryDetails, {
        divisionCode: this.budgetEntry.divisionCode
      });
      if (
        typeof item == "undefined" &&
        typeof this.budgetEntry.divisionCode != "" &&
        typeof this.budgetEntry.braCode != "" &&
        typeof this.budgetEntry.unitCode != "" &&
        typeof this.budgetEntry.sectionCode != ""
      ) {
        const newArray = Object.assign({}, this.budgetEntry, this.budgetEntry);
        this.budgetEntryDetails.push(new Object(newArray));
        //return;
      } else {
        item.quantity =
          parseInt(item.quantity) + parseInt(this.budgetEntry.quantity);
        item.agreedSalary = this.budgetEntry.agreedSalary;
        item.location = this.budgetEntry.location;
        item.locationCode = this.budgetEntry.locationCode;
        item.grpCode = this.budgetEntry.grpCode;
        item.group = this.budgetEntry.group;
        //return;
      }
    },
    newBudgetEntry() {
      return {
        durationFrom: moment(new Date()).format("YYYY-MM-DD"),
        durationTo: moment(new Date()).format("YYYY-MM-DD"),
        dateRequired: moment(new Date()).format("YYYY-MM-DD"),
        depCode: "",
        department: "",
        divisionCode: "",
        division: "",
        braCode: "",
        branch: "",
        unitCode: "",
        unit: "",
        sectionCode: "",
        section: "",
        positionCode: "",
        designation: "",
        quantity: 0,
        status: "",
        approver: "",
        locId: 0,
        locName: "",
        agreedSalary: 0,
        databaseCode: "",
        database: "",
        locCode: "",
        location: "",
        grpCode: "",
        group: "",
        approverRemarks: "",
        areaCode: "",
        area: "",
        projectCode: "",
        project: "",
        rankCode: "",
        rank: "",
        payHouseCode: "",
        payHouse: "",
        regionCode: "",
        region: "",
        employeeCategoryCode: ""
      };
    },
    save() {
      this.isSaving = true;
      this.$axios
        .post(`${this.apiEndpoint}`, {
          budgetEntryDto: this.budgetEntry,
          budgetEntryDetails: this.budgetEntryDetails
        })
        .then(({ data }) => {
          toast.show(data);

          this.isSaving = false;
          this.budgetEntry = this.newBudgetEntry();
          this.budgetEntryDetails = [];
        })
        .catch(({ response }) => {
          this.isSaving = false;
          toast.show(response.data);
        });
    },
    processForm() {
      var bodyFormData = new FormData();
      bodyFormData.append("budgetEntry", JSON.stringify(this.budgetEntry));
      bodyFormData.append(
        "budgetEntryDetails",
        JSON.stringify(this.budgetEntryDetails)
      );

      this.attachments.forEach(function(attachment) {
        bodyFormData.append("files", attachment);
      });

      this.$axios
        .post(`${this.apiEndpoint}`, bodyFormData)
        .then(({ data }) => {
          toast.show(data);
          this.budgetEntry = {};
          this.budgetEntryDetails = [];
        })
        .catch(({ response }) => {
          toast.show(response.data);
        });
    },
    initializeBudgetDetails() {
      return {
        id: 0,
        transactionNo: "",
        depCode: "",
        department: "",
        divisionCode: "",
        division: "",
        braCode: "",
        branch: "",
        unitCode: "",
        unit: "",
        sectionCode: "",
        section: "",
        positionCode: "",
        designation: "",
        quantity: 0,
        status: "",
        approver: "",
        locId: 0,
        locName: "",
        agreedSalary: 0,
        databaseCode: "",
        database: "",
        locCode: "",
        location: "",
        grpCode: "",
        group: "",
        approverRemarks: "",
        areaCode: "",
        area: "",
        projectCode: "",
        project: "",
        rankCode: "",
        rank: "",
        payHouseCode: "",
        payHouse: "",
        regionCode: "",
        region: "",
        employeeCategoryCode: ""
      };
    }
  },
  created() {}
};
</script>
