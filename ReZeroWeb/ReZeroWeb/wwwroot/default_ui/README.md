# 光年(Light Year Admin)后台管理系统模板

#### 演示网址
[http://lyear.itshubao.com](http://lyear.itshubao.com)

#### 模板说明文档
[http://www.bixiaguangnian.com/manual/lyearadmin.html](http://www.bixiaguangnian.com/manual/lyearadmin.html)

#### 交流群
![输入图片说明](https://images.gitee.com/uploads/images/2021/0419/100528_9d22ac7d_82992.png "光年后台模板交流群群聊二维码.png")

#### 其他插件整合示例
有些插件可能你并不一定能用上，这里对其他很多的一些插件整合以示例的形式出现，自己选择需要的插件下载。

[点击查看其他插件整合](https://gitee.com/yinqi/Light-Year-Example)

[各种整合插件演示地址](http://example.itshubao.com/)

#### 介绍
![light year admin](https://images.gitee.com/uploads/images/2019/0314/224956_3eb2a29a_82992.png "未命名-1.png")

光年(Light Year Admin)后台管理系统模板是一个基于Bootstrap v3.3.7的纯HTML模板。

[猛戳这里去iframe版本](https://gitee.com/yinqi/Light-Year-Admin-Using-Iframe)

[猛戳这里去Light Year Admin V4项目(基于bootstrap 4.4.1版本)](https://gitee.com/yinqi/Light-Year-Admin-v4)

[猛戳这里去Light Year Admin Using Iframe V4项目(基于bootstrap 4.4.1版本)](https://gitee.com/yinqi/Light-Year-Admin-Using-Iframe-v4)

[猛戳这里去Light Year Admin v5项目](https://gitee.com/yinqi/Light-Year-Admin-Template-v5)

[猛戳这里去Light Year Admin Using Iframe  v5项目](https://gitee.com/yinqi/Light-Year-Admin-Using-Iframe-v5)

作为后端开发人员，自己在做一些简单系统时，经常为了后台的模板烦恼，国内的少，也不太喜欢tab形式的；国外的又太复杂；vue什么框架的又不会用，因而想自己整理出来一个简单点的通用后台模板，结合自己的使用和国外模板的配色、细节处理，这就有了光年后台模板。

简洁而清新的后台模板，功能虽少，倒也满足简单的后台功能，也能够快速上手，希望大家支持。

因群里有小伙伴提到有和网友对于光年模板版权上的一些争执，我特意申请了软件著作权，在这里也再提一下，光年模板是完全开源免费的，大家可以放心在学习和商业中使用，无需找我授权。

版权号：2022SR0104200

#### 谁在用
- [pai-admin 一款基于TP6框架开发的后台管理系统](https://gitee.com/leapy/pai)
- [Family 族谱管理系统](https://gitee.com/GodLikeZeal/family)
- [springBoot+thymeleaf+springSecurity+mybatis框架实现cms内容管理和商城系统](https://gitee.com/wtsoftware/febs)
- [Daen创意云：一款php+mysql开发的web应用管理框架](https://gitee.com/daenmax/DaenCreativeCloud)
- [舒笑导航 - 一个属于程序员的网址导航](http://nav.shuxiao.net)
- [JMcms 一款基于TP6框架开发的多应用后台管理系统](https://gitee.com/lostmana/mingde)
- [Myadmin 一个基于ThinkPHP6的免费开源，快速简单，面向对象的应用研发架构](https://gitee.com/dream-kc/myadminV2)

#### 说明
网站主题切换，有默认白色，黑色，和渐变蓝绿色的半透明三种。另外：logo、顶部和侧边栏可单独调整颜色(黑色和半透明主题时选择白色不变)。

增加主题切换，增加了很多css，如果你不需要有主题切换功能，可将多余的内容删除掉。
- HTML页面中删除掉`<!--切换主题配色-->`之间的内容
- js中删除 line 137 - line 146
- css中删除 line 2937 - line 3946

主题切换并没有存到cookie中，大家根据自己实际情况，做cookie存储或者其他方式。

#### Class搜索小工具
[此网址可以方便找到v3和v4中自己想要的class](http://libs.itshubao.com/lyear-search-class/)

#### jquery-cookie保存设置示例
```
    // 使用时，请自行引入jquery.cookie.js
	// 读取cookie中的主题设置
	var the_logo_bg    = $.cookie('the_logo_bg'),
	    the_header_bg  = $.cookie('the_header_bg'),
	    the_sidebar_bg = $.cookie('the_sidebar_bg'), // iframe版本如果删除了下面一行，请把这一行的逗号改成分号
	    the_site_theme = $.cookie('the_site_theme'); // iframe版本可不需要这行
	
	if (the_logo_bg) $('body').attr('data-logobg', the_logo_bg);
	if (the_header_bg) $('body').attr('data-headerbg', the_header_bg);
	if (the_sidebar_bg) $('body').attr('data-sidebarbg', the_sidebar_bg);
	if (the_site_theme) $('body').attr('data-theme', the_site_theme); // iframe版本可不需要这行
        
        // 处理主题配色下拉选中
        $(".dropdown-skin :radio").each(function(){
            var $this = $(this),
                radioName = $this.attr('name');
            switch (radioName) {
                case 'site_theme':
                    $this.val() == the_site_theme && $this.prop("checked", true);
                    break;  // iframe版中不需要这个case
                case 'logo_bg':
                    $this.val() == the_logo_bg && $this.prop("checked", true);
                    break;
                case 'header_bg':
                    $this.val() == the_header_bg && $this.prop("checked", true);
                    break;
                case 'sidebar_bg':
                    $this.val() == the_sidebar_bg && $this.prop("checked", true);
            }
        });
	
	// 设置主题配色
	setTheme = function(input_name, data_name) {
	    $("input[name='"+input_name+"']").click(function(){
	        $('body').attr(data_name, $(this).val());
	        $.cookie('the_'+input_name, $(this).val());
	    });
	}
	setTheme('site_theme', 'data-theme'); // iframe版本可不需要这行
	setTheme('logo_bg', 'data-logobg');
	setTheme('header_bg', 'data-headerbg');
	setTheme('sidebar_bg', 'data-sidebarbg');
```
> 由jquery读取设置，可能会受到加载速度的影响


#### 特别感谢
- Bootstrap(去掉了自带的字体图标)
- JQuery
- bootstrap-colorpicker
- bootstrap-datepicker
- bootstrap-datetimepicker
- ion-rangeslider
- jquery-confirm
- jquery-tags-input
- bootstrap-notify
- Chart.js
- chosen.jquery.js
- perfect-scrollbar

### 更新记录
2020.08.01 更改侧边栏导航绑定click事件的方式

2020.05.16 调整图库

2020.07.02 修复弹窗样式上的bug

2020.06.14 新增通用样式，调整复选框单选框换行后左侧空白问题，调整tags插件

2020.06.14 新增label样式，调整datepicker插件样式和使用，调整silders插件样式和皮肤，改td垂直居中，增加登录页面

2019.12.08 修改cssbug，新增card-footer，增加图标的一些使用类

2019.08.04 修改样式bug

2019.05.13 略微调整单选框和复选框的样式

2019.04.24 新增文档示例页面增加多图上传样式（只有样式），调整标签插件样式和js的默认提示

2019.04.21 新增步骤条和向导插件，修改消息方法（新增自定义消失时间），增加错误页面，增加通知消息页面说明，调整设置页样式

2019.03.31 新增暗黑、半透明 两种主题样，所有页面都增加切换html

2019.03.30 修正bootstrap-datetimepicker图标bug，调整一些样式

2019.03.28 新增主题配色切换
- 颜色切换，主要针对logo，顶部，侧边栏
- 目前只在首页页面增加切换，并且未将设置存储到cookie

2019.03.25 微调单选框和复选框禁用下的颜色

2019.03.22 调整侧边栏隐藏/显示操作，开关改为一直可见，侧边栏自动隐藏临界值调整为1024px

2019.03.21 新增多级菜单，调整侧边栏一些样式(多级菜单的滚动条位置还需要观察)

2019.03.19 调整logo，修复滚动条bug

#### 登录页面
![光年模板登录页](https://images.gitee.com/uploads/images/2019/0316/223413_a840c9c2_82992.png "登录页面 - 光年(LightYear)后台管理系统模板.png")

#### 后台首页
![光年模板首页](https://images.gitee.com/uploads/images/2019/0314/231617_c0900993_82992.png "首页 - 光年(LightYear)后台管理系统模板.png")

#### 开关样式
![开关样式](https://images.gitee.com/uploads/images/2019/0316/224100_4a8494eb_82992.png "开关 - 光年(LightYear)后台管理系统模板.png")

#### 文档列表
![文档列表](https://images.gitee.com/uploads/images/2019/0316/223923_60231d3e_82992.png "文档列表 - 光年(LightYear)后台管理系统模板.png")