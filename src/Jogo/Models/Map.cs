using System.Reflection.Metadata;
using Raylib_cs;

class Map {
	IEntity[,] grid { get; init; }
	public int Rows => grid.GetLength(0); 
	public int Collumns => grid.GetLength(1); 
	public IEnumerable<IEntity> entities { get {
			for (int i = 0; i < grid.GetLength(0); i++)
				for (int j = 0; j < grid.GetLength(1); j++)
					if (grid[i, j] != null)
						yield return grid[i, j] ;
		}
	}
	
	public IEntity this[int i, int j]{
        get
        {
			if (0 > i || i >= Rows)
				throw new IndexOutOfRangeException();
			if (0 > j || j >= Collumns)
				throw new IndexOutOfRangeException();
            return grid[i, j]; 
        } 
          
        set
        {
            grid[i, j] = value ?? new EmptyEntity(); 
        } 
	}
	public IEntity? this[TileVec2 pos] 
    {
		get => this[pos.i, pos.j];
		set => this[pos.i, pos.j] = value; 
    } 

	public Map(IEntity[,] grid){
		this.grid = grid;
		for (int i = 0; i < grid.GetLength(0); i++)
			for (int j = 0; j < grid.GetLength(1); j++)
				if (grid[i, j] == null)
					grid[i, j] = new EmptyEntity();
	}
}