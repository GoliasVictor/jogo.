using System.Numerics;
using Raylib_cs;
class UIScene : IScene
{
    public Container root;

    public void Render()
    {
        root.Render();
    }

    public void Update()
    {
        root.Update();
    }
}