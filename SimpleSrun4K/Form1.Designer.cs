namespace SimpleSrun4K
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TB_UserName = new System.Windows.Forms.TextBox();
            this.TB_Password = new System.Windows.Forms.TextBox();
            this.TB_LoginUrl = new System.Windows.Forms.TextBox();
            this.TB_LogoutUrl = new System.Windows.Forms.TextBox();
            this.TB_CheckOnlineUrl = new System.Windows.Forms.TextBox();
            this.LB_UserName = new System.Windows.Forms.Label();
            this.LB_Password = new System.Windows.Forms.Label();
            this.LB_LoginUrl = new System.Windows.Forms.Label();
            this.LB_LogoutUrl = new System.Windows.Forms.Label();
            this.LB_CheckOnlineUrl = new System.Windows.Forms.Label();
            this.BTN_Login = new System.Windows.Forms.Button();
            this.BTN_Logout = new System.Windows.Forms.Button();
            this.BTN_ForceLogout = new System.Windows.Forms.Button();
            this.BTN_CheckOnline = new System.Windows.Forms.Button();
            this.RTB_Log = new System.Windows.Forms.RichTextBox();
            this.CB_AutoStart = new System.Windows.Forms.CheckBox();
            this.CB_AutoLogin = new System.Windows.Forms.CheckBox();
            this.NI_mini = new System.Windows.Forms.NotifyIcon(this.components);
            this.NIMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMainWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NIMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TB_UserName
            // 
            this.TB_UserName.Location = new System.Drawing.Point(91, 12);
            this.TB_UserName.Name = "TB_UserName";
            this.TB_UserName.Size = new System.Drawing.Size(303, 21);
            this.TB_UserName.TabIndex = 30;
            // 
            // TB_Password
            // 
            this.TB_Password.Location = new System.Drawing.Point(91, 39);
            this.TB_Password.Name = "TB_Password";
            this.TB_Password.PasswordChar = '*';
            this.TB_Password.Size = new System.Drawing.Size(303, 21);
            this.TB_Password.TabIndex = 31;
            // 
            // TB_LoginUrl
            // 
            this.TB_LoginUrl.Location = new System.Drawing.Point(91, 66);
            this.TB_LoginUrl.Name = "TB_LoginUrl";
            this.TB_LoginUrl.Size = new System.Drawing.Size(303, 21);
            this.TB_LoginUrl.TabIndex = 2;
            // 
            // TB_LogoutUrl
            // 
            this.TB_LogoutUrl.Location = new System.Drawing.Point(91, 93);
            this.TB_LogoutUrl.Name = "TB_LogoutUrl";
            this.TB_LogoutUrl.Size = new System.Drawing.Size(303, 21);
            this.TB_LogoutUrl.TabIndex = 3;
            // 
            // TB_CheckOnlineUrl
            // 
            this.TB_CheckOnlineUrl.Location = new System.Drawing.Point(91, 120);
            this.TB_CheckOnlineUrl.Name = "TB_CheckOnlineUrl";
            this.TB_CheckOnlineUrl.Size = new System.Drawing.Size(303, 21);
            this.TB_CheckOnlineUrl.TabIndex = 4;
            // 
            // LB_UserName
            // 
            this.LB_UserName.AutoSize = true;
            this.LB_UserName.Location = new System.Drawing.Point(12, 15);
            this.LB_UserName.Name = "LB_UserName";
            this.LB_UserName.Size = new System.Drawing.Size(59, 12);
            this.LB_UserName.TabIndex = 5;
            this.LB_UserName.Text = "UserName:";
            // 
            // LB_Password
            // 
            this.LB_Password.AutoSize = true;
            this.LB_Password.Location = new System.Drawing.Point(12, 42);
            this.LB_Password.Name = "LB_Password";
            this.LB_Password.Size = new System.Drawing.Size(59, 12);
            this.LB_Password.TabIndex = 6;
            this.LB_Password.Text = "Password:";
            // 
            // LB_LoginUrl
            // 
            this.LB_LoginUrl.AutoSize = true;
            this.LB_LoginUrl.Location = new System.Drawing.Point(12, 69);
            this.LB_LoginUrl.Name = "LB_LoginUrl";
            this.LB_LoginUrl.Size = new System.Drawing.Size(59, 12);
            this.LB_LoginUrl.TabIndex = 7;
            this.LB_LoginUrl.Text = "LoginUrl:";
            // 
            // LB_LogoutUrl
            // 
            this.LB_LogoutUrl.AutoSize = true;
            this.LB_LogoutUrl.Location = new System.Drawing.Point(12, 96);
            this.LB_LogoutUrl.Name = "LB_LogoutUrl";
            this.LB_LogoutUrl.Size = new System.Drawing.Size(65, 12);
            this.LB_LogoutUrl.TabIndex = 8;
            this.LB_LogoutUrl.Text = "LogoutUrl:";
            // 
            // LB_CheckOnlineUrl
            // 
            this.LB_CheckOnlineUrl.AutoSize = true;
            this.LB_CheckOnlineUrl.Location = new System.Drawing.Point(12, 123);
            this.LB_CheckOnlineUrl.Name = "LB_CheckOnlineUrl";
            this.LB_CheckOnlineUrl.Size = new System.Drawing.Size(71, 12);
            this.LB_CheckOnlineUrl.TabIndex = 9;
            this.LB_CheckOnlineUrl.Text = "CheckOLUrl:";
            // 
            // BTN_Login
            // 
            this.BTN_Login.Location = new System.Drawing.Point(198, 147);
            this.BTN_Login.Name = "BTN_Login";
            this.BTN_Login.Size = new System.Drawing.Size(95, 23);
            this.BTN_Login.TabIndex = 0;
            this.BTN_Login.Text = "Login";
            this.BTN_Login.UseVisualStyleBackColor = true;
            this.BTN_Login.Click += new System.EventHandler(this.BTN_Login_Click);
            // 
            // BTN_Logout
            // 
            this.BTN_Logout.Location = new System.Drawing.Point(299, 147);
            this.BTN_Logout.Name = "BTN_Logout";
            this.BTN_Logout.Size = new System.Drawing.Size(95, 23);
            this.BTN_Logout.TabIndex = 1;
            this.BTN_Logout.Text = "Logout";
            this.BTN_Logout.UseVisualStyleBackColor = true;
            this.BTN_Logout.Click += new System.EventHandler(this.BTN_Logout_Click);
            // 
            // BTN_ForceLogout
            // 
            this.BTN_ForceLogout.Location = new System.Drawing.Point(299, 176);
            this.BTN_ForceLogout.Name = "BTN_ForceLogout";
            this.BTN_ForceLogout.Size = new System.Drawing.Size(95, 23);
            this.BTN_ForceLogout.TabIndex = 12;
            this.BTN_ForceLogout.Text = "Force Logout";
            this.BTN_ForceLogout.UseVisualStyleBackColor = true;
            this.BTN_ForceLogout.Click += new System.EventHandler(this.BTN_ForceLogout_Click);
            // 
            // BTN_CheckOnline
            // 
            this.BTN_CheckOnline.Location = new System.Drawing.Point(198, 176);
            this.BTN_CheckOnline.Name = "BTN_CheckOnline";
            this.BTN_CheckOnline.Size = new System.Drawing.Size(95, 23);
            this.BTN_CheckOnline.TabIndex = 13;
            this.BTN_CheckOnline.Text = "Check Online";
            this.BTN_CheckOnline.UseVisualStyleBackColor = true;
            this.BTN_CheckOnline.Click += new System.EventHandler(this.BTN_CheckOnline_Click);
            // 
            // RTB_Log
            // 
            this.RTB_Log.Location = new System.Drawing.Point(14, 205);
            this.RTB_Log.Name = "RTB_Log";
            this.RTB_Log.Size = new System.Drawing.Size(380, 121);
            this.RTB_Log.TabIndex = 14;
            this.RTB_Log.Text = "";
            this.RTB_Log.TextChanged += new System.EventHandler(this.RTB_Log_TextChanged);
            // 
            // CB_AutoStart
            // 
            this.CB_AutoStart.AutoSize = true;
            this.CB_AutoStart.Location = new System.Drawing.Point(14, 151);
            this.CB_AutoStart.Name = "CB_AutoStart";
            this.CB_AutoStart.Size = new System.Drawing.Size(78, 16);
            this.CB_AutoStart.TabIndex = 15;
            this.CB_AutoStart.Text = "AutoStart";
            this.CB_AutoStart.UseVisualStyleBackColor = true;
            this.CB_AutoStart.CheckedChanged += new System.EventHandler(this.CB_AutoStart_CheckedChanged);
            // 
            // CB_AutoLogin
            // 
            this.CB_AutoLogin.AutoSize = true;
            this.CB_AutoLogin.Location = new System.Drawing.Point(14, 180);
            this.CB_AutoLogin.Name = "CB_AutoLogin";
            this.CB_AutoLogin.Size = new System.Drawing.Size(78, 16);
            this.CB_AutoLogin.TabIndex = 16;
            this.CB_AutoLogin.Text = "AutoLogin";
            this.CB_AutoLogin.UseVisualStyleBackColor = true;
            this.CB_AutoLogin.CheckedChanged += new System.EventHandler(this.CB_AutoLogin_CheckedChanged);
            // 
            // NI_mini
            // 
            this.NI_mini.ContextMenuStrip = this.NIMenuStrip;
            this.NI_mini.Icon = ((System.Drawing.Icon)(resources.GetObject("NI_mini.Icon")));
            this.NI_mini.Text = "SimpleSrun4K";
            this.NI_mini.Visible = true;
            this.NI_mini.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // NIMenuStrip
            // 
            this.NIMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.logoutToolStripMenuItem,
            this.showMainWindowToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.NIMenuStrip.Name = "NIMenuStrip";
            this.NIMenuStrip.Size = new System.Drawing.Size(153, 114);
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // showMainWindowToolStripMenuItem
            // 
            this.showMainWindowToolStripMenuItem.Name = "showMainWindowToolStripMenuItem";
            this.showMainWindowToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showMainWindowToolStripMenuItem.Text = "Show";
            this.showMainWindowToolStripMenuItem.Click += new System.EventHandler(this.showMainWindowToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 338);
            this.Controls.Add(this.CB_AutoLogin);
            this.Controls.Add(this.CB_AutoStart);
            this.Controls.Add(this.RTB_Log);
            this.Controls.Add(this.BTN_CheckOnline);
            this.Controls.Add(this.BTN_ForceLogout);
            this.Controls.Add(this.BTN_Logout);
            this.Controls.Add(this.BTN_Login);
            this.Controls.Add(this.LB_CheckOnlineUrl);
            this.Controls.Add(this.LB_LogoutUrl);
            this.Controls.Add(this.LB_LoginUrl);
            this.Controls.Add(this.LB_Password);
            this.Controls.Add(this.LB_UserName);
            this.Controls.Add(this.TB_CheckOnlineUrl);
            this.Controls.Add(this.TB_LogoutUrl);
            this.Controls.Add(this.TB_LoginUrl);
            this.Controls.Add(this.TB_Password);
            this.Controls.Add(this.TB_UserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SimpleSrun4K";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.NIMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_UserName;
        private System.Windows.Forms.TextBox TB_Password;
        private System.Windows.Forms.TextBox TB_LoginUrl;
        private System.Windows.Forms.TextBox TB_LogoutUrl;
        private System.Windows.Forms.TextBox TB_CheckOnlineUrl;
        private System.Windows.Forms.Label LB_UserName;
        private System.Windows.Forms.Label LB_Password;
        private System.Windows.Forms.Label LB_LoginUrl;
        private System.Windows.Forms.Label LB_LogoutUrl;
        private System.Windows.Forms.Label LB_CheckOnlineUrl;
        private System.Windows.Forms.Button BTN_Login;
        private System.Windows.Forms.Button BTN_Logout;
        private System.Windows.Forms.Button BTN_ForceLogout;
        private System.Windows.Forms.Button BTN_CheckOnline;
        private System.Windows.Forms.RichTextBox RTB_Log;
        private System.Windows.Forms.CheckBox CB_AutoStart;
        private System.Windows.Forms.CheckBox CB_AutoLogin;
        private System.Windows.Forms.NotifyIcon NI_mini;
        private System.Windows.Forms.ContextMenuStrip NIMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMainWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

