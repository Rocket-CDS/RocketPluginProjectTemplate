# RocketPluginProjectTemplate

**Download Here:** [https://github.com/Rocket-CDS/RocketPluginProjectTemplate](https://github.com/Rocket-CDS/RocketPluginProjectTemplate)  

## Introduction
Provides a working template for creating a Visual Studio PLUGIN project for a RocketCDS.

RocketCDS systems can have plugins to expand functionality.  RocketPluginProjectTemplate is a template to help create plugins for different systems.

By default all RocketCDS systems support plugins, but plugins can be blocked by the system creator.  Check the documentation of the system you wish to add a plugin to.

## Architechutre
RocketCDS plugins are always attached to a system and must have a unique name.  
In the "system.rules" the XML node "genxml/plugin" MUST be set to true.  This prevents the plugin processing on the scheulder like a system.


## Creating a new Project

**NOTE: Do NOT use an underscore ( _ ) in the new project name.  The underscore is used as a delimiter in RocketCDS.**  

Copy the template to your working environment. This is usually "/DesktopModules/DNNrocketModules/\<new plugin project>"  (Download zip and extract)  

You have 2 options to rename the template.  "Auto Replace" or "Manually".  

### Option 1 - Auto Replace
Use https://github.com/Rocket-CDS/RenameProj

Example...
```
RenameProj.exe D:\Nevoweb\Projects\DesktopModules\DNNrocketModules\RocketIntraSqlReports rocketintra
```
### Option 2 - Manually
Rename the project folder to be the new project plugin name.  
Rename Project files.  
Rename Solution files.  
Rename Resx File.  
Rename DNN manifest.  
Replace "rocketpluginprojecttemplate" with "\<newprojectname>" case-sensitive in Project.  
Replace "RocketPluginProjectTemplate" with "\<NewProjectName>" case-sensitive in Project.  
Replace "rocketsystemprojecttemplate" with the "\<systemkey>" of the system the plugin is for, case-sensitive in Project.  
Replace "RocketSystemProjectTemplate" with the "\<SystemKey>" of the system the plugin is for, case-sensitive in Project.  

### Final Actions 
Update "interfaceicon" node in system.rules file.  
Update plugin name in SideMenu.resx file.  

Ensure all projects in the solution are loaded. The project uses project references and expects the source code, but you can also add a reference to the assemblies. External assemblies are usually added to a "_external" folder underneath the project root.  

This project should now compile.

## IMPORTANT: DataObjectLimpet.cs
**Edit "DataObjectLimpet.cs" class.**  
The DataObjectLimpet.cs class should match the required data objects.  Usually copying the system DataObjectLimpet.cs Populate() method will be enough.  

## Database Changes

The project template uses an example a class limpet call "ArticleLimpet" and "ArticleListLimpet".  These are the data source access points.  By default they use the DNNrocket table, but this may need to be changed depending of what table the Rocket System is using.  

The database table column "TYPECODE" should usually be unique for the plugin, the template therefore appends the plugin name to the default TYPECODE name.

The plugin template expects to be using the system database tables, no database tables or SPROCs have been defined.  Although each plugin can have their own set of tables and SPROCs if required.  

NOTE: "articleLimpet" and "articleListLimpet" are examples and are expected to be replaced or altered to make the plugin work.

## Commands

**Any commands need to be defined in the "SystemDefaults.rules" file.**

**IMPORTANT: Any changes made to the interfaces, "system.rules" file, must be updated in the /DNNrocketmodules/"system name"/Plugins/"Plugin Name"/system.rules**.  This is usually only done on install, so if you change the interface in development, the "system.rules" file will need to be copied to the system plugin folder.

## Interfaces

Extra plugin interfaces or changes to the plugin interfaces, in the "system.rules" file, will NOT be updated in the system until the new interfaces have been loaded.  

To load the interfaces from the "system.rules":  

1. Edit the "/DesktopModules/DNNRocketModules/\<project name>/system.rules" system interface file.
2. Create a Folder in "/DesktopModules/DNNRocketModules/\<system name>/Plugins/\<plugin name new folder>" and place the "system.rules" file it the new folder.
3. Reinstall the plugin through the DNN UI.

## Providers

Remove any provider config from the providerdata list in system.rules, for providers not being used.  
NOTE: The provider XML with the plugin name should not be removed.  This identifies the pugin to the system, so it can be listed on the system plugin list.  

## [RocketDirectoryAPI]
If the plugin is going to be used with RocketDirectory then some code has been commented out, then needs to be used.  
Search the project for "[RocketDirectoryAPI]", this will identify the lines that need to be uncommented.  
