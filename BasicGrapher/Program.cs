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
            // 2) full path file name
            try
            {
                ChartJSON objChartJson = new ChartJSON(File.ReadAllText(args[0]));
                Chart chrtBarGraph = objChartJson.Chart;

                ChartImageFormat myFormat;
                string[] extension = args[1].Split('.');
                switch (extension[extension.Length - 1].ToLower())
                {
                    case "bmp":
                        myFormat = ChartImageFormat.Bmp;
                        break;
                    case "tiff":
                        myFormat = ChartImageFormat.Tiff;
                        break;
                    case "png":
                        myFormat = ChartImageFormat.Png;
                        break;
                    case "gif":
                        myFormat = ChartImageFormat.Gif;
                        break;
                    case "jpg":
                    case "jpeg":
                    default:
                        myFormat = ChartImageFormat.Jpeg;
                        break;
                }
                chrtBarGraph.SaveImage(args[1], myFormat);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error");
            }
        }
    }
}
