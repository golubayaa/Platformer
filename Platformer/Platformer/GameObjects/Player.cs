namespace Platformer
{
    public class Player : Entity
    {
        public float TicksOfImmunity { get; set; }
        public bool IsImmune { get; set; } = false;
        public int Health { get; set; } = MaxHealth;
        public override float PositionX => Model.GetInstance().PlayerPositionX;
        public override float PositionY => Model.GetInstance().PlayerPositionY;
        private float OffsetX => Model.GetInstance().PlayerPositionX - Model.GetInstance().OriginalX;
        private float OffsetY => Model.GetInstance().PlayerPositionY - Model.GetInstance().OriginalY;
        public static readonly int MaxHealth = 4;
        public readonly float TotalTicksOfImmunity = 20f;

        public override void UpdateRectangleAndAnimation()
        {
            if(OffsetX < 0)
            {
                SpriteSheet = Assets1.Player_Run_Left;
                AnimationFrames = 8;
            }

            if (OffsetX > 0)
            {
                SpriteSheet = Assets1.Player_Run_Right;
                AnimationFrames = 8;
            }

            if (OffsetY < 0)
            {
                SpriteSheet = Assets1.Player_Move_Up_Right;
                AnimationFrames = 2;
            }

            if (OffsetY < 0 && OffsetX < 0)
            {
                SpriteSheet = Assets1.Player_Move_Up_Left;
                AnimationFrames = 2;
            }

            if (OffsetY > 0)
            {
                SpriteSheet = Assets1.Player_Move_Down_Right;
                AnimationFrames = 2;
            }

            if (OffsetY > 0 && OffsetX < 0)
            {
                SpriteSheet = Assets1.Player_Move_Down_Left;
                AnimationFrames = 2;
            }

            if (OffsetX == 0 && OffsetY == 0)
            {
                SpriteSheet = Assets1.Player_Idle;
                AnimationFrames = 6;
            }

            Size = new Size(SpriteSheet.Width / AnimationFrames, SpriteSheet.Height);

            Rectangle = new Rectangle(new Point((int)(PositionX), (int)(PositionY)), Size);
        }
    }
}
