<!-- u250818 -->

[[🏠︎](../README.md)] ❬ [Documentation Guidelines](README.md)

***


<div align="center">

# The APCP Documentation Project

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../.github/img/logo/apcp-logo-dark-256x256.png">
    <source media="(prefers-color-scheme: light)" srcset="../../.github/img/logo/apcp-logo-light-256x256.png">
    <img alt="Fallback image description" src="../../.github/logo/apcp-logo-light-256x256.png">
  </picture>

## Markdown

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

### Blockquotes

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
```

### Colors

```markdown

```

## Output

The background color is `#ffffff` for light mode and `#000000` for dark mode.

### Blockquotes

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

### Colors




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
![alt text](../../../apcp/.github/img/logo/apcp-logo-neon-128x128.png "Optional title")

**A reference image**  

![alt text][apcp-logo]

[apcp-logo]: ../../../apcp/.github/img/logo/apcp-logo-neon-128x128.png "Optional title"
```

## Output

**An image**  
![alt text](../../../apcp/.github/img/logo/apcp-logo-neon-128x128.png "Optional title")

**A reference image**  
![alt text][apcp-logo]  

[apcp-logo]: ../../../apcp/.github/img/logo/apcp-logo-neon-128x128.png "Optional title"

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
Here's a simple footnote,[^1] and here's a longer one.[^bignote]

[^1]: This is the first footnote.

[^bignote]: Here's one with multiple paragraphs and code.

    Indent paragraphs to include them in the footnote.

    `{ my code }`

    Add as many paragraphs as you like.         | *               | *               | *               |
```

## Output

Here is a simple footnote[^1]

[^1]: This is the footnote text


<br>

***

[[🏠︎](../README.md)] ❬ [Documentation Guidelines](README.md)