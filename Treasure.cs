using Raylib_cs;

public class Treasure : GameObject
{
    public int _value;
    public Treasure(int x, int y) : base(x, y, Color.Gold)
    {
        Random random = new Random();
        _value = random.Next(1, 6);
    }

    public override void Draw()
    {
        Raylib.DrawText(_value.ToString(), _x, _y, 30, _color);
    }

    public override void ProcessActions()
    {
        _y += 10;
    }
}