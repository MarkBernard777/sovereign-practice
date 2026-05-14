// ============================================================
//  SNOW SURFER — C# CONCEPT PRACTICE
//  Covers every NEW C# concept introduced in Section 3
//  Run with: dotnet run
// ============================================================

using System;

// ════════════════════════════════════════════════════════════
//  CONCEPT 1: ACCESS MODIFIERS
//
//  Every variable and method has an access modifier.
//  It controls WHO can see and use that variable or method.
//
//  private  = only THIS class can use it  (default if you write nothing)
//  public   = ANY class can use it
//
//  In Unity you saw this error:
//  "canControlPlayer is inaccessible due to its protection level"
//  That's because canControlPlayer was private — CrashDetector
//  couldn't reach into PlayerController to change it.
//
//  The fix: make a PUBLIC METHOD that changes the private variable.
//  That way other scripts call the method — they never touch
//  the variable directly. This is called ENCAPSULATION.
// ════════════════════════════════════════════════════════════

class PlayerController
{
    // PRIVATE — only PlayerController can read or change these
    private float torqueAmount   = 10f;
    private float baseSpeed      = 15f;
    private float boostSpeed     = 20f;
    private bool  canControl     = true;
    private int   activePowerUps = 0;
    private float score          = 0f;

    // ════════════════════════════════════════════════════════
    //  CONCEPT 2: RETURN TYPES
    //
    //  A method's return type declares what it hands back
    //  when it finishes running.
    //
    //  void   = hands back nothing
    //  float  = hands back a decimal number
    //  bool   = hands back true or false
    //  int    = hands back a whole number
    //  string = hands back text
    //
    //  These methods below are called GETTERS.
    //  They give other classes read-only access to private variables
    //  without letting them change those variables directly.
    //
    //  In Unity: powerUp.GetPowerUpType()
    //            powerUp.GetValueChange()
    //            powerUp.GetTime()
    //  All of these were getter methods on the ScriptableObject.
    // ════════════════════════════════════════════════════════

    // Returns a float — the caller gets a decimal number back
    public float GetBaseSpeed()    { return baseSpeed; }
    public float GetBoostSpeed()   { return boostSpeed; }
    public float GetTorque()       { return torqueAmount; }
    public float GetScore()        { return score; }

    // Returns a bool — the caller gets true or false back
    public bool  GetCanControl()   { return canControl; }

    // Returns an int — the caller gets a whole number back
    public int   GetActivePowerUps() { return activePowerUps; }

    // Returns a string — the caller gets text back
    public string GetStatus()
    {
        if (!canControl) return "CRASHED — controls disabled";
        if (activePowerUps > 0) return $"POWERED UP ({activePowerUps} active)";
        return "Normal";
    }

    // ════════════════════════════════════════════════════════
    //  CONCEPT 3: METHOD PARAMETERS
    //
    //  Parameters let you pass information INTO a method
    //  when you call it. The method uses that information
    //  to do its job.
    //
    //  Syntax: void MethodName(type parameterName)
    //
    //  In Unity: void AddScore(int additionalScore)
    //            scoreManager.AddScore(100);
    //            — 100 goes into additionalScore
    //
    //  You can have multiple parameters separated by commas:
    //  void ActivatePowerUp(PowerUpSO powerUp)
    //  void SayName(string firstName, string lastName)
    // ════════════════════════════════════════════════════════

    // One parameter — how much score to add
    public void AddScore(int additionalScore)
    {
        score += additionalScore;
        Console.WriteLine($"  +{additionalScore} score! Total: {score}");
    }

    // One parameter — which power up to activate
    public void ActivatePowerUp(PowerUpData powerUp)
    {
        string type  = powerUp.GetPowerUpType();   // calling a getter
        float change = powerUp.GetValueChange();    // calling a getter

        if (type == "speed")
        {
            baseSpeed  += change;
            boostSpeed += change;
            Console.WriteLine($"  Speed power up! Base speed: {baseSpeed}, Boost speed: {boostSpeed}");
        }
        else if (type == "torque")
        {
            torqueAmount += change;
            Console.WriteLine($"  Torque power up! Torque: {torqueAmount}");
        }

        activePowerUps++;
        Console.WriteLine($"  Active power ups: {activePowerUps}");
    }

