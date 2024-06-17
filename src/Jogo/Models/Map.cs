using System.Reflection.Metadata;
using System.Linq;
using Raylib_cs;

class Map {
	public IEntity[,] FixedEntities { get; private init; }
	
	public List<IEntity> MovableEntities { get; private init; }
	public int Rows => FixedEntities.GetLength(0); 
	public int Collumns => FixedEntities.GetLength(1); 
	public IEnumerable<IEntity> AllEntities { get {
			for (int i = 0; i < FixedEntities.GetLength(0); i++)
				for (int j = 0; j < FixedEntities.GetLength(1); j++)
					if (FixedEntities[i, j] != null)
						yield return FixedEntities[i, j] ;
			foreach(var entity in MovableEntities){
				yield return entity;
			}
		}
	}
	
	public IEnumerable<IEntity> this[int i, int j]{
        get
        {
			if (0 > i || i >= Rows)
				throw new IndexOutOfRangeException();
			if (0 > j || j >= Collumns)
				throw new IndexOutOfRangeException();
            yield return FixedEntities[i, j];

			foreach (var entity in AllEntities)
				if (entity.Position.i == i && entity.Position.j == j )
					yield return entity;
        } 
	}
	public IEnumerable<IEntity> this[GridVec2 pos]  => this[pos.i, pos.j];

	public Map(IEntity[,] fixedEntities, IEnumerable<IEntity>  movableEntities){
		this.FixedEntities = fixedEntities;
		for (int i = 0; i < fixedEntities.GetLength(0); i++){
			for (int j = 0; j < fixedEntities.GetLength(1); j++){
				if (fixedEntities[i, j] == null)
					fixedEntities[i, j] = new EmptyEntity();
				fixedEntities[i, j].Position = new GridVec2(i, j);
			}
		}
		this.MovableEntities =  movableEntities.Where(e => e != null).ToList();
	}
}