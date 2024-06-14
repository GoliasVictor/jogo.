interface IScene<T> : ISystem<T> {
	public void Render(T context);
}

interface IScene : ISystem {
	public void Render();
}
