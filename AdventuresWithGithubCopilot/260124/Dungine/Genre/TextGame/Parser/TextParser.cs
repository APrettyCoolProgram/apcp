using System;
using System.Collections.Generic;
using System.Linq;

namespace Dungine.Genre.TextGame.Parser;

/// <summary>
/// Parses player input into commands and arguments
/// </summary>
public class TextParser
{
    private static readonly HashSet<string> Articles = new() { "a", "an", "the" };
    private static readonly HashSet<string> Prepositions = new() { "at", "to", "in", "into", "with", "using", "on", "from" };

    public ParsedCommand Parse(string userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput))
        {
            return new ParsedCommand 
            { 
                IsValid = false, 
                ErrorMessage = "Please enter a command." 
            };
        }

        List<string> inputTokens = Tokenize(userInput);
        
        if (inputTokens.Count == 0)
        {
            return new ParsedCommand 
            { 
                IsValid = false, 
                ErrorMessage = "Please enter a command." 
            };
        }

        string commandVerb = inputTokens[0].ToLower();
        List<string> commandArguments = inputTokens.Skip(1).ToList();

        return new ParsedCommand
        {
            IsValid = true,
            Verb = commandVerb,
            DirectObject = ExtractObject(commandArguments, objectIndex: 0),
            IndirectObject = ExtractObject(commandArguments, objectIndex: 1),
            Preposition = ExtractPreposition(commandArguments),
            RawArguments = commandArguments
        };
    }

    private List<string> Tokenize(string userInput)
    {
        List<string> words = userInput
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(word => word.Trim())
            .ToList();
            
        return words;
    }

    private string? ExtractObject(List<string> tokens, int objectIndex)
    {
        // Filter out articles and prepositions to get meaningful object words
        List<string> meaningfulWords = tokens
            .Where(token => 
            {
                string lowercaseToken = token.ToLower();
                bool isArticle = Articles.Contains(lowercaseToken);
                bool isPreposition = Prepositions.Contains(lowercaseToken);
                
                return !isArticle && !isPreposition;
            })
            .ToList();

        if (objectIndex < meaningfulWords.Count)
        {
            return meaningfulWords[objectIndex];
        }

        return null;
    }

    private string? ExtractPreposition(List<string> tokens)
    {
        string? foundPreposition = tokens.FirstOrDefault(token => 
            Prepositions.Contains(token.ToLower()));
            
        return foundPreposition;
    }
}

/// <summary>
/// Represents a parsed command
/// </summary>
public class ParsedCommand
{
    public bool IsValid { get; set; }
    public string? ErrorMessage { get; set; }
    public string? Verb { get; set; }
    public string? DirectObject { get; set; }
    public string? IndirectObject { get; set; }
    public string? Preposition { get; set; }
    public List<string> RawArguments { get; set; } = new();
}
