using System.Threading.Tasks;
using Raylib_cs;

class GameManager
{
    public const int SCREEN_WIDTH = 800;
    public const int SCREEN_HEIGHT = 600;

    private string _title;
    private List<GameObject> _gameObjects = new List<GameObject>();
    private int _lives = 3;
    private int _score = 0;

    public GameManager()
    {
        _title = "CSE 210 Game";
    }

    private bool IsCollision(GameObject first, GameObject second)
    {
        if ((first.GetRightEdge() >= second.GetLeftEdge()) &&
        (first.GetLeftEdge() <= second.GetRightEdge()) &&
        (first.GetBottomEdge() >= second.GetTopEdge()) &&
        (first.GetTopEdge() <= second.GetBottomEdge()))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// The overall loop that controls the game. It calls functions to
    /// handle interactions, update game elements, and draw the screen.
    /// </summary>
    public void Run()
    {
        Raylib.SetTargetFPS(60);
        Raylib.InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, _title);
        // If using sound, un-comment the lines to init and close the audio device
        // Raylib.InitAudioDevice();

        InitializeGame();

        while (!Raylib.WindowShouldClose())
        {
            HandleInput();
            ProcessActions();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            DrawElements();

            Raylib.EndDrawing();
        }

        // Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }

    /// <summary>
    /// Sets up the initial conditions for the game.
    /// </summary>
    private void InitializeGame()
    {
        Player player = new Player(400, 570);
        _gameObjects.Add(player);
        
    }

    /// <summary>
    /// Responds to any input from the user.
    /// </summary>
    private void HandleInput()
    {
        foreach (GameObject item in _gameObjects)
        {
            item.HandleInput();
        }
    }

    /// <summary>
    /// Processes any actions such as moving objects or handling collisions.
    /// </summary>
    private void ProcessActions()
    {
        foreach (GameObject item in _gameObjects)
        {
            item.ProcessActions();
        }

        Random random = new Random();
        if (random.Next(0, 100) < 5) // 5% chance to spawn a bomb in each frame
        {
            int randomX = random.Next(0, SCREEN_WIDTH);
            Bomb bomb = new Bomb(randomX, 0);
            _gameObjects.Add(bomb);
        }

        Random r = new Random();
        if (r.Next(0, 100) < 5) // 5% chance to spawn a treasure in each frame
        {
            int randomX = random.Next(0, SCREEN_WIDTH);
            Treasure treasure = new Treasure(randomX, 0);
            _gameObjects.Add(treasure);
        }

        Player player = _gameObjects.OfType<Player>().FirstOrDefault();
        if (player != null)
        {
            List<GameObject> itemsToRemove = new List<GameObject>();
            foreach (GameObject item in _gameObjects)
            {
                if (item is Bomb bomb && IsCollision(player, bomb))
                {
                    _lives --;
                    itemsToRemove.Add(bomb);

                    if (_lives <= 0)
                    {
                        EndGame();
                        return;
                    }
                }
                else if (item is Treasure treasure && IsCollision(player, treasure))
                {
                    _score += treasure._value;
                    itemsToRemove.Add(treasure);
                }
            }
            foreach (GameObject bomb in itemsToRemove)
            {
                _gameObjects.Remove(bomb);
            }
        }
    }

    /// <summary>
    /// Draws all elements on the screen.
    /// </summary>
    private void DrawElements()
    {
        foreach (GameObject item in _gameObjects)
        {
            item.Draw();
        }
        //display lives
        Raylib.DrawText($"{_lives}", 10, 10, 25, Color.Red);
        
        //display score
        Raylib.DrawText($"{_score}", 150, 10, 25, Color.Blue);
    }

    private void EndGame()
    {
        Raylib.ClearBackground(Color.White);
        Raylib.DrawText("Game Over!", 250, 200, 50, Color.Red);
        Raylib.DrawText($"Score: {_score}", 250, 300, 30, Color.Blue);
        Raylib.EndDrawing();
        Thread.Sleep(3000);
        Environment.Exit(0); 
    }
}