    // One parameter — which power up wore off
    public void DeactivatePowerUp(PowerUpData powerUp)
    {
        string type  = powerUp.GetPowerUpType();
        float change = powerUp.GetValueChange();

        if (type == "speed")
        {
            baseSpeed  -= change;
            boostSpeed -= change;
            Console.WriteLine($"  Speed power up expired. Speed back to: {baseSpeed}");
        }
        else if (type == "torque")
        {
            torqueAmount -= change;
            Console.WriteLine($"  Torque power up expired. Torque back to: {torqueAmount}");
        }

        activePowerUps--;
    }

    // No parameters — just disables controls
    // PUBLIC so CrashDetector can call it from outside this class
    public void DisableControls()
    {
        canControl = false;
        Console.WriteLine("  Controls DISABLED — player crashed!");
    }

    // No parameters — just re-enables controls (e.g. on level restart)
    public void EnableControls()
    {
        canControl = true;
        Console.WriteLine("  Controls ENABLED");
    }
}


// ════════════════════════════════════════════════════════════
//  CONCEPT 4: SCRIPTABLE OBJECTS (simulated)
//
//  A ScriptableObject is a DATA CONTAINER.
//  It holds variables but has NO game logic.
//  It lives as a file in your project — not on a game object.
//
//  Why separate data from logic?
//  — Easy to swap in different data (short speed vs long speed)
//  — Easy to edit without touching the code
//  — Reusable across many different objects
//
//  In Unity you created PowerUp.so with:
//    [SerializeField] string powerUpType;
//    [SerializeField] float  valueChange;
//    [SerializeField] float  time;
//
//  Then you accessed those via PUBLIC GETTER METHODS
//  rather than making the variables public directly.
//  This is ENCAPSULATION in practice.
// ════════════════════════════════════════════════════════════

class PowerUpData
{
    // Private data — nobody can change these from outside
    private string powerUpType;
    private float  valueChange;
    private float  duration;

    // Constructor — sets up the data when you create the object
    // This is like setting the values in Unity's Inspector
    public PowerUpData(string type, float change, float time)
    {
        powerUpType = type;
        valueChange = change;
        duration    = time;
    }

    // Getter methods — READ ONLY access to private variables
    public string GetPowerUpType() { return powerUpType; }
    public float  GetValueChange() { return valueChange; }
    public float  GetDuration()    { return duration; }

    // Returns a formatted string — a getter that returns string
    public string GetDescription()
    {
        return $"{powerUpType} power up | +{valueChange} | lasts {duration}s";
    }
}


// ════════════════════════════════════════════════════════════
//  CONCEPT 5: ACCESS MODIFIER — PUBLIC vs PRIVATE in practice
//
//  CrashDetector is a SEPARATE CLASS — like a separate script.
//  It can only call PUBLIC methods on PlayerController.
//  It CANNOT touch private variables directly.
// ════════════════════════════════════════════════════════════

class CrashDetector
{
    // This simulates FindFirstObjectOfType<PlayerController>()
    // In Unity: playerController = FindFirstObjectOfType<PlayerController>();
    private PlayerController playerController;

    public CrashDetector(PlayerController pc)
    {
        playerController = pc;
    }

    public void OnHeadHitFloor()
    {
        Console.WriteLine("\n  [CrashDetector] Head hit floor!");

        // This works — DisableControls() is PUBLIC
        playerController.DisableControls();

        // This would cause an error if uncommented:
        // playerController.canControl = false;
        // ERROR: canControl is private — inaccessible
    }
}


// ════════════════════════════════════════════════════════════
//  CONCEPT 6: VECTOR2
//
//  Vector2 holds TWO numbers: X and Y.
//  Used for 2D positions and directions.
//
//  In Snow Surfer: moveAction.ReadValue<Vector2>()
//  gave us the player's input direction:
//    X = -1 (left arrow) or +1 (right arrow) or 0
//    Y = -1 (down arrow) or +1 (up arrow) or 0
//
//  Console equivalent: a simple struct with X and Y
// ════════════════════════════════════════════════════════════

struct Vector2
{
    public float X;
    public float Y;

    public Vector2(float x, float y) { X = x; Y = y; }

    // Return type: string — describes the vector
    public string GetDescription()
    {
        if (X > 0)  return "pressing RIGHT → rotating clockwise";
        if (X < 0)  return "pressing LEFT  → rotating counterclockwise";
        if (Y > 0)  return "pressing UP    → boost active";
        return "no input";
    }

