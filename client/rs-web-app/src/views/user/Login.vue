<template>
    <div id="app">
        <v-app id="inspire">
            <img src="@/assets/banner.png" class="img-responsive"/>
            <v-content>
            <v-container fluid fill-height>
                <v-layout align-center justify-center>
                <v-flex xs12 sm8 md4>
                    <v-card class="elevation-12">
                    <v-toolbar dark color="primary">
                        <v-toolbar-title>Login</v-toolbar-title>
                        <v-spacer></v-spacer>
                        <v-icon>vpn_key</v-icon>
                    </v-toolbar>
                    <v-card-text>
                        <v-form>
                            <v-text-field prepend-icon="person" name="login" label="Username" type="text" v-model="user.username"></v-text-field>
                            <v-text-field prepend-icon="lock" name="password" label="Password" id="password" type="password" v-model="user.password"></v-text-field>
                        </v-form>
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="primary" @click.prevent="login" :loading="isLoading">Login</v-btn>
                    </v-card-actions>
                    </v-card>
                </v-flex>
                </v-layout>
            </v-container>
            </v-content>
            <v-snackbar
                v-model="snackbar.status"
                :bottom="true"
                :multi-line="snackbar.mode === 'multi-line'"
                :right="true"
                :timeout="snackbar.timeout"
                :vertical="snackbar.mode === 'vertical'"
                color="error"
                >
                {{ snackbar.text }}
                    <v-btn
                        color="white"
                        flat
                        @click="snackbar.status = false">
                        Close
                    </v-btn>
                </v-snackbar>
        </v-app>
    </div>
</template>
<script>
import Toast from "@/project-modules/toast"

let toast = new Toast();
export default {
  data() {
    return {
      user: {},
      apiEndpoint: "api/users",
      snackbar: {
        text: "",
        status: false,
        y: "right",
        x: null,
        mode: "",
        timeout: 6000
      },
      isLoading: false
    };
  },
  methods: {
    login() {
      this.isLoading = true
      this.$axios
        .post(`${this.apiEndpoint}/authenticate`, this.user)
        .then(response => {
          this.isLoading = false
          localStorage.setItem("_u", JSON.stringify(response.data));
        //   this.$router.push("/");
          window.location.href = "/"

        })
        .catch(err => {
          toast.show(err.response);
          this.isLoading = false;
        });
    }
  }
};
</script>
<style>
.img-responsive {
  max-width: 100%;
  height: auto;
  display: block;
}
</style>
