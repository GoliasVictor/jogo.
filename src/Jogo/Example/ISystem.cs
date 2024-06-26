interface IScene<T> : ISystem<T>
{
    void Render(T context);
    void ViewSizeChanged() {}
}
interface IScene : ISystem
{
    void Render();
    void ViewSizeChanged() {}
}

class LevelScene : IScene; 
class UIScene : IScene;

interface ISystem<T>
{
    void Update(T context);
}

interface ISystem
{
    void Update();
}

class TickUpdateSystem : ISystem<LevelScene>;
class CollisionSystem : ISystem<LevelScene>;
interface IAudio : ISystem;
