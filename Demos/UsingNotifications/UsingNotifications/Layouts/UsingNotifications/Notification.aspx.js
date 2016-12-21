var statusId = '';
var notifyId = '';

function AddNotification() {
    notifyId = SP.UI.Notify.addNotification("Hello World!", true);
}

function RemoveNotification() {
    SP.UI.Notify.removeNotification(notifyId);
    notifyId = '';
}

function AddStatus() {
    statusId = SP.UI.Status.addStatus("Status good!");
    SP.UI.Status.setStatusPriColor(statusId, 'red');
}

function RemoveLastStatus() {
    SP.UI.Status.removeStatus(statusId);
    statusId = '';
}

function RemoveAllStatus() {
    SP.UI.Status.removeAllStatus(true);
}
