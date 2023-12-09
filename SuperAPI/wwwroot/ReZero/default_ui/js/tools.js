var tools = { 
    formToJson: function (id) {
        var formData = {};
        var formElements = document.getElementById(id).elements;

        for (var i = 0; i < formElements.length; i++) {
            var element = formElements[i];
            if (element.name && element.value) {
                formData[element.name] = element.value;
            }
        }
        return formData;
    }
}
