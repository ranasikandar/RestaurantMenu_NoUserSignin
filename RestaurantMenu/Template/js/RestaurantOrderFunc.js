
function setCancelAccept(theID) {
    if (theID === "") {
        alertMe('information', 'Please Select a Valid Order', 'center', 2000);
        return false;
    }
    else {
        var x = document.getElementById("CAOrd_" + theID);
        PageMethods.set_path('<%: ResolveUrl("~/Restaurant/Orders.aspx")%>')
        PageMethods.MarkCancel(theID, NotiMarkCancel, NotiFail, theID);
    }
}

function NotiMarkCancel(response, context, MarkCancel) {
    if (response) {
        alertMe('information', response, 'center', 3000);
        var x = document.getElementById("CAOrd_" + context);
        x.hidden = true;
        var y = document.getElementById("OrdDiv" + context);
        y.hidden = true;
    }
}

///
function setPaid(theID) {
    if (theID === "") {
        alertMe('information', 'Please Select a Valid Order', 'center', 2000);
        return false;
    }
    else {
        var x = document.getElementById("PaidOrd_" + theID);
        PageMethods.set_path('<%: ResolveUrl("~/Restaurant/Orders.aspx")%>')
        PageMethods.MarkPaid(theID, NotiMarkPaid, NotiFail, theID);
    }
}

function NotiMarkPaid(response, context, MarkPaid) {
    if (response) {
        alertMe('information', response, 'center', 3000);
        var x = document.getElementById("PaidOrd_" + context);
        x.hidden = true;
        var y = document.getElementById("OrdDiv" + context);
        y.hidden = true;
    }
}

///
function setPrepare(theID) {
    if (theID === "") {
        alertMe('information', 'Please Select a Valid Order', 'center', 2000);
        return false;
    }
    else {
        var x = document.getElementById("PreOrd_" + theID);
        if (x.classList.value === 'ion ion-bonfire la-2x pointer') {
            PageMethods.set_path('<%: ResolveUrl("~/Restaurant/Orders.aspx")%>')
            PageMethods.MarkPrepare(theID, NotiMarkPrepare, NotiFail, theID);
        } else {
            PageMethods.set_path('<%: ResolveUrl("~/Restaurant/Orders.aspx")%>')
            PageMethods.MarkUnPrepare(theID, NotiMarkUnPrepare, NotiFail, theID);
        }
    }
}

function NotiMarkPrepare(response, context, MarkPrepare) {
    if (response) {
        alertMe('information', response, 'center', 3000);
        var x = document.getElementById("PreOrd_" + context);
        x.classList.value = 'la la-cutlery la-2x pointer';
        x.dataset.originalTitle = "Order UnPrepare and set as Placed";
    }
}

function NotiMarkUnPrepare(response, context, MarkUnPrepare) {
    if (response) {
        alertMe('information', response, 'center', 3000);
        var x = document.getElementById("PreOrd_" + context);
        x.classList.value = 'ion ion-bonfire la-2x pointer';
        x.dataset.originalTitle = "Order Prepared";
    }
}
///
function NotiFail(error) {
    alertMe('error', error, 'center', 5000);
    console.error();
}