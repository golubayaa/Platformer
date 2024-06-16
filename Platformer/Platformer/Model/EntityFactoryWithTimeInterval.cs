namespace Platformer
{
    class EntityFactoryWithTimeInterval<T> : EntityFactory<T> where T : Entity
    {
        private int currentTicks = 0;
        public IEnumerable<T?> CreateMany(int intervalInTicks, List<(float x, float y)> positions)
        {
            if (++currentTicks < intervalInTicks)
            {
                yield return null;
            }
            else
            {
                if (positions.Count == 0)
                    yield break;
                yield return Create(positions[0].x, positions[0].y);
                currentTicks = 0;
            }
        }
    }
}
