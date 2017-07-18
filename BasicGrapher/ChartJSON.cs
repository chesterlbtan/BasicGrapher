﻿using System;
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
            foreach (var json_title in (List<JsonStruct>)_value["titles"].Value)
            {
                Title tmpTitle = new Title();
                tmpTitle.Text = (string)json_title["text"].Value;
                tmpTitle.Docking = (Docking)Enum.Parse(typeof(Docking), (string)json_title["docking"].Value);
                if (json_title.ContainsKey("font"))
                    tmpTitle.Font = GetFont(json_title["font"]);
                dummy.Titles.Add(tmpTitle);
            }

            dummy.Size = new System.Drawing.Size((int)_value["size"]["width"].Value, (int)_value["size"]["height"].Value);
            foreach (var json_chrtArea in (List<JsonStruct>)_value["chartareas"].Value)
            {
                ChartArea tmpChrtArea = new ChartArea();
                tmpChrtArea.AxisX.Title = (string)json_chrtArea["axisx"]["title"].Value;
                if (json_chrtArea["axisx"].ContainsKey("font"))
                    tmpChrtArea.AxisX.TitleFont = GetFont(json_chrtArea["axisx"]["font"]);
                tmpChrtArea.AxisX.Interval = (int)json_chrtArea["axisx"]["interval"].Value;
                tmpChrtArea.AxisY.Title = (string)json_chrtArea["axisy"]["title"].Value;
                if (json_chrtArea["axisy"].ContainsKey("font"))
                    tmpChrtArea.AxisY.TitleFont = GetFont(json_chrtArea["axisy"]["font"]);
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
    }
}
