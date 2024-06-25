public class LevelManagerScene : IScene
{
    private const string LevelListPath = @"Levels/custom-levels.yaml";
    private LevelScene currentLevel;
    private int levelIndex;
    private readonly int levelCount = LevelLoader.GetLevelCount(LevelListPath);

    public LevelManagerScene(uint index) {
        levelIndex = (int) index;
        currentLevel = new LevelScene([new TickUpdateSystem(), new CollisionSystem(), new ItemCollectionSystem()], LevelLoader.LoadFromYaml(LevelListPath, levelIndex), true);
    }

    public void Render()
    {
        currentLevel.Render();
    }

    public void Update()
    {
        if(currentLevel.Player.PlayerKilled) {
            SetLevel((uint) levelIndex);
        }else if(currentLevel.levelWon) {
            SetLevel((uint) levelIndex + 1);
        }else {
            currentLevel.Update();
        }
    }

    private void SetLevel(uint index) {
        if(index >= levelCount) {
            GameSystem.currentScene = new MainMenuScene();
            return;
        }
        levelIndex = (int) index;

        currentLevel = new LevelScene([new TickUpdateSystem(), new CollisionSystem(), new ItemCollectionSystem()], LevelLoader.LoadFromYaml(LevelListPath, levelIndex), true);
    }
}