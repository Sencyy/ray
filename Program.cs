using Newtonsoft.Json;
using System.IO;
using Raylib_cs;
using System.Threading.Tasks;

internal static class Program {

    const string SERVER_ADDRESS = "127.0.0.1:8000";
    public static void Main(string[] args) {
        var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
        var server = new ServerHandler(SERVER_ADDRESS);

        Raylib.InitWindow(800, 640, "Raylib");

        World world = new World();


        if (args.Length > 0 && args[0] == "--restore") {
            string readstate = server.GetState().Result;
            Console.WriteLine(readstate.ToString());
            

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

        // Send current state to server
        server.SendState(state);


        // If another state already exists, delete it
        // if (File.Exists("state.json")) {
        //     File.Delete("state.json");
        // }

        // File.WriteAllText("state.json", state);
        // Console.WriteLine(state);   

        Raylib.CloseWindow();
    }
}
