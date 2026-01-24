extends CanvasLayer
class_name UIController

var hand_container: HBoxContainer
var slots_container: HBoxContainer
var end_turn_button: Button
var player_mana_label: Label
var opponent_mana_label: Label
var player_health_label: Label
var opponent_health_label: Label
var end_game_dialog: WindowDialog
var end_game_label: Label
var end_game_close_btn: Button
var selected_hand_index: int = -1

func _ready() -> void:
    hand_container = get_node("UIRoot/HandContainer")
    slots_container = get_node("UIRoot/SlotsContainer")
    end_turn_button = get_node("UIRoot/EndTurn")
    end_turn_button.connect("pressed", Callable(self, "_on_end_turn_pressed"))

    player_mana_label = get_node("UIRoot/PlayerManaLabel")
    opponent_mana_label = get_node("UIRoot/OpponentManaLabel")
    player_health_label = get_node("UIRoot/PlayerHealthLabel")
    opponent_health_label = get_node("UIRoot/OpponentHealthLabel")

    end_game_dialog = get_node("UIRoot/EndGameDialog")
    end_game_label = end_game_dialog.get_node("EndGameLabel")
    end_game_close_btn = end_game_dialog.get_node("EndGameClose")
    end_game_close_btn.connect("pressed", Callable(self, "_on_end_game_close_pressed"))

    for i in range(slots_container.get_child_count()):
        var btn = slots_container.get_child(i)
        btn.connect("pressed", Callable(self, "_on_slot_pressed"), [i])

func refresh_hand(cards: Array) -> void:
    hand_container.clear()
    for i in range(cards.size()):
        var c = cards[i]
        var b = Button.new()
        b.text = "%s (%d)" % [c.name, c.cost]
        b.connect("pressed", Callable(self, "_on_card_pressed"), [i])
        hand_container.add_child(b)

func update_mana(player_mana: int, opp_mana: int) -> void:
    if player_mana_label: player_mana_label.text = "Player Mana: %d" % player_mana
    if opponent_mana_label: opponent_mana_label.text = "Opponent Mana: %d" % opp_mana

func update_health(player_health: int, opp_health: int) -> void:
    if player_health_label: player_health_label.text = "Player Health: %d" % player_health
    if opponent_health_label: opponent_health_label.text = "Opponent Health: %d" % opp_health

func show_end_game(message: String) -> void:
    if end_game_label: end_game_label.text = message
    if end_game_dialog: end_game_dialog.popup_centered()
    get_tree().paused = true

func _on_end_game_close_pressed() -> void:
    if end_game_dialog: end_game_dialog.hide()
    get_tree().paused = false

func _on_card_pressed(index: int) -> void:
    selected_hand_index = index
    print("Selected card at hand index %d" % index)

func _on_slot_pressed(slot_index: int) -> void:
    var gc = get_tree().current_scene.get_node_or_null("GameController")
    if gc == null:
        gc = get_parent().get_node_or_null("GameController")
    if gc == null:
        return
    if selected_hand_index < 0:
        return
    var ok = gc.play_card_from_hand_to_slot(selected_hand_index, slot_index)
    if ok:
        selected_hand_index = -1
        refresh_hand(gc.get_player_hand())
        update_mana(gc.get_player_mana(), gc.get_opponent_mana())

func _on_end_turn_pressed() -> void:
    var gc = get_tree().current_scene.get_node_or_null("GameController")
    if gc == null:
        gc = get_parent().get_node_or_null("GameController")
    if gc:
        gc.end_player_turn()
