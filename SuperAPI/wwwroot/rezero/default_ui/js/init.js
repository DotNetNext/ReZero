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
                    if (this.userInfo.IsAdmin == true)
                    {
                        document.querySelectorAll('.manager').forEach(element => element.classList.remove('hide'));
                    }
                })
                .catch(error => {
                    console.error('获取用户信息失败:', error);
                });
        } 
    }
});