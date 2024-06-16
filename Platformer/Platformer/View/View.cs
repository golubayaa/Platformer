
namespace Platformer
{
    internal class View
    {
        public Player Player { get; } = new Player();
        public Attack Sword { get; } = new Attack();
        public List<Enemy> Enemies { get; private set; }
        public List<Fish> Fishes { get; private set; }
        public int ScreenSizeX { get; private set; }
        public float ScreenSizeY { get; private set; }
        public int PlyerStartPositionX { get; private set; }
        public int PlyerStartPositionY { get; private set; }
        public int WorldOffsetX => PlyerStartPositionX - (int)Player.PositionX;
        public int WorldOffsetY => PlyerStartPositionY - (int)Player.PositionY;

        private static readonly Bitmap standardBitmap = new(64, 64);
        private static readonly Bitmap[,] bitmaps = new Bitmap[Map.MapHeight, Map.MapWidth];
        public static List<(float, float)> Positions { get; private set; } = new List<(float, float)>
        { (0, 0), (1920, 1080), (500,700), (800, 1200), (200, 400),
        (-500,500), (-1200, -1200), (-2000, 500), (3000, 2000), (800, 2000),
        (0, 0), (1920, 1080), (500,700), (800, 1200), (200, 400),
        (-500,500), (-1200, -1200), (-2000, 500), (3000, 2000), (800, 2000),
        (2000, 0), (3920, 1080), (2500,700), (2800, 1200), (2200, 400),
        (1500,500), (800, -1200), (0, 500), (5000, 2000), (2800, 2000),
        (-500,2500), (-1200, 800), (-2000, 2500), (3000, 4000), (800, 4000),
        (2000, 2000), (3920, 3080), (2500,2700), (2800, 3200), (2200, 2400),
        (1500,2500), (800, 800), (0, 2500), (5000, 6000), (2800, 4000)};

    private readonly int enemyAnimationSpeed = 2;
        private readonly int playerAnimationSpeed = 4;
        private readonly int swordAnimationSpeed = 5;
        private readonly int fishAnimationSpeed = 10;
        private readonly EntityFactory<Enemy> enemyFactory = new EntityFactory<Enemy>();
        private readonly EntityFactoryWithTimeInterval<Enemy> enemyTimeFactory = new EntityFactoryWithTimeInterval<Enemy>();
        private int playerFrame = 0;
        private int enemyFrame = 0;
        private int fishFrame = 0;
        private int attackFrames = 0;
        
        private static View? instance;

        private View() { }

        public static View GetInstance()
        {
            instance ??= new View();

            return instance;
        }

        public void InitView(int Width, int Height)
        {
            Fishes = new List<Fish>();
            Enemies = new List<Enemy>() { new Enemy(10000,10000)};
            PlyerStartPositionX = (int)Player.PositionX;
            PlyerStartPositionY = (int)Player.PositionY;
            ScreenSizeX = Width; 
            ScreenSizeY = Height;
            Player.UpdateRectangleAndAnimation();
            Sword.UpdateRectangleAndAnimation();
            UpdateBitmaps();
        }

        public void OnPaint(object? sender, PaintEventArgs e)
        {
            foreach (var enemy in enemyTimeFactory.CreateMany(20,Positions))
            {
                if (enemy != null)
                {
                    Positions.RemoveAt(0);
                    Enemies.Add(enemy);
                }
            }
            var g = e.Graphics;
            DrawPlayer(sender, g);
            DrawMap(sender, g);
            DrawEnemies(sender, g);
            DrawAttack(sender, g);
            DrawFishes(sender, g);
        }

        private void DrawFishes(object? sender, Graphics g)
        {
            foreach (var fish in Fishes)
            {
                if (fishFrame > fish.AnimationFrames * fishAnimationSpeed - 1)
                {
                    fishFrame = 0;
                }

                g.DrawImage(fish.SpriteSheet, new Rectangle(new Point(fish.Rectangle.X + WorldOffsetX, fish.Rectangle.Y + WorldOffsetY), fish.Rectangle.Size),
                new Rectangle(new Point(fishFrame++ / fishAnimationSpeed * fish.SpriteSheetStep, 0), fish.Size), GraphicsUnit.Pixel);
            }
        }
        private void DrawEnemies(object? sender, Graphics g)
        {
            foreach (var enemy in Enemies)
            {
                if (enemyFrame > enemy.AnimationFrames * enemyAnimationSpeed - 1)
                {
                    enemyFrame = 0;
                }

                g.DrawImage(enemy.SpriteSheet, new Rectangle(new Point((int)enemy.PositionX + WorldOffsetX, (int)enemy.PositionY + WorldOffsetY) , enemy.Rectangle.Size),
                new Rectangle(new Point(enemyFrame++ / enemyAnimationSpeed * enemy.SpriteSheetStep, 0), enemy.Size), GraphicsUnit.Pixel);
            }
        }

        private void DrawAttack(object? sender, Graphics g)
        {
            if (!Sword.IsAttacking)
            {
                attackFrames = 0;
                return;
            }
            if (attackFrames > Sword.AnimationFrames * swordAnimationSpeed - 1)
            {
                attackFrames = 0;
            }

            g.DrawImage(Sword.SpriteSheet, new Rectangle(new Point(Sword.Rectangle.X + WorldOffsetX, Sword.Rectangle.Y + WorldOffsetY), Sword.Rectangle.Size),
               new Rectangle(new Point(attackFrames++ / swordAnimationSpeed * Sword.SpriteSheetStep, 0), Sword.Size), GraphicsUnit.Pixel);
        }

        private void DrawPlayer(object? sender, Graphics g)
        {
            if (playerFrame > Player.AnimationFrames * playerAnimationSpeed - 1)
            {
                playerFrame = 0;
            }

            g.DrawImage(Player.SpriteSheet, new Rectangle(new Point(PlyerStartPositionX, PlyerStartPositionY), Player.Size),
                new Rectangle(new Point(playerFrame++ / playerAnimationSpeed * Player.Size.Width, 0), Player.Size), GraphicsUnit.Pixel);         
        }

        private void DrawMap(object? sender, Graphics g)
        {
            for (int i = 0; i < Map.MapHeight; i++)
            {
                for (int j = 0; j < Map.MapWidth; j++)
                {
                    var currentSprite = bitmaps[i, j];

                    g.DrawImage(currentSprite, new RectangleF(new Point(i * currentSprite.Width + WorldOffsetX, j * currentSprite.Height + WorldOffsetY), currentSprite.Size));
                }
            }
        }

        public void UpdateBitmaps()
        {
            for (int i = 0; i < Map.MapHeight; i++)
            {
                for (int j = 0; j < Map.MapWidth; j++)
                {
                    UpdateBitmap(i, j);
                }
            }
        } 

        public void UpdateBitmap(int i, int j)
        {
            bitmaps[i, j] = Map.MapAsArrayOfTiles[i, j].Type != TileType.Nothing ?
                        new Bitmap("Resources\\World\\" + Map.MapAsArrayOfTiles[i, j].Type.ToString() + ".png")
                        : standardBitmap;
        }
    }
}
