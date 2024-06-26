using System.Numerics;

abstract class UIComponent
{
    public abstract Vector2 Size { get; } 

    public Container? Parent { get; set; };

    public Vector2 GetPosition();

    public virtual void Update(){ }

    public abstract void Render();
}