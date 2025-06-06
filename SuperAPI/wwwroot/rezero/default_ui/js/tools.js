﻿var isLoading = false;
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
    },
    getValue: function (id) {
        var element = document.getElementById(id);
        if (element) {
            return element.value;
        } else {
            return null;
        }
    },
    checkAuthorization: function () { 
        setTimeout(function () {

            var tokeEle = document.getElementById("txtToken");
            if (tokeEle) return;

            axios.get("/PrivateReZeroRoute/100004/GetDbTypeList", jwHeader)
                .then(response => {
                    this.dbTypeList = response.data;
                    isLoading = true;
                    this.error = null;
                })
                .catch(error => {
                    isLoading = true;
                    this.error = error.message;
                    this.data = null;
                });
        }, 3000)
    },
    jsonToUrl: function (json) {
        return Object.keys(json).map(function (key) {
            return encodeURIComponent(key) + '=' + encodeURIComponent(json[key]);
        }).join('&');
    },
    initColor: function () {
        if (localStorage.BgColorType == 1) {
            document.body.setAttribute('data-theme', 'dark');
        } else if (localStorage.BgColorType !== 'default') {
            document.body.setAttribute('data-theme', 'default');
        }
        var dropdown = document.querySelector('.dropdown-skin.open');

        if (dropdown) {
            // 移除 "open" 类，添加 "close" 类
            dropdown.classList.remove('open');
            dropdown.classList.add('close');
        }
    },
    appendQueryParams: function (url) {
        // 从当前 URL 中提取查询参数
        const searchParams = new URLSearchParams(window.location.search);
        const params = {};

        // 动态提取 token 和 model
        if (searchParams.has('token')) {
            params.token = searchParams.get('token');
        }
        if (searchParams.has('model')) {
            params.model = searchParams.get('model');
        }

        // 如果没有参数，直接返回原始 URL
        if (Object.keys(params).length === 0) {
            return url;
        }

        // 检查 URL 是否已经包含查询参数
        const separator = url.includes('?') ? '&' : '?';

        // 将参数对象转换为查询字符串
        const queryString = Object.keys(params)
            .map(key => `${encodeURIComponent(key)}=${encodeURIComponent(params[key])}`)
            .join('&');

        // 拼接 URL 和查询参数
        return url + separator + queryString;
    },
    validateDatalist: function (ele, datalistId) {
        const input = ele;
        const list = document.getElementById(datalistId);
        const options = list && list.options;
        const value = input.value;
        let isValid = false;
        if (options) {
            for (let i = 0; i < options.length; i++) {
                if (value === options[i].value) {
                    isValid = true;
                    break;
                }
            }
        }
        if (!isValid) {
            input.value = '';
        }
    },
    copyText: function (text)
    {
        navigator.clipboard.writeText(text).then(() => {
            tools.alert("复制成功");
        }).catch(err => {
            tools.alert("复制失败");
        });
    }, 
    formatDate: function (date) {
        if (!date) return '';
                const d = new Date(date);
                const month = '' + (d.getMonth() + 1);
                const day = '' + d.getDate();
                const year = d.getFullYear();
                return [year, month.padStart(2, '0'), day.padStart(2, '0')].join('-');
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
        if (isLoading) {
            tools.openLoading();
        }
        return config;
    }, function (error) {
        // 对请求错误做些什么  
        return Promise.reject(error);
    });

    // 设置响应拦截器  
    axios.interceptors.response.use(function (response) {
        if (isLoading) {
            tools.closeLoading();
        }
        return response;
    }, function (error) {
        if (error.response) {
            // 请求已发出，但服务器响应的状态码不在 2xx 范围内  
            if (error.response.status === 401) {
                if (isLoading) {
                    tools.closeLoading();
                }
                tools.alert("授权失败，自动跳到授权页面");
                // 如果是401错误（未授权），则跳转到登录页面  
                setTimeout(function () {
                    if (isloginPage) {
                        window.location.href = '/rezero/login.html';  
                    } else {
                        window.location.href = '/rezero/authorization.html';  
                    }
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