using Godot;
using System;
using System.Collections.Generic;

public partial class UIController : CanvasLayer
{
    private HBoxContainer handContainer;
    private HBoxContainer slotsContainer;
    private Button endTurnButton;
    private Label playerManaLabel;
    private Label opponentManaLabel;
    private Label playerHealthLabel;
    private Label opponentHealthLabel;
    private WindowDialog endGameDialog;
    private Label endGameLabel;
    private Button endGameCloseBtn;
    private int selectedHandIndex = -1;
    private GameController gameController;

    public override void _Ready()
    {
        handContainer = GetNode<HBoxContainer>("UIRoot/HandContainer");
        slotsContainer = GetNode<HBoxContainer>("UIRoot/SlotsContainer");
        endTurnButton = GetNode<Button>("UIRoot/EndTurn");
        endTurnButton.Pressed += OnEndTurnPressed;

        gameController = GetParent() as GameController;
        if (gameController == null)
        {
            var gd = GetTree().CurrentScene as Node;
            if (gd != null) gameController = gd.GetNodeOrNull<GameController>(".");
        }

        playerManaLabel = GetNode<Label>("UIRoot/PlayerManaLabel");
        opponentManaLabel = GetNode<Label>("UIRoot/OpponentManaLabel");
        playerHealthLabel = GetNode<Label>("UIRoot/PlayerHealthLabel");
        opponentHealthLabel = GetNode<Label>("UIRoot/OpponentHealthLabel");
        endGameDialog = GetNode<WindowDialog>("UIRoot/EndGameDialog");
        endGameLabel = endGameDialog.GetNode<Label>("EndGameLabel");
        endGameCloseBtn = endGameDialog.GetNode<Button>("EndGameClose");
        endGameCloseBtn.Pressed += OnEndGameClosePressed;

        // wire slot buttons
        for (int i = 0; i < slotsContainer.GetChildCount(); i++)
        {
            var btn = slotsContainer.GetChild<Button>(i);
            int idx = i;
            btn.Pressed += () => OnSlotPressed(idx);
        }
    }

    public void RefreshHand(List<Card> cards)
    {
        handContainer.Clear();
        for (int i = 0; i < cards.Count; i++)
        {
            var c = cards[i];
            var b = new Button();
            b.Text = $"{c.Name} ({c.Cost})";
            int idx = i;
            b.Pressed += () => OnCardPressed(idx);
            handContainer.AddChild(b);
        }
    }

    public void UpdateMana(int playerMana, int oppMana)
    {
        if (playerManaLabel != null) playerManaLabel.Text = $"Player Mana: {playerMana}";
        if (opponentManaLabel != null) opponentManaLabel.Text = $"Opponent Mana: {oppMana}";
    }

    public void UpdateHealth(int playerHealth, int oppHealth)
    {
        if (playerHealthLabel != null) playerHealthLabel.Text = $"Player Health: {playerHealth}";
        if (opponentHealthLabel != null) opponentHealthLabel.Text = $"Opponent Health: {oppHealth}";
    }

    public void ShowEndGame(string message)
    {
        if (endGameLabel != null) endGameLabel.Text = message;
        if (endGameDialog != null) endGameDialog.PopupCentered();
        GetTree().Paused = true;
    }

    private void OnEndGameClosePressed()
    {
        if (endGameDialog != null) endGameDialog.Hide();
        GetTree().Paused = false;
    }

    private void OnCardPressed(int handIndex)
    {
        selectedHandIndex = handIndex;
        GD.Print($"Selected card at hand index {handIndex}");
    }

    private void OnSlotPressed(int slotIndex)
    {
        if (selectedHandIndex < 0) return;
        var ok = gameController.PlayCardFromHandToSlot(selectedHandIndex, slotIndex);
        if (ok)
        {
            selectedHandIndex = -1;
            RefreshHand(gameController.GetPlayerHand());
            UpdateMana(gameController.GetPlayerMana(), gameController.GetOpponentMana());
        }
    }

    private void OnEndTurnPressed()
    {
        gameController.EndPlayerTurn();
    }
}
