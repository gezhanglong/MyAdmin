function DeletePost(url, data) {
    if (confirm('是否确定删除?')) {
        $.post(url, data, function () { 
            alert('删除成功');
            window.location.reload();
        });
    }
}

function DeletePostBox(url, data) { 
    $.post(url, data, function (json) {
        if (json.errcode == "0") { 
            $.ligerDialog.alert('删除成功', '提示', type);
            window.location.reload();
        } else {
            $.ligerDialog.alert("删除失败！errcode:" + json.errcode + ";errmsg:" + json.errmsg, '提示', type); 
        } 
        }); 
}


function setSelectVal(sel, val, callback) {
    if ($.browser.msie && $.browser.version == "6.0") {
        setTimeout(function () {
            sel.val(val);
            callback();
        }, 1);
    } else {
        sel.val(val);
    }
}