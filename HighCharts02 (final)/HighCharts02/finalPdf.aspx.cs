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
    public partial class finalPdf : System.Web.UI.Page
    {
        private SqlConnection con;
        private SqlCommand com;
        private string constr;

        private void connection()
        {
            constr = ConfigurationManager.ConnectionStrings["CRMConStr"].ConnectionString;
            con = new SqlConnection(constr);
            con.Open();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMaterialData();
                //Bindchart();
            }
        }

        private void BindMaterialData()
        {
            try
            {
                connection();
                com = new SqlCommand("sp_ChartItems", con);

                com.CommandType = CommandType.StoredProcedure;
                //SqlParameter[] sqlParams = {
                //    new SqlParameter("@startDate",startDate),
                //    new SqlParameter("@endDate",endDate)
                //};
                //com.Parameters.AddRange(sqlParams);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();


                DateTime endDate = DateTime.Now;
                DateTime startDate = endDate.AddYears(-1);
                string header = "Price Trend - Major Raw Materials (" + startDate.ToString("MMM-yy") + " - " + startDate.ToString("MMM-yy") + ")";

                Label1.Text = header;

                chtPurchases(startDate, endDate);


                rep1.DataSource = dt;
                rep1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        int RoundDown(int toRound)
        {
            return toRound - toRound % 10;
        }

        private void chtPurchases(DateTime startDate, DateTime endDate)
        {
            connection();

            com = new SqlCommand("sp_ChartPurchases", con);

            com.CommandType = CommandType.StoredProcedure;
            //SqlParameter[] sqlParams = {
            //    new SqlParameter("@startDate",startDate),
            //    new SqlParameter("@endDate",endDate)
            //};
            //com.Parameters.AddRange(sqlParams);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);


            //storing total rows count to loop on each Record  
            string[] XPointMember = new string[dt.Rows.Count];
            double[] YPointMember = new double[dt.Rows.Count];

            int count;
            double tCost = 0, pct = 0;

            for (count = 0; count < dt.Rows.Count; count++)
            {
                //storing Values for X axis  
                XPointMember[count] = (dt.Rows[count]["Material"]).ToString();
                //storing values for Y Axis  
                YPointMember[count] = Convert.ToDouble(dt.Rows[count]["NetCost"]);

                tCost += YPointMember[count];
            }

            for (count = 0; count < dt.Rows.Count; count++)
            {
                pct = (YPointMember[count] / tCost) * 100;
                pct = Math.Round(pct, 2, MidpointRounding.AwayFromZero);
                XPointMember[count] += ", \n" + Convert.ToString(YPointMember[count]) + ", " + Convert.ToString(pct) + "%";
            }

            string fy = "FY " + startDate.ToString("yyyy") + "-" + endDate.ToString("yy");

            Chart8.Titles[0].Text = "Raw Material Purchases " + fy + " (INR Lakhs)";
            Chart8.Titles[0].Font = new System.Drawing.Font("Tahoma", 8.5f, FontStyle.Bold);

            Chart8.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            Chart8.Series[0].ChartType = SeriesChartType.Pie;

            Chart8.Series[0].Points[0].Color = Color.Blue;
            Chart8.Series[0].Points[1].Color = Color.Red;
            Chart8.Series[0].Points[2].Color = Color.DarkViolet;
            Chart8.Series[0].Points[3].Color = Color.MediumSeaGreen;
            Chart8.Series[0].Points[4].Color = Color.PowderBlue;
            Chart8.Series[0].Points[5].Color = Color.Orange;

            //Chart8.Series[0].LegendText = "#VALX";
            //Chart8.Series[0].IsVisibleInLegend = true;

            Chart8.Series[0]["PieLabelStyle"] = "outside";
            Chart8.Series[0]["PieLineColor"] = "Gray";
            Chart8.Series[0]["PieStartAngle"] = "270";
            Chart8.ChartAreas[0].Area3DStyle.Enable3D = false;

            con.Close();

        }

        protected void rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                connection();
                Label lblMat = (Label)e.Item.FindControl("lblMetID");
                Label lblDesc = (Label)e.Item.FindControl("lblMatDesc");
                Label lblSource = (Label)e.Item.FindControl("lblMatSource");
                Label lblColor = (Label)e.Item.FindControl("lblMatColor");
                int MatID = Convert.ToInt32(lblMat.Text);

                DateTime endDate = DateTime.Now;
                DateTime startDate = endDate.AddYears(-1);
                com = new SqlCommand("sp_ChartMaterial", con);

                com.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParams = {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate),
                new SqlParameter("@materialID", MatID)
            };
                com.Parameters.AddRange(sqlParams);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);


                //storing total rows count to loop on each Record  
                DateTime[] XPointMember = new DateTime[dt.Rows.Count];
                double[] YPointMember = new double[dt.Rows.Count];

                int count;
                double minPt = 99999999, maxPt = 0;

                for (count = 0; count < dt.Rows.Count; count++)
                {
                    //storing Values for X axis  
                    XPointMember[count] = Convert.ToDateTime(dt.Rows[count]["Date"]);
                    //storing values for Y Axis  
                    YPointMember[count] = Convert.ToDouble(dt.Rows[count]["RateUnit"]);

                    if (minPt > YPointMember[count])
                        minPt = YPointMember[count];
                    if (maxPt < YPointMember[count])
                        maxPt = YPointMember[count];
                }

                //Span
                //Span1.Style.Add("display", "none");


                System.Web.UI.DataVisualization.Charting.Chart Chart1 = (System.Web.UI.DataVisualization.Charting.Chart)e.Item.FindControl("rep_chart");

                Chart1.Titles[0].Text = lblDesc.Text + " (INR/kg)";
                Chart1.Titles[0].Font = new System.Drawing.Font("Tahoma", 8.5f, FontStyle.Bold);

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
                Chart1.Series[0].Color = Color.FromName(Convert.ToString(lblColor.Text));
                Chart1.Series[0].ToolTip = Convert.ToString("#VALX, #VALY");
                Chart1.Series[0].ChartType = SeriesChartType.Line;
                Chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
                Axis x1 = Chart1.ChartAreas[0].AxisX;
                Axis y1 = Chart1.ChartAreas[0].AxisY;
                x1.Title = "Source :- " + lblSource.Text;
                x1.IntervalType = DateTimeIntervalType.Months;
                x1.Interval = 2;
                x1.IntervalAutoMode = IntervalAutoMode.FixedCount;
                x1.MajorGrid.Enabled = false;
                y1.MajorGrid.LineColor = Color.LightGray;
                y1.Minimum = RoundDown(Convert.ToInt32(0.95 * minPt));
                //y1.Maximum = RoundUp(Convert.ToInt32(1.05 * maxPt));

                con.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}