    // Return type: bool — is the player pressing anything?
    public bool HasInput() { return X != 0 || Y != 0; }

    public override string ToString() => $"({X}, {Y})";
}


// ════════════════════════════════════════════════════════════
//  MAIN PROGRAM — interactive demo of all concepts
// ════════════════════════════════════════════════════════════

class SnowSurferPractice
{
    static void Main(string[] args)
    {
        Console.Clear();
        PrintHeader();

        // Create our "scene objects" — like game objects in Unity
        var player   = new PlayerController();
        var detector = new CrashDetector(player);

        // Pre-built power ups — like ScriptableObjects you made in Unity
        var shortSpeed  = new PowerUpData("speed",  10f, 2f);
        var longSpeed   = new PowerUpData("speed",  10f, 10f);
        var shortTorque = new PowerUpData("torque", 10f, 2f);

        bool running = true;
        while (running)
        {
            PrintStatus(player);
            PrintMenu();

            string input = Console.ReadLine()?.ToUpper().Trim() ?? "";
            Console.Clear();

            switch (input)
            {
                // ── CONCEPT 2: Return types in action ──────────────────
                case "1":
                    Console.WriteLine("\n[ CONCEPT 2 — RETURN TYPES: Getter Methods ]\n");
                    Console.WriteLine($"  GetBaseSpeed()     returns float  → {player.GetBaseSpeed()}");
                    Console.WriteLine($"  GetBoostSpeed()    returns float  → {player.GetBoostSpeed()}");
                    Console.WriteLine($"  GetTorque()        returns float  → {player.GetTorque()}");
                    Console.WriteLine($"  GetScore()         returns float  → {player.GetScore()}");
                    Console.WriteLine($"  GetCanControl()    returns bool   → {player.GetCanControl()}");
                    Console.WriteLine($"  GetActivePowerUps()returns int    → {player.GetActivePowerUps()}");
                    Console.WriteLine($"  GetStatus()        returns string → \"{player.GetStatus()}\"");
                    Console.WriteLine("\n  These methods return private data safely.");
                    Console.WriteLine("  Nothing outside PlayerController can CHANGE these variables.");
                    Console.WriteLine("  They can only READ them through these getter methods.");
                    Pause();
                    break;

                // ── CONCEPT 3: Method parameters in action ─────────────
                case "2":
                    Console.WriteLine("\n[ CONCEPT 3 — METHOD PARAMETERS: AddScore ]\n");
                    Console.WriteLine("  In Unity: scoreManager.AddScore(100)");
                    Console.WriteLine("  The 100 goes into the 'additionalScore' parameter.\n");
                    player.AddScore(100);
                    Console.WriteLine("\n  Calling again with a different value:");
                    player.AddScore(500);
                    Console.WriteLine("\n  Same method, different parameter = different result.");
                    Pause();
                    break;

                // ── CONCEPT 4: ScriptableObjects ───────────────────────
                case "3":
                    Console.WriteLine("\n[ CONCEPT 4 — SCRIPTABLEOBJECTS: Power Up Data ]\n");
                    Console.WriteLine("  Three power ups (like your .asset files in Unity):\n");
                    Console.WriteLine($"  Short Speed  → {shortSpeed.GetDescription()}");
                    Console.WriteLine($"  Long Speed   → {longSpeed.GetDescription()}");
                    Console.WriteLine($"  Short Torque → {shortTorque.GetDescription()}");
                    Console.WriteLine("\n  Activating Short Speed power up...\n");
                    player.ActivatePowerUp(shortSpeed);
                    Console.WriteLine("\n  Read speed back via getter:");
                    Console.WriteLine($"  player.GetBaseSpeed() = {player.GetBaseSpeed()}");
                    Console.WriteLine("\n  Deactivating Short Speed power up...\n");
                    player.DeactivatePowerUp(shortSpeed);
                    Pause();
                    break;

                // ── CONCEPT 1: Access modifiers ────────────────────────
                case "4":
                    Console.WriteLine("\n[ CONCEPT 1 — ACCESS MODIFIERS: public vs private ]\n");
                    Console.WriteLine("  CrashDetector is a SEPARATE CLASS.");
                    Console.WriteLine("  It cannot touch player.canControl directly — it's private.");
                    Console.WriteLine("  It CAN call player.DisableControls() — that's public.\n");
                    detector.OnHeadHitFloor();
                    Console.WriteLine("\n  The player is now disabled. Re-enabling (simulating restart)...\n");
                    player.EnableControls();
                    Pause();
                    break;

                // ── CONCEPT 5: Vector2 ─────────────────────────────────
                case "5":
                    Console.WriteLine("\n[ CONCEPT 5 — VECTOR2: Two-value direction variable ]\n");
                    Console.WriteLine("  In Unity: moveVector = moveAction.ReadValue<Vector2>();\n");

                    var inputs = new[]
                    {
                        new Vector2( 1,  0),
                        new Vector2(-1,  0),
                        new Vector2( 0,  1),
                        new Vector2( 0,  0),
                    };

                    foreach (var v in inputs)
                    {
                        Console.WriteLine($"  Vector2{v,-12} → {v.GetDescription()}");
                        Console.WriteLine($"             HasInput() returns {v.HasInput()}\n");
                    }
                    Pause();
                    break;

                // ── Quick reference ────────────────────────────────────
                case "6":
                    PrintReference();
                    Pause();
                    break;

                case "Q":
                    running = false;
                    break;

                default:
                    Console.WriteLine("  Unknown input.");
                    break;
            }
        }

        Console.WriteLine("\n  Practice complete. Go build Snow Surfer!\n");
    }


