interface ISystem<T> {
	public void Update(T context);
}

interface ISystem {
	public void Update();
}