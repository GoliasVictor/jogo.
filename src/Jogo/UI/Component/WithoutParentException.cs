
class WithoutParentException : Exception
{
    public UIComponent Component { get; set; }
    public WithoutParentException(UIComponent component) : base("Component does not have a parent.")
    {
        Component = component;
    }

    public WithoutParentException(UIComponent component, string message) : base(message)
    {
        Component = component;
    }

    public WithoutParentException(UIComponent component,string message, Exception innerException) : base(message, innerException)
    {
        Component = component;
    }
}