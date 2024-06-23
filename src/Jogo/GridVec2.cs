/// <summary>
/// Represents a 2D grid vector with integer coordinates.
/// </summary>
struct GridVec2(int i, int j)
{
	/// <summary>
	/// The i-coordinate of the vector.
	/// </summary>
	public int i = i;

	/// <summary>
	/// The j-coordinate of the vector.
	/// </summary>
	public int j = j;

	/// <summary>
	/// Adds a vector to itself.
	/// </summary>
	public static GridVec2 operator +(GridVec2 v) => v;

	/// <summary>
	/// Adds two vectors together.
	/// </summary>
	public static GridVec2 operator +(GridVec2 v, GridVec2 u) => new(v.i + u.i, v.j + u.j);

	/// <summary>
	/// Negates the vector.
	/// </summary>
	public static GridVec2 operator -(GridVec2 v) => new(-v.i, -v.j);

	/// <summary>
	/// Subtracts one vector from another.
	/// </summary>
	public static GridVec2 operator -(GridVec2 v, GridVec2 u) => new(v.i - u.i, v.j - u.j);
	public static bool operator ==(GridVec2 v, GridVec2 u) =>  v.i == u.i && v.j == u.j;
	public static bool operator !=(GridVec2 v, GridVec2 u) =>  !(v == u);

	/// <summary>
	/// Represents a vector pointing upwards.
	/// </summary>
	public static readonly GridVec2 UP = new(-1, 0);

	/// <summary>
	/// Represents a vector pointing downwards.
	/// </summary>
	public static readonly GridVec2 DOWN = new(1, 0);

	/// <summary>
	/// Represents a vector pointing to the left.
	/// </summary>
	public static readonly GridVec2 LEFT = new(0, -1);

	/// <summary>
	/// Represents a vector pointing to the right.
	/// </summary>
	public static readonly GridVec2 RIGHT = new(0, 1);

	/// <summary>
	/// Represents the zero vector.
	/// </summary>
	public static readonly GridVec2 ZERO = new(0, 0);
}
