using System.Numerics;
using Raylib_cs;

class UIImage(Texture2D texture, Container? parent = null) : UIComponent(parent)
{
    public Texture2D Texture = texture;
    public override Vector2 Size => new(Texture.Width, Texture.Height);
    public override void Render()
    {
        var position = Parent.GetPosition(this);
        Raylib.DrawTexture(Texture, (int)position.X, (int)position.Y, new Color(0,0,0,0));
    }
}