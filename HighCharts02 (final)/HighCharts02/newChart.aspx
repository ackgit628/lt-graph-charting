<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newChart.aspx.cs" Inherits="HighCharts02.newChart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Price Trend - Major Raw Materials</title>
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

                    <asp:Chart ID="rep_chart" runat="server" Palette="None" PaletteCustomColors="192, 0, 0" Width="300px">
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

            
           <%-- <span id="span1" runat="server" style="width: auto; height: auto;">

                <asp:Chart ID="Chart1" runat="server" Palette="None" PaletteCustomColors="192, 0, 0" Width="300px">
                    <Titles>
                        <asp:Title Name="HMS" Font="Tahoma, 8.5pt, style=Bold"></asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" ChartType="Line" Color="Blue" ToolTip="#VALX, #VALY"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>            
                    <BorderSkin BackColor="" PageColor="192, 64, 0" />
                </asp:Chart>

            </span>

            <span id="span2" runat="server" style="width: auto; height: auto;">

                <asp:Chart ID="Chart2" runat="server" Height="300px" Palette="None" PaletteCustomColors="192, 0, 0" Width="300px">
                    <Titles>
                        <asp:Title Name="Nickel" Font="Tahoma, 8.5pt, style=Bold"></asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" ChartType="Line" Color="BlueViolet" ToolTip="#VALX, #VALY"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                    <BorderSkin BackColor="" PageColor="192, 64, 0" />
                </asp:Chart>

            </span>
            
            <span id="span3" runat="server" style="width: auto; height: auto;">
            
                <asp:Chart ID="Chart3" runat="server" Height="300px" Palette="None" PaletteCustomColors="192, 0, 0" Width="300px">
                    <Titles>
                        <asp:Title Name="FerroMoly" Font="Tahoma, 8.5pt, style=Bold"></asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" ChartType="Line" Color="Chocolate" ToolTip="#VALX, #VALY"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                    <BorderSkin BackColor="" PageColor="192, 64, 0" />
                </asp:Chart>

            </span>

            <span id="span4" runat="server" style="width: auto; height: auto;">

                <asp:Chart ID="Chart4" runat="server" Height="300px" Palette="None" PaletteCustomColors="192, 0, 0" Width="300px">
                    <Titles>
                        <asp:Title Name="LCFerroChrome" Font="Tahoma, 8.5pt, style=Bold"></asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" ChartType="Line" Color="DeepPink" ToolTip="#VALX, #VALY"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                    <BorderSkin BackColor="" PageColor="192, 64, 0" />
                </asp:Chart>

            </span>
        
            <span id="span5" runat="server" style="width: auto; height: auto;">

                <asp:Chart ID="Chart5" runat="server" Height="300px" Palette="None" PaletteCustomColors="192, 0, 0" Width="300px">
                    <Titles>
                        <asp:Title Name="HCFerroChrome" Font="Tahoma, 8.5pt, style=Bold"></asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" ChartType="Line" Color="Firebrick" ToolTip="#VALX, #VALY"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                    <BorderSkin BackColor="" PageColor="192, 64, 0" />
                </asp:Chart>

            </span>

            <span id="span6" runat="server" style="width: auto; height: auto;">

                <asp:Chart ID="Chart6" runat="server" Height="300px" Palette="None" PaletteCustomColors="192, 0, 0" Width="300px">
                    <Titles>
                        <asp:Title Name="Vanadium" Font="Tahoma, 8.5pt, style=Bold"></asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" ChartType="Line" Color="Green" ToolTip="#VALX, #VALY"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                    <BorderSkin BackColor="" PageColor="192, 64, 0" />
                </asp:Chart>     

            </span>

            <span id="span7" runat="server" style="width: auto; height: auto;">

                <asp:Chart ID="Chart7" runat="server" Height="300px" Palette="None" PaletteCustomColors="192, 0, 0" Width="300px">
                    <Titles>
                        <asp:Title Name="Graphite" Font="Tahoma, 8.5pt, style=Bold"></asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" ChartType="Line" Color="MediumOrchid" ToolTip="#VALX, #VALY"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                    <BorderSkin BackColor="" PageColor="192, 64, 0" />
                </asp:Chart>

            </span>
        --%>
        </div>


        <div style="margin-left: auto; margin-right: auto; text-align: center;">        

            <asp:button id="btnGenerate" Text="Generate PDF" runat="server" onclick="btnGenerate_Click" />

        </div>

    
    </form>

</body>
</html>
