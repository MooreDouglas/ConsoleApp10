namespace MyGame
{
    static class MyGame
    {
        private const int WindowWidth = 800;
        private const int WindowHeight = 600;

        private const string WindowTitle = "L o R B";

        private static void Main(string[] args)
        {
            Game.Initialize(WindowWidth, WindowHeight, WindowTitle);

            GameScene scene = new GameScene();
            Game.SetScene(scene);

            Game.Run();
        }
    }
}