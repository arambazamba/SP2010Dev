/// <reference name="MicrosoftAjax.js" />
/// <reference path="file://C:/Program Files/Common Files/Microsoft Shared/Web Server Extensions/14/TEMPLATE/LAYOUTS/SP.core.debug.js" />
/// <reference path="file://C:/Program Files/Common Files/Microsoft Shared/Web Server Extensions/14/TEMPLATE/LAYOUTS/SP.debug.js" />
/// <reference path="file://C:/Program Files/Common Files/Microsoft Shared/Web Server Extensions/14/TEMPLATE/LAYOUTS/SP.Ribbon.debug.js" />

function GetWebProperties() {

    var ctx = new SP.ClientContext.get_current();    
    this.web = ctx.get_web();
    ctx.load(this.web);
    ctx.executeQueryAsync(Function.createDelegate(this, this.onPropertiesSuccess),          Function.createDelegate(this, this.onFail));
}

function onPropertiesSuccess(sender, args) {
    var result = 'Web Title:' + this.web.get_title() + '<br/>ID:' + this.web.get_id() + '<br/>Created Date:' + this.web.get_created();
    var lblStatus = $("span[id*=lblStatus]");
    $(lblStatus).text(result);

}

function onFail(sender, args) {
    var fail = 'An Error occured:' + args.get_message();
    var lblStatus = $("span[id*=lblStatus]");
    $(lblStatus).text(fail);
}

var clientContext;
var web;
var myList;
var colListItem;

function ReadFromList() {

    var filter = $("input[id*=txtFilter]")[0].value;
    clientContext = SP.ClientContext.get_current();
    web = clientContext.get_web();    
    myList = web.get_lists().getByTitle('Customers'); 

    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml('<View><Query><Where><Eq><FieldRef Name="Title" /><Value Type="Text">'+ filter + '</Value></Eq></Where></Query></View>'); 
    collListItem = myList.getItems(camlQuery);

    clientContext.load(this.web);
    clientContext.load(myList);
    clientContext.load(collListItem, 'Include(ID, Title, FirstName)');
    clientContext.executeQueryAsync(Function.createDelegate(this, this.onQuerySucceeded),       Function.createDelegate(this, this.onFail));
}

function onQuerySucceeded(sender, args) {

    var result = "";
    var listItemEnumerator = collListItem.getEnumerator();
    while (listItemEnumerator.moveNext()) {
        var oListItem = listItemEnumerator.get_current();
        result += oListItem.get_item('FirstName') + ' ' + oListItem.get_item('Title') + '<br>'; 
    }
    var lblStatus = $("span[id*=lblStatus]");
    $(lblStatus).text(result);
}

function onAddDeleteSucceeded(sender, args) {
    window.location.reload();
}

function AddContact() {
    var lastname = $("input[id*=txtLastName]")[0].value;
    var firstname = $("input[id*=txtFirstName]")[0].value;

    var context = new SP.ClientContext.get_current();
    var web = context.get_web();
    var list = web.get_lists().getByTitle('Customers');
    var listItemCreationInfo = new SP.ListItemCreationInformation();
    var newItem = list.addItem(listItemCreationInfo);
    newItem.set_item('Title', lastname);
    newItem.set_item('FirstName', firstname);
    newItem.update();
    context.executeQueryAsync(Function.createDelegate(this, this.onAddDeleteSucceeded), Function.createDelegate(this, this.onFail));

}



