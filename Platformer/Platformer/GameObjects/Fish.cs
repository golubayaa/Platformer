namespace Platformer
{
    internal class Fish : Entity
    {
        public Fish(float posX, float posY) 
        {
            PositionX = posX;
            PositionY = posY;
            SpriteSheet = Assets1.Fish;
            AnimationFrames = 11;
            SpriteSheetStep = SpriteSheet.Width / AnimationFrames;
            Size = new Size(64, SpriteSheet.Height);
            Rectangle = new Rectangle(new Point((int)PositionX, (int)PositionY), Size);
        }

        public override void UpdateRectangleAndAnimation()
        {
            Rectangle = new Rectangle(new Point((int)PositionX, (int)PositionY), Size);
        }
    }
}
