function mostrarPanel()
{
    var pnl = document.getElementById('<%pnlDetalleMensaje.ClientID>');
    if(pnl.style.visibility == "visible")
    {
        pnl.style.visibility = "hidden";
    }
    else if (pnl.style.visibility == "hidden")
    {
        pnl.style.visibility = "visible";
    }
}