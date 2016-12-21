function ULS_SP() {
    if (ULS_SP.caller) {
        ULS_SP.caller.ULSTeamName = "Windows SharePoint Services 4";
        ULS_SP.caller.ULSFileName = "/_layouts/KBx/DocumentLinks.js";
    }
}

Type.registerNamespace('SPSample');

// RibbonApp Page Component
SPSample.PageComponent = function () {
    ULS_SP();
    SPSample.PageComponent.initializeBase(this);
}


SPSample.PageComponent.initialize = function () {
    ULS_SP();
    ExecuteOrDelayUntilScriptLoaded(Function.createDelegate(null, SPSample.PageComponent.initializePageComponent), 'SP.Ribbon.js');
}


SPSample.PageComponent.initializePageComponent = function () {
    ULS_SP();
    var ribbonPageManager = SP.Ribbon.PageManager.get_instance();
    if (null !== ribbonPageManager) {
        ribbonPageManager.addPageComponent(SPSample.PageComponent.instance);
        ribbonPageManager.get_focusManager().requestFocusForComponent(SPSample.PageComponent.instance);
    }
}


SPSample.PageComponent.refreshRibbonStatus = function () {
    SP.Ribbon.PageManager.get_instance().get_commandDispatcher().executeCommand(Commands.CommandIds.ApplicationStateChanged, null);
}


SPSample.PageComponent.prototype = {
    getFocusedCommands: function () {
        ULS_SP();
        return [];
    },
    getGlobalCommands: function () {
        ULS_SP();
        return getGlobalCommands();
    },
    isFocusable: function () {
        ULS_SP();
        return true;
    },
    receiveFocus: function () {
        ULS_SP();
        return true;
    },
    yieldFocus: function () {
        ULS_SP();
        return true;
    },
    canHandleCommand: function (commandId) {
        ULS_SP();
        return commandEnabled(commandId);
    },
    handleCommand: function (commandId, properties, sequence) {
        ULS_SP();
        return handleCommand(commandId, properties, sequence);
    }
}


// Register classes
SPSample.PageComponent.registerClass('SPSample.PageComponent', CUI.Page.PageComponent);
SPSample.PageComponent.instance = new SPSample.PageComponent();


// Notify waiting jobs
NotifyScriptLoadedAndExecuteWaitingJobs("/_layouts/KBx/DocumentLinks.js");

function DeleteLinks() {
    alert("Simulating Delete Links");
}

function ShowContactForm() {
        var oldTab = $("div.activeContainer");
        var seletedTab =  $("div.inactiveContainer");
    
        if ($(oldTab) != null) {
            $(oldTab).fadeOut(400);
            $(oldTab).css({ "display": "none", "visibility": "hidden" });
            $(oldTab).removeClass("activeContainer");
            $(oldTab).addClass("inactiveContainer");
        }

        if ($(seletedTab) != null) {
            $(seletedTab).fadeIn(900).animate({
            }, 900, function () {
                $(seletedTab).css({ "display": "block", "visibility": "visible", "position": "relative" });
                $(seletedTab).removeClass("inactiveContainer");
                $(seletedTab).addClass("activeContainer");
            });
        }
        window.scrollTo(0, 0);
}
