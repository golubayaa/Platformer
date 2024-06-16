using System;

namespace Platformer
{
    public class Enemy : Entity
    {
        public event Action<Enemy> OnEnemyKilled;
        public override float PositionX { get; set; }
        public override float PositionY { get; set; }
        public float DirectionX => Model.GetInstance().PlayerPositionX - PositionX;
        public float DirectionY => Model.GetInstance().PlayerPositionY - PositionY;
        public float DirectionLength => MathF.Sqrt(DirectionX * DirectionX + DirectionY * DirectionY);
        public Enemy(float posX, float posY)
        {
            PositionX = posX;
            PositionY = posY;
            SpriteSheet = Assets1.Enemy;
            AnimationFrames = 8;
            SpriteSheetStep = SpriteSheet.Width / AnimationFrames;
            Size = new Size(80, SpriteSheet.Height);
            Rectangle = new Rectangle(new Point((int)PositionX, (int)PositionY), Size);
        }

        public void SendEnemyKilled()
        {
            if (OnEnemyKilled != null) 
                OnEnemyKilled.Invoke(this);
            View.GetInstance().Fishes.Add(new Fish(PositionX, PositionY));
        }

        public override void UpdateRectangleAndAnimation()
        {
            Rectangle = new Rectangle(new Point((int)PositionX, (int)PositionY), Size);
        }
    }
}