    static void PrintHeader()
    {
        Console.WriteLine("════════════════════════════════════════════════════");
        Console.WriteLine("  SNOW SURFER — C# PRACTICE");
        Console.WriteLine("  Section 3 Concept Trainer");
        Console.WriteLine("════════════════════════════════════════════════════\n");
    }

    static void PrintStatus(PlayerController p)
    {
        Console.WriteLine("────────────────────────────────────────────────────");
        Console.WriteLine($"  Status       : {p.GetStatus()}");
        Console.WriteLine($"  Base Speed   : {p.GetBaseSpeed()}");
        Console.WriteLine($"  Boost Speed  : {p.GetBoostSpeed()}");
        Console.WriteLine($"  Torque       : {p.GetTorque()}");
        Console.WriteLine($"  Score        : {p.GetScore()}");
        Console.WriteLine($"  Can Control  : {p.GetCanControl()}");
        Console.WriteLine($"  Active PwrUps: {p.GetActivePowerUps()}");
        Console.WriteLine("────────────────────────────────────────────────────");
    }

    static void PrintMenu()
    {
        Console.WriteLine("\n  [1] Concept 2 — Return Types (getter methods)");
        Console.WriteLine("  [2] Concept 3 — Method Parameters (AddScore)");
        Console.WriteLine("  [3] Concept 4 — ScriptableObjects (PowerUpData)");
        Console.WriteLine("  [4] Concept 1 — Access Modifiers (public/private)");
        Console.WriteLine("  [5] Concept 5 — Vector2");
        Console.WriteLine("  [6] Quick Reference Table");
        Console.WriteLine("  [Q] Quit\n");
        Console.Write("  Choice: ");
    }

    static void PrintReference()
    {
        Console.WriteLine("\n[ QUICK REFERENCE — UNITY vs CONSOLE ]\n");
        Console.WriteLine("  UNITY                                   THIS SCRIPT");
        Console.WriteLine("  ──────────────────────────────────────  ──────────────────────────────");
        Console.WriteLine("  private float speed                     private float baseSpeed");
        Console.WriteLine("  public void DisableControls()           public void DisableControls()");
        Console.WriteLine("  public float GetBaseSpeed()             public float GetBaseSpeed()");
        Console.WriteLine("  void AddScore(int additionalScore)      void AddScore(int additionalScore)");
        Console.WriteLine("  ScriptableObject PowerUpSO              class PowerUpData");
        Console.WriteLine("  [SerializeField] string powerUpType     private string powerUpType");
        Console.WriteLine("  public string GetPowerUpType()          public string GetPowerUpType()");
        Console.WriteLine("  Vector2 moveVector                      struct Vector2");
        Console.WriteLine("  moveAction.ReadValue<Vector2>()         new Vector2(x, y)");
        Console.WriteLine("  FindFirstObjectOfType<PlayerCtrl>()     new CrashDetector(player)");
        Console.WriteLine("  Time.timeScale = 0                      game paused (no console equiv)");
        Console.WriteLine("  SceneManager.LoadScene(0)               running = false (restart sim)");
    }

    static void Pause()
    {
        Console.WriteLine("\n  Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}
