class_name Card

enum CardType { Offensive, Defensive, Spell, Item }

var id: String = ""
var name: String = ""
var type: int = CardType.Offensive
var cost: int = 0
var attack: int = 0
var defense: int = 0
var effect_id: String = ""

func _init(_id: String = "", _name: String = "Unnamed", _type: int = CardType.Offensive, _cost: int = 0, _attack: int = 0, _defense: int = 0, _effect_id: String = ""):
	id = _id
	name = _name
	type = _type
	cost = _cost
	attack = _attack
	defense = _defense
	effect_id = _effect_id

func to_string() -> String:
	return "%s (%d) Cost:%d ATK:%d DEF:%d" % [name, type, cost, attack, defense]
