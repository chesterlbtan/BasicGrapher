using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace BasicGrapher
{
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
            dummy.Titles.Add((string)_value["titles"].Value);
            dummy.Size = new System.Drawing.Size((int)_value["size"]["width"].Value, (int)_value["size"]["height"].Value);
            foreach (var json_chrtArea in (List<JsonStruct>)_value["chartareas"].Value)
            {
                ChartArea tmpChrtArea = new ChartArea();
                tmpChrtArea.AxisX.Title = (string)json_chrtArea["axisx"]["title"].Value;
                tmpChrtArea.AxisX.Interval = (int)json_chrtArea["axisx"]["interval"].Value;
                tmpChrtArea.AxisY.Title = (string)json_chrtArea["axisy"]["title"].Value;
                dummy.ChartAreas.Add(tmpChrtArea);
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
            
            return dummy;
        }
    }
}
