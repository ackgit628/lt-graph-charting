using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HighCharts02
{
    public partial class Chart : System.Web.UI.Page
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
                Bindchart();
            }
        }

        //private DataSet GetChartData(string startDate, string endDate)
        //{
        //    DataSet dt = null;
        //    try
        //    {

        //        dt = new DataSet();
        //        dt.Tables[0].Columns.Add("Date", Type.GetType("System.String"));
        //        dt.Tables[0].Columns.Add("Price", Type.GetType("System.Int32"));
        //        DataTable data = objRuleDAL.GetDashboardData(startDate, endDate);
        //        if (data != null && data.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < data.Rows.Count; i++)
        //            {
        //                DataRow dr = dt.NewRow();
        //                dr["DeptName"] = data.Rows[i]["DeptName"];
        //                dr["TotalCompliance"] = data.Rows[i]["Total"];
        //                dr["Complied"] = data.Rows[i]["complied"];
        //                dt.Rows.Add(dr);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + ex.Message + "');", true);
        //    }
        //    return dt;
        //}

        //private void LoadChartData(DataTable initialDataSource)
        //{
        //    try
        //    {
        //        if (initialDataSource != null && initialDataSource.Rows.Count > 0)
        //        {
        //            Chart1.Visible = true;
        //            for (int i = 1; i < initialDataSource.Columns.Count; i++)
        //            {

        //                foreach (DataRow dr in initialDataSource.Rows)
        //                {
        //                    int y = (int)dr[i];

        //                }

        //            }
        //        }
        //        else
        //        {
        //            Chart1.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + ex.Message + "');", true);
        //    }
        //}

        //public string TextPattern { get; set; }

        private void Bindchart()
        {
            connection();

            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddYears(-1);
            string header = "Price Trend - Major Raw Materials (" + startDate.ToString("MMM-yy") + " - " + startDate.ToString("MMM-yy") + ")";

            Label1.Text = header;


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


            string[] item = new string[dt.Rows.Count];
            string[] source = new string[dt.Rows.Count];

            int count;
            int[] itemCnt = { 0, 0, 0, 0, 0, 0, 0 };       //counters to check if an item exists in table  itemCnt[0] = cHMS, cNi, cFeMo 
                                                           //                                              itemCnt[3] = cLCFe, cHCFe, cFeV, cGr

            for (count = 0; count < dt.Rows.Count; count++)
            {
                //storing Values for item  
                item[count] = (dt.Rows[count]["t_dsca"]).ToString();
                //storing values for source  
                source[count] = (dt.Rows[count]["Source"]).ToString();
            }

            for (count = 0; count < dt.Rows.Count; count++)                         //setting count to check if item exists in table
            {
                if (item[count].Equals("HEAVY MELTING SCRAP - ALANG") && source[count].Length > 0)
                    itemCnt[0]++;

                if (item[count].Equals("NICKEL (Ni>99.8%)") && source[count].Length > 0)
                    itemCnt[1]++;

                if (item[count].Equals("Ferro Moly @ 60% Mo") && source[count].Length > 0)
                    itemCnt[2]++;

                if (item[count].Equals("Ferro Chrome (LC) @ 65% Cr") && source[count].Length > 0)
                    itemCnt[3]++;

                if (item[count].Equals("Ferro Chrome (HC) @ 65% Cr") && source[count].Length > 0)
                    itemCnt[4]++;

                if (item[count].Equals("Ferro Vanadium @ 80% V") && source[count].Length > 0)
                    itemCnt[5]++;

                if (item[count].Equals("Graphite Electrode") && source[count].Length > 0)
                    itemCnt[6]++;
            }


            chtPurchases(startDate, endDate);

            

            if (itemCnt[0] > 0)                                                 //HMS     //checking if chart should be displayed
                chtHMS(startDate, endDate, item[10], source[10]);               //since hms raw material id is 11 (known to me). same for other elements
            else
                span1.Style.Add("display", "none");

            if (itemCnt[1] > 0)                                                //Nickel
                chtNickel(startDate, endDate, item[8], source[8]);
            else
                span2.Style.Add("display", "none");

            if (itemCnt[2] > 0)                                                //Ferro Moly
                chtFeMo(startDate, endDate, item[4], source[4]);
            else
                span3.Style.Add("display", "none");

            if (itemCnt[3] > 0)                                                //LC FeCr
                chtLCFeCr(startDate, endDate, item[3], source[3]);
            else
                span4.Style.Add("display", "none");

            if (itemCnt[4] > 0)                                                //HC FeCr
                chtHCFeCr(startDate, endDate, item[2], source[2]);
            else
                span5.Style.Add("display", "none");

            if (itemCnt[5] > 0)                                                //FerroV
                chtFeV(startDate, endDate, item[7], source[7]);
            else
                span6.Style.Add("display", "none");

            if (itemCnt[6] > 0)                                                //Graphite
                chtGraphite(startDate, endDate, item[19], source[19]);
            else
                span7.Style.Add("display", "none");


            
            con.Close();

        }

        //int RoundUp(int toRound)
        //{
        //    if (toRound % 10 == 0) return toRound;
        //    return (10 - toRound % 10) + toRound;
        //}

        int RoundDown(int toRound)
        {
            return toRound - toRound % 10;
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {

            // fileName in required format
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddYears(-1);
            string endDateStr = endDate.ToString("MMM-yy");
            string startDateStr = startDate.ToString("MMM-yy");

            string fileName = "PriceTrend(" + startDateStr + "-" + endDateStr + ").pdf";

            //string fileName = "Tst.pdf";
            /*
            Document pdfDoc = new Document(PageSize.A3, 10f, 0f, 10f, 0f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            using (MemoryStream stream = new MemoryStream())
            {
                Chart1.SaveImage(stream, ChartImageFormat.Png);
                iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                chartImage.ScalePercent(75f);
                pdfDoc.Add(chartImage);

                //Chart2.SaveImage(stream, ChartImageFormat.Png);
                //chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                //chartImage.ScalePercent(75f);
                //pdfDoc.Add(chartImage);
                pdfDoc.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }*/



            //downloads path directory
            //string downloadsPath = Server.MapPath("~/"); ;

            //url
            string[] url = new string[] { "http://localhost:54554/newPdf.aspx" };

            //pdf options
            //string userName =  "--username"  + ConfigurationManager.AppSettings[PdfUserName];
            //string password =  "--password"  + ConfigurationManager.AppSettings[PdfPassword];
            //string footerFile =  /*--footer-html  + */ConfigurationManager.AppSettings[url] /*+ ConfigurationManager.AppSettings[FooterFile]*/;
            string[] options = new string[] { "--javascript-delay 1650", "--print-media-type", "--orientation Landscape", "--page-size A3"}; //, userName, password };

            string pdfHtmlToPdfExePath = "wkhtmltopdf\\bin\\wkhtmltopdf.exe";

            PdfGenerator.HtmlToPdf(@"~\", fileName, url, options, pdfHtmlToPdfExePath);
            
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

        private void chtHMS(DateTime startDate, DateTime endDate, string item, string source)
        {
            connection();

            com = new SqlCommand("sp_ChartMaterial", con);

            com.CommandType = CommandType.StoredProcedure;
            SqlParameter[] sqlParams = {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate),
                new SqlParameter("@materialID", 11)
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

            Chart1.Titles[0].Text = item + " (INR/kg)";
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
            Chart1.Series[0].Color = Color.Blue;
            Chart1.Series[0].ToolTip = Convert.ToString("#VALX, #VALY");
            Chart1.Series[0].ChartType = SeriesChartType.Line;
            Chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
            Axis x1 = Chart1.ChartAreas[0].AxisX;
            Axis y1 = Chart1.ChartAreas[0].AxisY;
            x1.Title = "Source :- " + source;
            x1.IntervalType = DateTimeIntervalType.Months;
            x1.Interval = 2;
            x1.IntervalAutoMode = IntervalAutoMode.FixedCount;
            x1.MajorGrid.Enabled = false;
            y1.MajorGrid.LineColor = Color.LightGray;
            y1.Minimum = RoundDown(Convert.ToInt32(0.95 * minPt));
            //y1.Maximum = RoundUp(Convert.ToInt32(1.05 * maxPt));

            con.Close();
        }

        private void chtNickel(DateTime startDate, DateTime endDate, string item, string source)
        {
            connection();

            com = new SqlCommand("sp_ChartMaterial", con);

            com.CommandType = CommandType.StoredProcedure;
            SqlParameter[] sqlParams = {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate),
                new SqlParameter("@materialID", 9)
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

            Chart2.Titles[0].Text = item + " (INR/kg)";
            Chart2.Titles[0].Font = new System.Drawing.Font("Tahoma", 8.5f, FontStyle.Bold);

            Chart2.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            //Chart2.Series[0].LegendText = "LME Nickel";
            //Chart2.Series[0].ToolTip = "Current Value = " + YPointMember[ChartData.Rows.Count - 1].ToString() + " Rs/kg";
            Chart2.Series[0].Points[count - 1].IsValueShownAsLabel = true;
            Chart2.Series[0].Points[count - 1].LabelBackColor = Color.White;
            Chart2.Series[0].Points[count - 1].LabelBorderColor = Color.Black;
            Chart2.Series[0].Points[count - 1].LabelBorderWidth = 1;
            Chart2.Series[0].Points[count - 1].Font = new System.Drawing.Font("Tahoma", 10f);
            Chart2.Series[0].IsVisibleInLegend = false;
            Chart2.Series[0].BorderWidth = 1;
            Chart2.Series[0].Color = Color.BlueViolet;
            Chart2.Series[0].ToolTip = Convert.ToString("#VALX, #VALY");
            Chart2.Series[0].ChartType = SeriesChartType.Line;
            Chart2.Series[0].MarkerStyle = MarkerStyle.Circle;
            Axis x2 = Chart2.ChartAreas[0].AxisX;
            Axis y2 = Chart2.ChartAreas[0].AxisY;
            x2.Title = "Source :- " + source;
            x2.IntervalType = DateTimeIntervalType.Months;
            x2.Interval = 2;
            x2.IntervalAutoMode = IntervalAutoMode.FixedCount;
            x2.MajorGrid.Enabled = false;
            y2.MajorGrid.LineColor = Color.LightGray;
            y2.Minimum = RoundDown(Convert.ToInt32(0.95 * minPt));
            //y2.Maximum = RoundUp(Convert.ToInt32(1.05 * maxPt));

            con.Close();
        }

        private void chtFeMo(DateTime startDate, DateTime endDate, string item, string source)
        {
            connection();

            com = new SqlCommand("sp_ChartMaterial", con);

            com.CommandType = CommandType.StoredProcedure;
            SqlParameter[] sqlParams = {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate),
                new SqlParameter("@materialID", 5)
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

            Chart3.Titles[0].Text = item + " (INR/kg)";
            Chart3.Titles[0].Font = new System.Drawing.Font("Tahoma", 8.5f, FontStyle.Bold);

            Chart3.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            //Chart3.Series[0].LegendText = "Ferro Moly";
            //Chart3.Series[0].ToolTip = "Current Value = " + YPointMember[ChartData.Rows.Count - 1].ToString() + " Rs/kg";
            Chart3.Series[0].Points[count - 1].IsValueShownAsLabel = true;
            Chart3.Series[0].Points[count - 1].LabelBackColor = Color.White;
            Chart3.Series[0].Points[count - 1].LabelBorderColor = Color.Black;
            Chart3.Series[0].Points[count - 1].LabelBorderWidth = 1;
            Chart3.Series[0].Points[count - 1].Font = new System.Drawing.Font("Tahoma", 10f);
            Chart3.Series[0].IsVisibleInLegend = false;
            Chart3.Series[0].BorderWidth = 1;
            Chart3.Series[0].Color = Color.Chocolate;
            Chart3.Series[0].ToolTip = Convert.ToString("#VALX, #VALY");
            Chart3.Series[0].ChartType = SeriesChartType.Line;
            Chart3.Series[0].MarkerStyle = MarkerStyle.Circle;
            Axis x3 = Chart3.ChartAreas[0].AxisX;
            Axis y3 = Chart3.ChartAreas[0].AxisY;
            x3.Title = "Source :- " + source;
            x3.IntervalType = DateTimeIntervalType.Months;
            x3.Interval = 2;
            x3.IntervalAutoMode = IntervalAutoMode.FixedCount;
            x3.MajorGrid.Enabled = false;
            y3.MajorGrid.LineColor = Color.LightGray;
            y3.Minimum = RoundDown(Convert.ToInt32(0.95 * minPt));
            //y3.Maximum = RoundUp(Convert.ToInt32(1.05 * maxPt));

            con.Close();
        }

        private void chtLCFeCr(DateTime startDate, DateTime endDate, string item, string source)
        {
            connection();

            com = new SqlCommand("sp_ChartMaterial", con);

            com.CommandType = CommandType.StoredProcedure;
            SqlParameter[] sqlParams = {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate),
                new SqlParameter("@materialID", 4)
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

            Chart4.Titles[0].Text = item + " (INR/kg)";
            Chart4.Titles[0].Font = new System.Drawing.Font("Tahoma", 8.5f, FontStyle.Bold);

            Chart4.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            //Chart4.Series[0].LegendText = "LC Ferro\nChrome";
            //Chart4.Series[0].ToolTip = "Current Value = " + YPointMember[ChartData.Rows.Count - 1].ToString() + " Rs/kg";
            Chart4.Series[0].Points[count - 1].IsValueShownAsLabel = true;
            Chart4.Series[0].Points[count - 1].LabelBackColor = Color.White;
            Chart4.Series[0].Points[count - 1].LabelBorderColor = Color.Black;
            Chart4.Series[0].Points[count - 1].LabelBorderWidth = 1;
            Chart4.Series[0].Points[count - 1].Font = new System.Drawing.Font("Tahoma", 10f);
            Chart4.Series[0].IsVisibleInLegend = false;
            Chart4.Series[0].BorderWidth = 1;
            Chart4.Series[0].Color = Color.DeepPink;
            Chart4.Series[0].ToolTip = Convert.ToString("#VALX, #VALY");
            Chart4.Series[0].ChartType = SeriesChartType.Line;
            Chart4.Series[0].MarkerStyle = MarkerStyle.Circle;
            Axis x4 = Chart4.ChartAreas[0].AxisX;
            Axis y4 = Chart4.ChartAreas[0].AxisY;
            x4.Title = "Source :- " + source;
            x4.IntervalType = DateTimeIntervalType.Months;
            x4.Interval = 2;
            x4.IntervalAutoMode = IntervalAutoMode.FixedCount;
            x4.MajorGrid.Enabled = false;
            y4.MajorGrid.LineColor = Color.LightGray;
            y4.Minimum = RoundDown(Convert.ToInt32(0.95 * minPt));
            //y4.Maximum = RoundUp(Convert.ToInt32(1.05 * maxPt));

            con.Close();
        }

        private void chtHCFeCr(DateTime startDate, DateTime endDate, string item, string source)
        {
            connection();

            com = new SqlCommand("sp_ChartMaterial", con);

            com.CommandType = CommandType.StoredProcedure;
            SqlParameter[] sqlParams = {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate),
                new SqlParameter("@materialID", 3)
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

            Chart5.Titles[0].Text = item + " (INR/kg)";
            Chart5.Titles[0].Font = new System.Drawing.Font("Tahoma", 8.5f, FontStyle.Bold);

            Chart5.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            //Chart5.Series[0].LegendText = "LC Ferro\nChrome";
            //Chart5.Series[0].ToolTip = "Current Value = " + YPointMember[ChartData.Rows.Count - 1].ToString() + " Rs/kg";
            Chart5.Series[0].Points[count - 1].IsValueShownAsLabel = true;
            Chart5.Series[0].Points[count - 1].LabelBackColor = Color.White;
            Chart5.Series[0].Points[count - 1].LabelBorderColor = Color.Black;
            Chart5.Series[0].Points[count - 1].LabelBorderWidth = 1;
            Chart5.Series[0].Points[count - 1].Font = new System.Drawing.Font("Tahoma", 10f);
            Chart5.Series[0].IsVisibleInLegend = false;
            Chart5.Series[0].BorderWidth = 1;
            Chart5.Series[0].Color = Color.Firebrick;
            Chart5.Series[0].ToolTip = Convert.ToString("#VALX, #VALY");
            Chart5.Series[0].ChartType = SeriesChartType.Line;
            Chart5.Series[0].MarkerStyle = MarkerStyle.Circle;
            Axis x5 = Chart5.ChartAreas[0].AxisX;
            Axis y5 = Chart5.ChartAreas[0].AxisY;
            x5.Title = "Source :- " + source;
            x5.IntervalType = DateTimeIntervalType.Months;
            x5.Interval = 2;
            x5.IntervalAutoMode = IntervalAutoMode.FixedCount;
            x5.MajorGrid.Enabled = false;
            y5.MajorGrid.LineColor = Color.LightGray;
            y5.Minimum = RoundDown(Convert.ToInt32(0.95 * minPt));
            //y5.Maximum = RoundUp(Convert.ToInt32(1.05 * maxPt));

            con.Close();
        }

        private void chtFeV(DateTime startDate, DateTime endDate, string item, string source)
        {
            connection();

            com = new SqlCommand("sp_ChartMaterial", con);

            com.CommandType = CommandType.StoredProcedure;
            SqlParameter[] sqlParams = {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate),
                new SqlParameter("@materialID", 8)
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

            Chart6.Titles[0].Text = item + " (INR/kg)";
            Chart6.Titles[0].Font = new System.Drawing.Font("Tahoma", 8.5f, FontStyle.Bold);

            Chart6.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            //Chart6.Series[0].LegendText = "Ferro\nVanadium";
            //Chart6.Series[0].ToolTip = "Current Value = " + YPointMember[ChartData.Rows.Count - 1].ToString() + " Rs/kg";
            Chart6.Series[0].Points[count - 1].IsValueShownAsLabel = true;
            Chart6.Series[0].Points[count - 1].LabelBackColor = Color.White;
            Chart6.Series[0].Points[count - 1].LabelBorderColor = Color.Black;
            Chart6.Series[0].Points[count - 1].LabelBorderWidth = 1;
            Chart6.Series[0].Points[count - 1].Font = new System.Drawing.Font("Tahoma", 10f);
            Chart6.Series[0].IsVisibleInLegend = false;
            Chart6.Series[0].BorderWidth = 1;
            Chart6.Series[0].Color = Color.Green;
            Chart6.Series[0].ToolTip = Convert.ToString("#VALX, #VALY");
            Chart6.Series[0].ChartType = SeriesChartType.Line;
            Chart6.Series[0].MarkerStyle = MarkerStyle.Circle;
            Axis x6 = Chart6.ChartAreas[0].AxisX;
            Axis y6 = Chart6.ChartAreas[0].AxisY;
            x6.Title = "Source :- " + source;
            x6.IntervalType = DateTimeIntervalType.Months;
            x6.Interval = 2;
            x6.IntervalAutoMode = IntervalAutoMode.FixedCount;
            x6.MajorGrid.Enabled = false;
            y6.MajorGrid.LineColor = Color.LightGray;
            y6.Minimum = RoundDown(Convert.ToInt32(0.95 * minPt));
            //y6.Maximum = RoundUp(Convert.ToInt32(1.05 * maxPt));

            con.Close();
        }

        private void chtGraphite(DateTime startDate, DateTime endDate, string item, string source)
        {
            connection();

            com = new SqlCommand("sp_ChartMaterial", con);

            com.CommandType = CommandType.StoredProcedure;
            SqlParameter[] sqlParams = {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate),
                new SqlParameter("@materialID", 20)
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

            Chart7.Titles[0].Text = item + " (INR/kg)";
            Chart7.Titles[0].Font = new System.Drawing.Font("Tahoma", 8.5f, FontStyle.Bold);

            Chart7.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            //Chart7.Series[0].LegendText = "Graphite\nElectrode";
            //Chart7.Series[0].ToolTip = "Current Value = " + YPointMember[ChartData.Rows.Count - 1].ToString() + " Rs/kg";
            Chart7.Series[0].Points[count - 1].IsValueShownAsLabel = true;
            Chart7.Series[0].Points[count - 1].LabelBackColor = Color.White;
            Chart7.Series[0].Points[count - 1].LabelBorderColor = Color.Black;
            Chart7.Series[0].Points[count - 1].LabelBorderWidth = 1;
            Chart7.Series[0].Points[count - 1].Font = new System.Drawing.Font("Tahoma", 10f);
            Chart7.Series[0].IsVisibleInLegend = false;
            Chart7.Series[0].BorderWidth = 1;
            Chart7.Series[0].Color = Color.MediumOrchid;
            Chart7.Series[0].ToolTip = Convert.ToString("#VALX, #VALY");
            Chart7.Series[0].ChartType = SeriesChartType.Line;
            Chart7.Series[0].MarkerStyle = MarkerStyle.Circle;
            Axis x7 = Chart7.ChartAreas[0].AxisX;
            Axis y7 = Chart7.ChartAreas[0].AxisY;
            x7.Title = "Source :- " + source;
            x7.IntervalType = DateTimeIntervalType.Months;
            x7.Interval = 2;
            x7.IntervalAutoMode = IntervalAutoMode.FixedCount;
            x7.MajorGrid.Enabled = false;
            y7.MajorGrid.LineColor = Color.LightGray;
            y7.Minimum = RoundDown(Convert.ToInt32(0.95 * minPt));
            //y7.Maximum = RoundUp(Convert.ToInt32(1.05 * maxPt));

            con.Close();
        }
    }
}