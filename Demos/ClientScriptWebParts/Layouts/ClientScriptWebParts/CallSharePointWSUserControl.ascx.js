function GetListsForWeb()
{
    var soapEnv =
        "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'> \
            <soapenv:Body> \
                <GetListCollection xmlns='http://schemas.microsoft.com/sharepoint/soap/'> \
                </GetListCollection> \
            </soapenv:Body> \
        </soapenv:Envelope>";

    $.ajax({
        url: "http://chiron/_vti_bin/lists.asmx",
        type: "POST",
        dataType: "xml",
        data: soapEnv,
        complete: processResult,
        contentType: "text/xml; charset=\"utf-8\""
    });
}

function processResult(xData, status) {
    $(xData.responseXML).find("List").each(function () {
        $("#data").append("<li>" + $(this).attr("Title") + "</li>");
    });
}