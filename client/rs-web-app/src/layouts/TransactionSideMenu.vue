<template>
    <v-list class="grey lighten-4">
        <v-list-group
                prepend-icon="settings"
                no-action>
            <v-list-tile slot="activator">
                <v-list-tile-title>Summary</v-list-tile-title>
            </v-list-tile>
            <v-tooltip right>
                <v-list-tile @click="$router.push('/company-info')" slot="activator">
                    <v-list-tile-title>Transaction Summary</v-list-tile-title>
                </v-list-tile>
                <span>Transaction Summary</span>
            </v-tooltip>
            <v-tooltip right>
                <v-list-tile @click="$router.push('/company-info')" slot="activator">
                    <v-list-tile-title>Openings Summary</v-list-tile-title>
                </v-list-tile>
                <span>Openings Summary</span>
            </v-tooltip>
           
        </v-list-group>

        <v-list-group
                prepend-icon="settings"
                no-action>
            <v-list-tile slot="activator">
                <v-list-tile-title>Budget</v-list-tile-title>
            </v-list-tile>
            <v-tooltip right>
                <v-list-tile @click="$router.push('/budget-entry')" slot="activator">
                    <v-list-tile-title>Budget Entry</v-list-tile-title>
                </v-list-tile>
                <span>Budget Entry</span>
            </v-tooltip>
            <v-tooltip right>
                <v-list-tile @click="$router.push('/budget-entry-approval')" slot="activator">
                    <v-list-tile-title>Budget Entry Approval</v-list-tile-title>
                </v-list-tile>
                <span>Budget Entry Approval</span>
            </v-tooltip>
            <v-tooltip right>
                <v-list-tile @click="$router.push('/returned-budget-entry')" slot="activator">
                    <v-list-tile-title>Returned Budget Entry</v-list-tile-title>
                </v-list-tile>
                <span>Returned Budget Entry</span>
            </v-tooltip>
             <v-tooltip right>
                <v-list-tile @click="$router.push('/budget-history')" slot="activator">
                    <v-list-tile-title>Budget History</v-list-tile-title>
                </v-list-tile>
                <span>Budget History</span>
            </v-tooltip>
        </v-list-group>
        <!-- Personnel Requisition -->
        <v-list-group
                prepend-icon="settings"
                no-action>
            <v-list-tile slot="activator">
                <v-list-tile-title>Personnel Requisition</v-list-tile-title>
            </v-list-tile>
            <template v-for="(item,key) in prItems">
                <v-tooltip right :key="key">
                    <v-list-tile @click="$router.push(item.route)" slot="activator">
                        <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                    </v-list-tile>
                    <span>{{ item.title}}</span>
                </v-tooltip>
            </template>
            
         
        </v-list-group>
        <!-- Filtering -->
        <v-list-group
                prepend-icon="settings"
                no-action>
            <v-list-tile slot="activator">
                <v-list-tile-title>Flitering</v-list-tile-title>
            </v-list-tile>
            <template v-for="(item,key) in filteringItems">
                <v-tooltip right :key="key">
                    <v-list-tile @click="$router.push(item.route)" slot="activator">
                        <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                    </v-list-tile>
                    <span>{{ item.title}}</span>
                </v-tooltip>
            </template>
        </v-list-group>
        <!-- Scheduling -->
        <v-list-group
                prepend-icon="settings"
                no-action>
            <v-list-tile slot="activator">
                <v-list-tile-title>Scheduling</v-list-tile-title>
            </v-list-tile>
            <template v-for="(item,key) in schedulingItems">
                <v-tooltip right :key="key">
                    <v-list-tile @click="$router.push(item.route)" slot="activator">
                        <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                    </v-list-tile>
                    <span>{{ item.title}}</span>
                </v-tooltip>
            </template>
        </v-list-group>
        <!-- Screening -->
        <v-list-group
                prepend-icon="settings"
                no-action>
            <v-list-tile slot="activator">
                <v-list-tile-title>Screening</v-list-tile-title>
            </v-list-tile>
            <template v-for="(item,key) in screeningItems">
                <v-tooltip right :key="key">
                    <v-list-tile @click="$router.push(item.route)" slot="activator">
                        <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                    </v-list-tile>
                    <span>{{ item.title}}</span>
                </v-tooltip>
            </template>
        </v-list-group>
        <!-- Employment -->
        <v-list-group
                prepend-icon="settings"
                no-action>
            <v-list-tile slot="activator">
                <v-list-tile-title>Employment</v-list-tile-title>
            </v-list-tile>
            <template v-for="(item,key) in employmentItems">
                <v-tooltip right :key="key">
                    <v-list-tile @click="$router.push(item.route)" slot="activator">
                        <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                    </v-list-tile>
                    <span>{{ item.title}}</span>
                </v-tooltip>
            </template>
        </v-list-group>
        <!-- Close Transactions -->
        <v-list-group
                prepend-icon="settings"
                no-action>
            <v-list-tile slot="activator">
                <v-list-tile-title>Close Transactions</v-list-tile-title>
            </v-list-tile>
            <template v-for="(item,key) in closeTransItems">
                <v-tooltip right :key="key">
                    <v-list-tile @click="$router.push(item.route)" slot="activator">
                        <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                    </v-list-tile>
                    <span>{{ item.title}}</span>
                </v-tooltip>
            </template>
        </v-list-group>
    </v-list>
</template>
<script>
export default {
    data() {
        return {
            prItems: [
                { title: 'PR Entry', route: ''},
                { title: 'PR Entry Approval', route: ''},
                { title: 'Returned PRF', route: ''},
                { title: 'PR History', route: ''},
                { title: 'Extend PRF', route: ''},
                { title: 'Extend PRF Approval', route: ''}
            ],
            filteringItems: [
                { title: 'Applicants Filtering Entry', route: ''},
                { title: 'Remove Applicants Filtering', route: '/'},
            ],
            schedulingItems: [
                { title: 'Schedule of Evaluator Screening', route: ''},
                { title: 'Schedule of Evaluator Approval', route: ''},
                { title: 'Change Schedule of Evaluator Screening', route: ''}
            ],
            screeningItems: [
                { title: 'Schedule of Applicants Screening', route: ''},
                { title: 'Screening Entry', route: '/'},
            ],
            employmentItems: [
                { title: 'Final Selection Applicants', route: ''},
                { title: 'Process Employment', route: '/'},
            ],
            closeTransItems: [
                { title: 'Personnel Requisition', route: ''},
                { title: 'Budget Entry', route: ''},
                { title: 'Screening', route: ''}
            ]
        }
    }
}
</script>

