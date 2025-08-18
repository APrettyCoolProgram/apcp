> Last updated August 3, 2025

# APCP > Guidelines > Source Code > File Headers

# ProjectInfo files

Every project should have a [`ProjectInfo`](../project/project-info.md) file with the following header:

```csharp
/* %ProjectName%
 * %ProjectDescription
 * %ProjectVersion%
 *
 * %RepositoryURL%
 * Copyright (c) %CopyrightHolder%. All rights reserved.
 * Licensed under the %LicenseName% license.
 */
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
```

# Csharp

All `.cs` files should have a file header comment.

```csharp
/* %Namespace%.%ClassName%.cs
 * uYYMMDD_code
 * uYYMMDD_documentation
 */
```

For example:

```csharp
/* dvn.App.CommandLine.cs
 * u250801_code
 * u250801_documentation
 */
```
