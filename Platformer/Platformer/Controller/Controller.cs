using System.Windows.Forms;

namespace Platformer
{
    internal class Controller
    {
        public int PlayerDirectionX { get; private set; }
        public int PlayerDirectionY { get; private set; }
        private static Controller instance;

        private Controller() { }

        public static Controller GetInstance()
        {
            instance ??= new Controller();

            return instance;
        }

        public void InitController()
        {
            PlayerDirectionX = 0;
            PlayerDirectionY = 0;
        }

        public void OnKeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    PlayerDirectionY = -1;
                    break;
                case Keys.S:
                    PlayerDirectionY = 1;
                    break;
                case Keys.A:
                    PlayerDirectionX = -1;
                    break;
                case Keys.D:
                    PlayerDirectionX = 1;
                    break;
            }
        }

        public void OnKeyUp(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    PlayerDirectionY = 0;
                    break;
                case Keys.S:
                    PlayerDirectionY = 0;
                    break;
                case Keys.A:
                    PlayerDirectionX = 0;
                    break;
                case Keys.D:
                    PlayerDirectionX = 0;
                    break;
            }
        }

        public void OnMouseDown(object? sender, MouseEventArgs e)
        {
            View.GetInstance().Sword.IsAttacking = true;
        }
    }
}
