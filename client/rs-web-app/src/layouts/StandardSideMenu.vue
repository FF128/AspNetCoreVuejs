<template>
    <v-list class="white">
        <!-- <v-list-tile @click="$router.push('/designation-duties-requirements')">
            <v-list-tile-action>
                <v-icon>home</v-icon>
            </v-list-tile-action>
            <v-list-tile-title>Designation</v-list-tile-title>
        </v-list-tile> -->
        <!-- General Setup -->
        <v-list-group
                prepend-icon="settings"
                no-action>
            <v-list-tile slot="activator">
                <v-list-tile-title>General Setup</v-list-tile-title>
            </v-list-tile>
            <v-tooltip right>
                <v-list-tile @click="$router.push('/company-info')" slot="activator">
                    <v-list-tile-title>Company Information</v-list-tile-title>
                </v-list-tile>
                <span>Company Information</span>
            </v-tooltip>
            
           
        </v-list-group>
        <!-- Personal Information -->
        <v-list-group
                prepend-icon="group"
                no-action>
            <v-list-tile slot="activator">
                <v-list-tile-title>Personal Information</v-list-tile-title>
            </v-list-tile>
            <v-list-tile v-for="(item,key) in personal" :key="key" @click.prevent="$router.push(item.link)">
                <v-list-tile-title>{{item.title}}</v-list-tile-title>
            </v-list-tile>
        </v-list-group>
        <!-- Employment Information -->
        <v-list-group
                prepend-icon="supervisor_account">
            <v-list-tile slot="activator">
                <v-list-tile-title>Employment Information</v-list-tile-title>
            </v-list-tile>
             <!-- Employee Level -->
            <v-list-group
                no-action
                sub-group>
                <v-list-tile slot="activator">
                    <v-list-tile-title>Employee Level</v-list-tile-title>
                </v-list-tile>
              
                <v-list-tile v-for="(item,key) in employeeLevel" :key="key"
                    @click="$router.push(item.link)">
                    <v-list-tile-title>{{item.title}}</v-list-tile-title>
                    <v-list-tile-action>
                        <v-icon></v-icon>
                    </v-list-tile-action>
                </v-list-tile>
            </v-list-group>
            <!-- Organization Level -->
            <v-list-group
                no-action
                sub-group>
                <v-list-tile slot="activator">
                    <v-list-tile-title>Organizational Level</v-list-tile-title>
                </v-list-tile>
              
                <v-list-tile v-for="(item,key) in orgranizationalLevel" :key="key"
                    @click="$router.push(item.link)">
                    <v-list-tile-title>{{item.title}}</v-list-tile-title>
                    <v-list-tile-action>
                        <v-icon></v-icon>
                    </v-list-tile-action>
                </v-list-tile>
            </v-list-group>

        </v-list-group>
        <!-- Employment Other Information -->
        <v-list-group
                prepend-icon="account_circle">
            <v-list-tile slot="activator">
                <v-list-tile-title>Employment Other Information</v-list-tile-title>
            </v-list-tile>
            <!-- Education -->
            <v-list-group
                no-action
                sub-group>
                <v-list-tile slot="activator">
                    <v-list-tile-title>Education</v-list-tile-title>
                </v-list-tile>
              
                <v-list-tile v-for="(item,key) in education" :key="key"
                    @click="$router.push(item.link)">
                    <v-list-tile-title>{{item.title}}</v-list-tile-title>
                    <v-list-tile-action>
                        <v-icon></v-icon>
                    </v-list-tile-action>
                </v-list-tile>
            </v-list-group>
            <!-- Others -->
            <v-list-group
                no-action
                sub-group>
                <v-list-tile slot="activator">
                    <v-list-tile-title>Others</v-list-tile-title>
                </v-list-tile>
                <v-tooltip v-for="(item,key) in others" :key="key" right>
                    <v-list-tile 
                        @click="$router.push(item.link)" slot="activator">
                        <v-list-tile-title>{{item.title}}</v-list-tile-title>
                        <v-list-tile-action>
                            <v-icon></v-icon>
                        </v-list-tile-action>
                    </v-list-tile>
                    <span>{{item.title}}</span>
                </v-tooltip>
            </v-list-group>
            
        </v-list-group>
    </v-list>
</template>
<<script>
export default {
    data() {
        return {
            cruds: [],
            personal: [
                { title: 'Citizenship', link: '/citizenship' },
                { title: 'Religion', link: '/religion' }
            ],
            employeeLevel: [
                { title: 'Employee Status File', link: "/employee-status-file"},
                { title: 'Job Level', link: "/job-level"},
                { title: 'Grade', link: "/grade"},
                { title: 'Step', link: "/step"},
                { title: 'Designation File', link: "/designation-file"},
            ],
            orgranizationalLevel: [
                { title: 'Area', link: "/area"},
                { title: 'Branch', link: "/branch"},
                { title: 'Department', link: "/department"},
                { title: 'Division', link: "/division"},
                { title: 'Location', link: "/location"},
                { title: 'Unit', link: "/unit"},
                { title: 'Section', link: "/section"},
                { title: 'Rank', link: "/rank"},
                { title: 'Project Code', link: "/project-code"},
                { title: 'Pay House', link: "/pay-house"},
                { title: 'Region', link: "/region"},
                { title: 'Levels of Employee', link: "/levels-of-employee"},
            ],
            education: [
                { title: 'Course/Degree', link: "/course"},
                { title: 'Major', link: "/major"},
                { title: 'School Name', link: "/school"}
            ],
            others: [
                { title: 'Duties and Responsibilities', link: "/duties-responsibilities"},
                { title: 'Field of Interest', link: "/field-interest"},
                { title: 'Government Exams', link: "/gov-exams"},
                { title: 'Job Requirements', link: "/job-requirements"},
                { title: 'Languages and Dialects', link: "/language"},
                { title: 'Licenses', link: "/license"},
                { title: 'Residence Type', link: "/residence-type"},
                { title: 'Skills', link: "/skills"},
                { title: 'Documents Submitted', link: "/documents"}
            ]
        }
    }
}
</script>
