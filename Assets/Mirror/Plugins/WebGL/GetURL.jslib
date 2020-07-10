mergeInto(LibraryManager.library, {
    GetURLFromPage: function () {
        console.log("get url from page")
        var returnStr = window.top.location.href;
        var bufferSize = lengthBytesUTF8(returnStr) + 1
        var buffer = _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize);
        console.log("get url from page called")
        return buffer;
    },
 
    GetQueryParam: function(paramId) {
        console.log("get query param called")
        var urlParams = new URLSearchParams(location.search);
        var param = urlParams.get(Pointer_stringify(paramId));
        console.log("JavaScript read param: " + param);
        var bufferSize = lengthBytesUTF8(param) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(param, buffer, bufferSize);
        return buffer;
    }
});