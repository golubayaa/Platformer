namespace Platformer
{
    public partial class Game : Form
    {
        readonly Controller controller = Controller.GetInstance();
        readonly Model model = Model.GetInstance();
        readonly View view = View.GetInstance();

        private event Action update;
        private bool isGameOver = false;

        public Game()
        {
            InitializeComponent();
            Paint += View.GetInstance().OnPaint;
            KeyDown += Controller.GetInstance().OnKeyDown;
            KeyUp += Controller.GetInstance().OnKeyUp;
            MouseDown += Controller.GetInstance().OnMouseDown;
            MouseMove += Model.GetInstance().OnMouseMove;
            timer1.Interval = 16;
            timer1.Tick += new EventHandler(UpdateLogicAndView);
            Model.OnGameOver += EndGame;
            Init();
            KeyDown += OnKeyDown;
            FormClosing += OnFormClosing;
            Bounds = Screen.PrimaryScreen.Bounds;
            EnemyCounter.Text = "Врагов осталось: " + (View.Positions.Count + view.Enemies.Count);
        }

        private void Init()
        {
            timer1.Start();

            controller.InitController();
            view.InitView(Width, Height);
            model.InitModel(this);

            update += view.Player.UpdateRectangleAndAnimation;
            update += view.Sword.UpdateRectangleAndAnimation;
            update += model.MovePlayer;
            update += model.MoveSword;
            update += model.MoveEnemies;
            foreach (var enemy in View.GetInstance().Enemies)
            {
                update += enemy.UpdateRectangleAndAnimation;
            }
        }

        public void UpdateLogicAndView(object? sender, EventArgs e)
        {
            EnemyCounter.Text = "Врагов осталось: " + (View.Positions.Count + view.Enemies.Count);

            if (View.Positions.Count + view.Enemies.Count == 0)
            {
                isGameOver = true;
                Winning.Visible = true;
            }
            if (!isGameOver)
            {
                update.Invoke();

                Invalidate();
            }
        }

        public void UpdateHealthBar()
        {
            switch (view.Player.Health)
            {
                case 0:
                    HealthBar.Image = Assets1.Health0of4;
                    break;
                case 1:
                    HealthBar.Image = Assets1.Health1of4;
                    break;
                case 2:
                    HealthBar.Image = Assets1.Health2of4;
                    break;
                case 3:
                    HealthBar.Image = Assets1.Health3of4;
                    break;
                case 4:
                    HealthBar.Image = Assets1.Health4of4;
                    break;

            }
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                GameExit();
        }

        private void GameExit()
        {
            if (MessageBox.Show("Are you sure you want to leave?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
                Application.Restart();
            }
        }

        public void OnFormClosing(object? sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public void EndGame()
        {
            isGameOver = true;
            GameOver.Visible = true;
        }
    }
}
