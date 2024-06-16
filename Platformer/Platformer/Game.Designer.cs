namespace Platformer
{
    partial class Game
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            GameOver = new PictureBox();
            HealthBar = new PictureBox();
            Winning = new PictureBox();
            EnemyCounter = new Label();
            ((System.ComponentModel.ISupportInitialize)GameOver).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HealthBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Winning).BeginInit();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Tick += UpdateLogicAndView;
            // 
            // GameOver
            // 
            GameOver.BackColor = Color.Transparent;
            GameOver.Image = Assets1.Game_over;
            GameOver.Location = new Point(300, 418);
            GameOver.Name = "GameOver";
            GameOver.Size = new Size(1319, 152);
            GameOver.SizeMode = PictureBoxSizeMode.AutoSize;
            GameOver.TabIndex = 1;
            GameOver.TabStop = false;
            GameOver.Visible = false;
            // 
            // HealthBar
            // 
            HealthBar.BackColor = Color.Transparent;
            HealthBar.Image = Assets1.Health4of4;
            HealthBar.Location = new Point(12, 12);
            HealthBar.Name = "HealthBar";
            HealthBar.Size = new Size(261, 54);
            HealthBar.SizeMode = PictureBoxSizeMode.AutoSize;
            HealthBar.TabIndex = 2;
            HealthBar.TabStop = false;
            // 
            // Winning
            // 
            Winning.BackColor = Color.Transparent;
            Winning.BackgroundImage = Assets1.Winning;
            Winning.Location = new Point(447, 396);
            Winning.Name = "Winning";
            Winning.Size = new Size(1071, 197);
            Winning.SizeMode = PictureBoxSizeMode.AutoSize;
            Winning.TabIndex = 3;
            Winning.TabStop = false;
            Winning.Visible = false;
            // 
            // EnemyCounter
            // 
            EnemyCounter.AutoSize = true;
            EnemyCounter.BackColor = Color.Transparent;
            EnemyCounter.Font = new Font("Mongolian Baiti", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            EnemyCounter.ForeColor = SystemColors.ButtonFace;
            EnemyCounter.Location = new Point(279, 23);
            EnemyCounter.Name = "EnemyCounter";
            EnemyCounter.Size = new Size(214, 29);
            EnemyCounter.TabIndex = 4;
            EnemyCounter.Text = "Врагов осталось:";
            // 
            // Game
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Assets1.Background;
            ClientSize = new Size(1784, 861);
            Controls.Add(EnemyCounter);
            Controls.Add(Winning);
            Controls.Add(HealthBar);
            Controls.Add(GameOver);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Game";
            Text = "Platformer";
            ((System.ComponentModel.ISupportInitialize)GameOver).EndInit();
            ((System.ComponentModel.ISupportInitialize)HealthBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)Winning).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public System.Windows.Forms.Timer timer1;
        private PictureBox GameOver;
        public PictureBox HealthBar;
        private PictureBox Winning;
        private Label EnemyCounter;
    }
}
