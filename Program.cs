using Raylib_cs;

internal static class Program {
    public static void Main() {
        Raylib.InitWindow(800, 640, "Raylib");
        World world = new World();
        var player = world.AddObject(new Player(new System.Numerics.Vector2(30, 30), 5f));
        Raylib.SetTargetFPS(75);

        while (!Raylib.WindowShouldClose()) {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            // Run Update() function of every object in world
            foreach (var obj in world.objects) {
                obj.Update();
            }
            Console.WriteLine("FPS: " + Raylib.GetFPS() + " Frametime: " + Raylib.GetFrameTime());


            Raylib.EndDrawing();

        }

        Raylib.CloseWindow();
    }
}
