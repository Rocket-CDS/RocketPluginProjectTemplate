# RocketPluginProjectTemplate

## Introduction
Provides a working template for creating a Visual Studio PLUGIN project for a RocketCDS.

RocketCDS systems can have plugins to expand functionality.  This project is a template to help create plugins for different systems.

By default all RocketCDS systems support plugins, but plugins can be blocked by the system creator.  Check the documentation of the system you wish to add a plugin to.

## Architechutre
RocketCDS plugins are always attached to a system and must have a unique name.


## Creating a new Project
Copy the template to you working environment. This is usually "/DesktopModules/DNNrocketModules/\<new plugin project>"

Rename the project folder to be the new project plugin name, if not already done.

Replace "rocketpluginprojecttemplate" with "\<newprojectname>" case-sensitive in Project

Replace "RocketPluginProjectTemplate" with "\<NewProjectName>" case-sensitive in Project

Replace "rocketsystemprojecttemplate" with the "\<systemkey>" of the system the plugin is for, case-sensitive in Project

Replace "RocketSystemProjectTemplate" with the "\<SystemKey>" of the system the plugin is for, case-sensitive in Project

Rename Project files

Rename Solution files

Rename Resx File.

Rename DNN manifest.

Ensure all projects in the solution are loaded. The project uses project references and expects the source code, but you can also add a reference to the assemblies. External assemblies are usually added to a "_external" folder underneath the project root.

This project should be able to be compiled.

## Database Changes

The project template uses an example a class limpet call "ArticleLimpet" and "ArticleListLimpet".  These are the data source access points.  By default they use the DNNrocket table, but this may need to be changed depending of what table the Rocket System is using.  

The database table column "TYPECODE" should usually be unique for the plugin, the template therefore appends the plugin name to the default TYPECODE name.

The plugin template expects to be using the system database tables, no database tables or SPROCs have been defined.  Although each plugin can have their own set of tables and SPROCs is required.  

NOTE: "ArticleLimpet" and "ArticleListLimpet" are examples and are expected to be replaced or altered to make the plugin work.



