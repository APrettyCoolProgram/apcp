<!-- u251023 -->

[[🏠︎](../README.md)]

<div align="center">

### The APCP Documentation Project

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../.github/img/logo/apcp-logo-dark-256x256.png">
    <source media="(prefers-color-scheme: light)" srcset="../../.github/img/logo/apcp-logo-light-256x256.png">
    <img alt="Fallback image description" src="../../.github/logo/apcp-logo-light-256x256.png">
  </picture>

# The ProjectInfo file

</div>

Each project should have a `ProjectInfo` file with XML Documentation (or equivalent) containing the following information about the project:

* A detailed description of the project
* Information about project resources (repositories, websites, etc.)
* Project documentation

The XML documentation can either be in the source code, or in an external file.

## ProjectInfo.cs

A C# project would have a `ProjectInfo.cs` file with the following content:

```csharp
// =============================================================================
// %ProjectName%
// %ProjectDescription%
// %ProjectURL%
// Copyright (c) A Pretty Cool Program. All rights reserved.
// Licensed under the Apache 2.0 license.
// -----------------------------------------------------------------------------
// Release YY.MM -OR- Version X.y.z
// Build YYMMDD
// =============================================================================

// uYYMMDD_code
// uYYMMDD_documentation

namespace %Namespace%;

    ///<summary>Provides additional information about the Tingen Web Service.</summary>
    ///<remarks>
    ///  <para>
    ///    <b>About %ProjectName%</b><br/>
    ///    About the project.
    ///  </para>
    ///  <para>
    ///    <b>Project resources</b><br/>
    ///    <see href = "https://github.com/repository">%ProjectName% repository</see><br/>
    ///    <see href="https://awebsite.com/">A website</see>
    ///  </para>
    ///  <para>
    ///    <b>Documentation</b>
    ///    <br/>
    ///    <see href="https://github.com/repository/documentation">Documentation</see>
    ///  </para>
    ///</remarks>
    internal class ProjectInfo
    {
        // This class is only used for informational purposes, and does not contain executable code.
    }
```

For example:

```csharp
// =============================================================================
// dvn
// A command line utility for managing development environments.
// https://github.com/APrettyCoolProgram/dvn
// Copyright (c) A Pretty Cool Program. All rights reserved.
// Licensed under the Apache 2.0 license.
// -----------------------------------------------------------------------------
// Version 1.0.1
// Build 250802
// =============================================================================

// u250802_code
// u250802_documentation

namespace dvn;

    ///<summary>Provides additional information about the dnv</summary>
    ///<remarks>
    ///  <para>
    ///    <b>About dvn</b><br/>
    ///    dvn is a command-line utility for managing development environments.
    ///  </para>
    ///  <para>
    ///    <b>Project resources</b><br/>
    ///    <see href = "https://github.com/APrettyCoolProgram/dvn">dvn repostitory</see><br/>
    ///  </para>
    ///  <para>
    ///    <b>Documentation</b>
    ///    <br/>
    ///    <see href="https://github.com/APrettyCoolProgram/dvn/blob/main/README.md">dvn documentation</see>
    ///  </para>
    ///</remarks>
    internal class ProjectInfo
    {
        // This class is only used for informational purposes, and does not contain executable code.
    }
```

<br>

***

[[🏠︎](../README.md)]
