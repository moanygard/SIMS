<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .jsonDiv{
            border-style: solid;
            border-width : thin;
            width: 200px;
            height: 140px;
            margin-left: 20%;
            margin-top: 5%;
            

        }
        .HtmlDiv{
            border-style: solid;
            border-width : thin;
            width: 200px;
            height: 140px;
            margin-left: 20%;
            margin-top: 3%;
           
        }
        .csvFile{
             border-style: solid;
            border-width : thin;
            width: 200px;
            height: 140px;
            margin-left: 20%;
            margin-top: 3%;
        }
        #btnClick {
            width:50%;
            margin-top: 12%;
            margin-left:25%;
            margin-right:25%;
        }
        #btnClick2{
            width:50%;
            margin-top: 2%;
            margin-left:25%;
            margin-right:25%;
        }
        #btnClick3{
            width:60%;
            margin-top: 2%;
            margin-left:22%;
            margin-right:25%;
        }
        p{
            text-align:center;
            font-weight :bold;
            font-size: large;
        }
        h5{
            text-align:center;
        }
        #form1{
            border : 1px solid red;
        }
        .DataOwner{
            border : 1px solid black;
            text-align:center;
            background-color: black;
            color:whitesmoke;
        }
    </style>
</head>
<body>
     <div class="DataOwner">
            <h3>Data Owner Website</h3>
        </div>
    <form id="form1" runat="server">
       


        <div class="jsonDiv">
            <p>JSON</p>
            <asp:Button ID="btnClick" runat="server" Text="Access URL" OnClick="Button1_Click" />
          

        </div>

        <div class="HtmlDiv">
            <p>HTML</p>
            <h5>API-documentation</h5>
             <asp:Button ID="btnClick2" runat="server" Text="Access URL" OnClick="Button2_Click" />
        </div>

        <div class="csvFile">
            <p>CSV</p>
            <h5>CSV-File</h5>
            <asp:Button ID="btnClick3" runat="server" Text="Download Data" OnClick="Button3_Click" />
        </div>
    </form>
</body>
</html>
