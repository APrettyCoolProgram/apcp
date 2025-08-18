> Last updated August 3, 2025

# APCP > Guidelines > Projects > ProjectInfo

Each project should have a `ProjectInfo` file containing information about the project.

For example, a C# project would have a `ProjectInfo.cs` file with the following content:

```csharp
/* %ProjectName%
 * %ProjectDescription
 * %ProjectVersion%
 *
 * %RepositoryURL%
 * Copyright (c) %CopyrightHolder%. All rights reserved.
 * Licensed under the %LicenseName% license.
 */

namespace %Namespace%;

internal class %ClassName%
{
    // This class is only used for informational purposes.
}
```

For example:

```csharp
/* dvn
 * A command line utility for managing development environments.
 * v1.0.1 (b250802)
 *
 * https://github.com/APrettyCoolProgram/dvn
 * Copyright (c) A Pretty Cool Program. All rights reserved.
 * Licensed under the Apache 2.0 license.
 */

namespace dvn;

internal class ProjectInfo
{
    // This class is only used for informational purposes.
}
```
