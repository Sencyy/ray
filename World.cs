public class World {
    public List<IFrameObject> objects;


    public World() {
        objects = new List<IFrameObject>();
    }

    public ObjectID AddObject(IFrameObject obj) {
        objects.Add(obj);
        return new ObjectID(objects.Count() - 1);
    }

    public IFrameObject Access(ObjectID id) {
        return objects[id.Retrieve()];
    }

    
}