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
    public partial class Test2 : System.Web.UI.Page
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


            com = new SqlCommand("FetchData3", con);

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
                string[] XPointMember = new string[ChartData.Rows.Count];
                double[] YPointMember = new double[ChartData.Rows.Count];

                int count;
                double tCost = 0, pct = 0;

                for (count = 0; count < ChartData.Rows.Count; count++)
                {
                    //storing Values for X axis  
                    XPointMember[count] = (ChartData.Rows[count]["Material"]).ToString();
                    //storing values for Y Axis  
                    YPointMember[count] = Convert.ToDouble(ChartData.Rows[count]["NetCost"]);

                    tCost += YPointMember[count];
                }

                for (count = 0; count < ChartData.Rows.Count; count++)
                {
                    pct = (YPointMember[count] / tCost) * 100;
                    pct = Math.Round(pct, 2, MidpointRounding.AwayFromZero);
                    XPointMember[count] += ", \n" + Convert.ToString(YPointMember[count]) + ", " + Convert.ToString(pct) + "%";
                }

                Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);
                Chart1.Series[0].ChartType = SeriesChartType.Pie;

                Chart1.Series[0].Points[0].Color = Color.Blue;
                Chart1.Series[0].Points[1].Color = Color.Red;
                Chart1.Series[0].Points[2].Color = Color.DarkViolet;
                Chart1.Series[0].Points[3].Color = Color.MediumSeaGreen;
                Chart1.Series[0].Points[4].Color = Color.PowderBlue;
                Chart1.Series[0].Points[5].Color = Color.Orange;

                //Chart1.Series[0].LegendText = "#VALX";
                //Chart1.Series[0].IsVisibleInLegend = true;

                Chart1.Series[0]["PieLabelStyle"] = "outside";
                Chart1.Series[0]["PieLineColor"] = "Gray";
                Chart1.Series[0]["PieStartAngle"] = "270";
                Chart1.ChartAreas[0].Area3DStyle.Enable3D = false;

            }
        }
    }
}