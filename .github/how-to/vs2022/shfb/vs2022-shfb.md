<!-- u250602 -->

# Sandcastle Help File Builder

* [Integration with Visual Studio 2022](#integration-with-visual-studio-2022)
* [Create a new Visual Studio 2022 Documentation Project](#create-a-new-visual-studio-2022-documentation-project)
* [Generating Sandcastle documentation](#generating-sandcastle-documentation)

# Integration with Visual Studio 2022

Install the latest version of the [Sandcastle Help File Builder](https://github.com/EWSoftware/SHFB).

It is recommended to also install the [Extended XML Doc Comments Provider (VS2022+)](https://marketplace.visualstudio.com/items?itemName=EWoodruff.ExtendedDocCommentsProvider2022) extension.

# Create a new Visual Studio 2022 Documentation Project

You will need to create a Documentation Project for each project you want to document.

Documentation projects should start with `sfhb-`.

For example, the Documentation Project for the `myproject` would be named `sfhb-myproject`.

To create the new project:

1. Start Visual Studio
2. Create a new `Sandcastle Help File Builder Project`
3. Name the project `shfb-%Project-Name%` (example: `shfb-myproject`)
4. The location should be `myproject-documentation/sandcastle/%Project-Name%`
5. Click "Create"
6. Close the project

## Add the documentation project to the solution

1. Open the *solution* that the documentation will be created for
2. Add a new Solution Folder named `SHFB`
3. Add the documentation project to the `SHFB` folder

## Generate XML documentation

In the project you are creating documentation for:

1. Right-click -> **Properties**
2. **Build** -> **Output**
3. Check **XML documentation file**
4. Change the file path to `AppData\XmlDoc\generated.xml

## Configure the documentation project

### Project Properties -> Build

1. Change **Framework version** to the correct framework

![](./shfb/build.png)

### Project Properties -> Help File

1. Change the **Help title**
2. Change the ** Help file version**
3. Add the following to **Additional footer content**  
```html
<div align="center">
Please see the <a href="https://github.com/spectrum-health-systems/myproject">MyProject</a> for more information.
</div>
```

![](help-file.png)

(Ignore what the fields in this screenshot say, they will eventually be updated)

### Project Properties -> Help 1/Website

No changes.

### Project Properties -> MS Help Viewer

No changes.

### Project Properties -> Summaries

No changes.

### Project Properties -> Visibility

1. Check **Internal members**
2. Check **Private fields**
3. Check **Private members**

![](visibility.png)

### Project Properties -> Missing Tags

No changes.

### Project Properties -> Paths

1. Change **Help content output path** to `..\..\docs\API\%Project-Name`

![](paths.png)

(Ignore what the fields in this screenshot say, they will eventually be updated)

### Project Properties -> Components

No changes.

### Project Properties -> Plug-Ins

No changes.

### Project Properties -> Transform Args

No changes.

### Project Properties -> User Defined

No changes.

### Project Properties -> Build Events

No changes.

## Add the project to the Documentation Sources

Using the documentation project:

1. Right-click -> *Documentation Sources*
2. **Add documentation source**
3. Add the `.shfbproj` file

## Exclude documentation projects from the Solution build

On the VS2022 menu bar:

1. **Build** -> **Configuration Manager**
2. Uncheck *Build* for all documentation projects

Save everything

# Generating Sandcastle documentation

<div align="center">

```mermaid
flowchart TB
  %% Components
  Start@{shape: sm-circ, label: "Start the workflow\n[sm-circ]"}
  CleanSolution@{shape: rounded, label: "Clean Visual Studio solution"}
  BuildSolution@{shape: rounded, label: "Build Visual Studio solution"}
  BuildSandcastle@{shape: rounded, label: "Build Sandcastle documentation"}
  CommitChanges@{shape: rounded, label: "Commit changes to\nTingen-Documentation repository"}

  %% Layout
  Start --> CleanSolution:::R2_ --> BuildSolution:::R2_ --> BuildSandcastle:::U2_ --> CommitChanges:::G2_
  %% Styles
  classDef R2_ stroke:#f9ebea,stroke-width:3px,fill:#CD6155,color:#f9ebea
  classDef P2_ stroke:#f5eef8,stroke-width:3px,fill:#af7ac5,color:#f5eef8
  classDef U2_ stroke:#eaf2f8,stroke-width:3px,fill:#5499c7,color:#eaf2f8
  classDef G2_ stroke:#e9f7ef,stroke-width:3px,fill:#52be80,color:#e9f7ef
  classDef Hidden display: none;
```

</div>

## GitHub Pages

A few minutes after the `myproject` repository has been updated, the GitHub Pages site will be refreshed.
