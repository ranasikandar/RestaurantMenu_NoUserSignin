<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TermsAndConditions.ascx.cs" Inherits="RestaurantMenu.Menu.UC.TermsAndConditions" %>
<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header">
            <h4 class="modal-title"><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> Terms And Conditions</h4>
            <button type="button" class="close" data-dismiss="modal">
                <span aria-hidden="true">×</span>
                <span class="sr-only">close</span>
            </button>
        </div>
        <div class="modal-body">
            <p>Terms And Conditions are beeing updated Please stay tune for updates Thanks.</p>
            <p>Terms And Conditions are beeing updated Please stay tune for updates Thanks.</p>
            <p>Terms And Conditions are beeing updated Please stay tune for updates Thanks.</p>
            <p>Terms And Conditions are beeing updated Please stay tune for updates Thanks.</p>
            <p>Terms And Conditions are beeing updated Please stay tune for updates Thanks.</p>
            <p>Terms And Conditions are beeing updated Please stay tune for updates Thanks.</p>
            <p>Terms And Conditions are beeing updated Please stay tune for updates Thanks.</p>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-shadow" data-dismiss="modal">Close</button>
        </div>
    </div>
</div>
