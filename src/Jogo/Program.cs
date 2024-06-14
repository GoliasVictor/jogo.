using Raylib_cs;

/// TODO: Add GameObject and UI classes for game handling containing Update and Render methods

namespace Game;

static class GameSystem {
    private const int WindowWidth = 800;
    private const int WindowHeight = 600;
    private const string WindowName = "Elements";
    private static Color ClearColor = Color.DarkGray;
    
    private static int TargetFPS = 60;
    
    /// TODO: Add GameObject and UI element lists

    static void Main() {
        Raylib.InitWindow(WindowWidth, WindowHeight, WindowName);

        Raylib.SetTargetFPS(TargetFPS);
        Raylib.InitAudioDevice();

        while (!Raylib.WindowShouldClose())
        {
            Update();
            Render();
        }

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }

    private static void Update() { // Is it possible to make this async? Is it worth it?
        UpdateObjects();
        UpdateUI();
    }

    private static void Render() { // Is it necessary to implement render layers?
        Raylib.BeginDrawing();

            Raylib.ClearBackground(ClearColor);
            RenderObjects();
            RenderUI();

        Raylib.EndDrawing();
    }

    private static void UpdateObjects() {
         
    }

    private static void UpdateUI() {

    }

    private static void RenderObjects() {

    }

    private static void RenderUI() {

    }
}