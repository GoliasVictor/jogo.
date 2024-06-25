using System.Numerics;

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