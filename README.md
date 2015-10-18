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

The primary use of this library is the generation of map images with data from different sources. In contrast to client mapping libraries like [xServer.NET](http://xserverinternet.azurewebsites.net/xserver.net/), which is a "retained renderer", ShapMap is an "immediate renderer". This means an image is directly by drawing directives. The scenarios for this approach:

* In client applications, if you don't need interaction but only static images. See the demo application in this project.
* As renderer inside a client control, see the "ShapeFile" sample in [xServer.NET DemoCenter](http://xserverinternet.azurewebsites.net/xserver.net/)
* As as Tile/Imagery service for web applications, see [here for a sample](https://github.com/ptv-logistics/ajaxmaps-shapefile)
