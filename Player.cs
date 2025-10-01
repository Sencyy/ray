using System.Numerics;
using Raylib_cs;

public class Player : IFrameObject {
    public Vector2 position;
    public Vector2 size;
    public float speed;

    public Player(Vector2 size, float speed) {
        this.position = new Vector2();
        this.size = size;
        this.speed = speed;
    }

    public void Update() {
        Raylib.DrawRectangleV(position, size, Color.DarkGray);

        if (Raylib.IsKeyDown(KeyboardKey.W)) {
            position.Y -= speed;
        }
            if (Raylib.IsKeyDown(KeyboardKey.S)) {
                this.position.Y += this.speed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.A)) {
                this.position.X -= this.speed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.D)) {
                this.position.X += this.speed;
            }
    }
}