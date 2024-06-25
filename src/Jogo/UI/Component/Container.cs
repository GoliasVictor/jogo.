using System.Numerics;


abstract class Container(Container? parent) : UIComponent(parent) {
    public abstract Vector2 GetPosition(UIComponent component);
    public abstract List<UIComponent> GetChildren();
    public override void Render()
    {
        foreach(var node in GetChildren()){
            node.Render();
        }
    }
    public override void Update()
    {
        foreach(var node in GetChildren()){
            node.Update();
        }
    }
}
