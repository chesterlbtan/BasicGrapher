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
            // 1) json file with all the information
            // 2) file name without extension
            try
            {
                ChartJSON objChartJson = new ChartJSON(File.ReadAllText(args[0]));
                Chart chrtBarGraph = objChartJson.Chart;
                chrtBarGraph.SaveImage(args[1] + ".png", ChartImageFormat.Png);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error");
            }
        }
    }
}
