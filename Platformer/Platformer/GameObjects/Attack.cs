namespace Platformer
{
    class Attack : Entity
    {
        public bool IsAttacking { get; set; }
        new float PositionX => Model.GetInstance().SwordPositionX;
        new float PositionY => Model.GetInstance().SwordPositionY;

        public Rectangle Hitbox { get; private set; }

        public Attack() 
        {
            SpriteSheetStep = 64;
            SpriteSheet = Assets1.Attack;
            AnimationFrames = 20;
            Size = new Size(SpriteSheet.Width / AnimationFrames, SpriteSheet.Height);
            if (float.IsNaN(PositionX) || float.IsNaN(PositionY))
            {
                Hitbox = new Rectangle(new Point((int)PositionX, (int)PositionY), new Size((int)(Size.Width), (int)(Size.Height * 1.5)));
                Rectangle = new Rectangle(new Point((int)PositionX - Size.Width / 2, (int)PositionY - Size.Height / 2), Size);
            }
        }

        public override void UpdateRectangleAndAnimation()
        {
            if (float.IsNaN(PositionX) || float.IsNaN(PositionY))
                return;
            Rectangle = new Rectangle(new Point((int)PositionX - Size.Width / 2, (int)PositionY - Size.Height / 2), Size);
            Hitbox = new Rectangle(new Point((int)PositionX, (int)PositionY), new Size((int)(Size.Width), (int)(Size.Height * 1.5)));
        }
    }
}
