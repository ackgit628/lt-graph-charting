using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace HighCharts02
{
    public partial class Test : System.Web.UI.Page
    {
        private SqlConnection con;
        private SqlCommand com;
        private string constr;

        private void connection()
        {
            constr = ConfigurationManager.ConnectionStrings["ManthanConStr"].ConnectionString;
            con = new SqlConnection(constr);
            con.Open();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindchart();
            }
        }

        private void Bindchart()
        {
            connection();


            com = new SqlCommand("FetchData2", con);

            com.CommandType = CommandType.StoredProcedure;
            //SqlParameter[] sqlParams = {
            //    new SqlParameter("@startDate",startDate),
            //    new SqlParameter("@endDate",endDate)
            //};
            //com.Parameters.AddRange(sqlParams);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);


            for (int i = 0; i < ds.Tables.Count; i++)
            {
                DataTable ChartData = ds.Tables[i];

                //storing total rows count to loop on each Record  
                DateTime[] XPointMember = new DateTime[ChartData.Rows.Count];
                double[] YPointMember = new double[ChartData.Rows.Count];

                int count;
                double minPt = 99999999, maxPt = 0;

                for (count = 0; count < ChartData.Rows.Count; count++)
                {
                    //storing Values for X axis  
                    XPointMember[count] = Convert.ToDateTime(ChartData.Rows[count]["Date"]);
                    //storing values for Y Axis  
                    YPointMember[count] = Convert.ToDouble(ChartData.Rows[count]["Rate"]);

                    if (minPt > YPointMember[count])
                        minPt = YPointMember[count];
                    if (maxPt < YPointMember[count])
                        maxPt = YPointMember[count];

                }

                switch (i)
                {
                    case 0:
                        Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);
                        //Chart1.Series[0].LegendText = "HMS Alang";
                        //Chart1.Series[0].ToolTip = "Current Value = " + YPointMember[ChartData.Rows.Count - 1].ToString() + " Rs/kg";
                        Chart1.Series[0].Points[count - 1].IsValueShownAsLabel = true;
                        Chart1.Series[0].Points[count - 1].LabelBackColor = Color.White;
                        Chart1.Series[0].Points[count - 1].LabelBorderColor = Color.Black;
                        Chart1.Series[0].Points[count - 1].LabelBorderWidth = 1;
                        Chart1.Series[0].Points[count - 1].Font = new System.Drawing.Font("Tahoma", 10f);
                        Chart1.Series[0].IsVisibleInLegend = false;
                        Chart1.Series[0].BorderWidth = 1;
                        Chart1.Series[0].ChartType = SeriesChartType.Line;
                        Chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
                        //Chart1.Series[0].SmartLabelStyle.Enabled = true;
                        //Chart1.Series[0].SmartLabelStyle.CalloutStyle = LabelCalloutStyle.Box;
                        //Chart1.Series[0].SmartLabelStyle.
                        //Chart1.Series[0].ArgumentScaleType = ScaleType.DateTime;
                        //Chart1.Series[0].ValueScaleType = ScaleType.Numerical;
                        Axis x1 = Chart1.ChartAreas[0].AxisX;
                        Axis y1 = Chart1.ChartAreas[0].AxisY;
                        x1.Title = "Source :- Steel Mint";
                        x1.IntervalType = DateTimeIntervalType.Months;
                        x1.Interval = 1;
                        x1.IntervalAutoMode = IntervalAutoMode.FixedCount;
                        x1.MajorGrid.Enabled = false;
                        y1.MajorGrid.LineColor = Color.LightGray;
                        y1.Minimum = RoundDown(Convert.ToInt32(0.9 * minPt));
                        //y1.Maximum = RoundUp(Convert.ToInt32(1.05 * maxPt));
                        y1.RoundAxisValues();
                        //x1
                        //Chart1.DataManipulator.InsertEmptyPoints(1, IntervalType.Months, "Series1");
                        
                        break;
                }
            }
        }

        int RoundDown(int toRound)
        {
            return toRound - toRound % 10;
        }
    }
}