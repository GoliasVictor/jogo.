using System.Reflection.Metadata;
using System.Linq;
using Raylib_cs;

/// <summary>
/// Represents a game map that contains fixed and movable entities.
/// </summary>
class Map {
	/// <summary>
	/// Gets the fixed entities on the map.
	/// </summary>
	public IEntity[,] FixedEntities { get; private init; }

	/// <summary>
	/// Gets the movable entities on the map.
	/// </summary>
	public List<IEntity> MovableEntities { get; private init; }

	/// <summary>
	/// Gets the number of rows in the map.
	/// </summary>
	public int Rows => FixedEntities.GetLength(0);

	/// <summary>
	/// Gets the number of columns in the map.
	/// </summary>
	public int Collumns => FixedEntities.GetLength(1);

	/// <summary>
	/// Gets all entities on the map, including both fixed and movable entities.
	/// </summary>
	public IEnumerable<IEntity> AllEntities {
		get {
			for (int i = 0; i < FixedEntities.GetLength(0); i++) {
				for (int j = 0; j < FixedEntities.GetLength(1); j++) {
					if (FixedEntities[i, j] != null) {
						yield return FixedEntities[i, j];
					}
				}
			}
			foreach (var entity in MovableEntities) {
				yield return entity;
			}
		}
	}

	/// <summary>
	/// Gets the entities at the specified position on the map.
	/// </summary>
	/// <param name="i">The row index.</param>
	/// <param name="j">The column index.</param>
	/// <returns>The entities at the specified position.</returns>
	public IEnumerable<IEntity> this[int i, int j] {
		get {
			if (0 > i || i >= Rows) {
				throw new IndexOutOfRangeException();
			}
			if (0 > j || j >= Collumns) {
				throw new IndexOutOfRangeException();
			}
			yield return FixedEntities[i, j];

			foreach (var entity in AllEntities) {
				if (entity.Position.i == i && entity.Position.j == j) {
					yield return entity;
				}
			}
		}
	}

	/// <summary>
	/// Gets the entities at the specified position on the map.
	/// </summary>
	/// <param name="pos">The position.</param>
	/// <returns>The entities at the specified position.</returns>
	public IEnumerable<IEntity> this[GridVec2 pos] => this[pos.i, pos.j];

	/// <summary>
	/// Initializes a new instance of the <see cref="Map"/> class.
	/// </summary>
	/// <param name="fixedEntities">The fixed entities on the map.</param>
	/// <param name="movableEntities">The movable entities on the map.</param>
	public Map(IEntity[,] fixedEntities, IEnumerable<IEntity> movableEntities) {
		this.FixedEntities = fixedEntities;
		for (int i = 0; i < fixedEntities.GetLength(0); i++) {
			for (int j = 0; j < fixedEntities.GetLength(1); j++) {
				if (fixedEntities[i, j] == null) {
					fixedEntities[i, j] = new EmptyEntity();
				}
				fixedEntities[i, j].Position = new GridVec2(i, j);
			}
		}
		this.MovableEntities = movableEntities.Where(e => e != null).ToList();
	}
}