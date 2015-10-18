# SharpMap.Ptv
SharpMap Addons for PTV Services and Data

![screenshot](https://raw.githubusercontent.com/ptv-logistics/SharpMap.Ptv/master/Screenshot.png "screenshot")

# What is this?

This is an extension for the Open Source .NET library [SharpMap](http://sharpmap.codeplex.com/) to add support for PTV services and data. The library contains:

* A SharpMap Tranformation for PTV_Mercator
* A SharpMap Layer for PTV xMapServer
* A SharpMap VectorLayer for PTV AddressMonitor POIs
* A SharpMap Provider for PTV Map&Market areas

# How can i use it?

The primary use of this library is the generation of map images with data from different sources. In contrast to client mapping libraries like [xServer.NET](http://xserverinternet.azurewebsites.net/xserver.net/), which is a "retained renderer", SharpMap is an "immediate renderer". This means an image is directly by drawing directives. The scenarios for this approach:

* In client applications, if you don't need interaction but only static images. See the demo application in this project.
* As renderer inside a client control, see the "ShapeFile" sample in [xServer.NET DemoCenter](http://xserverinternet.azurewebsites.net/xserver.net/)
* As as Tile/Imagery service for web applications, see [here for a sample](https://github.com/ptv-logistics/ajaxmaps-shapefile)

So you can use this library in two directions: Either overlay PTV content on 3rd-party base maps, or 3rd-party content on a PTV map.

# What do i need to start?

To build the project, you need Microsoft Visual Studio or the free [Community Edition](https://www.visualstudio.com/products/visual-studio-community-vs.aspx).
 
To run the code, you need an installed PTV xMapServer or an xServer internet subscription. Go to http://xserver.ptvgroup.com/en-uk/products/ptv-xserver-internet/test/ to get a trial token.
