using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace BasicGrapher
{
    static class Extension
    {
        public static void With<T>(this T obj, Action<T> a)
        {
            a(obj);
        }

        public static void SetEnum<T>(this T obj, MyJSON.JsonStruct input)
        {
            obj = SetEnum<T>(input);
        }
        public static T SetEnum<T>(MyJSON.JsonStruct input)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), (string)input.Value);
            }
            catch (ArgumentException argex)
            {
                Console.WriteLine("ArgumentException: " + argex.Message);
                Console.WriteLine("The key \"" + input.ParentName + "\" can only have these values below:");
                Console.WriteLine(string.Join("\n", Enum.GetNames(typeof(T))));
                throw;
            }
        }
    }
    class ChartJSON : MyJSON
    {
        private Chart __chart;

        public ChartJSON(string strJson) : base (strJson)
        {
            __chart = ConvertToChart();
        }

        public Chart Chart { get { return __chart; } }

        public override string ToString()
        {
            return base.ToString();
        }

        private Chart ConvertToChart()
        {
            Chart dummy = new Chart();

            dummy.With(a =>
            {
                foreach (var json_title in (List<JsonStruct>)_value["titles"].Value)
                    a.Titles.Add(CreateTitle(json_title));

                a.Size = new System.Drawing.Size((int)_value["size"]["width"].Value, (int)_value["size"]["height"].Value);

                foreach (var json_chrtArea in (List<JsonStruct>)_value["chartareas"].Value)
                    a.ChartAreas.Add(CreateChartArea(json_chrtArea));

                foreach (var json_series in (List<JsonStruct>)_value["series"].Value)
                    dummy.Series.Add(CreateSeries(json_series));
            });
            return dummy;
        }

        private System.Drawing.Font GetFont(JsonStruct json_font)
        {
            System.Drawing.Font tmpFont = new System.Drawing.Font((string)json_font["fontname"].Value, (int)json_font["size"].Value);
            if (json_font.ContainsKey("style"))
            {
                System.Drawing.FontStyle tmpStyle = System.Drawing.FontStyle.Regular;
                foreach (var x in (List<JsonStruct>)json_font["style"].Value)
                    tmpStyle |= Extension.SetEnum<System.Drawing.FontStyle>(x);
                tmpFont = new System.Drawing.Font(tmpFont, tmpStyle);
            }
            return tmpFont;
        }

        private System.Drawing.Color GetColor(string json_color)
        {
            return System.Drawing.Color.FromName(json_color);
        }

        private Title CreateTitle(JsonStruct json_title)
        {
            Title tmpTitle = new Title();
            tmpTitle.Text = (string)json_title["text"].Value;
            if (json_title.ContainsKey("docking"))
                tmpTitle.Docking.SetEnum(json_title["docking"]);
            if (json_title.ContainsKey("font"))
                tmpTitle.Font = GetFont(json_title["font"]);
            if (json_title.ContainsKey("forecolor"))
                tmpTitle.ForeColor = GetColor((string)json_title["forecolor"].Value);
            return tmpTitle;
        }

        private ChartArea CreateChartArea(JsonStruct json_chartArea)
        {
            ChartArea tmpChrtArea = new ChartArea();

            if (json_chartArea.ContainsKey("axisx"))
                tmpChrtArea.AxisX.With(axisX =>
                {
                    var json_axisX = json_chartArea["axisx"];
                    axisX.Title = (string)json_axisX["title"].Value;
                    if (json_axisX.ContainsKey("font"))
                        axisX.TitleFont = GetFont(json_axisX["font"]);
                    if (json_axisX.ContainsKey("forecolor"))
                        axisX.TitleForeColor = GetColor((string)json_axisX["forecolor"].Value);
                    if (json_axisX.ContainsKey("minimum"))
                        axisX.Minimum = (int)json_axisX["minimum"].Value;
                    if (json_axisX.ContainsKey("maximum"))
                        axisX.Maximum = (int)json_axisX["maximum"].Value;
                    if (json_axisX.ContainsKey("interval"))
                        axisX.Interval = (int)json_axisX["interval"].Value;

                    if (json_axisX.ContainsKey("labelstyle"))
                        axisX.LabelStyle.With(xLabelStyle =>
                        {
                            var json_xLblStyle = json_axisX["labelstyle"];
                            if (json_xLblStyle.ContainsKey("angle"))
                                xLabelStyle.Angle = (int)json_xLblStyle["angle"].Value;
                            if (json_xLblStyle.ContainsKey("enabled"))
                                xLabelStyle.Enabled = Convert.ToBoolean((string)json_xLblStyle["enabled"].Value);
                            if (json_xLblStyle.ContainsKey("font"))
                                xLabelStyle.Font = GetFont(json_xLblStyle["font"]);
                            if (json_xLblStyle.ContainsKey("forecolor"))
                                xLabelStyle.ForeColor = GetColor((string)json_xLblStyle["forecolor"].Value);
                            if (json_xLblStyle.ContainsKey("format"))
                                xLabelStyle.Format = (string)json_xLblStyle["format"].Value;
                            if (json_xLblStyle.ContainsKey("interval"))
                                xLabelStyle.Interval = (int)json_xLblStyle["interval"].Value;
                            if (json_xLblStyle.ContainsKey("intervaloffset"))
                                xLabelStyle.IntervalOffset = (int)json_xLblStyle["intervaloffset"].Value;
                            if (json_xLblStyle.ContainsKey("intervaloffsettype"))
                                xLabelStyle.IntervalOffsetType.SetEnum(json_xLblStyle["intervaloffsettype"]);
                            if (json_xLblStyle.ContainsKey("intervaltype"))
                                xLabelStyle.IntervalType.SetEnum(json_xLblStyle["intervaltype"]);
                            if (json_xLblStyle.ContainsKey("isendlabelvisible"))
                                xLabelStyle.IsEndLabelVisible = (bool)json_xLblStyle["isendlabelvisible"].Value;
                            if (json_xLblStyle.ContainsKey("isstaggered"))
                                xLabelStyle.IsStaggered = (bool)json_xLblStyle["isstaggered"].Value;
                            if (json_xLblStyle.ContainsKey("truncatedlabels"))
                                xLabelStyle.TruncatedLabels = (bool)json_xLblStyle["truncatedlabels"].Value;
                        });
                });

            if (json_chartArea.ContainsKey("axisy"))
                tmpChrtArea.AxisY.With(axisY =>
                {
                    var json_axisY = json_chartArea["axisy"];
                    axisY.Title = (string)json_axisY["title"].Value;
                    if (json_axisY.ContainsKey("font"))
                        axisY.TitleFont = GetFont(json_axisY["font"]);
                    if (json_axisY.ContainsKey("forecolor"))
                        axisY.TitleForeColor = GetColor((string)json_axisY["forecolor"].Value);
                    if (json_axisY.ContainsKey("minimum"))
                        axisY.Minimum = (int)json_axisY["minimum"].Value;
                    if (json_axisY.ContainsKey("maximum"))
                        axisY.Maximum = (int)json_axisY["maximum"].Value;
                    if (json_axisY.ContainsKey("interval"))
                        axisY.Interval = (int)json_axisY["interval"].Value;

                    if (json_axisY.ContainsKey("labelstyle"))
                        axisY.LabelStyle.With(yLabelStyle =>
                        {
                            var json_yLblStyle = json_axisY["labelstyle"];
                            if (json_yLblStyle.ContainsKey("angle"))
                                yLabelStyle.Angle = (int)json_yLblStyle["angle"].Value;
                            if (json_yLblStyle.ContainsKey("enabled"))
                                yLabelStyle.Enabled = Convert.ToBoolean((string)json_yLblStyle["enabled"].Value);
                            if (json_yLblStyle.ContainsKey("font"))
                                yLabelStyle.Font = GetFont(json_yLblStyle["font"]);
                            if (json_yLblStyle.ContainsKey("forecolor"))
                                yLabelStyle.ForeColor = GetColor((string)json_yLblStyle["forecolor"].Value);
                            if (json_yLblStyle.ContainsKey("format"))
                                yLabelStyle.Format = (string)json_yLblStyle["format"].Value;
                            if (json_yLblStyle.ContainsKey("interval"))
                                yLabelStyle.Interval = (int)json_yLblStyle["interval"].Value;
                            if (json_yLblStyle.ContainsKey("intervaloffset"))
                                yLabelStyle.IntervalOffset = (int)json_yLblStyle["intervaloffset"].Value;
                            if (json_yLblStyle.ContainsKey("intervaloffsettype"))
                                yLabelStyle.IntervalOffsetType.SetEnum(json_yLblStyle["intervaloffsettype"]);
                            if (json_yLblStyle.ContainsKey("intervaltype"))
                                yLabelStyle.IntervalType.SetEnum(json_yLblStyle["intervaltype"]);
                            if (json_yLblStyle.ContainsKey("isendlabelvisible"))
                                yLabelStyle.IsEndLabelVisible = (bool)json_yLblStyle["isendlabelvisible"].Value;
                            if (json_yLblStyle.ContainsKey("isstaggered"))
                                yLabelStyle.IsStaggered = (bool)json_yLblStyle["isstaggered"].Value;
                            if (json_yLblStyle.ContainsKey("truncatedlabels"))
                                yLabelStyle.TruncatedLabels = (bool)json_yLblStyle["truncatedlabels"].Value;
                        });
                });

            return tmpChrtArea;
        }

        private Series CreateSeries(JsonStruct json_series)
        {
            Series tmpSeries = new Series();
            foreach (var json_points in (List<JsonStruct>)json_series["points"].Value)
            {
                DataPoint tmpPoint = new DataPoint();
                tmpPoint.AxisLabel = (string)json_points["axislabel"].Value;
                List<double> tmpYvalues = new List<double>();
                foreach (var json_Yvalue in (List<JsonStruct>)json_points["yvalues"].Value)
                    tmpYvalues.Add((int)json_Yvalue.Value);
                tmpPoint.YValues = tmpYvalues.ToArray();
                tmpSeries.Points.Add(tmpPoint);
            }
            tmpSeries.XValueType.SetEnum(json_series["xvaluetype"]);
            return tmpSeries;
        }
    }
}
