# BasicGrapher
BasicGrapher is a basic tool to graph basic data into charts image.
It is use through command line and uses JSON to format the chart.
The chart image is saved in PNG format.

## Usage
Use via command line
```command line
BasicGrapher <json_file> <image_name>
```
<*json_file*> determines the format of the chart. See below for sample or see code *Sample.json*.

<*image_name*> determines the output image full file name.

## Sample <*json_file*>
The <*json_file*> follows the Chart Object of the VB.NET
```json
{
  "titles": [
    {
      "docking": "Top",
      "text": "MyTitle",
      "font": {
        "fontname": "Courier New",
        "size": 16,
        "style": [ "Bold", "Underline" ]
      }
    }
  ],
  "size": {
    "height": 400,
    "width": 600
  },
  "chartareas": [
    {
      "axisx": {
        "title": "axisX name",
        "interval": 1
      },
      "axisy": {
        "title": "axisY name"
      }
    }
  ],
  "series": [
    {
      "points": [
        {
          "axislabel": "x0",
          "yvalues": [ 9 ]
        },
        {
          "axislabel": "x1",
          "yvalues": [ 10 ]
        },
        {
          "axislabel": "x2",
          "yvalues": [ 8 ]
        },
        {
          "axislabel": "x3",
          "yvalues": [ 5 ]
        }
      ],
      "xvaluetype": "String"
    }
  ]
}
```
