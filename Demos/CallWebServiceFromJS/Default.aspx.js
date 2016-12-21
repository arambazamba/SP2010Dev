
function CallWebservice() {
    var mynumber = $("input[id*=txtMyNumber]")[0].value;
    DummyService.DoubleMe(mynumber, WebServiceCalled);    
}

function WebServiceCalled(result) {
    var lblResult = $("span[id*=lblResult]");
    $(lblResult).text(result);
}