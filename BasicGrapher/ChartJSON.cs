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
                    });

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
