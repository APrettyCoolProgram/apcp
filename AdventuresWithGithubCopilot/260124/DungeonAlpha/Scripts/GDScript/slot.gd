class_name Slot

var occupied_card = null

func is_empty() -> bool:
    return occupied_card == null

func place(card) -> void:
    occupied_card = card

func clear() -> void:
    occupied_card = null
