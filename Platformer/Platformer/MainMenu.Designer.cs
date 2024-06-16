namespace Platformer
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PlayButton = new Button();
            ContolsButton = new Button();
            ExitButton = new Button();
            Title = new Label();
            SuspendLayout();
            // 
            // PlayButton
            // 
            PlayButton.BackgroundImage = Assets1.Play_Button;
            PlayButton.BackgroundImageLayout = ImageLayout.Stretch;
            PlayButton.Location = new Point(25, 405);
            PlayButton.Name = "PlayButton";
            PlayButton.Size = new Size(179, 63);
            PlayButton.TabIndex = 0;
            PlayButton.UseVisualStyleBackColor = true;
            PlayButton.Click += PlayButtonOnClick;
            // 
            // ContolsButton
            // 
            ContolsButton.BackgroundImage = Assets1.Controls_Button;
            ContolsButton.BackgroundImageLayout = ImageLayout.Stretch;
            ContolsButton.Location = new Point(25, 540);
            ContolsButton.Name = "ContolsButton";
            ContolsButton.Size = new Size(179, 63);
            ContolsButton.TabIndex = 1;
            ContolsButton.UseVisualStyleBackColor = true;
            ContolsButton.Click += ControlsButton_Click;
            // 
            // ExitButton
            // 
            ExitButton.BackgroundImage = Assets1.Exit_Button;
            ExitButton.BackgroundImageLayout = ImageLayout.Stretch;
            ExitButton.Location = new Point(25, 662);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(179, 63);
            ExitButton.TabIndex = 2;
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButtonOnClick;
            // 
            // Title
            // 
            Title.AutoSize = true;
            Title.BackColor = Color.Transparent;
            Title.Font = new Font("Arial Narrow", 78F, FontStyle.Regular, GraphicsUnit.Point);
            Title.ForeColor = SystemColors.ButtonFace;
            Title.Location = new Point(25, 81);
            Title.Name = "Title";
            Title.Size = new Size(603, 122);
            Title.TabIndex = 3;
            Title.Text = "ПУТЬ ВОИНА";
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Assets1.Background;
            ClientSize = new Size(1920, 1080);
            Controls.Add(Title);
            Controls.Add(ExitButton);
            Controls.Add(ContolsButton);
            Controls.Add(PlayButton);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainMenu";
            Text = "MainMenu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button PlayButton;
        private Button ContolsButton;
        private Button ExitButton;
        private Label Title;
    }
}