using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platformer
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            Bounds = Screen.PrimaryScreen.Bounds;
        }

        private void PlayButtonOnClick(object sender, EventArgs e)
        {
            new Game().Show();
            Hide();
        }

        private void ExitButtonOnClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ControlsButton_Click(object sender, EventArgs e)
        {
            new Controls().Show();
        }
    }
}
