using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using MyOil.Models;

namespace MyOil.Controllers
{
    public class ChartController : Controller
    {
        //
        // GET: /Chart/
        OilEntities db = new OilEntities();


        public FileResult MonthChart()
        {
            IList<ChartModel> infoes = GetMonthInfoes();
            Chart chart = new Chart();
            chart.Width = 800;
            chart.Height = 200;
            chart.BackColor = Color.FromArgb(211, 223, 240);
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BackSecondaryColor = Color.White;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.BorderlineWidth = 1;
            chart.Palette = ChartColorPalette.BrightPastel;
            chart.BorderlineColor = Color.FromArgb(26, 59, 105);
            chart.RenderType = RenderType.BinaryStreaming;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;
            
            chart.Titles.Add(CreateTitle("统计"));
            chart.Legends.Add(CreateLegend("费用"));
            SeriesChartType chartType = SeriesChartType.Column;
            chart.Series.Add(CreateSeries(infoes, chartType,0,"费用"));
           // chart.Series.Add(CreateSeries(infoes, chartType, 1));
            chart.ChartAreas.Add(CreateChartArea());

            MemoryStream ms = new MemoryStream();
            chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");

        }

        public FileResult TimeChart()
        {
            IList<ChartModel> infoes = GetMonthInfoes();
            Chart chart = new Chart();
            chart.Width = 800;
            chart.Height = 200;
            chart.BackColor = Color.FromArgb(211, 223, 240);
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BackSecondaryColor = Color.White;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.BorderlineWidth = 1;
            chart.Palette = ChartColorPalette.BrightPastel;
            chart.BorderlineColor = Color.FromArgb(26, 59, 105);
            chart.RenderType = RenderType.BinaryStreaming;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;

            chart.Titles.Add(CreateTitle("按月统计"));
            chart.Legends.Add(CreateLegend("费用"));
            chart.Legends.Add(CreateLegend("油量"));
            SeriesChartType chartType = SeriesChartType.Column;
            chart.Series.Add(CreateSeries(infoes, chartType, 0, "费用"));
            chart.Series.Add(CreateSeries(infoes, chartType, 1, "油量"));
            
            chart.ChartAreas.Add(CreateChartArea());

            MemoryStream ms = new MemoryStream();
            chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }

        private IList<ChartModel> GetMonthInfoes()
        {
            IList<ChartModel> r = new List<ChartModel>();
            //从OilConsumeInfoes中选择最近12个月的油耗、费用、时间
            var sd = db.OilConsumeInfoes.GroupBy(c => c.refuelTime.Substring(0, 7)).OrderByDescending(c => c.Key).Take(12).OrderBy(c => c.Key).Select(o => new ChartModel { time = o.Key, cost = o.Sum(s => s.totalCost), quant = o.Sum(s => s.oilQuantity) });
            foreach (ChartModel item in sd)
            {
                r.Add(item);
            }
            
           // r.OrderBy(c=>c.quant); IList排序？
            return r;
           
        }

        private Title CreateTitle(string ti)
        {

            Title title = new Title();
            title.Text = ti;
            title.ShadowColor = Color.FromArgb(32, 0, 0, 0);
            title.Font = new Font("Trebuchet MS", 14F, FontStyle.Bold);
            title.ShadowOffset = 3;
            title.ForeColor = Color.FromArgb(26, 59, 105);
            return title;
        }

        private Series CreateSeries(IList<ChartModel> data,SeriesChartType sct,int flag,string name)
        {
            Series seriesDetail = new Series();
            seriesDetail.Name = name;
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = Color.FromArgb(198, 99, 99);
            seriesDetail.ChartType = sct;
            seriesDetail.BorderWidth = 2;
            seriesDetail.MarkerBorderWidth = 30;
            DataPoint point;

            foreach (ChartModel result in data)
            {
                point = new DataPoint();
                point.AxisLabel = result.time;
                if(flag == 0)
                    point.YValues = new double[] { double.Parse(result.cost.ToString()) };
                else
                    point.YValues = new double[] { double.Parse(result.quant.ToString()) };
                
                seriesDetail.Points.Add(point);
            }

            seriesDetail.ChartArea = "Result Chart";
            return seriesDetail;

        }

        private Legend CreateLegend(string label)
        {
            Legend lg = new Legend();
            lg.BackColor = Color.FromArgb(198, 99, 99);
            lg.Name = label;
            lg.Font = new Font("Trebuchet MS", 6F, FontStyle.Bold);;
            return lg;
        }

        private ChartArea CreateChartArea()
        {
            ChartArea chartArea = new ChartArea();
            chartArea.Name = "Result Chart";
            chartArea.BackColor = Color.Transparent;
            chartArea.AxisX.IsLabelAutoFit = false;
            chartArea.AxisY.IsLabelAutoFit = false;
            chartArea.AxisX.LabelStyle.Font =
               new Font("Verdana,Arial,Helvetica,sans-serif",
                        8F, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font =
               new Font("Verdana,Arial,Helvetica,sans-serif",
                        8F, FontStyle.Regular);
            chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.Interval = 1;
            
            return chartArea;
        }
    }
}
