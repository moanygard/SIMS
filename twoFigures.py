import math
import pandas as pd
from bokeh.io import output_file, show
from bokeh.palettes import Category20c
from bokeh.plotting import figure
from bokeh.transform import cumsum
from bokeh.models import LabelSet, ColumnDataSource
from bokeh.layouts import column

output_file("pie.html")

data = pd.read_csv('C:\\Users\\Sundsvall\\Documents\\DATA\\SIMS\\TestDataVisualization.csv', encoding='latin-1')

# Pie Chart

# Get theme
themeData = data[['data_theme_id']]
data = themeData.groupby("data_theme_id").value_counts().reset_index(name = 'number_of_themes')
#percentages = data/data.sum()
data['angle'] = data['number_of_themes']/data['number_of_themes'].sum()*2*math.pi
data['color'] = Category20c[len(data)]

# First figure
p = figure(plot_height=350, title="Pie Chart", toolbar_location=None, x_range=(-0.5, 1.0))
p.wedge(x=0, y=1, radius=0.4,
        start_angle=cumsum('angle', include_zero=True), end_angle=cumsum('angle'),
        line_color="white", fill_color='color', legend_label='theme', source=data)

data["data_theme_id"] = data["data_theme_id"].astype(str)
data["data_theme_id"] = data["data_theme_id"].str.pad(35, side = "left")
source = ColumnDataSource(data)

labels = LabelSet(x=0, y=1, text='data_theme_id',
        angle=cumsum('angle', include_zero=True), source=source, render_mode='canvas')

p.add_layout(labels)
p.axis.axis_label=None
p.axis.visible=False
p.grid.grid_line_color = None

##################### NEXT PLOT Line Plot #######################

dataPlot2 = pd.read_csv('C:\\Users\\Sundsvall\\Documents\\DATA\\SIMS\\TestDataVisualization.csv', encoding='latin-1')

# Get Date and Usage
dateUsageData = dataPlot2[['date_of_usage', 'is_downloaded']]
totalNumberOfUsage = dateUsageData.groupby('date_of_usage')['is_downloaded'].sum().reset_index(name = 'number_of_downloads')
x = totalNumberOfUsage['date_of_usage']
#y = totalNumberOfUsage['number_of_downloads']

# Get Date and Usage
themeData = dataPlot2[['data_theme_id']]
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

# Color of the wedges
color = ["violet", "blue", "green", "yellow", "red"]
color = color[0:percentages.count()]

# Add first plot
pl = figure(width=250, height=250, background_fill_color="#fafafa",
           x_range=x, title="Usage of open data", #plot_width=450, 
           x_axis_label='Date', y_axis_label='Accesses', sizing_mode="stretch_both")#, tools=TOOLS)

# Render glyphs
pl.line(x='date_of_usage', y='number_of_downloads',
       color="dodgerblue", legend_label='Total usage',line_width=1.5, source=totalNumberOfUsage)
pl.circle(x='date_of_usage', y='number_of_downloads',
       color="blue", legend_label='Values', source=totalNumberOfUsage)#,line_width=2)  source=source,
pl.legend.click_policy="hide"

show(column(p, pl))