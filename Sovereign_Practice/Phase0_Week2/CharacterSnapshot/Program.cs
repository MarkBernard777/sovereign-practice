/* //What you're building: A formatted character readout for a Sovereign character. This is the raw data version of what the Week 2 checkpoint's CharacterData class will hold.
Requirements — write these from scratch, no hints:

Declare variables for: character name (string), primary motivation (string), loyalty score (decimal, 0.0–1.0), composure score (int, 0–100), relationship depth (string: "Low", "Moderate", or "High"), turns served (int)
Use realistic Sovereign values — use Lord Chancellor Aldric Vorn from the Phase 4 specs (motivation: Order, loyalty: 0.82, composure: 71, depth: High, turns: 14)
Print a formatted character card using string interpolation and \t tab formatting: */




string characterName = "Lord Chancellor Aldric Vorn";
string primaryMotivation = "Order";
decimal loyaltyScore = 0.82m;
int composureScore = 71;
string relationshipDepth = "High";
int turnsServed = 14;

Console.WriteLine("CHARACTER PROFILE");
Console.WriteLine("-----------------");
Console.WriteLine($"Name: \t\t\t{characterName}");
Console.WriteLine($"Motivation: \t\t{primaryMotivation}");
Console.WriteLine($"Loyalty: \t\t{loyaltyScore:F2}");
Console.WriteLine($"Composure: \t\t{composureScore}");
Console.WriteLine($"Relationship Depth: \t{relationshipDepth}");
Console.WriteLine($"Turns Served: \t\t{turnsServed}");