# AL.Advanced

[![master 0.0.0.2](https://img.shields.io/badge/master-0.0.0.2-green.svg?style=flat-square)]()

AL.Advanced is the attempt to offer an alternative compiler to Microsoft.

---

## Background

Since my first year as an apprentice I am not a big fan of C/AL. I am constantly lacking in functions and I find many things awkward. That's why I started researching Navision and especially code processing and generation at a relatively early stage. Over time my knowledge has evolved, but also the situation and requirements. Ultimately, the original plan was simply to replace C/AL with C#. With the release of AL, I also want to provide a conversion between the formats and an extended and customized version of AL.

## Visions

I have three visions of Navision.

1. __advanced NAV:__ without modifying the Microsoft base I extend NAV especially C/AL or AL and provide C#. It is merely a more pleasant form of development. 
2. __Modified NAV:__ I extend the functions of NAV and C/AL or AL and provide functions that Microsoft does not deliver because they cannot or do not want to. This is difficult from a licensing point of view.
3. __community NAV:__ Similar to Chromium zu Chorme, there is an open source base for the community as far as possible. This includes not only a own AL version but also a community object base.

## State

Currently I am developing the 4th generation of my compiler based on. NET core or standard. It takes into account three language variants. Currently I am working on the processing of code units and the basic structure. By the way, I also release help tools from the project.

## Languages overview

* C# => Primary objective. Al. dll for implementation, easy to use in the c# syntax, easy to read. Follow the C# style guides.
* ALFormatting => Current C# format that is stored in the database. The result of C/AL or AL.
* ALModifyed => An optimized C# format for the database.
* AL => The new language development from Microsoft which will replace C/al.
* C/AL => The classic syntax displayed in finsql.
* ALExtended => An improved version of AL not supported by MS.

## Features

* Codeunits -> Work in progress
* Pages
* Reports
* Html Reports
* XML Ports
* Apps
* own App format / nuspec
* fob reader / writer => currently i am analyzing fob files. File headers can already be created.
* C/AL editing  => needs fov 

---

* Visual Studio extension
* VS code extension
* standalone tool -> first prototype (Nav Object Explorer)  

---

* Administrative Tool
* NAV mod
* Own object base