extends Node
class_name TurnManager

var turn_number: int = 1
var player_mana: int = 1
var opponent_mana: int = 1

func _ready():
    player_mana = 1
    opponent_mana = 1

func start_turn(is_player: bool) -> void:
    var mana = min(10, turn_number)
    if is_player:
        player_mana = mana
    else:
        opponent_mana = mana

func end_turn() -> void:
    turn_number += 1
