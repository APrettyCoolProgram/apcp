extends Node2D
class_name GameController

var player_deck
var enemy_deck
var player_hand
var enemy_hand
var player_slots = []
var enemy_slots = []
var turn_manager
var ui
var player_health = 10
var enemy_health = 10

func _ready() -> void:
    turn_manager = TurnManager.new()
    add_child(turn_manager)

    var templates = []
    templates.append(Card.new("c_sword", "Iron Sword", Card.CardType.Offensive, 1, 2, 0))
    templates.append(Card.new("c_shield", "Leather Shield", Card.CardType.Defensive, 1, 0, 2))
    templates.append(Card.new("c_fire", "Fireball", Card.CardType.Spell, 2, 3, 0))
    templates.append(Card.new("c_potion", "Minor Potion", Card.CardType.Item, 1, 0, 1))

    var p_cards = []
    var e_cards = []
    for i in range(40):
        p_cards.append(_clone_card(templates[i % templates.size()]))
        e_cards.append(_clone_card(templates[(i + 1) % templates.size()]))

    player_deck = Deck.new(p_cards)
    enemy_deck = Deck.new(e_cards)
    player_deck.shuffle()
    enemy_deck.shuffle()

    player_hand = Hand.new()
    enemy_hand = Hand.new()

    for i in range(3):
        player_slots.append(Slot.new())
        enemy_slots.append(Slot.new())

    for i in range(5):
        player_hand.add(player_deck.draw())
        enemy_hand.add(enemy_deck.draw())

    ui = get_node_or_null("UI")
    if ui:
        ui.refresh_hand(get_player_hand())
        ui.update_health(player_health, enemy_health)
        ui.update_mana(turn_manager.player_mana, turn_manager.opponent_mana)

    set_name("GameController")
    start_player_turn()

func _clone_card(template):
    return Card.new(template.id, template.name, template.type, template.cost, template.attack, template.defense, template.effect_id)

func get_player_hand() -> Array:
    return player_hand.cards

func get_player_mana() -> int:
    return turn_manager.player_mana

func get_opponent_mana() -> int:
    return turn_manager.opponent_mana

func play_card_from_hand_to_slot(hand_index: int, slot_index: int) -> bool:
    if hand_index < 0 or hand_index >= player_hand.count():
        return false
    if slot_index < 0 or slot_index >= player_slots.size():
        return false
    var card = player_hand.cards[hand_index]
    if card.cost > turn_manager.player_mana:
        print("Not enough mana to play this card.")
        return false
    if not player_slots[slot_index].is_empty():
        print("Slot is occupied.")
        return false
    turn_manager.player_mana -= card.cost
    player_hand.remove_at(hand_index)
    player_slots[slot_index].place(card)
    if ui:
        ui.refresh_hand(get_player_hand())
        ui.update_mana(turn_manager.player_mana, turn_manager.opponent_mana)
    return true

func start_player_turn() -> void:
    turn_manager.start_turn(true)
    var drawn = player_deck.draw()
    if drawn != null:
        player_hand.add(drawn)
    if ui:
        ui.refresh_hand(get_player_hand())
        ui.update_mana(turn_manager.player_mana, turn_manager.opponent_mana)
    print("Start Player Turn %d - Mana: %d - Hand: %d" % [turn_manager.turn_number, turn_manager.player_mana, player_hand.count()])

func end_player_turn() -> void:
    turn_manager.start_turn(false)
    var edrawn = enemy_deck.draw()
    if edrawn != null:
        enemy_hand.add(edrawn)
    if ui:
        ui.update_mana(turn_manager.player_mana, turn_manager.opponent_mana)
    _ai_play()
    var game_over = resolve_combat()
    if game_over:
        return
    turn_manager.end_turn()
    start_player_turn()

func _ai_play() -> void:
    for i in range(enemy_hand.count()):
        var c = enemy_hand.cards[i]
        if c.cost <= turn_manager.opponent_mana:
            var slot = -1
            for s in range(enemy_slots.size()):
                if enemy_slots[s].is_empty():
                    slot = s
                    break
            if slot >= 0:
                turn_manager.opponent_mana -= c.cost
                enemy_hand.remove_at(i)
                enemy_slots[slot].place(c)
                print("Enemy played %s to slot %d" % [c.name, slot])
                break

func resolve_combat() -> bool:
    for i in range(3):
        var p = player_slots[i].occupied_card
        var e = enemy_slots[i].occupied_card
        if p and e:
            e.defense -= p.attack
            p.defense -= e.attack
            print("Slot %d: %s vs %s -> pDEF:%d, eDEF:%d" % [i, p.name, e.name, p.defense, e.defense])
            if p.defense <= 0:
                player_slots[i].clear()
            if e.defense <= 0:
                enemy_slots[i].clear()
        elif p and not e:
            enemy_health -= p.attack
            print("Slot %d: %s hits enemy for %d -> enemyHealth:%d" % [i, p.name, p.attack, enemy_health])
        elif e and not p:
            player_health -= e.attack
            print("Slot %d: %s hits player for %d -> playerHealth:%d" % [i, e.name, e.attack, player_health])
    if ui:
        ui.update_health(player_health, enemy_health)
    if enemy_health <= 0 and player_health <= 0:
        if ui: ui.show_end_game("Draw! Both combatants fell.")
        return true
    elif enemy_health <= 0:
        if ui: ui.show_end_game("You Win!")
        return true
    elif player_health <= 0:
        if ui: ui.show_end_game("You Lose!")
        return true
    return false
