public class World {
    public List<WorldObject> objects;


    public World() {
        objects = new List<WorldObject>();
    }

    public ObjectID AddObject(WorldObject obj) {
        objects.Add(obj);
        return new ObjectID(objects.Count() - 1);
    }

    public WorldObject Access(ObjectID id) {
        return objects[id.Retrieve()];
    }

    
}