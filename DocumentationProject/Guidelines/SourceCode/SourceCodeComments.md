<!-- 260101 -->

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="https://github.com/APrettyCoolProgram/APCP/blob/main/.github/img/logo/apcp-logo-dark-128x128.png">
    <source media="(prefers-color-scheme: light)" srcset="https://github.com/APrettyCoolProgram/APCP/blob/main/.github/img/logo/apcp-logo-light-128x128.png">
    <img alt="Fallback image description" src="https://github.com/APrettyCoolProgram/APCP/blob/main/.github/img/logo/apcp-logo-light-128x128.png">
  </picture>

  ### APCP ❭ Documentation Project ❭ Guidelines ❭ Documentation ❭ **Source Code Comments**

</div>

***

## C#

### Comments that start with `//`

- Contain important information that either cannot be infered from the source code, or needs further clarification
- Should not be removed from the source code
- Can span multiple lines
- Have a maximum width of 80 characters

### Comments that start with `/* */`

- Provide provide additional information or narrative about a block of code
- Can be removed from the source code
- Can span multiple lines
- Have a maximum width of 80 characters

## Prefixes

Comments may have the following prefixes:

```csharp
// DEVNOTE is a call-out comment specific to a block of code
// DEPRECIATED indicates that code has been depreciated, but not yet removed
// REVIEW indicates that code should be reviewed and/or refactor
// TODO indicates that there is something to be done.
```
