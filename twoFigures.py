from bokeh.io import output_file, show
from bokeh.palettes import Category20c
from bokeh.plotting import figure
from bokeh.transform import cumsum
from bokeh.models import LabelSet, ColumnDataSource
from bokeh.layouts import column
import pandas as pd
import math

output_file("twoFigures.html")

pieData = pd.read_csv('C:\\Users\\Sundsvall\\Documents\\DATA\\SIMS\\TestDataVisualization.csv', encoding='latin-1')
lineData = pd.read_csv('C:\\Users\\Sundsvall\\Documents\\DATA\\SIMS\\TestDataVisualization.csv', encoding='latin-1')
# Get Date and Usage
dateUsageData = lineData[['date_of_usage', 'is_downloaded']]
totalNumberOfUsage = dateUsageData.groupby('date_of_usage')['is_downloaded'].sum().reset_index(name = 'number_of_downloads')
x = totalNumberOfUsage['date_of_usage']

# Get Date and Usage
themeData = lineData[['data_theme_id']]
share = themeData.groupby("data_theme_id").value_counts()
percentages = share/share.sum()
radians = [math.radians((percent)*360) for percent in percentages]

# Center of the Pie Chart
x1 = 0
y1 = 0
# Radius of the glyphs
rad = 1

# Starting angle values
start_angle = [math.radians(0)]
prev = start_angle[0]
for i in radians[:-1]:
    start_angle.append(i + prev)
    prev = i + prev

# Ending angle vlaues
end_angle = start_angle[1:] + [math.radians(0)]

# Color of the wedges (maximum 5 themes rigth now)
color = ["violet", "blue", "green", "yellow", "red"]
color = color[0:percentages.count()]

# Add first plot
linePlot = figure(width=250, height=250, background_fill_color="#fafafa",
           x_range=x, title="Usage of open data", #plot_width=450, 
           x_axis_label='Date', y_axis_label='Accesses', sizing_mode="stretch_both")#, tools=TOOLS)

# Render glyphs
linePlot.line(x='date_of_usage', y='number_of_downloads',
       color="dodgerblue", legend_label='Total usage',line_width=1.5, source=totalNumberOfUsage)
linePlot.circle(x='date_of_usage', y='number_of_downloads',
       color="blue", legend_label='Values', source=totalNumberOfUsage)#,line_width=2)  source=source,
linePlot.legend.click_policy="hide"

##################### Pie Chart #######################

# Get theme
themeData = pieData[['data_theme_id']]
pieData = themeData.groupby("data_theme_id").value_counts().reset_index(name = 'number_of_themes')
#percentages = data/data.sum()
pieData['angle'] = pieData['number_of_themes']/pieData['number_of_themes'].sum()*2*math.pi
pieData['color'] = Category20c[len(pieData)]

# First figure
pie = figure(plot_height=350, title="Pie Chart", toolbar_location=None, x_range=(-0.5, 1.0))
pie.annular_wedge(x=0, y=1, inner_radius=0.2, outer_radius=0.4,
        start_angle=cumsum('angle', include_zero=True), end_angle=cumsum('angle'),
        line_color="white", fill_color='color', legend_label='theme', source=pieData)

pieData["data_theme_id"] = pieData["data_theme_id"].astype(str)
pieData["data_theme_id"] = pieData["data_theme_id"].str.pad(29, side = "left")
source = ColumnDataSource(pieData)

labels = LabelSet(x=0, y=1, text='data_theme_id',
        angle=cumsum('angle', include_zero=True), source=source, render_mode='canvas')

pie.add_layout(labels)
pie.axis.axis_label=None
pie.axis.visible=False
pie.grid.grid_line_color = None

show(column(linePlot, pie))