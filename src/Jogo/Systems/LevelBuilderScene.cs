using Raylib_cs;

/// <summary>
/// A tool for creating and editing level in the game
/// </summary>
public class LevelBuilderScene : IScene
{
    /// <summary>
    /// Keybinds for LevelBuilder map manipulation. 
    /// </summary>
    private static class BuilderKey {
        public const KeyboardKey Player = KeyboardKey.Zero;
        public const KeyboardKey Wall = KeyboardKey.One;
        public const KeyboardKey Block = KeyboardKey.Two;
        public const KeyboardKey Enemy = KeyboardKey.Three; 
        public const KeyboardKey Item = KeyboardKey.Four;
        public const KeyboardKey Key = KeyboardKey.Five;
        public const KeyboardKey Door = KeyboardKey.Six;
        public const KeyboardKey Goal = KeyboardKey.Nine;
        public const KeyboardKey Fire = KeyboardKey.Kp1;
        public const KeyboardKey Water = KeyboardKey.Kp2;
        public const KeyboardKey Grass = KeyboardKey.Kp3;
        public const KeyboardKey None = KeyboardKey.Kp0;
    }


    private const string CustomLevelPath = @"Levels/custom-levels.yaml";

    private bool _isTesting = false;
    private Map _map;
    private LevelScene _testScene;
    private string _mapBackup = "";
    private GridVec2 _selection = GridVec2.ZERO;
    private int index;

    private bool IsTesting {
        get => _isTesting;
        set {
            if(value) {
                _mapBackup = LevelLoader.ParseMap(_map);
            }else{
                _map = LevelLoader.LoadFromString(_mapBackup);
                _testScene = new LevelScene([new TickUpdateSystem(), new CollisionSystem(), new ItemCollectionSystem()], _map);
            }
            _isTesting = value;
        }
    }

    /// <summary>
    /// Generates a new LevelBuilderScene and loads the map stored in "Levels/custom-levels.yaml" relative to the index.
    /// </summary>
    /// <param name="index">Index of the level.</param>
    public LevelBuilderScene(int index) {
        this.index = index;
        _map = LevelLoader.LoadFromYaml(CustomLevelPath, index);
        _mapBackup = LevelLoader.ParseMap(_map);
        _testScene = new LevelScene([new TickUpdateSystem(), new CollisionSystem(), new ItemCollectionSystem()], _map);
    }

    public void Update()
    {
        if(Raylib.IsKeyPressed(KeyboardKey.Enter)) IsTesting = !IsTesting;

        if(IsTesting) {
            _testScene.Update();
            if (_testScene.Player.PlayerKilled || _testScene.levelWon){
                _testScene = new LevelScene([new TickUpdateSystem(), new CollisionSystem(), new ItemCollectionSystem()], _map);
                IsTesting = false;
            }
        }else {
            _selection.i += (int) Raylib.IsKeyPressed(KeyboardKey.Down) - (int) Raylib.IsKeyPressed(KeyboardKey.Up);
            _selection.j += (int)(Raylib.IsKeyPressed(KeyboardKey.Right) - Raylib.IsKeyPressed(KeyboardKey.Left));

            if(_selection.i < 0) _selection.i = 9;
            if(_selection.j < 0) _selection.j = 9;
            if(_selection.i >= 10) _selection.i = 0;
            if(_selection.j >= 10) _selection.j = 0;
            HandleCell();

            if(Raylib.IsKeyDown(KeyboardKey.LeftControl) && Raylib.IsKeyPressed(KeyboardKey.S)) {
                index = LevelLoader.StoreMap(_map, CustomLevelPath, index);
            }
        }
    }

    /// <summary>
    /// Manages cell operations based on BuilderKey presses.
    /// </summary>
    private void HandleCell() {
        int cmd = Raylib.GetKeyPressed();
        IEntity? entity = (_map[_selection].Count() > 0)? _map[_selection].First() : null;
        if(entity is Player)
            return;
        if(entity is Goal && _map.Entities.Count(e => e is Goal) == 1)
            return; 
        if(entity != null) _map.Entities.Remove(entity);
        while(cmd != 0) {
            switch((KeyboardKey) cmd) {
                case BuilderKey.None: entity = null;
                break;
                case BuilderKey.Player:
                    Player player = _testScene.Player;
                    player.Position = _selection;
                    entity = null;
                break;
                case BuilderKey.Block:
                    entity = new Box(_selection);
                break;
                case BuilderKey.Enemy:
                    entity = new Enemy(_selection, Element.Water, true);
                break;
                case BuilderKey.Fire:
                    if(entity is Box || entity is WaterEntity) {
                        entity = new FireEntity(_selection);
                    }else if(entity is Enemy) {
                        ((Enemy) entity).Element = Element.Fire;
                    }else if(entity is ElementChangerEntity) {
                        entity = new ElementChangerEntity(_selection, Element.Fire);
                    }
                break;
                case BuilderKey.Water:
                    if(entity is Box || entity is FireEntity) {
                        entity = new WaterEntity(_selection);
                    }else if(entity is Enemy) {
                        ((Enemy) entity).Element = Element.Water;
                    }else if(entity is ElementChangerEntity) {
                        entity = new ElementChangerEntity(_selection, Element.Water);
                    }
                break;
                case BuilderKey.Grass:
                    if(entity is FireEntity || entity is WaterEntity) {
                        entity = new Box(_selection);
                    }else if(entity is Enemy) {
                        ((Enemy) entity).Element = Element.Leaf;
                    }else if(entity is ElementChangerEntity) {
                        entity = new ElementChangerEntity(_selection, Element.Leaf);
                    }
                break;
                case BuilderKey.Wall:
                    entity = new Wall(_selection);
                break;
                case BuilderKey.Door:
                    entity = new DoorEntity(_selection);
                break;
                case BuilderKey.Key:
                    entity = new Key(_selection);
                break;
                case BuilderKey.Item:
                    entity = new ElementChangerEntity(_selection, Element.Water);
                break;
                case BuilderKey.Goal:
                    entity = new Goal(_selection);
                break;
                default: break;
            }
            cmd = Raylib.GetKeyPressed();
        }
        if(entity != null) _map.Entities.Add(entity);
    }

    public void Render()
    {
        _testScene.Render();
        if(IsTesting) {
            Raylib.DrawText("Testing...", 0, 0, 20, Color.RayWhite);
        }else {
            Raylib.BeginMode2D(_testScene._camera);

                Raylib.DrawRectangleLines(_selection.j * GameSystem.TileSize, _selection.i * GameSystem.TileSize, GameSystem.TileSize, GameSystem.TileSize, Color.RayWhite);

            Raylib.EndMode2D();
        }
        Raylib.DrawFPS(0,120);
    }

    public void ViewSizeChanged() {
        _testScene.ViewSizeChanged();
    }

}