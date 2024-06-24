using System.Numerics;
using Raylib_cs;

class UIText(string text, Color color, int fontSize = 16, int spacing = 1, Container? parent = null) : UIComponent(parent)
{
    public string Text = text;
    public Color color = color;
    public int FontSize = fontSize;
    public int Spacing = spacing;
    public override Vector2 Size
    {
        get
        {
            return Raylib.MeasureTextEx(Raylib.GetFontDefault(), Text, FontSize, Spacing);
        }
    }
    public override void Render()
    {
        var position = Parent.GetPosition(this);
        Raylib.DrawTextEx(Raylib.GetFontDefault(), Text, position, FontSize, Spacing, color);

    }
}