struct GridVec2(int i, int j)
{
	public int i = i;
	public int j = j;

    public static GridVec2 operator +(GridVec2 v) => v;
	public static GridVec2 operator +(GridVec2 v, GridVec2 u) => new(v.i+u.i,v.j+u.j);
	public static GridVec2 operator -(GridVec2 v) => new(-v.i, -v.j);
	public static GridVec2 operator -(GridVec2 v, GridVec2 u) => new(v.i - u.i, v.j - u.j);

	public static readonly GridVec2 UP = new(-1,0);
	public static readonly GridVec2 DOWN = new(1,0);
	public static readonly GridVec2 LEFT = new(0,-1);
	public static readonly GridVec2 RIGHT = new(0,1);
	public static readonly GridVec2 ZERO = new(0,0);
}
