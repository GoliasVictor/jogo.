class Map {
	IEntity[,] map ;
	IEnumerable<IEntity> entities { get {
			for (int i = 0; i < map.GetLength(0); i++)
				for (int j = 0; j < map.GetLength(1); j++)
					if (map[i, j] != null)
						yield return map[i, j] ;
		}
	}
}