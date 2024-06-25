static class GameSystem {

    static GameSystem() {
        currentScene = new MainMenuScene();
    }

    static void Main() {
        try
        {
			// Inicializa recursos e cenas 
            try {
                SpriteAtlas.LoadAtlas();
            }
            catch(ResourceLoadException e) {
                currentScene = new ErrorScene("...");
                /// log de erro
            }

            while (!ShouldExit) {
                try {
                    Update();
                    Render();
                } 
                catch(Exception e) {
                    /// logexception
                    currentScene = new ErrorScene("...");
                }
                if (Raylib.WindowShouldClose())
                    ShouldExit = true;
            }
			// Fecha o raylib
        } catch(Exception e) {
			// log exception
		}
    }

    private static void Update() {
        currentScene.Update();
        audio.Update();
    }

    private static void Render() {
        Raylib.BeginDrawing();
            Raylib.ClearBackground(ClearColor);
            currentScene.Render();
        Raylib.EndDrawing();
    }


}
