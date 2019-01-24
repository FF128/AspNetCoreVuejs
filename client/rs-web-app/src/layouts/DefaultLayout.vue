<template>
    <div class="container">
        <nav class="navbar" style="border-bottom: 2px solid #E6E9ED">
            <div class="navbar-brand">
                <a class="navbar-item" :to="{ name: 'home' }">
                    {{title}}
                </a>
                <div class="navbar-burger burger" onclick="document.querySelector('.navbar-menu').classList.toggle('is-active');">
                <span></span>
                <span></span>
                <span></span>
                </div>
            </div>

            <div id="navbarExampleTransparentExample" class="navbar-menu">
                <div class="navbar-end">
                    <router-link class="navbar-item" to="/company-info">Company Info</router-link>
                    <router-link class="navbar-item" to="/co">Link 2</router-link>
                    <div class="navbar-item has-dropdown is-hoverable">
                        <a class="navbar-link" href="#">
                            Link 3
                        </a>
                        <div class="navbar-dropdown is-boxed">
                            <router-link class="navbar-item" to="/physicians">Link 3.1</router-link>
                        </div>
                    </div>
                    <a class="navbar-item" href="#" @click="logout">
                        Logout
                    </a>
                </div>
            </div>
        </nav>
        <aside class="menu" v-if="aside">
            <p class="menu-label">
                General
            </p>
            <ul class="menu-list">
                <li><a>Dashboard</a></li>
                <li><a>Customers</a></li>
            </ul>
            <p class="menu-label">
                Administration
            </p>
            <ul class="menu-list">
                <li><a>Team Settings</a></li>
                <li>
                <a class="is-active">Manage Your Team</a>
                <ul>
                    <li><a>Members</a></li>
                    <li><a>Plugins</a></li>
                    <li><a>Add a member</a></li>
                </ul>
                </li>
                <li><a>Invitations</a></li>
                <li><a>Cloud Storage Environment Settings</a></li>
                <li><a>Authentication</a></li>
            </ul>
            <p class="menu-label">
                Transactions
            </p>
            <ul class="menu-list">
                <li><a>Payments</a></li>
                <li><a>Transfers</a></li>
                <li><a>Balance</a></li>
            </ul>
        </aside>
        <br/>
        <router-view></router-view>
        <br/>
        <footer class="footer">
        <div class="content has-text-centered">
            <p>
                © 2019 128 Tech Consulting Inc.
            </p>
        </div>
        </footer>
    </div>
</template>
<script>
export default {
  data() {
    return {
      aside: false,
      title: "Recruitment System"
    };
  },
  methods: {
    logout() {}
  }
};
</script>
