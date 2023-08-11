namespace Klondike
{
    partial class CardGame
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.게임ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.설명ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.랭킹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Time = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.재시작ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.게임ToolStripMenuItem,
            this.Time});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1139, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 게임ToolStripMenuItem
            // 
            this.게임ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.설명ToolStripMenuItem,
            this.랭킹ToolStripMenuItem,
            this.재시작ToolStripMenuItem,
            this.toolStripSeparator1,
            this.종료ToolStripMenuItem});
            this.게임ToolStripMenuItem.Name = "게임ToolStripMenuItem";
            this.게임ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.게임ToolStripMenuItem.Text = "메뉴";
            // 
            // 설명ToolStripMenuItem
            // 
            this.설명ToolStripMenuItem.Name = "설명ToolStripMenuItem";
            this.설명ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.설명ToolStripMenuItem.Text = "설명";
            this.설명ToolStripMenuItem.Click += new System.EventHandler(this.설명ToolStripMenuItem_Click);
            // 
            // 랭킹ToolStripMenuItem
            // 
            this.랭킹ToolStripMenuItem.Name = "랭킹ToolStripMenuItem";
            this.랭킹ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.랭킹ToolStripMenuItem.Text = "랭킹";
            this.랭킹ToolStripMenuItem.Click += new System.EventHandler(this.랭킹ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.종료ToolStripMenuItem.Text = "종료";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.종료ToolStripMenuItem_Click);
            // 
            // Time
            // 
            this.Time.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Time.AutoSize = false;
            this.Time.BackColor = System.Drawing.Color.DodgerBlue;
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(60, 20);
            this.Time.Text = "1일째";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 10;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // 재시작ToolStripMenuItem
            // 
            this.재시작ToolStripMenuItem.Name = "재시작ToolStripMenuItem";
            this.재시작ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.재시작ToolStripMenuItem.Text = "재시작";
            this.재시작ToolStripMenuItem.Click += new System.EventHandler(this.재시작ToolStripMenuItem_Click_1);
            // 
            // CardGame
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1139, 763);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "CardGame";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.CardGame_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CardGame_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CardGame_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CardGame_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CardGame_MouseUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 게임ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Time;
        private System.Windows.Forms.ToolStripMenuItem 설명ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 랭킹ToolStripMenuItem;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.ToolStripMenuItem 재시작ToolStripMenuItem;
    }
}

