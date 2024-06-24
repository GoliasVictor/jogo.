using System.Numerics;

class FixedPositionContainer : UnitaryContainer  

{
    Vector2 Position;

    public FixedPositionContainer(Vector2 position, UIComponent component, Container? parent = null) : base(component, parent)
    {
        Position = position;
    }

    public override Vector2 GetPosition(UIComponent component)
    {
        var origin = Parent?.GetPosition(this) ?? Vector2.Zero;
        return origin + Position;
    }

}