using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleSrun4K
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Srun4KManager Manager;
        bool IsInInitialize;
        private void Form1_Load(object sender, EventArgs e)
        {
            IsInInitialize = true;

            Manager = Srun4KManager.Create();

            TB_UserName.DataBindings.Add("Text", Manager.ConfigInfo, "UserName");
            TB_Password.DataBindings.Add("Text", Manager.ConfigInfo, "Password");
            TB_LoginUrl.DataBindings.Add("Text", Manager.ConfigInfo, "LoginUrl");
            TB_LogoutUrl.DataBindings.Add("Text", Manager.ConfigInfo, "LogoutUrl");
            TB_CheckOnlineUrl.DataBindings.Add("Text", Manager.ConfigInfo, "CheckOnlineUrl");

            TB_UserName.TextChanged += Controls_Changed;
            TB_Password.TextChanged += Controls_Changed;
            TB_LoginUrl.TextChanged += Controls_Changed;
            TB_LogoutUrl.TextChanged += Controls_Changed;
            TB_CheckOnlineUrl.TextChanged += Controls_Changed;

            TB_Password.PasswordChar = '*';

            if (Manager.ConfigInfo.AutoLogin)
            {
                CB_AutoLogin.CheckState = CheckState.Checked;
            }
            else
            {
                CB_AutoLogin.CheckState = CheckState.Unchecked;
            }

            if (Manager.ConfigInfo.AutoStart)
            {
                if(AutoStartup.Check())
                {
                    CB_AutoStart.CheckState = CheckState.Checked;
                }
                else
                {
                    Manager.ConfigInfo.AutoStart = false;
                    Manager.ConfigIsChange = true;
                    SaveChanges();
                }
            }
            else
            {
                CB_AutoStart.CheckState = CheckState.Unchecked;
            }

            BTN_Login.Focus();

            Manager.ConfigIsChange = false;

            IsInInitialize = false;

            if(Manager.ConfigInfo.AutoLogin)
            {
                if (System.Environment.CurrentDirectory == System.Environment.SystemDirectory)
                {
                    WindowState = FormWindowState.Minimized;
                }
                Thread autoLogin = new Thread(OnAutoLogin);
                autoLogin.Start();
            }
        }


        #region 向底部Log窗口追加日志

        delegate void AddTextCallBack(string text);

        void OnAutoLogin()
        {
            Thread.Sleep(1000);
            AddTextToLog("正在自动登录……");
            AddTextToLog(Manager.AutoLogin().Info);
        }

        #endregion

        private void AddTextToLog(string text)
        {
            if (RTB_Log.InvokeRequired)
            {
                AddTextCallBack stcb = new AddTextCallBack(AddTextToLog);
                this.Invoke(stcb, new object[] { text });
            }
            else
            {
                RTB_Log.Text += "\n" + text + "\n";
            }
        }

        void Controls_Changed(object sender, EventArgs e)
        {
            Manager.ConfigIsChange = true;
        }

        private void SaveChanges()
        {
            if (Manager.ConfigIsChange)
            {
                AddTextToLog("正在保存配置……");
                BasicReturnInfo writeResult = Manager.ReWriteConfig();

                if (writeResult)
                {
                    Manager.ConfigIsChange = false;
                    AddTextToLog("保存成功");
                }
                else
                {
                    MessageBox.Show("错误码: 0x" + writeResult.Code.ToString("x").PadLeft(8, '0'));
                    AddTextToLog("保存失败");
                    AddTextToLog(writeResult.Info);
                }
            }
        }

        private void BTN_Login_Click(object sender, EventArgs e)
        {
            SaveChanges();

            Thread temp = new Thread(ThreadFunction_Login);
            temp.Start();
        }

        void ThreadFunction_Login()
        {
            AddTextToLog("正在登录……");
            BasicReturnInfo loginResult = Manager.Login();
            AddTextToLog(loginResult.Info);
        }

        private void BTN_Logout_Click(object sender, EventArgs e)
        {
            SaveChanges();

            Thread temp = new Thread(ThreadFunction_Logout);
            temp.Start();
        }

        void ThreadFunction_Logout()
        {
            AddTextToLog("正在登出……");
            BasicReturnInfo logoutResult = Manager.Logout();
            AddTextToLog(logoutResult.Info);
        }

        private void BTN_CheckOnline_Click(object sender, EventArgs e)
        {
            SaveChanges();

            Thread temp = new Thread(ThreadFunction_CheckOnline);
            temp.Start();
        }

        void ThreadFunction_CheckOnline()
        {
            AddTextToLog("正在测试在线状态……");
            BasicReturnInfo checkonlineResult = Manager.CheckOnline();
            AddTextToLog(checkonlineResult.Info);
        }

        private void BTN_ForceLogout_Click(object sender, EventArgs e)
        {
            SaveChanges();

            Thread temp = new Thread(ThreadFunction_ForceLogout);
            temp.Start();
        }

        void ThreadFunction_ForceLogout()
        {
            AddTextToLog("正在强制登出……");
            BasicReturnInfo forcelogoutResult = Manager.ForceLogout();
            AddTextToLog(forcelogoutResult.Info);
        }

        private void CB_AutoStart_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_AutoStart.CheckState == CheckState.Checked)
            {
                Manager.ConfigInfo.AutoStart = true;
            }
            else
            {
                Manager.ConfigInfo.AutoStart = false;
            }

            if(!IsInInitialize)
            {
                Thread temp = new Thread(ThreadFuction_ChangeAutoStart);
                temp.Start();
                Manager.ConfigIsChange = true;
                SaveChanges();
            }
            
        }

        void ThreadFuction_ChangeAutoStart()
        {
            BasicReturnInfo cas = Manager.ChangeAutoStartState();

            if(cas)
            {
                if(Manager.ConfigInfo.AutoStart)
                {
                    AddTextToLog("自动启动设置成功");
                }
                else
                {
                    AddTextToLog("自动启动取消成功");
                }
            }
            else
            {
                if (Manager.ConfigInfo.AutoStart)
                {
                    AddTextToLog("自动启动设置失败");
                }
                else
                {
                    AddTextToLog("自动启动取消失败");
                }

                AddTextToLog(cas.Info);
            }
        }

        private void CB_AutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            if(CB_AutoLogin.CheckState == CheckState.Checked)
            {
                Manager.ConfigInfo.AutoLogin = true;
            }
            else
            {
                Manager.ConfigInfo.AutoLogin = false;
            }

            if(!IsInInitialize)
            {
                Manager.ConfigIsChange = true;
                SaveChanges();
            }
        }

        private void RTB_Log_TextChanged(object sender, EventArgs e)
        {
            RTB_Log.SelectionStart = RTB_Log.Text.Length; //Set the current caret position at the end
            RTB_Log.ScrollToCaret(); //Now scroll it automatically
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
                //托盘区图标隐藏
                NI_mini.Visible = false;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮
            if (WindowState == FormWindowState.Minimized)
            {
                //隐藏任务栏区图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                NI_mini.Visible = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
            else
            {
                e.Cancel = true;
            } 
            */
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BTN_Login_Click(sender, e);
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BTN_Logout_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
            else
            {
                // e.Cancel = true;
            } 
            */
        }

        private void showMainWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }




    }
}
