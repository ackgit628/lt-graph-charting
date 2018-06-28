<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="finalPdf.aspx.cs" Inherits="HighCharts02.finalPdf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
                        
        
        <%--<iframe src="newPdf.aspx" style="height:200%; width:100%;"></iframe>--%>
        
        <div style="margin-left: auto; margin-right: auto; text-align: center;">
        
            <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>

        </div>

        <div id="container" style="width: auto; height: auto;">

            <asp:Chart ID="Chart8" runat="server" Palette="None" PaletteCustomColors="192, 0, 0" Height="400px" Width="600px">
                <Titles>
                    <asp:Title Name="Purchases" Font="Tahoma, 8.5pt, style=Bold"></asp:Title>
                </Titles>
                <Series>
                    <asp:Series Name="Series1" ToolTip="#VALX"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>            
                <BorderSkin BackColor="" PageColor="192, 64, 0" />
            </asp:Chart>


            <asp:Repeater id="rep1" runat="server" OnItemDataBound="rep1_ItemDataBound" >

                <ItemTemplate>
                    
                    <span id="Span1" runat="server" style="width:auto; height:auto">
                        <asp:Label Text='<%#Bind("RawMaterialID") %>' ID="lblMetID" runat="server" Visible="false" />
                        <asp:Label Text='<%#Bind("t_dsca") %>' ID="lblMatDesc" runat="server" Visible="false"/>
                        <asp:Label Text='<%#Bind("Source") %>' ID="lblMatSource" runat="server" Visible="false"/>
                        <asp:Label Text='<%#Bind("Color") %>' ID="lblMatColor" runat="server" Visible="false"/>
                    </span>

                    <asp:Chart ID="rep_chart" runat="server" Palette="None" PaletteCustomColors="192, 0, 0" Height="400px" Width="400px">
                        <Titles>
                            <asp:Title Name="Title1"></asp:Title>
                        </Titles>
                        <Series>
                            <asp:Series Name="Series1"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>            
                        <BorderSkin BackColor="" PageColor="192, 64, 0" />
                    </asp:Chart>

                </ItemTemplate>

            </asp:Repeater>

        </div>
    </form>
</body>
</html>
