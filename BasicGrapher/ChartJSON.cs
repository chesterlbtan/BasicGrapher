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

        public static T GetEnum<T>(MyJSON.JsonStruct input)
        {
            return (T)Enum.Parse(typeof(T), (string)input.Value);
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
                {
                    Title tmpTitle = new Title();
                    tmpTitle.Text = (string)json_title["text"].Value;
                    if (json_title.ContainsKey("docking"))
                        tmpTitle.Docking = (Docking)Enum.Parse(typeof(Docking), (string)json_title["docking"].Value);
                    if (json_title.ContainsKey("font"))
                        tmpTitle.Font = GetFont(json_title["font"]);
                    if (json_title.ContainsKey("forecolor"))
                        tmpTitle.ForeColor = GetColor((string)json_title["forecolor"].Value);
                    a.Titles.Add(tmpTitle);
                }

                a.Size = new System.Drawing.Size((int)_value["size"]["width"].Value, (int)_value["size"]["height"].Value);

                foreach (var json_chrtArea in (List<JsonStruct>)_value["chartareas"].Value)
                {
                    ChartArea tmpChrtArea = new ChartArea();

                    if (json_chrtArea.ContainsKey("axisx"))
                        tmpChrtArea.AxisX.With(x =>
                        {
                            var axisX = json_chrtArea["axisx"];
                            x.Title = (string)axisX["title"].Value;
                            if (axisX.ContainsKey("font"))
                                x.TitleFont = GetFont(axisX["font"]);
                            if (axisX.ContainsKey("forecolor"))
                                x.TitleForeColor = GetColor((string)axisX["forecolor"].Value);
                            if (axisX.ContainsKey("minimum"))
                                x.Minimum = (int)axisX["minimum"].Value;
                            if (axisX.ContainsKey("maximum"))
                                x.Maximum = (int)axisX["maximum"].Value;
                            if (axisX.ContainsKey("interval"))
                                x.Interval = (int)axisX["interval"].Value;

                            if (axisX.ContainsKey("labelstyle"))
                                x.LabelStyle.With(xLabelStyle =>
                                {
                                    var json_xLblStyle = axisX["labelstyle"];
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
                                        xLabelStyle.IntervalOffsetType = Extension.GetEnum<DateTimeIntervalType>(json_xLblStyle["intervaloffsettype"]);
                                    if (json_xLblStyle.ContainsKey("intervaltype"))
                                        xLabelStyle.IntervalType = Extension.GetEnum<DateTimeIntervalType>(json_xLblStyle["intervaltype"]);
                                    if (json_xLblStyle.ContainsKey("isendlabelvisible"))
                                        xLabelStyle.IsEndLabelVisible = (bool)json_xLblStyle["isendlabelvisible"].Value;
                                    if (json_xLblStyle.ContainsKey("isstaggered"))
                                        xLabelStyle.IsStaggered = (bool)json_xLblStyle["isstaggered"].Value;
                                    if (json_xLblStyle.ContainsKey("truncatedlabels"))
                                        xLabelStyle.TruncatedLabels = (bool)json_xLblStyle["truncatedlabels"].Value;
                                });
                        });

                    if (json_chrtArea.ContainsKey("axisy"))
                        tmpChrtArea.AxisY.With(y =>
                        {
                            var axisY = json_chrtArea["axisy"];
                            y.Title = (string)axisY["title"].Value;
                            if (axisY.ContainsKey("font"))
                                y.TitleFont = GetFont(axisY["font"]);
                            if (axisY.ContainsKey("forecolor"))
                                y.TitleForeColor = GetColor((string)axisY["forecolor"].Value);
                            if (axisY.ContainsKey("minimum"))
                                y.Minimum = (int)axisY["minimum"].Value;
                            if (axisY.ContainsKey("maximum"))
                                y.Maximum = (int)axisY["maximum"].Value;
                            if (axisY.ContainsKey("interval"))
                                y.Interval = (int)axisY["interval"].Value;

                            if (axisY.ContainsKey("labelstyle"))
                                y.LabelStyle.With(yLabelStyle =>
                                {
                                    var json_yLblStyle = axisY["labelstyle"];
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
                                        yLabelStyle.IntervalOffsetType = Extension.GetEnum<DateTimeIntervalType>(json_yLblStyle["intervaloffsettype"]);
                                    if (json_yLblStyle.ContainsKey("intervaltype"))
                                        yLabelStyle.IntervalType = Extension.GetEnum<DateTimeIntervalType>(json_yLblStyle["intervaltype"]);
                                    if (json_yLblStyle.ContainsKey("isendlabelvisible"))
                                        yLabelStyle.IsEndLabelVisible = (bool)json_yLblStyle["isendlabelvisible"].Value;
                                    if (json_yLblStyle.ContainsKey("isstaggered"))
                                        yLabelStyle.IsStaggered = (bool)json_yLblStyle["isstaggered"].Value;
                                    if (json_yLblStyle.ContainsKey("truncatedlabels"))
                                        yLabelStyle.TruncatedLabels = (bool)json_yLblStyle["truncatedlabels"].Value;
                                });
                        });

                    a.ChartAreas.Add(tmpChrtArea);
                }

                foreach (var json_series in (List<JsonStruct>)_value["series"].Value)
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
                    tmpSeries.XValueType = (ChartValueType)Enum.Parse(typeof(ChartValueType), (string)json_series["xvaluetype"].Value);
                    dummy.Series.Add(tmpSeries);
                }
            });
            return dummy;
        }

        private System.Drawing.Font GetFont(JsonStruct json_font)
        {
            System.Drawing.Font tmpFont;
            try
            {
                tmpFont = new System.Drawing.Font((string)json_font["fontname"].Value, (int)json_font["size"].Value);
                if (json_font.ContainsKey("style"))
                {
                    System.Drawing.FontStyle tmpStyle = System.Drawing.FontStyle.Regular;
                    foreach (var x in (List<JsonStruct>)json_font["style"].Value)
                        tmpStyle |= (System.Drawing.FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), (string)x.Value);
                    tmpFont = new System.Drawing.Font(tmpFont, tmpStyle);
                }
            }
            catch (ArgumentException argex)
            {

                throw;
            }
            return tmpFont;
        }

        private System.Drawing.Color GetColor(string json_color)
        {
            return System.Drawing.Color.FromName(json_color);
        }
    }
}
