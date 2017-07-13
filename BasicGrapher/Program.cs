using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace BasicGrapher
{
    class Program
    {
        static void Main(string[] args)
        {
            //should specify the following
            // 1) sourceData (full file name)
            // 2) chart title
            // 3) height
            // 4) width
            try
            {
                string fileName = args[0];
                int height = int.Parse(args[2]);
                int width = int.Parse(args[3]);

                string[] data = File.ReadAllLines(fileName);
                string x_name = data[0].Split(',')[0];
                string y_name = data[0].Split(',')[1];
                string[] x_values = new string[data.Length - 1];
                double[] y_values = new double[data.Length - 1];
                for (int i = 1; i < data.Length; i++)
                {
                    x_values[i - 1] = data[i].Split(',')[0];
                    y_values[i - 1] = double.Parse(data[i].Split(',')[1]);
                }


                Chart chrtBarGraph = new Chart();
                chrtBarGraph.Titles.Add(args[1]);
                chrtBarGraph.Size = new System.Drawing.Size(width, height);

                ChartArea area = new ChartArea();
                area.AxisX.Title = x_name;
                area.AxisX.Interval = 1;
                area.AxisY.Title = y_name;

                Series xyz = new Series();
                for (int i = 0; i < y_values.Length; i++)
                {
                    DataPoint aPoint = new DataPoint();
                    aPoint.AxisLabel = x_values[i];
                    aPoint.SetValueY(y_values[i]);
                    xyz.Points.Add(aPoint);
                }
                xyz.XValueType = ChartValueType.String;
                chrtBarGraph.Series.Add(xyz);

                chrtBarGraph.ChartAreas.Add(area);
                chrtBarGraph.SaveImage("barGraph.png", ChartImageFormat.Png);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.StackTrace, ex.Message);
            }
        }
    }
}
