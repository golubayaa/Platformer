namespace Platformer
{
    public abstract class Entity
    {
        public virtual float PositionX { get; set; }
        public virtual float PositionY { get; set; }
        public int AnimationFrames { get; protected set; }
        public Image SpriteSheet { get; protected set; }
        public Size Size { get; protected set; }
        public Rectangle Rectangle { get; protected set; }

        public int SpriteSheetStep { get; protected set; }

        public virtual void UpdateRectangleAndAnimation() { }
    }
}
