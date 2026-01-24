using Godot;
using System;
using System.Collections.Generic;
using Dungine.Core;
using Dungine.Genre.TextGame.Parser;

namespace Dungine.Genre.TextGame.Commands;

/// <summary>
/// Base class for all game commands
/// </summary>
public abstract class Command
{
    public abstract string[] Verbs { get; }
    public abstract string Description { get; }
    
    public abstract string Execute(ParsedCommand parsed, GameState state);
    
    public virtual bool CanExecute(ParsedCommand parsed, GameState state) => true;
}

/// <summary>
/// Processes and executes player commands
/// </summary>
public partial class CommandProcessor : Node
{
    private Dictionary<string, Command> _commands = new();
    private TextParser _parser = new();

    public override void _Ready()
    {
        RegisterDefaultCommands();
    }

    private void RegisterDefaultCommands()
    {
        RegisterCommand(new LookCommand());
        RegisterCommand(new GoCommand());
        RegisterCommand(new TakeCommand());
        RegisterCommand(new DropCommand());
        RegisterCommand(new InventoryCommand());
        RegisterCommand(new ExamineCommand());
        RegisterCommand(new HelpCommand());
        RegisterCommand(new QuitCommand());
    }

    public void RegisterCommand(Command command)
    {
        foreach (var verbName in command.Verbs)
        {
            string normalizedVerb = verbName.ToLower();
            _commands[normalizedVerb] = command;
        }
    }

    public string ProcessInput(string userInput, GameState gameState)
    {
        ParsedCommand parsedCommand = _parser.Parse(userInput);

        if (!parsedCommand.IsValid)
        {
            return parsedCommand.ErrorMessage ?? "Invalid command.";
        }

        if (parsedCommand.Verb == null)
        {
            return "I don't understand that command.";
        }

        if (!_commands.TryGetValue(parsedCommand.Verb, out var commandToExecute))
        {
            return $"I don't understand '{parsedCommand.Verb}'. Type 'help' for a list of commands.";
        }

        if (!commandToExecute.CanExecute(parsedCommand, gameState))
        {
            return "You can't do that right now.";
        }

        try
        {
            string commandResult = commandToExecute.Execute(parsedCommand, gameState);
            return commandResult;
        }
        catch (Exception ex)
        {
            GD.PrintErr($"Error executing command: {ex.Message}");
            return "Something went wrong with that command.";
        }
    }

    public List<Command> GetAllCommands()
    {
        var uniqueCommandList = new HashSet<Command>();
        
        foreach (var registeredCommand in _commands.Values)
        {
            uniqueCommandList.Add(registeredCommand);
        }
        
        return new List<Command>(uniqueCommandList);
    }
}
