<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SkillsManagerUserControl.ascx.cs" Inherits="Lab09.SkillsManager.SkillsManagerUserControl" %>
<asp:TreeView ID="skillsMap" runat="server" ShowLines="true">
</asp:TreeView><br/><br/>
<a href="javascript:showSkillAdder();" id="linkAdder" style="display:none;">
  &raquo;Add Skill Mapping
</a>
<div id="divSkillsManager" style="display:none; padding:5px">
    <table cellpadding='3' cellspacing='3'>
        <tr>
            <td align='center' colspan='2'><b>Skill Mapper</b><br/></td>
        </tr>
        <tr>
            <td>Select a Job:</td>
            <td><select id="Jobs" name="Jobs" /></td>
        </tr>
        <tr>
            <td>Select a Skill:</td>
            <td><select id="Skills" name="Skills" /></td>
        </tr>
        <tr>
            <td colspan='2' align='right'>
                <input type="button" value="Add Skill Mapping" style='width:125px;' onclick="addSkillMapping()" />
                <br />
                <input type="button" value="Cancel" style='width:125px' onclick="onCancel()" />
            </td>
        </tr>
    </table>
</div>
<SharePoint:ScriptLink ID="showDialog" runat="server" Name="sp.js" Localizable="false" LoadAfterUI="true" />
<script language="ecmascript" type="text/ecmascript">
    var thisDialog;
    var jobSelector;
    var skillSelector;
    var clientCtx;
    var skillsWeb;
    var mashupList;
    var jobList;
    var skillList;
    var jobItems;
    var skillItems;
    var mashupItems;
    var camlQuery;
    var listItemEnumerator;
    var listItem;
    var selectedJob;
    var selectedSkill;
    _spBodyOnLoadFunctionNames.push("Initialize()");
    function Initialize() {
        var ctx = new SP.ClientContext.get_current();
        var site = ctx.get_site();
        ctx.load(site);
        ctx.executeQueryAsync(onSuccessLoad,onFailLoad);
        function onFailLoad(sender, args) {
            alert('err');
        }
        function onSuccessLoad() {
            alert(site.get_url());
        }








        clientCtx = new SP.ClientContext('/skills');
        skillsWeb = clientCtx.get_web();
        jobList = skillsWeb.get_lists().getByTitle("Jobs");
        skillList = skillsWeb.get_lists().getByTitle("Skills");
        mashupList = skillsWeb.get_lists().getByTitle("Mashup");
        camlQuery = new SP.CamlQuery();
        camlQuery.set_viewXml('<View><RowLimit>50</RowLimit></View>');
        jobItems = jobList.getItems(camlQuery);
        skillItems = skillList.getItems(camlQuery);
        clientCtx.load(skillsWeb);
        clientCtx.load(jobList);
        clientCtx.load(skillList);
        clientCtx.load(jobItems, 'Include(Id, Title, Level)');
        clientCtx.load(skillItems, 'Include(Id, Title, Importance)');
        clientCtx.executeQueryAsync(onSkillsWebLoaded, errOnSkillsWebLoaded);
    }
    function errOnSkillsWebLoaded(sender, args) {
        alert(args.get_message());
    }
    function onSkillsWebLoaded() {
        var linkAdder = document.getElementById("linkAdder");
        linkAdder.style.display = "inline";
    }
    function showSkillAdder() {
        jobSelector = document.getElementById("Jobs");
        skillSelector = document.getElementById("Skills");
        jobSelector.options.length = 0;
        skillSelector.options.length = 0;
        listItemEnumerator = jobItems.getEnumerator();   
        while (listItemEnumerator.moveNext()) {
            listItem = listItemEnumerator.get_current();
            jobSelector.options[jobSelector.options.length] = new Option(listItem.get_item('Title') + ': ' + listItem.get_item('Level'), listItem.get_id());
        }
        listItemEnumerator = skillItems.getEnumerator();
        while (listItemEnumerator.moveNext()) {
            listItem = listItemEnumerator.get_current();
            skillSelector.options[skillSelector.options.length] = new Option(listItem.get_item('Title') + ': ' + listItem.get_item('Importance'), listItem.get_id());
        }
        var divSkillsManager = document.getElementById("divSkillsManager");
        divSkillsManager.style.display = "block";
        var dialog = { html: divSkillsManager, title: 'Add Skills Mapping', allowMaximize: true, showClose: false, width: 400, height: 200};
        thisDialog = SP.UI.ModalDialog.showModalDialog(dialog);
    }
    function hideSkillMapper() {
        thisDialog.close();
        window.location.reload(true);
    }
    function addSkillMapping() {
        selectedJob = jobSelector.options[jobSelector.selectedIndex].value;
        selectedSkill = skillSelector.options[skillSelector.selectedIndex].value;
        clientCtx.load(mashupList);
        camlQuery = new SP.CamlQuery();
        camlQuery.set_viewXml('<View><Query><Where><Eq>' +
        '<FieldRef Name=\'LookupJobs\'/><Value Type=\'Integer\'>' + selectedJob + '</Value>' +
        '</Eq></Where></Query><RowLimit>50</RowLimit></View>');
        mashupItems = mashupList.getItems(camlQuery);
        clientCtx.load(mashupItems);
        clientCtx.executeQueryAsync(onCurrentMapLoaded, errOnCurrentMapLoaded);
    }
    function onCurrentMapLoaded() {
        var alreadySelected = false;
        listItemEnumerator = mashupItems.getEnumerator();
        while (listItemEnumerator.moveNext()) {
            listItem = listItemEnumerator.get_current();
            if (listItem.get_item('LookupSkills') == selectedSkill) {
                alreadySelected = true;
                break;
            }
        }
        if (!alreadySelected) {
            var listItemInfo = new SP.ListItemCreationInformation();
            var newListItem = mashupList.addItem(listItemInfo);
            newListItem.set_item('LookupJobs', selectedJob);
            newListItem.set_item('LookupSkills', selectedSkill);
            newListItem.update();
            clientCtx.executeQueryAsync(onMappingAdded, errOnMappingAdded);
            return;
        }
        hideSkillMapper();
    }
    function errOnCurrentMapLoaded(sender, args) {
        alert(args.get_message());
    }
    function onMappingAdded(sender, args) {
        hideSkillMapper();
    }
    function errOnMappingAdded(sender, args) {
        alert(args.get_message());
    }
    function onSkillMapped() {
        hideSkillMapper();
    }
    function onCancel() {
        hideSkillMapper();
    }
</script>


