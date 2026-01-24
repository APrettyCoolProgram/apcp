class_name Deck

var cards: Array = []

func _init(_cards: Array = []):
    cards = _cards.duplicate()

func shuffle() -> void:
    var n = cards.size()
    for i in range(n - 1, 0, -1):
        var j = randi() % (i + 1)
        var tmp = cards[i]
        cards[i] = cards[j]
        cards[j] = tmp

func draw():
    if cards.empty():
        return null
    return cards.pop_front()

func count() -> int:
    return cards.size()
