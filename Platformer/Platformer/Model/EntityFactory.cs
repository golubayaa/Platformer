namespace Platformer
{
    class EntityFactory<T> : Entity where T : Entity
    {
        public T? Create(float x, float y)
        {
             return Activator.CreateInstance(typeof(T), new object[] { x, y }) as T;
        }

        public IEnumerable<T?> CreateMany(params (float x, float y)[] positions)
        {
            foreach (var pos in positions)
                yield return Create(pos.x, pos.y);            
        }
    }
}
