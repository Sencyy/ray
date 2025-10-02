using Newtonsoft.Json;
using System.IO;
using Raylib_cs;

internal static class Program {
    public static void Main(string[] args) {
        var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
        Raylib.InitWindow(800, 640, "Raylib");
        World world = new World();
        if (args.Length > 0 && args[0] == "--restore") {
            var readstate = File.ReadAllText("state.json");
            
            world = JsonConvert.DeserializeObject<World>(readstate, jsonSettings);
            if (world == null) {
                Console.Error.WriteLine("ERROR: Failed to read state");
                Environment.Exit(1);
            }
        }
        else {
            // If not restoring, add it's initial objects
            world.AddObject(new Player(new System.Numerics.Vector2(30, 30), 5f));
        }

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
        var state = JsonConvert.SerializeObject(world, jsonSettings);

        // If another state already exists, delete it
        if (File.Exists("state.json")) {
            File.Delete("state.json");
        }

        File.WriteAllText("state.json", state);
        // Console.WriteLine(state);   

        Raylib.CloseWindow();
    }
}
