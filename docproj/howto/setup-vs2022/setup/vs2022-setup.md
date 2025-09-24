<!--
    Last updated: 250317
-->

> **This document was last updated** March 17, 2025 

<div align="center">

   <!--
    This image was created by Midjourney using the following prompt:
    "closeup of a highway sign with the words HOW TO SETUP VISUAL STUDIO 2022"
   -->
  ![logo](howto-logo.png)

</div>

CONTENTS  
[Installing Visual Studio 2022]()  
[Installing Visual Studio 2022 extensions]()  
[Setting up Visual Studio 2022]()  

This document will walk through the steps I take to setup my Visual Studio 2022 (v17.13.3) environment for C# development.

# Installing Visual Studio 2022

Installing Visual Studio 2022 is pretty straight forward, just download the [installer](https://visualstudio.microsoft.com/vs/) and follow the prompts.

## Visual Studio Workflows

The bulk of the install process is choosing the workflows you will need for development.

For example, I've installed the following workflows for developing web services using C#:

* ASP.NET and web development
* Azure Development
* .NET desktop development

The workflows you specify will take care of the other components of the installation process.

# Installing Visual Studio 2022 extensions

These are the extensions I install. You may want/need to install other extensions.

To install an extension:

1. Go to `Extensions -> Manage Extensions...`
2. Click the `Browse` tab
3. Search for the extension name
4. Click "Install"

## Recommended extensions

Microsoft has an extension pack that contains a bunch of individual extensions that are pretty great:

* [Productivity Power Tools 2022](https://marketplace.visualstudio.com/items?itemName=VisualStudioPlatformTeam.ProductivityPowerPack2022)
  * Align Assignments 2022
  * Double-Click Maximize 2022
  * Copy As Html 2022
  * Fix Mixed Tabs 2022
  * Match Margin 2022
  * Middle-Click Scroll 2022
  * Peek Help 2022
  * Shrink Empty Lines 2022
  * Solution Error Visualizer 2022
  * Time Stamp Margin 2022

[Mads Kristensen](https://www.madskristensen.net/) is a Principal Product Manager for Visual Studio, and has made some awesome extensions:

* [Add New File (64-bit)](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.AddNewFile64)
* [Clean Solution](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.CleanSolution)
* [Color Preview](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.ColorPreview)
* [Editor Enhancements](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.EditorEnhancements64)
* [File Differ](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.FileDiffer)
* [File Icons](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.FileIcons)
* [Image Optimizer (64-bit)](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.ImageOptimizer64bit)
* [Image Preview](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.ImagePreview)
* [Markdown Editor v2](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor2)
* [Show Selection Length](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.ShowSelectionLength)
* [Solution Colors](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.SolutionColors)
* [Trailing Whitespace Visualizer](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.TrailingWhitespace64)
* [Tweaks 2022](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.Tweaks2022)

And a few other useful extensions:

* [Collapse Comments](https://marketplace.visualstudio.com/items?itemName=MattLaceyLtd.CollapseComments)
* [Copy Folder Tree](https://marketplace.visualstudio.com/items?itemName=iyulab.CopyFolderTree)
* [Doc Preview](https://marketplace.visualstudio.com/items?itemName=OlegShilo.DocPreview)
* [Editor Guidelines](https://marketplace.visualstudio.com/items?itemName=PaulHarrington.EditorGuidelinesPreview)
* [JSON Pretty](https://marketplace.visualstudio.com/items?itemName=Hemax2000.JsonPretty)
* [Roslynator 2022](https://marketplace.visualstudio.com/items?itemName=josefpihrt.Roslynator2022)
* [Solution Mapper](https://marketplace.visualstudio.com/items?itemName=TJGokken.solmap2025)
* [Viasfora](https://marketplace.visualstudio.com/items?itemName=TomasRestrepo.Viasfora)
* [Visual Studio Spell Checker (VS2022 and Later)](https://marketplace.visualstudio.com/items?itemName=EWoodruff.VisualStudioSpellCheckerVS2022andLater)
* [VSColorOutput64](https://marketplace.visualstudio.com/items?itemName=MikeWard-AnnArbor.VSColorOutput64)
* [XAML Styler for Visual Studio 2022](https://marketplace.visualstudio.com/items?itemName=TeamXavalon.XAMLStyler2022)

I also use [Claudia IDE](https://marketplace.visualstudio.com/items?itemName=kbuchi.ClaudiaIDE) so I can put an image of The Dude in the background to help me abide.

## Themes

Themes/color schemes/etc. are a personal choice. I use these:

* [Dracula Official](https://marketplace.visualstudio.com/items?itemName=dracula-theme.dracula)
* [Synthwave '84 Reborn](https://marketplace.visualstudio.com/items?itemName=Fasteroid.Synthwave84VS)

## Optional extensions

There are a few extensions I install that I generally leave disabled, and only use in specific cases:

* [Pretty Doc Comments](https://marketplace.visualstudio.com/items?itemName=OlivierJacot-Descombes.PrettyDocComments)  
  If you use a lot of inline XML documentation, this can help visualize what it looks like when you use an external documentation formatter like [Sandcastle](https://ewsoftware.github.io/SHFB/html/bd1ddb51-1c4f-434f-bb1a-ce2135d3a909.htm). It does make things a bit more noisy when coding, so I leave it disabled unless I am working on documentation.

* [Code Cleanup On Save](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.CodeCleanupOnSave)  
  This is a helpful extension *if you have setup your Code Cleanup profiles*.

* [Comment Remover](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.CommentRemover)  
  Cool idea, probably pretty dangerous.

* [SonarQube for Visual Studio 2022](https://marketplace.visualstudio.com/items?itemName=SonarSource.SonarLintforVisualStudio2022)

# Configuring Visual Studio 2022 - Options

The majority of Visual Studio's configuration will be done in the `Options` window, which you can get to by going to `Tools -> Options...`

Things to remember about this list:

* It contains screenshots of settings that are either modified, or are kept for historical reasons.
* Non-modified option pages are excluded (otherwise this list would be too long).
* These modifications are what I use for C#/web service development. You may want/need to make additional modifications.

## Visual Studio specific

### Environment

  * **General** - [screenshot](settings-environment-general-01.png)

  * **Documents** - [screenshot](settings-environment-documents-01.png)

  * **Tabs and Windows** - [screenshot](settings-environment-tabs-and-windows-01.png)

  * **Trailing Whitespace** - [screenshot](settings-environment-trailing-whitespace-01.png)

  * **Task List** - [screenshot](settings-environment-task-list-01.png)

### Projects and Solutions

  * **General** - [screenshot](settings-projects-and-solutions-general-01.png)

  * **Build and Run** - [screenshot](settings-projects-and-solutions-build-and-run-01.png)

### Text Editor

  * **General** - [screenshot](settings-text-editor-general-01.png)

  * **Advanced** - [screenshot](settings-text-editor-advanced-01.png)

  * **Code Cleanup**  

    * [Profile 1](code-cleanup-profile-1-01.png)
    * [Profile 2](code-cleanup-profile-2-01.png)

#### All Languages

  * **General** - [screenshot](settings-text-editor-all-languages-general-01.png)

  * **Scroll Bars** - [screenshot](settings-text-editor-all-languages-scroll-bars-01.png)

  * **Tabs** - [screenshot](settings-text-editor-all-languages-tabs-01.png)

#### C#

  * **Advanced** - [screenshot 1](settings-text-editor-csharp-advanced-01.png) | [screenshot 2](settings-text-editor-csharp-advanced-02.png)
  
##### Code Style  

  Most of the settings in Code Style can have one of the following Severities:

  | Severity | Description |
  | -------- | ----------- |
  | `Error` | Violations appear as build errors and cause builds to fail. |
  | `Refactoring Only` | Violations aren't visible to the user.|
  | `Suggestion` | Violations appear as build messages and as suggestions in the Visual Studio IDE. |
  | `Warning` |  Violations appear as build warnings but do not cause builds to fail. |

  * **General** - [screenshot 1](settings-text-editor-csharp-code-style-general-01.png) | [screenshot 2](settings-text-editor-csharp-code-style-general-02.png)

  * **Formatting - Indentation** - [screenshot](settings-text-editor-csharp-code-style-formatting-indentation-01.png)
  * **Formatting - New Lines** - [screenshot](settings-text-editor-csharp-code-style-formatting-new-lines-01.png)
  * **Formatting - Spacing** - [screenshot](settings-text-editor-csharp-code-style-formatting-spacing-01.png)
  * **Formatting - Wrapping** - [screenshot](settings-text-editor-csharp-code-style-formatting-wrapping-01.png)
  * **Naming** - [screenshot](settings-text-editor-csharp-code-style-naming-01.png)
  * **Intellisense** - [screenshot](settings-text-editor-csharp-code-style-intellisense-01.png)

#### Intellisense

  * **Intellisense** - [screenshot](settings-text-editor-csharp-intellisense-01.png)

#### XAML

  * **General** - [screenshot](settings-text-editor-xaml-formatting-general-01.png)
  * **Spacing** - [screenshot](settings-text-editor-xaml-formatting-spacing-01.png)
  * **Miscellaneous** - [screenshot](settings-text-editor-xaml-miscellaneous-01.png)

#### GitHub

  * [CoPilot](#github-copilot)

### Editor Guidelines

1. Go to `Tools > Options > Environment > Fonts and Colors`
2. Choose `Guideline` from the *Display Items* list
3. Pick a color you like!
