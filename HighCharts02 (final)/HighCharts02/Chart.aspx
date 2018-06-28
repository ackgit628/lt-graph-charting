<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="HighCharts02.Chart" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HighCharts</title>

    <%--<script src="http://code.highcharts.com/highcharts.js"></script>
    <script src="http://code.highcharts.com/modules/data.js"></script>
    <script src="http://code.highcharts.com/highcharts-3d.js"></script>
    <script src="http://code.highcharts.com/modules/heatmap.js"></script>
    <script src="http://code.highcharts.com/highcharts-more.js"></script>
    <script src="http://code.highcharts.com/modules/funnel.js"></script>
    <script src="http://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/series-label.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
    <script src="http://code.highcharts.com/modules/heatmap.js"></script>
    <script src="http://code.highcharts.com/modules/treemap.js"></script>
    <script src="http://code.highcharts.com/modules/drilldown.js"></script>
    <script src="http://code.highcharts.com/modules/solid-gauge.js"></script>--%>

    <%--<script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/series-label.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>--%>
    <%--<script src="testStyleSheet1.css"></script>--%>
    <%--<script src="testChart.js"></script>--%>
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
            
            <span id="span1" runat="server" style="width: auto; height: auto;">

                <asp:Chart ID="Chart1" runat="server" Palette="None" PaletteCustomColors="192, 0, 0" Width="300px">
                    <Titles>
                        <asp:Title Name="HMS"></asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1"></asp:Series>
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
                        <asp:Title Name="Nickel"></asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1"></asp:Series>
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
                        <asp:Title Name="FerroMoly"></asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1"></asp:Series>
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
                        <asp:Title Name="LCFerroChrome"></asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1"></asp:Series>
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
                        <asp:Title Name="HCFerroChrome"></asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1"></asp:Series>
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
                        <asp:Title Name="Vanadium"></asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1"></asp:Series>
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
                        <asp:Title Name="Graphite"></asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                    <BorderSkin BackColor="" PageColor="192, 64, 0" />
                </asp:Chart>

            </span>
        
        </div>


        <div style="margin-left: auto; margin-right: auto; text-align: center;">        

            <asp:button id="btnGenerate" Text="Generate PDF" runat="server" onclick="btnGenerate_Click" />

        </div>

    
    </form>
    


      <%--<script type="text/javascript">
        Highcharts.chart('container', {

            title: {
                text: 'Raw Material Costing Sheet'
            },

            //subtitle: {
            //    text: 'Source: thesolarfoundation.com'
            //},
            series: [{
                name: 'Series1',
                data: []
            }, {
                name: 'Series2',
                data: []
            }, {
                name: 'Series3',
                data: []
            }],

            yAxis: {
                title: {
                    text: 'Price (INR/kg)'
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle'
            },

            responsive: {
                rules: [{
                    condition: {
                        maxWidth: 500
                    },
                    chartOptions: {
                        legend: {
                            layout: 'horizontal',
                            align: 'center',
                            verticalAlign: 'bottom'
                        }
                    }
                }]
            }
        });
    </script>--%>


</body>
</html>
