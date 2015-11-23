<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepoTest.aspx.cs" Inherits="SharePoint.Repository.Web.Layouts.SharePoint.Repository.Web.RepoTest" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
        <div >
        <div >
            <div >
                <asp:Panel runat="server" ID="panel">
                    <table>
                        <tr>
                            <th>Location</th>
                            <td>
                                <div>
                                    <div>
                                        <div>
                                            <asp:TextBox runat="server" ID="FirstLunch" />
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div>
                    <table id="locationTable">
                        <tr>
                            <th>Location</th>
                            <td>
                                <div>
                                    <div>
                                        <div>
                                            <input type="text" class="txt" />
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <a href="#" class="plus"><span><b>+</b></span></a>
                            </td>
                            <td>
                                <a href="#" class="minus"><span><b>-</b></span></a>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div>
        <asp:HiddenField runat="server" ID="hdnLocations" />
        <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server"><span>Save</span></asp:LinkButton>
        <asp:Repeater ID="lv" runat="server">
            
        </asp:Repeater>

        <asp:Repeater ID="rp" runat="server">
            
        </asp:Repeater>
    </div>
    <script type="text/javascript">
        var locations = new Array();
        var locationArray = new Array();
        var newRow = "<tr>" +
                        "<th>Location</th>" +
                        "<td>" +
                            "<div class='form-item'>" +
                                "<div class='inpt'>" +
                                    "<div class='inpt-cont'>" +
                                        "<input type='text' class='txt' />" +
                                    "</div>" +
                                "</div>" +
                            "</div>" +
                        "</td>" +
                        "<td>" +
							"<a href='#' class='btn-red fr plus'><span>+</span></a>" +
                        "</td>" +
                        "<td>" +
                            "<a href='#' class='btn-red fr minus'><span>-</span></a>" +
						"</td>" +
                    "</tr>";

        $('#locationTable').on('click', '.plus', function (e) {
            e.preventDefault();
            var txt = $(this).closest("tr").find(".txt").val();
            if (txt != undefined && txt != null && txt != "") {
                $("#locationTable").append(newRow);
                locations.push(txt);
                $("#<%=hdnLocations.ClientID%>").val(JSON.stringify(locations));
            }
            else
                alert("enter a location");
        });

        $("#locationTable").on("click", '.minus', function (e) {
            e.preventDefault();
            var txt = $(this).closest("tr").find(".txt").val();
            $.grep(locations, function (val, index) {
                if (txt == val) {
                    locations.pop(val)
                    return;
                }
            });
            $(this).closest("tr").remove();
            $("#<%=hdnLocations.ClientID%>").val(JSON.stringify(locations));
            //console.log("eksiler : " + $(".minus").length);
            console.log(locations);
        });
    </script>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Application Page
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
My Application Page
</asp:Content>
