using Godot;
using System.Collections.Generic;

public static class CardLoader
{
    public static List<Card> LoadAll(string path)
    {
        var list = new List<Card>();
        var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        if (file == null) return list;
        var text = file.GetAsText();
        file.Close();

        var result = JSON.Parse(text);
        if (result.Error != Error.Ok) return list;

        var arr = result.Result as Godot.Collections.Array;
        if (arr == null) return list;

        foreach (var o in arr)
        {
            var d = o as Godot.Collections.Dictionary;
            if (d == null) continue;
            string id = d.Contains("id") ? d["id"].ToString() : "";
            string name = d.Contains("name") ? d["name"].ToString() : "Unnamed";
            CardType type = CardType.Offensive;
            if (d.Contains("type")) EnumTryParse(d["type"].ToString(), out type);
            int cost = d.Contains("cost") ? (int)ConvertToInt(d["cost"]) : 0;
            int attack = d.Contains("attack") ? (int)ConvertToInt(d["attack"]) : 0;
            int defense = d.Contains("defense") ? (int)ConvertToInt(d["defense"]) : 0;

            list.Add(new Card(id, name, type, cost, attack, defense));
        }

        return list;
    }

    private static bool EnumTryParse(string str, out CardType type)
    {
        type = CardType.Offensive;
        switch (str.ToLower())
        {
            case "offensive": type = CardType.Offensive; return true;
            case "defensive": type = CardType.Defensive; return true;
            case "spell": type = CardType.Spell; return true;
            case "item": type = CardType.Item; return true;
            default: return false;
        }
    }

    private static int ConvertToInt(object o)
    {
        if (o is int) return (int)o;
        if (o is long) return (int)(long)o;
        if (o is float) return (int)(float)o;
        int v;
        int.TryParse(o.ToString(), out v);
        return v;
    }
}
