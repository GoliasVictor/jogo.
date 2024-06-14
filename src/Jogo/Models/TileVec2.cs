struct TileVec2 {
	public int i;
	public int j;
	public  TileVec2(int i , int j){
		this.i = i;
		this.j = j;
	}
	public static TileVec2 operator +(TileVec2 v) => v;
	public static TileVec2 operator +(TileVec2 v, TileVec2 u) => new(v.i+u.i,v.j+u.j);
	public static TileVec2 operator -(TileVec2 v) => new(-v.i, -v.j);
	public static TileVec2 operator -(TileVec2 v, TileVec2 u) => new(v.i - u.i, v.j - u.j);

	public static readonly TileVec2 UP = new(-1,0);
	public static readonly TileVec2 DOWN = new(1,0);
	public static readonly TileVec2 LEFT = new(0,-1);
	public static readonly TileVec2 RIGHT = new(0,1);
}
