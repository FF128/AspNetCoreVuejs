<template>
    <v-form>
        <v-container>
            <v-layout wrap row>
                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.code"
                        :counter="10"
                        label="Code"
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md4>
                    <v-text-field
                        v-model="ci.description"
                        :counter="50"
                        label="Description"
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md5>
                    <v-text-field
                        v-model="ci.address"
                        label="Address"
                        required
                    ></v-text-field>
                </v-flex>

                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.zipCode"
                        :counter="10"
                        label="Zip Code"
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md4>
                    <v-text-field
                        v-model="ci.city"
                        :counter="50"
                        label="City"
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md5>
                    <v-text-field
                        v-model="ci.province"
                        label="Province"
                        required
                    ></v-text-field>
                </v-flex>

                <v-flex 
                    xs12 sm6 md4>
                    <v-text-field
                        v-model="ci.telNum"
                        :counter="10"
                        label="Tel No."
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md4>
                    <v-text-field
                        v-model="ci.email"
                        :counter="50"
                        :rules="emailRules"
                        label="Email"
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md4>
                    <v-text-field
                        v-model="ci.tin"
                        label="TIN No."
                        required
                    ></v-text-field>
                </v-flex>

                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.sss"
                        label="SSS No."
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.philhealth"
                        label="Philhealth No."
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.sss"
                        label="Pag-ibig No."
                        required
                    ></v-text-field>
                </v-flex>
                <v-flex 
                    xs12 sm6 md3>
                    <v-text-field
                        v-model="ci.sss"
                        label="BIR Branch Code"
                        required
                    ></v-text-field>
                </v-flex>

                <v-flex md12>
                    <img :src="logoForReports.imageUrl" height="150" v-if="logoForReports.imageUrl"/>
                    <v-text-field label="Logo for Reports" @click='pickFile' v-model='logoForReports.imageName' prepend-icon='attach_file'></v-text-field>
                    <input
						type="file"
						style="display: none"
						ref="image"
						accept="image/*"
						@change="onFilePicked"
					>
                </v-flex>
                
                <v-flex md12>
                    <img :src="logoForSite.imageUrl" height="150" v-if="logoForSite.imageUrl"/>
                    <v-text-field label="Logo for Site" @click='pickFile' v-model='logoForSite.imageName' prepend-icon='attach_file'></v-text-field>
                    <input
						type="file"
						style="display: none"
						ref="image"
						accept="image/*"
						@change="onFilePicked"
					>
                </v-flex>

                <v-flex>
                    <img :src="contentForSite.imageUrl" height="150" v-if="contentForSite.imageUrl"/>
                    <v-text-field label="Content for Site" @click='pickFile' v-model='contentForSite.imageName' prepend-icon='attach_file'></v-text-field>
                    <input
						type="file"
						style="display: none"
						ref="image"
						accept="image/*"
						@change="onFilePicked"
					>
                </v-flex>
            </v-layout>
        </v-container>
    </v-form>
</template>
<script>
let imageContent = {
    imageName: '',
    imageUrl: '',
    imageFile: '',
}
export default {
    data() {
        return {
            ci: {},
            isSaving: false,
            emailRules: [
                v => !!v || 'E-mail is required',
                v => /.+@.+/.test(v) || 'E-mail must be valid'
            ],
            logoForReports: {
                imageName: '',
                imageUrl: '',
                imageFile: ''
            },
            logoForSite: imageContent,
            contentForSite: imageContent

        }
    },
    methods: {
        save() {
            this.$axios.post("api/companyInfo", this.ci)
                .then(response => {
                    console.log(response.data);
                })
                .catch(err => console.log(err));
        },
        onFilePicked (e) {
			const files = e.target.files
			if(files[0] !== undefined) {
				this.logoForReports.imageName = files[0].name
				if(this.logoForReports.imageName.lastIndexOf('.') <= 0) {
					return
				}
				const fr = new FileReader ()
				fr.readAsDataURL(files[0])
				fr.addEventListener('load', () => {
					this.logoForReports.imageUrl = fr.result
					this.logoForReports.imageFile = files[0] // this is an image file that can be sent to server...
				})
			} else {
				this.logoForReports.imageName = ''
				this.logoForReports.imageFile = ''
				this.logoForReports.imageUrl = ''
			}
        },
        pickFile () {
            this.$refs.image.click ()
        }

    }
}
</script>

