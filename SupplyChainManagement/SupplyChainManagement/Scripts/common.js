(function () {
    $.fn.setUpload = function (uploadUrl, callback, beforeUpload) {
        var url = uploadUrl;
        if (typeof (uploadUrl) == "function") {
            url = uploadUrl();
        }
        var file = $(this);
        var form = file.parents("form");
        file.unbind("change").bind("change", function () {
            if (beforeUpload && !beforeUpload()) {
                reset();
                return;
            }
            var targetId = "iframe_upload" + Math.random();
            var iframe = $('<iframe width="0" height="0" frameborder="0" id="' + targetId + '" name="' + targetId + '">');
            document.body.appendChild(iframe[0]);
            form.attr({
                target: targetId,
                action: url,
                enctype: "multipart/form-data",
                method: "POST"

            });
            form.submit();
            iframe.load(function () {
                var content = $(this).contents().find("body").html();
                try {
                    var json = eval("(" + content + ")");
                    callback(json);
                } catch (ex) {
                    alert("上传出错了" + ex.message);
                }
                reset();
                iframe.remove();
            });
        });

        function reset() {
            file.val("");
            var fileId = file.attr("id");
            file.replaceWith(file.clone());
            form.removeAttr("target");
            form.removeAttr("enctype");
            $("#" + fileId).setUpload(uploadUrl, callback, beforeUpload);
        }
    };

    $.fn.serializeObject = function () {
        var form = this[0];
        if (!form) return null;
        var data = {};

        var arr = $(this).serializeArray();
        $.each(arr, function (i, item) {
            setFields(data, item.name, item.value);
        });

        return data;

        function setFields(data, name, value) {
            if (name.indexOf('.') > 0) {
                var names = name.split('.');
                var subName = names[1];
                name = names[0];
                if (!data[name]) data[name] = {};
                setFields(data[name], subName, value);
                return;
            }

            if (data[name]) {
                data[name] += "," + value;
            }
            else {
                data[name] = value;
            }
        }
    };

    $.fn.post = function (callback) {

        var options = {
            url: $(this).attr("action"),
            data: $(this).serialize(),
            success: function () {
                if (typeof (callback) == "function") {
                    callback();
                }
                else if (typeof (callback) == "string") {
                    location.href = callback;
                }
            }
        };
        $.request(options);
    };

    $.request = function (url, data, success, error, global) {
        var options = null;
        if (arguments.length == 1) {
            switch (typeof (arguments[0])) {
                case "object":
                    options = arguments[0];
                    break;
                case "string":
                    options = { url: url };
            }
        } else {
            switch (typeof (arguments[1])) {
                case "function":
                    options = {
                        url: arguments[0],
                        data: null,
                        success: arguments[1],
                        error: arguments.length > 2 ? arguments[2] : null,
                        global: arguments.length > 3 ? arguments[3] : null,
                    };
                    break;
                default:
                    options = {
                        url: url,
                        data: data,
                        success: success,
                        error: error,
                        global: global
                    };
                    break;
            }
        }

        $.ajax({
            type: options.data ? "POST" : "GET",
            dataType: "json",
            global: options.global == undefined,
            url: options.url,
            data: options.data
        }).done(function (json, statusText, xhr) {
            if (options.success) {
                options.success(json, statusText, xhr);
            }
        }).fail(function (xhr) {
            var data = eval("(" + xhr.responseText + ")");
            if (options.error) {
                options.error(data);
            }
        });
    };

})();

$.ajaxSetup({
    beforeSend: function (xhr) {
        xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
    }
});

$(document).ajaxError(function (event, jqxhr, settings, exception) {
    if (jqxhr.responseText) {
        try {
            var result = $.parseJSON(jqxhr.responseText);
            alert(result.message || result.content || "未知错误");
        } catch (ex) {

        }
    }
});
