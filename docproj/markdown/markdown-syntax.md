<!-- u251023 -->

[[🏠︎](../README.md)]

***

<div align="center">

### The APCP Documentation Project

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../.github/img/logo/apcp-logo-dark-256x256.png">
    <source media="(prefers-color-scheme: light)" srcset="../../.github/img/logo/apcp-logo-light-256x256.png">
    <img alt="Fallback image description" src="../../.github/logo/apcp-logo-light-256x256.png">
  </picture>

# Markdown syntax

</div>

### CONTENTS

* [Overview](#overview)
* [Headings](#headings)
* [Callouts](#callouts)
* [Paragraphs and line breaks](#paragraphs-and-line-breaks)
* [Emphasis](#emphasis)
* [Lists](#lists)
* [Links](#links)
* [Images](#images)
* [Code blocks and escape characters](#code-blocks-and-escape-characters)
* [Separators](#separators)
* [Tables](#tables)
* [Footnotes](#footnotes)

# Overview

[Markdown](https://en.wikipedia.org/wiki/Markdown) Markdown is a text-to-HTML conversion tool for web writers. Markdown allows you to write using an easy-to-read, easy-to-write plain text format, then convert it to structurally valid HTML.

# Headings

* Blank line before and after a heading

## Code

```markdown
# H1
## H2
### H3
#### H4
##### H5
###### H6
```

## Output
# H1
## H2
### H3
#### H4
##### H5
###### H6

# Callouts

## Code

```markdown
> Single line blockquote

> Multiple line
> blockquote

> A blockquote  
> that has  
>> A *nested*  
>> blockquote!

> # You can do cool things with blockquotes, like:
> * Use headings
> * Use lists  
> * **Ephasize stuff**
> <br>  
> Even put a link to [The APCP Documentation Project](https://github.com/APrettyCoolProgram/apcp/blob/main/docproj/README.md)!

**Callout alerts**
> [!NOTE]
> Useful information that users should know, even when skimming content.

> [!TIP]
> Helpful advice for doing things better or more easily.

> [!IMPORTANT]
> Key information users need to know to achieve their goal.

> [!WARNING]
> Urgent info that needs immediate user attention to avoid problems.

> [!CAUTION]
> Advises about risks or negative outcomes of certain actions.

```

## Output

> Single line blockquote

> Multiple line
> blockquote

> A blockquote  
> that has  
>> A *nested*  
>> blockquote!

> # You can do cool things with blockquotes, like:
> * Use headings
> * Use lists  
> * **Ephasize stuff**
> <br>  
> Even put a link to [The APCP Documentation Project](https://github.com/APrettyCoolProgram/apcp/blob/main/docproj/README.md)!

> [!NOTE]
> Useful information that users should know, even when skimming content.

> [!TIP]
> Helpful advice for doing things better or more easily.

> [!IMPORTANT]
> Key information users need to know to achieve their goal.

> [!WARNING]
> Urgent info that needs immediate user attention to avoid problems.

> [!CAUTION]
> Advises about risks or negative outcomes of certain actions.


# Paragraphs and line breaks

## Code

```markdown
Paragraphs have blank lines between them.

Like this.

And line breaks are done either with trailing whitepace  
or<br>
the `<br>` tag

```

## Output

Paragraphs have blank lines between them.

Like this.

And line breaks are done either with trailing whitepace  
or<br>
the `<br>` tag

# Emphasis

## Code

```markdown
*Italic*  
**Bold**  
~~strikethrough~~  
This is <sub>subscript</sub>  
This is <sup>superscript</sup>  
<ins>underline</ins>  

And *~~you~~ **can*** <sub>com*bine*</sub> <sup><ins>all</ins> of</sup> <ins>these!</ins>
```

## Output

*Italic*  
**Bold**  
~~strikethrough~~  
This is <sub>subscript</sub>  
This is <sup>superscript</sup>  
<ins>underline</ins>  

And *~~you~~ **can*** <sub>com*bine*</sub> <sup><ins>all</ins> of</sup> <ins>these!</ins>

# Lists

## Code

```markdown
1. Fruit
    * Apple
      - Granny Smith
      - Fuji
      - Macintosh
    * Orange
      - Small
      - Large
    * Pear
      - Green
2. Color
    * Red  
      Trailing two spaces above
    * Blue  
      Allows for a description below
```

## Output

1. Fruit
    * Apple
      - Granny Smith
      - Fuji
      - Macintosh
    * Orange
      - Small
      - Large
    * Pear
      - Green
2. Color
    * Red  
      Trailing two spaces above
    * Blue  
      Allows for a description below
3. ToDo
    * [x] First thing
    * [ ] Second thing
    * [ ] Third thing

# Links

## Code

```markdown
**A simple URL link**  
<https://github.com/APrettyCoolProgram/apcp/blob/main/docproj/README.md>

**A text link**  
[The APCP Documentation Project](https://github.com/APrettyCoolProgram/apcp/blob/main/docproj/README.md)

**An image link**  
[![The APCP logo](../../../apcp/.github/img/logo/apcp-logo-neon-128x128.png "Optional discription")](https://github.com/APrettyCoolProgram/apcp/blob/main/docproj/README.md)

**Reference links**  
Read the [Mermaid.js][1] or the [XML Documentation (for C#)][xmldoc] documents!

[1]: https://github.com/APrettyCoolProgram/apcp/blob/main/docproj/doc/mermaid-js.md  "Optional Mermaid.js hover text"
[xmldoc]: https://github.com/APrettyCoolProgram/apcp/blob/main/docproj/doc/xml-csharp.md  "Optional XML Documentation hover text"
```

## Output

**A simple URL link**  
<https://github.com/APrettyCoolProgram/apcp/blob/main/docproj/README.md>

**A text link**  
[The APCP Documentation Project](https://github.com/APrettyCoolProgram/apcp/blob/main/docproj/README.md)

**An image link**  
[![The APCP logo](../../../apcp/.github/img/logo/apcp-logo-neon-128x128.png "Optional discription")](https://github.com/APrettyCoolProgram/apcp/blob/main/docproj/README.md)

**Reference links**  
Read the [Mermaid.js][1] or the [XML Documentation (for C#)][xmldoc] documents!

[1]: https://github.com/APrettyCoolProgram/apcp/blob/main/docproj/doc/mermaid-js.md  "Optional Mermaid.js hover text"
[xmldoc]: https://github.com/APrettyCoolProgram/apcp/blob/main/docproj/doc/xml-csharp.md  "Optional XML Documentation hover text"

# Images

## Code

```markdown
**An image**  
![alt text](../../.github/img/logo/apcp-logo-neon-128x128.png "Optional title")

**A reference image**  
![alt text][apcp-logo]

[apcp-logo]: ../../.github/img/logo/apcp-logo-neon-128x128.png "Optional title"

**Light/dark mode images**  
<picture>
  <source media="(prefers-color-scheme: dark)" srcset="../../.github/img/logo/apcp-logo-dark-256x256.png">
  <source media="(prefers-color-scheme: light)" srcset="../../.github/img/logo/apcp-logo-light-256x256.png">
  <img alt="Cannot determine light/dark mode" src="../../.github/logo/apcp-logo-light-256x256.png">
</picture>
```

## Output

**An image**  
![alt text](../../.github/img/logo/apcp-logo-neon-128x128.png "Optional title")

**A reference image**  
![alt text][apcp-logo]

[apcp-logo]: ../../.github/img/logo/apcp-logo-neon-128x128.png "Optional title"

**Light/dark mode images**  
<picture>
  <source media="(prefers-color-scheme: dark)" srcset="../../.github/img/img/logo/apcp-logo-dark-256x256.png">
  <source media="(prefers-color-scheme: light)" srcset="../../.github/img/img/logo/apcp-logo-light-256x256.png">
  <img alt="Cannot determine light/dark mode" src="../../.github/img/logo/apcp-logo-light-256x256.png">
</picture>

# Code blocks and escape characters

The following characters can be escaped:
```text
\
`
*
_
{ }
[ ]
< >
( )
#
+
-
.
!
|
```

## Code

**Escape characters**  
\`This should be rendered as a line of code, but it isn't\`  

**Single line of code**  
\`Code goes here\`

**Multiple lines of code**  
\```text  
Code  
goes  
here  
\```

## Output

**Escape characters**  
\`This should be rendered as a line of code, but it isn't\`

**Single line of code**  
`Code goes here`

**Multiple lines of code**  
```text
Code
goes
here
```

# Separators

## Code

```markdown
**Horizonal rule**
Blank line before...

***

...and after
```

## Output

Blank line before...

***

...and after

# Tables

## Code

```markdown
| Standard        | Left            | Center          | Right           |
| --------------- | :-------------- | :-------------: | --------------: |
| Justifed        | Justifed        | Justifed        | Justifed        |
| *               | *               | *               | *               |
```

## Output

| Standard        | Left            | Center          | Right           |
| --------------- | :-------------- | :-------------: | --------------: |
| Justifed        | Justifed        | Justifed        | Justifed        |
| *               | *               | *               | *               |

# Footnotes

## Code

```markdown
Here is a simple footnote[^1].

A footnote can also have multiple lines[^2].

[^1]: My reference.
[^2]: To add line breaks within a footnote, prefix new lines with 2 spaces.
  This is a second line.
```

## Output

Here is a simple footnote[^1].

A footnote can also have multiple lines[^2].

[^1]: My reference.
[^2]: To add line breaks within a footnote, prefix new lines with 2 spaces.  
  This is a second line.

<br>

***

[[🏠︎](../README.md)]