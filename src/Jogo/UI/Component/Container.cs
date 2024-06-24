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

abstract class UnitaryContainer : Container{
    public UIComponent Component { get; private set; }
    public override Vector2 Size => Component.Size;

    public UnitaryContainer(UIComponent component, Container? parent = null) : base(parent)
    {
        Component = component;
        Component.Parent = this;
    }
    public override List<UIComponent> GetChildren()
    {
        return [Component];
    }
}