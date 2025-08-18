[[🏠︎](https://github.com/APrettyCoolProgram)] ❬ [Guidelines](../README.md) ❬ [Documentation Guidelines](README.md)

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../img/logo/apcp-logo-dark-128x128.png">
    <source media="(prefers-color-scheme: light)" srcset="../../img/logo/apcp-logo-light-128x128.png">
    <img alt="Fallback image description" src="../../img/logo/apcp-logo-light-128x128.png">
  </picture>

# APCP Documentation Project

</div>


CONTENTS
- [XML documentation guidelines](#xml-documentation-guidelines)
  - [Classes](#classes)
    - [Class methods](#class-methods)
  - [Example of external Class XML Documentation](#example-of-external-class-xml-documentation)
  - [Common stuff](#common-stuff)
  - [XML documentation standards](#xml-documentation-standards)
  - [Documentation timestamps](#documentation-timestamps)
- [Source code XML documentation](#source-code-xml-documentation)
- [External XML documentation](#external-xml-documentation)
- [XML documentation formatting](#xml-documentation-formatting)
  - [Text Formatting](#text-formatting)
  - [Special Characters](#special-characters)
- [XML documentation examples](#xml-documentation-examples)
  - [\<code\> and \<c\> tags](#code-and-c-tags)
  - [\<example\> tag](#example-tag)
  - [\<remarks\> tag](#remarks-tag)
  - [\<see\> tag](#see-tag)
  - [\<seealso\> tag](#seealso-tag)
  - [\<value\> tag](#value-tag)
- [Complete XML documentation example](#complete-xml-documentation-example)

# XML documentation guidelines

Projects should use both ***source code*** and ***external*** XML documentation.

## Source code XML documentation

Source code XML documentation should provide all *required* information about a component.

The following XML documentation tags should **always** be in the source code, since they are helpful when viewing the source code.

- `<summary>`
- `<param>`
- `<returns>`
- `<value>`
- `<see>`
- `<seealso>`

The `<remarks>` tag **can be** in the source code, as long as they follow the [source code documentation structure](#source-code-documentation-structure):

## External XML documentation

External XML documentation should provide *non-essential but helpful* detailed information, examples, and other resources for a component.

The following XML documentation tags are *generally* stored in external files, in order to keep the source code cleaner/more readable:

- `<code>`
- `<example>`
- `<exception>`

## What should be documented<!-- omit in toc -->

The following source code components should be commented:

## Classes

```csharp
/// <summary>What the class does.</summary>
/// <remarks>
///     Important information, or additional details, about this class.
/// </remarks>
```

You should point to existing documentation, if it exists:

```csharp
/// <seealso href= "https://github.com/spectrum-health-systems/Tingen-Documentation">Tingen documentation</seealso>
```

If there is an external XML documentation file:

```csharp
/// <include file='XmlDoc/%Namespace%.%ClassName%_doc.xml' path='%Namespace%/Type[@name=Class]/%ClassName%/*'/>
```

### Class properties

```csharp
/// <summary>What the property is for.</summary>
/// <remarks>
///     Important information, or additional details, about this property.
/// </remarks>
/// <value> The default value of the property <value>
```

If there is an external XML documentation file:

```csharp
/// <include file='XmlDoc/%Namespace%.%ClassName%_doc.xml' path='%Namespace%/Property[@name="Property"]/%PropertyName%/*'/>
```

### Class methods

```csharp
/// <summary>What the method does.</summary>
/// <remarks>
///     Important information, or additional details, about this method.
/// </remarks>
/// <param name="parameter">If the method has parameters.</param>
/// <returns>If the method returns something.</returns>
```

Any examples or extensive remarks should be placed in an external file:

```csharp
/// <include file='XmlDoc/%Namespace%.%ClassName%_doc.xml' path='%Namespace%/Method[@name="Method"]/%MethodName%/*'/>
```

## Example of external Class XML Documentation

External Class XML documentation should look like this:

```xml
<!-- u241023 -->

<!-- This is the external XML Documentation file for %Namespace%cs -->

<%Namespace%>

    <!-- Classes -->
    <Type name="Class">

        <!-- ClassName% -->
            <%ClassName%>
                <remarks>
                    <para>
                        Additional detailed remarks about the class.
                    </para>
                </remarks>
                <seealso href="%DocumentationURL">Project documentation</seealso>
            </%ClassName%>

    </Type>
	
    <!-- Properties -->
    <Type name="Property">

        <!-- PropertyName% -->
            <%PropertyName%>
                <remarks>
                    <para>
                        Additional detailed remarks about the property.
                    </para>
                </remarks>
            </%CPropertyName%>

    </Type>

    OR

    <!-- Properties -->
    <Type name="Method>">
        <!-- Properties for this class are defined in the common file. -->
    </Type>
	
    <!-- Methods -->
    <Type name="Method">

        <!-- %MethodName%() -->
        <%MethodName%>
            <remarks>
                <para>
                    Additional detailed remarks about the method.
                </para>
            </remarks>
        </%MethodName%>

        OR

    <!-- Methods -->
    <Type name="Method>">
        <!-- Methods for this class are documented in the source code. -->
    </Type>

	</Type>

</%Namespace%>
```

Please note there are blank lines in the following locations:
* After `<%Namespace%>` and before `</%Namespace%>`
* After `<%ClassName%>` and before `</%ClassName%>`
* After `<%PropertyName%>` and before `</%PropertyName%>`
* After `<%MethodName%>` and before `</%MethodName%>`
* After `<Type name="">` and before `</Type name="">`

Class documentation:
* The class documentation block starts with a `<!-- Classes -->` comment
* In general, there should only be a single class description
* Individual class blocks are separated by blank lines
* Individual class blocks start with a `<!-- %ClassName.cs% -->` comment

Property documentation:
* The class documentation block starts with a `<!-- Properties -->` comment
* Individual property blocks are separated by blank lines
* Individual property blocks start with a `<!-- %PropertyName% -->` comment

Method documentation:
* The method documentation block starts with a `<!-- Methods -->` comment
* Individual method blocks are separated by blank lines
* Individual method blocks start with a `<!-- %MethodName()% -->` comment

Also:
* Class descriptions should contain any external sources, such as documentation, etc.

## Common stuff

Common stuff should be documented in an external file named XmlDoc/%Namespace%-Common_doc.xml`

Common stuff should look like this:

```csharp
/// <summary>What the class does.</summary>
/// <include file='XmlDoc/%Namespace%-Common_doc.xml' path='%Namespace%-Common/Type[@name=%TypeName%]/%EntryName%/*'/>
```

The documentation should look like this:

```xml
 <!-- Logging -->
 <Type name="%TypeName%">

    <!--  %EntryName% [ <include file='XmlDoc/%Namespace%-Common_doc.xml' path='%Namespace%-Common/Type[@name="%TypeName%"]/%EntryName%/*'/> ] -->
    <%EntryName%>
        <remarks>
            <para>
                Description of the entry.
            </para>
        </remarks>
    </%EntryName%>

 </Type>
```

## XML documentation standards

In addition to [Microsoft's XML documentation standards](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/):

- All publicly visible types and their public members are documented.
- It is recommended that private members also be documented.
- It is recommended that comment text length not exceed 85 characters.
- XML documentation is written using complete sentences ending, with full stops.
- XML documentation has a maximum width of 120 characters.
- XML documentation tags should be properly indented.
- Italics may be used to bring attention to important information.
- Terms in bold (`<b>Term</b>`) must be accompanied by a `<seealso>` tag.

Also, please see [Microsoft's recommended XML tags for C#](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/recommended-tags).

## Documentation timestamps

Each file should contain an HTML comment at the top of the file indicating the last changed date:

```html
<!-- 
    u241031
    This is the external XML Documentation file for the %Namespace%.%ClassName% class.
-->
```

## Source code documentation structure<!-- omit in toc -->

Source code XML documentation should follow these guidelines:

- Comments (including tags) should fit on a single line
- Comments (not including tags) should not exceed 120 characters

## Example of source code XML documentation<!-- omit in toc -->

```csharp
/// <summary>Does a thing.</summary>
/// <param name="aParameter">A parameter.</param>
/// <remarks>This thing does a thing.</remarks>
/// <returns>A bunch of things.</returns>
```

# External XML documentation



If a `<remarks>` entry is longer than a single line, it should contained in an external file, not the source code.

## External files<!-- omit in toc -->

Each *namespace* has it's own external XML documentation file containing the external XML documentation for all *classes* in the namespace. The file is located in the project's `./XmlDoc/` folder with the syntax of `%namespace%_doc.xml`

For example, XML documentation for `Namespace.Thing` namespace is located in the `./XmlDoc/Namespace.Thing_doc.xml` file, and contains all of the external XML documentation for all of the classes in the `Namespace.Thing` namespace.

## Example of source code XML documentation<!-- omit in toc -->

```xml
<!-- u240805-->

<%Namespace%>

	<!--
		%Namespace%.%ClassName%.cs
	-->
    <Cs name="%ClassName%">
        <%MethodName%>
            <remarks>
                <para>
                    This is a list
                    <list type="bullet">
                        <item>Bullet 1</item>
                        <item>Bullet 2</item>
                    </list>
                    <br/>
                    More comments.
                    <br/>
                    More comments.
                </para>
            </remarks>
        </%MethodName%>
        <%PropertyName%>
            <remarks>
                <para>
                    Remarks about the property.
                </para>
            </remarks>
        </%PropertyName%>
    </Cs>
</%Namespace%>
```

## Referencing external XML documentation in the source code<!-- omit in toc -->

To reference external XML documentation in the source code, add the following line at the end of the source code XML documentation:

```csharp
/// <include file='XmlDoc/%Namespace%_doc.xml' path='%Namespace%/Cs[@name="%ClassName%"]/%MethodName%/*'/>
```

For example:

```csharp
/// <summary>Does a thing.</summary>
/// <param name="aParameter">A parameter.</param>
/// <param name="anotherParameter">Another paramtere.</param>
/// <returns>Some things.</returns>
/// <include file='XmlDoc/%Namespace%_doc.xml' path='%Namespace%/Cs[@name="%ClassName%"]/%MethodName%/*'/>
```

# XML documentation formatting

## Text Formatting

XML documentation comments can using the following formatting tags:

- `<b>bold</b>`
- `<i>italic</i>`
- `<u>underline</u>`

## Special Characters

XML documentation can include the following special characters:

- Greater than: `&gt;`
- Lesser than: `&lt;`
- Space: `&nbsp;`

# XML documentation examples

## &lt;code&gt; and &lt;c&gt; tags

To insert a single line of code, or inline code, `<c>just put the code between these</c>`

To create a code block:

```csharp
/// <code>
///	  if (i = 3}
///	  {
///	    print "Hello world!"
///	  }
/// <code>
```

## &lt;example&gt; tag

Example comments don't show in Visual Studio, but look fine in generated documentation.

An example of...uh...an example XML comment:

```csharp
/// <example>
///     Here is an example of the code:
///     <code>
///		  if(numberOne == numberTwo)
///		  {
///         return true;
///       }
///       else
///       {
///         return false;
///       }
///     </code>
/// </example>
```

## &lt;list&gt; tag<!-- omit in toc -->

### Bullet list<!-- omit in toc -->

Bulleted lists should follow these guidelines:

- All items (including tags) should fit on a single line
- It is recommended that item text not exceed 85 characters, and should not exceed 120 characters
- Items should not end, or contain, periods
- Proper indenting should be used

```xml
<para>
    An introduction:
    <list type="bullet">
      <item>Bullet 1</item>
      <item>Bullet 1</item>
      <item>Bullet 1</item>
    </list>
</para>
```

### Numbered list<!-- omit in toc -->

TBA

### Tables<!-- omit in toc -->

Tables should follow these guidelines:

- An optional `listheader` may be used
- All items (including tags) should fit on a single line
- It is recommended that item text not exceed 85 characters, and should not exceed 120 characters
- Items should not end, or contain, periods
- Proper indenting should be used

```xml
<para>
    An introduction:
    <list type="table">
        <listheader>
            <term>Term header</term>
            <description>Description header</description>
        </listheader>
        <item>
            <term>Term 1</term>
            <description>Description 1</description>
        </item>
        <item>
            <term>Term 2</term>
            <description>Description 3</description>
        </item>
    </list>
</para>
```

## &lt;remarks&gt; tag

Remarks can contain complex lists:

```csharp
/// <remarks>
///     <para>
///         This method needs two things:
///         <list type="number">
///             <item>A number</item>
///             <item>Another number</item>
///         </list>
///     </para>
///     <para>
///         Numbers to be compared <u>must</u>:
///         <list type="bullet">
///             <item>
///                 Be &gt;= zero
///             </item>
///             <item>
///                 Be <i>less</i> than <c>100</c>
///             </item>
///             <item>
///                 Not be an <b>even</b> number
///             </item>
///         </list>
///     </para>
/// </remarks>
```

Remarks can contain tables, but they kind of look wonky in Visual Studio (but look good in DocFX):

```csharp
/// <remarks>
///     <para>
///         Keep in mind:
///         <list type="table">
///             <listheader>
///                 <term>Number</term>
///                 <description>Validity</description>
///             </listheader>
///             <item>
///                 <term>1</term>
///                 <description>Valid.</description>
///             </item>
///             <item>
///                 <term>2</term>
///                 <description>Invalid</description>
///             </item>
///             <item>
///                 <term>200</term>
///                 <description>Invalid</description>
///             </item>
///         </list>
///     </para>
/// </remarks>
```

## &lt;see&gt; tag

For simple references:

```csharp
This is a reference to <see cref="AThing"/>.
```

For more complex references:

```csharp
This is a reference to <see cref="ANamespace.AClass.AProperty">AProperty</see>.
```

For a URL/webpage:

```csharp
Please see the <see href="github.com/spectrum-health-systems/Tingen-Documentation/blob/main/Glossary.md#avatar-optionobject">Tingen documentation</see> for more information.
```

## &lt;seealso&gt; tag

```csharp
<seealso href="https://github.com/spectrum-health-systems/Tingen-Documentation/blob/main/Glossary.md#avatar-system-codes">System Codes</seealso>
```

## &lt;value&gt; tag

The `<value>` tag defines a value for something.

Example:

```csharp
/// <value>Default value is <c>false</c></value>
```

<!-- 

<paramref name="ReturnOptionObject"/>

See <see cref="GetVersion()"/> to add doubles.

<seealso cref="GetVersion()"/>

<i>Module</i>

<see cref="member"/>
<see cref="member">Link text</see>
<see href="link">Link Text</see>
<see langword="keyword"/>

<seealso cref="member"/>

<seealso href="link">Link Text</seealso>
 -->

# Complete XML documentation example

This example of an XML documentation comment details the correct tag order:

```csharp
/// <summary>Compares two numbers to determine if they are the same.</summary>
/// <param name="numberOne">The first number.</param>
/// <param name="numberTwo">The second number.</param>
/// <remarks>
///     <para>
///         This method needs two things:
///         <list type="number">
///             <item>A number</item>
///             <item>Another number</item>
///         </list>
///     </para>
///     <para>
///         Numbers to be compared <u>must</u>:
///         <list type="bullet">
///             <item>
///                 Be &gt;= zero
///             </item>
///             <item>
///                 Be <i>less</i> than <c>100</c>
///             </item>
///             <item>
///                 Not be an <b>even</b> number
///             </item>
///         </list>
///     </para>
///     <para>
///         Keep in mind:
///         <list type="table">
///             <listheader>
///                 <term>Number</term>
///                 <description>Validity</description>
///             </listheader>
///             <item>
///                 <term>1</term>
///                 <description>Valid.</description>
///             </item>
///             <item>
///                 <term>2</term>
///                 <description>Invalid</description>
///             </item>
///             <item>
///                 <term>200</term>
///                 <description>Invalid</description>
///             </item>
///         </list>
///     </para>
///     <para>
///         For more information please review the <see href="../link/GoesHere.html#Anchor">Abatab Documentation Project</see>.
///     </para>
/// </remarks>
/// <example>
///     Here is an example of the code:
///     <code>
///     if(numberOne == numberTwo)
///     {
///         return true;
///     }
///     else
///     {
///         return false;
///     }
///     </code>
/// </example>
/// <returns>A boolean of true or false.</returns>
/// <value>Default value is <c>false</c></value>
```
