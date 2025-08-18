<!--
    This should be broken down into smaller parts/files.
-->

> Last updated August 3, 2025

# APCP > Guidelines > Source Code > C# > Methods

# Variables

- Variables are declared in their own block at the top of a method.
- Implicit variables are declared before explicit variables.
- A blank line follows variable declarations.
- Return statements are separated by a blank line.

```csharp
public static int MethodOne()
{
    var numberOne   = 1;
    var numberTwo   = 2;
    int numberThree = 3;

    var total = numberOne + numberTwo + numberThree;

    return total;
}
```

# Code blocks

## if...else and if...else if...else

- `return` statements appear at the end of the method, not in the `if...if else...else` block.
- `return` statements are preceded by a blank line.

```csharp
public static int MethodOne(bool doWork)
{
	string workStatus = "";
	
    if (doWork)
    {
        // Code goes here

		workStatus = "Work done."
    }
    else
    {
        // Code goes here

		workStatus = "Work not done."
    }

    return workStatus;
}
```

## switch

- `case` *statements* are not followed by a blank line.
- `case` *blocks* are followed by a blank line.
- `break` and `return` statements are preceded *and* followed by blank lines.
- There is always a `default` case.

```csharp
public static void DoThing()
{
    switch (thing)
    {
        case "valueOne":
            // Code goes here
            
            break;

        case "valueOne":
            // Code goes here

            break;

        default:
            // Code goes here

            break;
    }
}
```

```csharp
public static int ReturnThing()
{
    switch (thing)
    {
        case "returnOne":
	        // Code goes here
	        
            return 1;

        case "returnTwo":
	        // Code goes here
	        
            return 2;

        default:
	        // Code goes here
        
            return 0;
    }
}
```
