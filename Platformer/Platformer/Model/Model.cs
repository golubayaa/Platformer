namespace Platformer
{
    internal class Model
    {
        public static Action? OnGameOver { get; set; }

        public float PlayerPositionX { get; private set; } = 15 * Map.TileSize.Width;
        public float PlayerPositionY { get; private set; } = Map.TileSize.Height * 9 - 1;
        public float SwordPositionX { get; private set; }
        public float SwordPositionY { get; private set; }
        private float PlayerDirectionX => Controller.GetInstance().PlayerDirectionX;
        private float PlayerDirectionY => Controller.GetInstance().PlayerDirectionY;
        public float OriginalX { get; private set; }
        public float OriginalY { get; private set; }
        public bool IsJumping { get; private set; } = false;
        private Player Player => View.GetInstance().Player;
        private Attack Attack => View.GetInstance().Sword;
        private List<Enemy> Enemies => View.GetInstance().Enemies;
        private List<Fish> Fishes => View.GetInstance().Fishes;

        public readonly float playerSpeed = 7f;
        private readonly float enemySpeed = 15f;
        private readonly float standardJumpForce = 15f;
        private readonly float standardJumpForceDecrement = 0.5f;
        private float playerJumpForce = 0f;
        private float cursorLocalPositionX;
        private float cursorLocalPositionY;
        private static readonly float standardSwordOrbitRadius = 50f;
        private readonly float SwordOrbitIncrement = 10f;
        private float swordOrbitRadius = standardSwordOrbitRadius;
        private int attackTickCount;
        private int attackTicks = 20;
        private float lastStableStateX = float.NaN;
        private float lastStableStateY = float.NaN;
        private float swordDirectionX;
        private float swordDirectionY;

        Game game;

        private bool IsGrounded(Entity entity, int offset)
        {
            foreach (var tile in Map.GetNeighborsByCoordinates(entity.PositionX, entity.PositionY))
            {
                if (tile.Type != TileType.Nothing
                    && Rectangle.Intersect(new Rectangle(new Point((int)entity.PositionX + offset,
                    (int)entity.PositionY + offset), new Size(entity.Size.Width - 2 * offset, entity.Size.Height - 2 * offset)), tile.Rectangle)
                    != Rectangle.Empty)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsDead(Enemy enemy)
        {
            return Attack.IsAttacking && Rectangle.Intersect(enemy.Rectangle, Attack.Hitbox) != Rectangle.Empty;
        }

        private static Model? instance;

        private Model() { }

        public static Model GetInstance()
        {
            instance ??= new Model();

            return instance;
        }

        public void InitModel(Game form)
        {
            this.game = form;
            SwordPositionX = PlayerPositionX + Player.Size.Height / 2 +
                swordOrbitRadius * cursorLocalPositionX / MathF.Sqrt(cursorLocalPositionX * cursorLocalPositionX + cursorLocalPositionY * cursorLocalPositionY);

            SwordPositionY = PlayerPositionY + Player.Size.Width / 2 +
                swordOrbitRadius * cursorLocalPositionY / MathF.Sqrt(cursorLocalPositionX * cursorLocalPositionX + cursorLocalPositionY * cursorLocalPositionY);

            OriginalX = PlayerPositionX;
            OriginalY = PlayerPositionY;
            cursorLocalPositionX = PlayerPositionX - Player.Size.Height / 2;
            cursorLocalPositionY = PlayerPositionY - Player.Size.Width / 2;
        }

        public void MoveEnemies()
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].PositionX += enemySpeed * Enemies[i].DirectionX / Enemies[i].DirectionLength;
                Enemies[i].PositionY += enemySpeed * Enemies[i].DirectionY / Enemies[i].DirectionLength;
                Enemies[i].UpdateRectangleAndAnimation();

                foreach (var tile in Map.GetNeighborsByCoordinates(Enemies[i].PositionX, Enemies[i].PositionY))
                {
                    if (tile.Type != TileType.Nothing && RectangleF.Intersect(tile.Rectangle, Enemies[i].Rectangle) != RectangleF.Empty)
                    {
                        Map.DestroyTile(tile.Rectangle.X / Map.TileSize.Width, tile.Rectangle.Y / Map.TileSize.Height);
                    }
                }

                if (IsDead(Enemies[i]))
                {
                    Enemies[i].SendEnemyKilled();
                    Enemies.RemoveAt(i);
                }
            }
        }

        public void MoveSword()
        {
            if (!Attack.IsAttacking)
            {
                swordDirectionX = 0;
                swordDirectionY = 0;
                return;
            }

            if (attackTickCount >= attackTicks)
            {
                swordOrbitRadius = standardSwordOrbitRadius;
                Attack.IsAttacking = false;
                attackTickCount = 0;
                return;
            }

            swordOrbitRadius += SwordOrbitIncrement;

            attackTickCount += 1;

            if (swordDirectionX == 0) swordDirectionX = cursorLocalPositionX
                / MathF.Sqrt(cursorLocalPositionX * cursorLocalPositionX + cursorLocalPositionY * cursorLocalPositionY);
            if (swordDirectionY == 0) swordDirectionY = cursorLocalPositionY
                / MathF.Sqrt(cursorLocalPositionX * cursorLocalPositionX + cursorLocalPositionY * cursorLocalPositionY);

            SwordPositionX = PlayerPositionX + Player.Size.Height / 2 + swordOrbitRadius * swordDirectionX;

            SwordPositionY = PlayerPositionY + Player.Size.Width / 2 + swordOrbitRadius * swordDirectionY;

            foreach (var tile in Map.GetNeighborsByCoordinates(Attack.Rectangle.X, Attack.Rectangle.Y))
            {
                if (tile.Type != TileType.Nothing && RectangleF.Intersect(tile.Rectangle, Attack.Hitbox) != RectangleF.Empty)
                {
                    Map.DestroyTile(tile.Rectangle.X / Map.TileSize.Width, tile.Rectangle.Y / Map.TileSize.Height);
                }
            }
        }

        public void MovePlayer()
        {
            (OriginalX, OriginalY) = (PlayerPositionX, PlayerPositionY);

            EatFish();

            (PlayerPositionX,
                PlayerPositionY) = ValidatePlayerPosition();

            (PlayerPositionX,
                PlayerPositionY) = MoveByInput();

            if (!IsJumping || IsGrounded(Player, 1))
                (PlayerPositionX,
                    PlayerPositionY) = MoveByGravity();

            (PlayerPositionX,
                PlayerPositionY) = MoveByJump();

            if (IsGrounded(Player, 1))
                PlayerPositionY = (int)(PlayerPositionY + Player.Size.Height / 2) / Map.TileSize.Height * Map.TileSize.Height - 1;


            Map.IsPlayerInValidPosition(PlayerPositionX, PlayerPositionY);

            if (Player.TicksOfImmunity < 1 && Enemies.Any(x => Rectangle.Intersect(x.Rectangle, Player.Rectangle) != Rectangle.Empty))
            {
                Player.Health--;
                Player.TicksOfImmunity = Player.TotalTicksOfImmunity;
                game.UpdateHealthBar();
                if (Player.Health < 1)
                    GameOver();
            }

            Player.TicksOfImmunity--;           
        }

        private void EatFish()
        {
            for (int i = 0; i < Fishes.Count; i++)
            {
                if (Rectangle.Intersect(Player.Rectangle, Fishes[i].Rectangle) != Rectangle.Empty)
                {
                    Fishes.Remove(Fishes[i]);
                    Player.Health = Player.Health < Player.MaxHealth ? Player.Health + 1 : Player.MaxHealth;
                    game.UpdateHealthBar();
                }
            }
        }

        private (float, float) ValidatePlayerPosition()
        {
            if (IsGrounded(Player, 4) && !float.IsNaN(lastStableStateX) && !float.IsNaN(lastStableStateY))
                return (lastStableStateX, lastStableStateY);

            lastStableStateX = PlayerPositionX;
            lastStableStateY = PlayerPositionY;
            return (PlayerPositionX, PlayerPositionY);
        }

        private (float, float) MoveByInput()
        {
            var nextX = PlayerPositionX + PlayerDirectionX * playerSpeed;
            var nextY = PlayerPositionY;
            foreach (var tile in Map.GetNeighborsByCoordinates(PlayerPositionX, PlayerPositionY))
            {
                if (Rectangle.Intersect(new Rectangle(new Point((int)nextX + 1, (int)nextY + 1), new Size(Player.Size.Width - 2, Player.Size.Height - 2)),
                    tile.Rectangle) != Rectangle.Empty && tile.Type != TileType.Nothing)
                {
                    return (PlayerPositionX, PlayerPositionY);
                }
            }

            return (nextX, nextY);
        }

        private (float, float) MoveByGravity()
        {
            return (PlayerPositionX, PlayerPositionY + 7f);
        }

        private (float, float) MoveByJump()
        {
            if (PlayerDirectionY < 0 && IsGrounded(Player, 0) && !IsJumping)
            {
                IsJumping = true;
                playerJumpForce = standardJumpForce;
            }

            playerJumpForce -= standardJumpForceDecrement;

            if (playerJumpForce < 1f)
            {
                IsJumping = false;
                return (PlayerPositionX, PlayerPositionY);
            }

            foreach (var tile in Map.GetNeighborsByCoordinates(PlayerPositionX, PlayerPositionY))
            {
                if (Rectangle.Intersect(new Rectangle(new Point((int)PlayerPositionX + 1, (int)(PlayerPositionY - playerJumpForce) + 1),
                    new Size(Player.Size.Width - 2, Player.Size.Height - 2)),
                    tile.Rectangle) != Rectangle.Empty && tile.Type != TileType.Nothing)
                {
                    return (PlayerPositionX, PlayerPositionY);
                }
            }

            return (PlayerPositionX, PlayerPositionY - playerJumpForce);
        }

        public void GameOver() => OnGameOver.Invoke();

        public void OnMouseMove(object? sender, MouseEventArgs e)
        {
            cursorLocalPositionX = e.Location.X - View.GetInstance().WorldOffsetX - PlayerPositionX - Player.Size.Height / 2;
            cursorLocalPositionY = e.Location.Y - View.GetInstance().WorldOffsetY - PlayerPositionY - Player.Size.Width / 2;
        }
    }
}