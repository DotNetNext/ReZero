tools.initColor();


var masterVueObj=new Vue({
    el: '#liUser',
    data: {
        userInfo: {
            Avatar: 'images/users/avatar.jpg',
            UserName: 'ReZero'
        }
    },
    mounted() {
        this.fetchUserInfo();
    },
    methods: {
        fetchUserInfo() {
            axios.get('/Public/User', jwHeader)
                .then(response => {
                    this.userInfo = response.data;
                })
                .catch(error => {
                    console.error('获取用户信息失败:', error);
                });
        } 
    }
});