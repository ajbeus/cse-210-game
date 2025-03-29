using Raylib_cs;

public class Player : GameObject
{
    public Player(int x, int y) : base(x, y, Color.Blue)
    {
        _width = 50;
        _height = 10;
        _color = Color.Blue;

    }

    public override void Draw()
    {
        Raylib.DrawRectangle(_x, _y, _width, _height, _color);
    }

    public override void HandleInput()
    {
        if (Raylib.IsKeyDown(KeyboardKey.Right) && _x < 800 - _width)
        {
            _x += 10;
        }

        if (Raylib.IsKeyDown(KeyboardKey.Left) && _x > 0)
        {
            _x -= 10;
        }
    }

}