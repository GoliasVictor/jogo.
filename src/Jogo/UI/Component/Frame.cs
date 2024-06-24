using System.Numerics;
using Raylib_cs;

class Frame : UnitaryContainer

{
    public Color BackgroundColor;
    public Color BorderColor;
    public float BorderThick;

    public Frame(Color backgroundColor, UIComponent component, Color borderColor = default, float borderThick = 0, Container? parent = null) : base(component, parent)
    {
        BackgroundColor = backgroundColor;
        BorderColor = borderColor;
        BorderThick = borderThick;
    }

    public override Vector2 GetPosition(UIComponent component)
    {
        return Parent.GetPosition(this) + new Vector2(BorderThick, BorderThick) ;
    }

    public override List<UIComponent> GetChildren()
    {
        return [Component];
    }

    public override void Render()
    {
        Vector2 pos = Parent.GetPosition(this);
        Raylib.DrawRectangle((int)pos.X, (int)pos.Y, (int)Size.X, (int)Size.Y, BackgroundColor);
        Rectangle rect = new((int)pos.X, (int)pos.Y, (int)Size.X, (int)Size.Y);
        Raylib.DrawRectangleLinesEx(rect, BorderThick, BorderColor);
        base.Render();
    }
 
}