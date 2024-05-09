var tools = {
    highlightErrorFields: function (data) {
        if (!data.ErrorParameters) {
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

                var spanElement = document.createElement("span");
                spanElement.style.color = "red";
                spanElement.style.marginLeft = "5px"; // 调整距离，可根据需要更改
                spanElement.innerHTML = error.Message;

                // 将 span 元素添加到当前元素的后面
                elements[i].parentNode.insertBefore(spanElement, elements[i].nextSibling);
            }
            setTimeout(function () {
                for (var i = 0; i < elements.length; i++) {
                    elements[i].style.border = "";
                    // 移除当前元素后面的 span 元素
                    var nextSibling = elements[i].nextSibling;
                    if (nextSibling && nextSibling.tagName === "SPAN") {
                        elements[i].parentNode.removeChild(nextSibling);
                    }
                }
            }, 3000);
        });
    },
    alert: function (msg) {
        $(divAlertBody).html(msg);
        btnAlert.click();
    },
    objectToQueryString: function (obj) {
        return Object.keys(obj)
            .filter(key => obj[key] !== null && obj[key] !== undefined) // Filter out null and undefined values
            .map(key => key + '=' + encodeURIComponent(obj[key]))
            .join('&');
    },
    formToJson: function (formId) {
        var form = document.getElementById(formId);
        var formData = new FormData(form);
        var jsonData = {};

        formData.forEach(function (value, key) {
            jsonData[key] = value;
        });

        return jsonData;
    },
    isValidURLPattern: function (inputString) {
        var url = "https://www.rezero.com" + inputString;
        var pattern = /^((http|https|ftp):\/\/)?([a-zA-Z0-9_-]+\.)+[a-zA-Z]{2,6}(\/[a-zA-Z0-9_.-]*)*\/?$/;
        return (pattern.test(url));
    },
    ensureNumeric: function (inputField) {
        // 尝试将输入值转换为数字
        var value = inputField.value;

        // 尝试将输入值转换为数字
        var numericValue = parseFloat(value);

        // 如果转换失败，则将值设置为 0
        if (isNaN(numericValue)) {
            numericValue = 0;
        }

        // 将更新后的值设置回输入框
        inputField.value = numericValue;
    },
    showSpecifiedElementAndHideOthers: function (elementToShowId, elementsToHideIds) {
        // 显示指定元素并隐藏其他元素
        document.getElementById(elementToShowId).style.display = "block";
        for (var i = 0; i < elementsToHideIds.length; i++) {
            document.getElementById(elementsToHideIds[i]).style.display = "none";
        }
    },
    assignValuesToObject: function (sourceObject, targetObject) {
        Object.keys(sourceObject).forEach(function (key) {
            if (targetObject.hasOwnProperty(key)) {
                targetObject[key] = sourceObject[key];
            } else {
                console.warn(`Property '${key}' does not exist in the target object.`);
            }
        });  
    },
    openLoading: function () {
        var loadingOverlay = document.getElementById('loadingOverlay');
        loadingOverlay.style.display = 'flex';
    },
    closeLoading: function () {
        var loadingOverlay = document.getElementById('loadingOverlay');
        loadingOverlay.style.display = 'none';
    }
}
Array.prototype.removeArrayItem = function (item) {
    const index = this.indexOf(item);
    if (index !== -1) {
        this.splice(index, 1);
    }
    return this;
};
setTimeout(function () {
    // 设置请求拦截器  
    axios.interceptors.request.use(function (config) {
        tools.openLoading();
        return config;
    }, function (error) {
        // 对请求错误做些什么  
        return Promise.reject(error);
    });

    // 设置响应拦截器  
    axios.interceptors.response.use(function (response) {
        tools.closeLoading();
        return response;
    }, function (error) {
        if (error.response) {
            // 请求已发出，但服务器响应的状态码不在 2xx 范围内  
            if (error.response.status === 401) {
                tools.closeLoading();
                tools.alert("授权失败，自动跳到授权页面");
                // 如果是401错误（未授权），则跳转到登录页面  
                setTimeout(function () {
                    window.location.href = '/rezero/authorization.html'; // 替换为你想要跳转的地址
                }, 2000);

            } else {
                // 处理其他状态码  
                return Promise.reject(error);
            }
        } else if (error.request) {
            // 请求已发出，但没有收到响应  
            console.log('Request:', error.request);
            return Promise.reject(error);
        } else {
            // 发生一些其他问题在设置请求时触发了一个错误  
            console.log('Error', error.message);
            return Promise.reject(error);
        }  
    });  

},2000)