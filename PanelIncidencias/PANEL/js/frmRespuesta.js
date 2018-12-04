//function ShowPopUp(e) {
//    var ReturnValue = window.showModalDialog("frmRespuestas.aspx", e.paras, "dialogWidth=220px;dialogHeight=120px;scroll:no; status:no;")    
//    alert(ReturnValue);
//}

window.onunload = refreshParent;
function refreshParent() {
    var retVal = confirm("Are you sure ?");
    if( retVal == true ){
        window.opener.location.reload();
    else return false;
 }