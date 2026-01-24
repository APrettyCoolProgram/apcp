class_name Hand

var cards: Array = []

func add(card) -> void:
    if card != null:
        cards.append(card)

func remove_at(index: int):
    if index >= 0 and index < cards.size():
        var c = cards[index]
        cards.remove_at(index)
        return c
    return null

func count() -> int:
    return cards.size()
