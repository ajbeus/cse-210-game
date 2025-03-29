using System.ComponentModel;
using Raylib_cs;

public abstract class GameObject
{
    protected int _x;
    protected int _y;
    protected int _width;
    protected int _height;
    protected Color _color;

    public GameObject(int x, int y, Color color)
    {
        _x = x;
        _y = y;
        _color = color;
    }

    public virtual int GetLeftEdge()
    {
        return _x;
    }

    public virtual int GetRightEdge()
    {
        return _x + _width;
    }

    public virtual int GetTopEdge()
    {
        return _y;
    }

    public virtual int GetBottomEdge()
    {
        return _y + _height;
    }

    public abstract void Draw();

    public virtual void HandleInput()
    {
        // default behavior is to do nothing
    }

    public virtual void ProcessActions()
    {
        // default behavior is to do nothing
    }

}