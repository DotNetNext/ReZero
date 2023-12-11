function resizeIframe() {
    setInterval(function () {

        var iframe = document.getElementById('myIframe');
        if (iframe) {
            var height = iframe.contentWindow.document.body.scrollHeight;
            iframe.style.height = (height) + 'px';
        }

    }, 50)
}

// 在页面加载和 iframe 加载完成时调整 iframe 高度
window.onload = resizeIframe;
document.getElementById('myIframe').onload = resizeIframe;

// 你还可以在窗口大小发生变化时触发调整，以确保动态适应
window.onresize = resizeIframe;