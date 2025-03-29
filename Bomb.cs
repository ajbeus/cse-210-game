using Raylib_cs;

public class Bomb : GameObject
{
    public Bomb(int x, int y) : base(x, y, Color.Red)
    {
    }

    public override void Draw()
    {
        Raylib.DrawCircle(_x, _y, 10, _color);
    }

    public override void ProcessActions()
    {
        _y += 10;
    }
}