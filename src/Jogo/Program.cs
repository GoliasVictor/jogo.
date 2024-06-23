using Raylib_cs;


/// <sumary>
/// Static class Responsible for starting and running the game.  
///</sumary>
static class GameSystem {
    public const int DefaultWindowWidth = 800;
    public const int DefaultWindowHeight = 600;
    private const string DefaultWindowName = "Elements";
    public const int TileSize = 16;

    private static Color ClearColor = Color.DarkGray;
    private static int targetFPS = 60;
    private static IScene currentScene;
    static GameSystem(){
        var grid = new IEntity[17, 17];
        currentScene = new LevelScene([new PlayerMovementSystem()], new Map(grid,[
            new PlayerEntity(new GridVec2(8,8)),
            new BlockEntity(new GridVec2(10,10)),
        ]));
        
    }
    static void Main() {
        Raylib.InitWindow(DefaultWindowWidth, DefaultWindowHeight, DefaultWindowName);

        Raylib.SetTargetFPS(targetFPS);
        Raylib.InitAudioDevice();

        SpriteAtlas.LoadAtlas();

        while (!Raylib.WindowShouldClose())
        {
            Update();
            Render();
        }

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }


    /// <summary>
    /// <para> Called inside the Game Loop for handling logic.</para>
    /// <para> Calls <c>GameSystem.UpdateObjects()</c> and <c>GameSystem.UpdateUI()</c>.</para>
    /// <seealso cref="M:Game.GameSystem.UpdateObjects"/>
    /// <seealso cref="M:Game.GameSystem,UpdateUI"/> 
    /// </summary>
    private static void Update() {
        UpdateObjects();
        UpdateUI();
    }

    /// <summary>
    /// <para> Called inside the Game Loop for rendering items on screen.</para>
    /// <para> Renders Objects first, then UI.</para>
    /// <seealso cref="M:Game.GameSystem.RenderObjects"/>
    /// <seealso cref="M:Game.GameSystem.RenderUI"/> 
    /// </summary>
    private static void Render() {
        Raylib.BeginDrawing();

            Raylib.ClearBackground(ClearColor);
            RenderObjects();
            RenderUI();

        Raylib.EndDrawing();
    }

    /// <summary>
    /// <para> Called by <c>GameSystem.Update()</c>.</para>
    /// <para> Call <c>Object.Update()</c> for every <c>Object</c> in list.</para>
    /// <seealso cref="M:Game.GameSystem.Update"/> 
    /// <seealso cref="M:Game.GameSystem.UpdateUI"/> 
    /// <seealso cref="M:Game.GameSystem.RenderObjects"/>
    /// </summary>
    private static void UpdateObjects() {
        currentScene.Update();
    }

    /// <summary>
    /// <para> Called by <c>GameSystem.Update()</c>.</para>
    /// <para> Call <c>UI.Update()</c> for every <c>UI Element</c> in list.</para>
    /// <seealso cref="M:Game.GameSystem.Update"/> 
    /// <seealso cref="M:Game.GameSystem.UpdateObject"/> 
    /// <seealso cref="M:Game.GameSystem.RenderUI"/>
    /// </summary>
    private static void UpdateUI() {

    }

    /// <summary>
    /// <para> Called by <c>GameSystem.Render()</c>.</para>
    /// <para> Call <c>Object.Render()</c> for every <c>Object</c> in list.</para>
    /// <seealso cref="M:Game.GameSystem.Render"/> 
    /// <seealso cref="M:Game.GameSystem.RenderUI"/>
    /// <seealso cref="M:Game.GameSystem.UpdateObjects"/> 
    /// </summary>
    private static void RenderObjects() {
        currentScene.Render();
    }

    /// <summary>
    /// <para> Called by <c>GameSystem.Render()</c>.</para>
    /// <para> Call <c>UI.Render()</c> for every <c>UI Element</c> in list.</para>
    /// <seealso cref="M:Game.GameSystem.Render"/> 
    /// <seealso cref="M:Game.GameSystem.UpdateUI"/> 
    /// <seealso cref="M:Game.GameSystem.RenderObjects"/>
    /// </summary>
    private static void RenderUI() {

    }
}