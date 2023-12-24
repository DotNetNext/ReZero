var tools = {
    highlightErrorFields: function (data) {
        if (!data.ErrorParameters)
        {
            tools.alert(data.message);
            return;
        } 
        data.ErrorParameters.forEach(function (error) {
            // 获取 Name 属性的值
            var fieldName = error.Name;

            // 查找具有相同 name 属性值的所有元素
            var elements = document.getElementsByName(fieldName);

            // 循环遍历找到的元素，设置样式
            for (var i = 0; i < elements.length; i++) {
                elements[i].style.border = "2px solid red";
            }
            setTimeout(function () {
                for (var i = 0; i < elements.length; i++) {
                    elements[i].style.border = "";
                }
            }, 3000);
        });
    },
    alert: function (msg)
    {
        $(divAlertBody).html(msg);
        btnAlert.click();
    }
}
