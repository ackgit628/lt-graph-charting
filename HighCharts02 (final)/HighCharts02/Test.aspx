<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="HighCharts02.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container" style="width: auto; height: auto;">
        <asp:Label runat="server" ID="lblAxistype"></asp:Label>

        <asp:Chart ID="Chart1" runat="server" Palette="None" PaletteCustomColors="192, 0, 0" Height="600px" Width="800px">
            <Titles>
                <asp:Title Name="HMS" Text="      HMS Alang Prices (INR/kg)" Font="Tahoma, 8.5pt, style=Bold"></asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Series1" ChartType="Line" Color="Blue" ToolTip="#VALX, #VALY"></asp:Series>
            </Series>
            <%--<Legends>
                <asp:Legend Name="LegendChart1" Alignment="Center" Title="Raw Material"></asp:Legend>
            </Legends>--%>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
            </ChartAreas>            
            <BorderSkin BackColor="" PageColor="192, 64, 0" />
        </asp:Chart>

    </div>
    </form>
</body>
</html>
