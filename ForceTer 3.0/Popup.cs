using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ForceTer
{
    public partial class Popup : Form
    {
        private ForceTer ft;
        private string icoAddress;
        private string names;
        public Popup(ForceTer ft)
        {
            InitializeComponent();
            this.ft = ft;
            this.DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
        }

        private void Popup_Load(object sender, EventArgs e)
        {
            LoadForm();
        }
        private void LoadForm()//设置窗口
        {
            this.Icon = Properties.Resources.ForceTer;
            this.BackgroundImage = Sqlite.GetBackgroundImage();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.TopMost = true;
        }

        public void LoadButton(Boolean exit, Boolean confirm, Boolean canel, string ok, string no, Boolean hot)//装载右上角按钮和确定取消按钮
        {
            if (exit) LoadImage(Properties.Resources._11, this.Width - 45 - 4, 0, 45, 22, "关闭", hot);
            if (confirm) SetLabelButton(this.Width / 4 - 50 + 25, this.Height - 45, 100, 24, ok, hot);
            if (canel) SetLabelButton(this.Width / 4 * 3 - 50 - 25, this.Height - 45, 100, 24, no, hot);
        }
        public void LoadButtonManySetting(string init, string scn, string def, string ok, string no, Boolean hot)//装载右上角按钮和确定取消按钮
        {
            LoadImage(Properties.Resources._11, this.Width - 45 - 4, 0, 45, 22, "关闭", hot);
            SetLabelButton(this.Width / 3 - 50, this.Height - 135, 100, 24, init, hot);
            SetLabelButton(this.Width / 3 * 2 - 50, this.Height - 135, 100, 24, scn, hot);
            SetLabelButton(this.Width / 4 - 50 - 25, this.Height - 45, 100, 24, def, hot);
            SetLabelButton(this.Width / 4 * 2 - 50, this.Height - 45, 100, 24, ok, hot);
            SetLabelButton(this.Width / 4 * 3 - 50 + 25, this.Height - 45, 100, 24, no, hot);
        }
        public void LoadButtonMany(string def, string ok, string no, Boolean hot)//装载右上角按钮和确定取消按钮
        {
            LoadImage(Properties.Resources._11, this.Width - 45 - 4, 0, 45, 22, "关闭", hot);
            SetLabelButton(this.Width / 4 - 50 - 25, this.Height - 45, 100, 24, def, hot);
            SetLabelButton(this.Width / 4 * 2 - 50, this.Height - 45, 100, 24, ok, hot);
            SetLabelButton(this.Width / 4 * 3 - 50 + 25, this.Height - 45, 100, 24, no, hot);
        }
        public void LoadButtonAttribute(string ok, Boolean hot)//装载右上角按钮和确定取消按钮
        {
            LoadImage(Properties.Resources._11, this.Width - 45 - 4, 0, 45, 22, "关闭", hot);
            SetLabelButtonOneOK(this.Width / 4 * 2 - 50, this.Height - 45, 100, 24, ok, hot);
        }
        public void LoadImage(Image image, int left, int top, int width, int height, string name, Boolean hot)//装载右上角按钮
        {
            PictureBox pb = new PictureBox();
            pb.Image = image;
            pb.Left = left;
            pb.Top = top;
            pb.Width = width;
            pb.Height = height;
            pb.BackColor = Color.Transparent;
            pb.SizeMode = PictureBoxSizeMode.CenterImage;
            ToolTip tt = new ToolTip();
            tt.SetToolTip(pb, name);
            pb.MouseEnter += Pb_MouseEnter4;
            if (hot)
            {
                pb.MouseClick += Pb_MouseClick3;
            }
            else
            {
                pb.MouseClick += Pb_MouseClick4;
            }
            pb.MouseLeave += Pb_MouseLeave2;
            this.Controls.Add(pb);
        }
        private void Pb_MouseClick3(object sender, MouseEventArgs e)//鼠标点击关闭,(当前点击的是添加里边的关闭)
        {
            ft.Show();
            this.Close();
            ft.LoadMenu("热门应用", true);
            ft.ResetKey();
        }
        private void Pb_MouseClick4(object sender, MouseEventArgs e)//鼠标点击关闭（这个点击的是对话框里的关闭）
        {
            ft.Enabled = true;
            ft.contextMenuStrip1.Enabled = true;
            ft.Show();
            this.Close();
        }
        private void Pb_MouseLeave2(object sender, EventArgs e)//鼠标离开
        {
            PictureBox pb = (PictureBox)sender;
            pb.BackColor = Color.FromArgb(0, 0, 0, 0);
        }
        private void Pb_MouseEnter4(object sender, EventArgs e)//鼠标进入了关闭按钮
        {
            PictureBox pb = (PictureBox)sender;
            pb.BackColor = Color.FromArgb(180, 230, 10, 10);
        }
        public void LoadAddForm(int width, int height)//设置显示内容（这个是添加程序的显示窗口）
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = width;
            this.Height = height;
            Rectangle rect = Screen.GetWorkingArea(this);
            this.Left = rect.Width / 2 - this.Width / 2;
            this.Top = rect.Height / 2 - this.Height / 2;
            SetLabel("添加程序", 12, 10, "微软雅黑", 10);
            SetLabel("把程序拖放到下方的地址栏中，可直接获取程序信息。", 100, 70, "微软雅黑", 9);
            SetLabel("路径：", 65, 98, "微软雅黑", 9);
            SetLabel("名称：", 55, 170, "微软雅黑", 9);
            SetLabel("分类：", 55, 210, "微软雅黑", 9);
            SetLabel("图标（拖入新文件更换）", 286, 145, "微软雅黑", 9);
            SetFalseTextBox(109, 96, 291, 20, "微软雅黑", 9, "", true);
            SetTextBox(99, 168, 155, "");
            SetComboBox(99, 208, 100, new List<string> { "", "娱乐游戏", "社交聊天", "影视音乐", "工具软件", "目录位置" });
            SetCheckBox(210, 207, 50, "常用");
            SetPictureBox(286, 170, 60, 60, false, true, null, false);
            SetPictureBox(356, 170, 60, 60, true, true, null, false);
            SetPanel(40, 50, 400, 80);
        }
        public void LoadPopup(int width, int height, string name, string filename, string id)//加载重命名类型的界面
        {
            this.FormBorderStyle = FormBorderStyle.None;
            SetLabel(name, 12, 10, "微软雅黑", 10);
            this.Width = width;
            this.Height = height;
            Rectangle rect = Screen.GetWorkingArea(this);
            this.Left = rect.Width / 2 - this.Width / 2;
            this.Top = rect.Height / 2 - this.Height / 2;
            SetLabel("当前名称：", 65, 68, "微软雅黑", 9);
            SetFalseTextBox(134, 66, 266, 20, "微软雅黑", 9, filename, true);
            SetLabel("新的名称：", 65, 108, "微软雅黑", 9);
            SetTextBox(134, 106, 266, filename);
            this.Tag = id;
        }
        public void LoadIcon(int width, int height, string name, string filename, string id)//加载修改图标的界面
        {
            this.FormBorderStyle = FormBorderStyle.None;
            SetLabel(name, 12, 10, "微软雅黑", 10);
            this.Width = width;
            this.Height = height;
            Rectangle rect = Screen.GetWorkingArea(this);
            this.Left = rect.Width / 2 - this.Width / 2;
            this.Top = rect.Height / 2 - this.Height / 2;
            filename = filename.Length > 22 ? filename.Substring(0, 21) + " ..." : filename;
            SetLabel("更换 <" + filename + " > 的图标 .", 12, 170, "微软雅黑", 10);
            SetLabel("当前程序图标", 82, 68, "微软雅黑", 9);
            SetLabel("请在下方拖入新的图标", 265, 68, "微软雅黑", 9);
            SetPictureBox(this.Width / 2 - 25, this.Height / 2 - 10, 50, 20, false, false, Properties.Resources._goto, true);
            SetPictureBox(this.Width / 4 - 20 - 35, this.Height / 2 - 30, 60, 60, false, false, Files.ReadImageFile("image\\" + id + ".png"), false);
            SetPictureBox(this.Width / 4 - 20 + 35, this.Height / 2 - 30, 60, 60, true, false, Files.ReadImageFile("image\\" + id + ".png"), false);
            SetPictureBox(this.Width / 4 * 3 - 40 - 35, this.Height / 2 - 30, 60, 60, false, true, null, false);
            SetPictureBox(this.Width / 4 * 3 - 40 + 35, this.Height / 2 - 30, 60, 60, true, true, null, false);
            this.Tag = id;
        }
        public void LoadBackgroud(int width, int height, string name)//加载修改图标的界面
        {
            this.FormBorderStyle = FormBorderStyle.None;
            SetLabel(name, 12, 10, "微软雅黑", 10);
            this.Width = width;
            this.Height = height;
            Rectangle rect = Screen.GetWorkingArea(this);
            this.Left = rect.Width / 2 - this.Width / 2;
            this.Top = rect.Height / 2 - this.Height / 2;
            SetLabel("当前系统皮肤", this.Width / 4 - 47, 55, "微软雅黑", 10);
            SetLabel("请在下方拖入新的皮肤图片", this.Width / 4 * 3 - 83, 55, "微软雅黑", 10);
            SetPictureBoxBackgroud(this.Width / 4 - 120, this.Height / 2 - 90, 240, 180, false, Sqlite.GetBackgroundImage());
            SetPictureBox(this.Width / 2 - 25, this.Height / 2 - 10, 50, 20, false, false, Properties.Resources._goto, true);
            SetPictureBoxBackgroud(this.Width / 4 * 3 - 120, this.Height / 2 - 90, 240, 180, true, null);
        }
        public void LoadSetting(int width, int height, string name)//加载系统设置的界面
        {
            names = name;
            this.FormBorderStyle = FormBorderStyle.None;
            SetLabel(name, 12, 10, "微软雅黑", 10);
            this.Width = width;
            this.Height = height;
            Rectangle rect = Screen.GetWorkingArea(this);
            this.Left = rect.Width / 2 - this.Width / 2;
            this.Top = rect.Height / 2 - this.Height / 2;
            SetCheckBoxSetting(50, 50, 150, "开机启动");
            SetCheckBoxSetting(50, 90, 150, "前台显示");
            SetCheckBoxSetting(50, 130, 150, "退出时提示");
            SetCheckBoxSetting(230, 50, 150, "打开程序后隐藏");
            SetCheckBoxSetting(230, 90, 150, "打开文件位置后隐藏");
            SetLabel("设置分辨率：", 55, 210, "微软雅黑", 9);
            SetComboBox(135, 208, 210, new List<string> { "800×600",
            "1024×768(推荐)", "1152×864", "1280×600", "1280×720", "1280×768",
            "1280×800", "1280×960", "1280×1024", "1360×768", "1366×768",
            "1400×1050", "1440×900", "1600×900", "1600×1050", "1920×1080"});
        }
        private void SetCheckBoxSetting(int x, int y, int width, string text)//设置候选框
        {
            CheckBox cb = new CheckBox();
            cb.Left = x;
            cb.Top = y;
            cb.Text = text;
            cb.Width = width;
            cb.BackColor = Color.Transparent;
            cb.Tag = text;
            this.Controls.Add(cb);
        }
        public void LoadAttribute(int width, int height, string name, object o)//加载查看属性
        {
            this.FormBorderStyle = FormBorderStyle.None;
            SetLabel(name, 12, 10, "微软雅黑", 10);
            this.Width = width;
            this.Height = height;
            Rectangle rect = Screen.GetWorkingArea(this);
            this.Left = rect.Width / 2 - this.Width / 2;
            this.Top = rect.Height / 2 - this.Height / 2;
            Menu m = (Menu)(o);
            int x = 68;
            int y = 0;
            string temp = m.Type == 1 ? "娱乐游戏" : m.Type == 2 ? "社交聊天" : m.Type == 3 ? "影视音乐" : m.Type == 4 ? "工具软件" : "目录位置";
            string[] s = new string[] { "显示名称", "文件类型", "文件地址", "当前图标", "文件分类", "文件热度", "当前图标" };
            foreach (System.Reflection.PropertyInfo p in m.GetType().GetProperties())
            {
                if (p.Name == "Id") continue;
                SetLabel(s[y] + "：", 35, x, "微软雅黑", 9);
                SetFalseTextBox(104, x - 2, 345, 20, "微软雅黑", 9, p.Name == "Type" ? temp : p.GetValue(m, null).ToString(), true);
                x += 30;
                y++;
            }
            y = 0;
            s = new string[] { "文件大小", "创建日期", "修改日期", "访问日期", "文件名" };
            FileInfo f = new FileInfo(m.Address);
            string[] value = new string[] {
                m.Type == 5 ? "0 字节" : f.Length.ToString() +" 字节",
                f.CreationTime.ToString(),
                f.LastWriteTime.ToString(),
                f.LastAccessTime.ToString(),
                f.Name };
            foreach (string str in s)
            {
                SetLabel(str + "：", 35, x, "微软雅黑", 9);
                SetFalseTextBox(104, x - 2, 345, 20, "微软雅黑", 9, value[y], true);
                y++;
                x += 30;
            }

        }
        private void SetFalseTextBox(int x, int y, int width, int height, string font, int fontsize, string text, Boolean enabled)//禁用的只显示虚假输入框
        {
            TextBox ltb = new TextBox();
            ltb.Left = x;
            ltb.Top = y;
            ltb.Width = width;
            ltb.Height = height;
            ltb.BackColor = Color.White;
            ltb.Font = new Font(font, fontsize);
            ltb.AllowDrop = true;
            ltb.DragEnter += Tb_DragEnter;
            ltb.DragDrop += Tb_DragDrop;
            ltb.Tag = "address";
            ltb.ReadOnly = true;
            ltb.Text = text;
            ltb.Enabled = enabled;
            this.Controls.Add(ltb);
        }
        public void Tb_DragDrop(object sender, DragEventArgs e)//拖入操作
        {

            icoAddress = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            string suname = icoAddress.Substring(icoAddress.LastIndexOf(".") + 1, icoAddress.Length - icoAddress.LastIndexOf(".") - 1);
            if (suname == "lnk")
            {
                WshShell shell = new WshShell();
                IWshShortcut iw = (IWshShortcut)shell.CreateShortcut(icoAddress);
                icoAddress = iw.TargetPath == "" ? icoAddress : iw.TargetPath;
            }
            Boolean fd = System.IO.File.Exists(icoAddress);
            string filename = Path.GetFileName(icoAddress);

            this.Tag = fd && filename.Contains(".") ? filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - filename.LastIndexOf(".") - 1) : "";
            suname = fd ? suname = Path.GetFileNameWithoutExtension(icoAddress) : filename;
            foreach (Control ctrl in this.Controls)
            {
                if ((string)ctrl.Tag == "address")
                {
                    ctrl.Text = icoAddress;
                }
                else if ((string)ctrl.Tag == "name")
                {
                    ctrl.Text = suname;
                }
                else if ((string)ctrl.Tag == "icon")
                {
                    ((PictureBox)ctrl).Image = fd ? Icon.ExtractAssociatedIcon(icoAddress).ToBitmap() : Properties.Resources.Project;
                }
                else if ((string)ctrl.Tag == "type")
                {
                    ((ComboBox)ctrl).SelectedIndex = fd ? 0 : 5;
                    ((ComboBox)ctrl).Enabled = fd ? true : false;
                }
            }
        }
        private void Tb_DragEnter(object sender, DragEventArgs e)//开启拖放
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Link;
            else e.Effect = DragDropEffects.None;
        }

        private void SetLabelButtonOneOK(int x, int y, int width, int height, string name, Boolean hot)//设置自定义label按钮
        {
            Label lb = new Label();
            lb.Left = x;
            lb.Top = y;
            lb.Width = width;
            lb.Height = height;
            lb.Image = Properties.Resources._12;
            lb.BackColor = Color.Transparent;
            lb.Text = name;
            lb.TextAlign = ContentAlignment.MiddleCenter;
            lb.ForeColor = Color.White;
            lb.MouseEnter += Lb_MouseEnter;
            lb.MouseLeave += Lb_MouseLeave;
            lb.MouseDown += Lb_MouseDown;
            lb.MouseUp += Lb_MouseUp;
            lb.MouseClick += Pb_MouseClick4;
            this.Controls.Add(lb);
        }
        private void SetLabelButton(int x, int y, int width, int height, string name, Boolean hot)//设置自定义label按钮
        {
            Label lb = new Label();
            lb.Left = x;
            lb.Top = y;
            lb.Width = width;
            lb.Height = height;
            lb.Image = Properties.Resources._12;
            lb.BackColor = Color.Transparent;
            lb.Text = name;
            lb.TextAlign = ContentAlignment.MiddleCenter;
            lb.ForeColor = Color.White;
            lb.MouseEnter += Lb_MouseEnter;
            lb.MouseLeave += Lb_MouseLeave;
            lb.MouseDown += Lb_MouseDown;
            lb.MouseUp += Lb_MouseUp;
            if (name == "取消")
            {
                if (hot)
                {
                    lb.MouseClick += Pb_MouseClick3;
                }
                else
                {
                    lb.MouseClick += Pb_MouseClick4;
                }
            }
            else if (name == "还原默认")
            {
                lb.MouseClick += Lb_MouseClick4;
            }
            else if (name == "初始化")
            {
                lb.MouseClick += Lb_MouseClick2;
            }
            else if (name == "扫描桌面")
            {
                lb.MouseClick += Lb_MouseClick5;
            }
            else if (name == "应用设置")
            {
                lb.MouseClick += Lb_MouseClick3;
            }
            else
            {
                if (hot)
                {
                    lb.MouseClick += Lb_MouseClick;
                }
                else
                {
                    lb.MouseClick += Lb_MouseClick1;
                }
            }
            this.Controls.Add(lb);
        }
        private List<string> scanMenu(string desktop)
        {
            List<string> ls = new List<string>();
            DirectoryInfo di = new DirectoryInfo(desktop);
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo fi in files)
            {
                string aFile = fi.Name;
                aFile = aFile.Substring(aFile.LastIndexOf(".") + 1, (aFile.Length - aFile.LastIndexOf(".") - 1)); //扩展名
                if (aFile.ToLower() == "lnk")
                {
                    WshShell shell = new WshShell();
                    IWshShortcut iw = (IWshShortcut)shell.CreateShortcut(desktop + "\\" + fi.Name);
                    if ("" == iw.TargetPath) continue;
                    if (!System.IO.File.Exists(iw.TargetPath)) continue;
                    ls.Add(fi.Name);
                }
            }
            return ls;
        }
        private void Lb_MouseClick5(object sender, MouseEventArgs e)//扫描桌面
        {
            if (Sqlite.GetMenuAll() > 0)
            {
                MessageBox.Show(" Error : \r\n\t扫描功能只能用于：初始化后第一次使用！ ", "ForceTer");
                return;
            }
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string publicdesktop = desktop.Substring(0,desktop.LastIndexOf("\\"));
            publicdesktop = publicdesktop.Substring(0, publicdesktop.LastIndexOf("\\")) + "\\Public\\Desktop";

            List<string> ls = scanMenu(desktop);
            List<string> pls = scanMenu(publicdesktop);
            if (MessageBox.Show("共扫描到 < " + (ls.Count+pls.Count) + " > 个有效的快捷方式，是否要将他们加入到系统列表？", "ForceTer 3.0", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                List<Menu> lm = new List<Menu>();
                foreach (string a in ls)
                {
                    Menu m = new Menu();
                    m.Id = Guid.NewGuid().ToString();
                    m.Filetype = "exe";
                    m.Icon = m.Id + ".png";
                    WshShell shell = new WshShell();
                    IWshShortcut iw = (IWshShortcut)shell.CreateShortcut(desktop + "\\" + a);
                    m.Address = iw.TargetPath;
                    m.Name = a.Substring(0, a.LastIndexOf("."));
                    m.Type = 1;
                    m.Hot = 1;
                    lm.Add(m);
                }
                foreach (string a in pls)
                {
                    Menu m = new Menu();
                    m.Id = Guid.NewGuid().ToString();
                    m.Filetype = "exe";
                    m.Icon = m.Id + ".png";
                    WshShell shell = new WshShell();
                    IWshShortcut iw = (IWshShortcut)shell.CreateShortcut(publicdesktop + "\\" + a);
                    m.Address = iw.TargetPath;
                    m.Name = a.Substring(0, a.LastIndexOf("."));
                    m.Type = 1;
                    m.Hot = 1;
                    lm.Add(m);
                }
                foreach (Menu m in lm)
                {
                    Console.WriteLine(m.Address);

                    Files.SaveImageFile(m.Id + ".png", Icon.ExtractAssociatedIcon(m.Address).ToBitmap());
                    Sqlite.AddMenu(m);
                }
                RemoveLoad();
                ft.Show();
                ft.LoadMenu("热门应用", true);
                ft.ResetKey();
                Pb_MouseClick4(sender, e);
            }
        }
        private void Lb_MouseClick4(object sender, MouseEventArgs e)//还原默认
        {
            SetSetting(new Setting(false, true, true, false, false, "1024×768(推荐)", 1024, 768));
            if (names != "ForceTer Setting")
            {
                if (MessageBox.Show("是否将背景图还原为初始图像 ？", "ForceTer 3.0", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Sqlite.SetBackgroundImage("\\localhost");
                    ft.BackgroundImage = Sqlite.GetBackgroundImage();
                    ft.Enabled = true;
                    ft.contextMenuStrip1.Enabled = true;
                    ft.Show();
                    this.Close();
                }
            }
        }
        private void Lb_MouseClick3(object sender, MouseEventArgs e)//应用设置里边的修改内容
        {
            Setting s = new Setting();
            Setting setting = Sqlite.GetSetting();
            foreach (Control ctrl in this.Controls)
            {
                if ((string)ctrl.Tag == "开机启动")
                {
                    Boolean strat = (((CheckBox)ctrl).CheckState == CheckState.Checked);
                    if (setting.Start != strat)
                    {
                        if (!AddStart(strat))
                        {
                            ((CheckBox)ctrl).CheckState = setting.Start?CheckState.Checked:CheckState.Unchecked;
                            MessageBox.Show(" 提醒 : \r\n\t未同意管理员请求，设置失败！ ", "ForceTer");
                            return;
                        }
                    }
                    s.Start = ((CheckBox)ctrl).CheckState == CheckState.Checked ? true : false;
                }
                else if ((string)ctrl.Tag == "前台显示")
                {
                    s.Top = ((CheckBox)ctrl).CheckState == CheckState.Checked ? true : false;
                }
                else if ((string)ctrl.Tag == "退出时提示")
                {
                    s.Exit = ((CheckBox)ctrl).CheckState == CheckState.Checked ? true : false;
                }
                else if ((string)ctrl.Tag == "打开程序后隐藏")
                {
                    s.Openfile = ((CheckBox)ctrl).CheckState == CheckState.Checked ? true : false;
                }
                else if ((string)ctrl.Tag == "打开文件位置后隐藏")
                {
                    s.Openfoder = ((CheckBox)ctrl).CheckState == CheckState.Checked ? true : false;
                }
                else if ((string)ctrl.Tag == "type")
                {
                    s.Desktop = ((ComboBox)ctrl).Text;
                }
            }
            Sqlite.SetSetting(s);
            ft.ReloadForm();
            Pb_MouseClick4(sender, e);
        }
        public Boolean AddStart(Boolean start)//设置开机启动
        {
            MessageBox.Show(" 提醒 : \r\n\t请授予程序操作权限，否则无法操作开机启动项！ ", "ForceTer");
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = "Advauthority.exe";
            p.StartInfo.Arguments = start ? "AddStart" : "DelStart";
            p.Start();
            p.WaitForExit();
            return SelectFile(start);
        }
        public Boolean SelectFile(Boolean start)
        {
            int i = 0;
            while (true)
            {
                if (System.IO.File.Exists("ForceTer.lnk") == start)
                {
                    return true;
                }
                else
                {
                    if (i > 20) return false;
                    Thread.Sleep(100);
                    i++;
                }
            }
        }

        private void Lb_MouseClick2(object sender, MouseEventArgs e)
        {
            if (names == "ForceTer Setting")
            {
                if (MessageBox.Show("\r\n是否将ForceTer还原为初始状态？\r\n此操作会将所有添加的图标，背景图，点击率，等内容删除（不可恢复） ！", "ForceTer 3.0", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (Sqlite.GetSetting().Start) AddStart(false);//设置开机启动的项目
                    Sqlite.SetBackgroundImage("\\localhost");
                    Sqlite.SetSetting(new Setting(false, true, true, false, false, "1024×768(推荐)", 1024, 768));
                    ft.ReloadForm();
                    ft.BackgroundImage = Sqlite.GetBackgroundImage();
                    ft.Enabled = true;
                    ft.contextMenuStrip1.Enabled = true;
                    Sqlite.DelMenuAll();
                    Files.DeleteFileAll();
                    ft.imageList1.Images.Clear();
                    ft.lv.Clear();
                    ft.lv.Items.Clear();
                    ft.Show();
                    this.Close();
                }
                return;
            }

        }
        private void Lb_MouseClick1(object sender, MouseEventArgs e)//点击了确定程序按钮
        {
            foreach (Control ctrl in this.Controls)
            {
                if ((string)ctrl.Tag == "name")
                {
                    string name = ((TextBox)ctrl).Text;
                    if (name.Trim() != "")
                    {
                        Sqlite.RemMenuName(this.Tag.ToString(), name);
                    }
                    Pb_MouseClick4(sender, e);
                    ft.LoadMenu(ft.JudgeType(), true);
                    return;
                }
                if ((string)ctrl.Tag == "icon")
                {
                    if (icoAddress == null) return;
                    Image i = System.IO.File.Exists(icoAddress) ? Icon.ExtractAssociatedIcon(icoAddress).ToBitmap() : Properties.Resources.Project;
                    Files.DeleteFile(this.Tag.ToString() + ".png");
                    Files.SaveImageFile(this.Tag.ToString() + ".png", i);
                    ft.LoadMenu(ft.JudgeType(), true);
                    Pb_MouseClick4(sender, e);
                }
                if ((string)ctrl.Tag == "png")
                {
                    if (icoAddress == null) return;
                    System.IO.File.Copy(icoAddress, "image\\backgroud." + this.Tag.ToString(), true);
                    Sqlite.SetBackgroundImage("backgroud." + this.Tag.ToString());
                    ft.BackgroundImage = Sqlite.GetBackgroundImage();
                    Pb_MouseClick4(sender, e);
                }
            }
        }
        private void Lb_MouseClick(object sender, MouseEventArgs e)//点击了添加程序按钮
        {
            Menu m = new Menu();
            m.Id = Guid.NewGuid().ToString();
            m.Filetype = this.Tag.ToString().ToLower();
            m.Icon = m.Id + ".png";
            foreach (Control ctrl in this.Controls)
            {
                if ((string)ctrl.Tag == "address")
                {
                    if (ctrl.Text.Trim() != "")
                    {
                        m.Address = ctrl.Text;
                    }
                    else
                    {
                        return;
                    }
                }
                else if ((string)ctrl.Tag == "name")
                {
                    if (ctrl.Text.Trim() != "")
                    {
                        m.Name = ctrl.Text;
                    }
                    else
                    {
                        MessageBox.Show(" Error : \r\n\tEnter your project NAME !!!   名称为空！ ", "ForceTer");
                        return;
                    }
                }
                else if ((string)ctrl.Tag == "type")
                {
                    int num = ((ComboBox)ctrl).SelectedIndex;
                    if (num != 0)
                    {
                        m.Type = ((ComboBox)ctrl).SelectedIndex;
                    }
                    else
                    {
                        MessageBox.Show(" Error : \r\n\tTYPE cannot be null !!!   分类选择错误！ ", "ForceTer");
                        return;
                    }
                }
                else if ((string)ctrl.Tag == "hot")
                {
                    m.Hot = ((CheckBox)ctrl).CheckState == CheckState.Checked ? 1 : 0;
                }
            }
            if (Sqlite.SelectMenuName(m.Name))
            {
                MessageBox.Show(" Error : \r\n\tThe NAME already exists !!!   名字已经存在了！ ", "ForceTer");
                return;
            }
            else
            {
                Files.SaveImageFile(m.Id + ".png", System.IO.File.Exists(icoAddress) ? Icon.ExtractAssociatedIcon(icoAddress).ToBitmap() : Properties.Resources.Project);
                Sqlite.AddMenu(m);
                RemoveLoad();
                if (MessageBox.Show(" Success : \r\n\t项目添加成功，点击确认按钮返回 ForceTer 3.0 ! ", "ForceTer", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    ft.Show();
                    this.Close();
                    ft.LoadMenu("热门应用", true);
                    ft.ResetKey();
                }
            }
        }
        private void RemoveLoad()//初始化窗口
        {
            this.Tag = null;
            foreach (Control ctrl in this.Controls)
            {
                if ((string)ctrl.Tag == "address")
                {
                    ctrl.Text = "";
                }
                else if ((string)ctrl.Tag == "name")
                {
                    ctrl.Text = "";
                }
                else if ((string)ctrl.Tag == "type")
                {
                    ((ComboBox)ctrl).SelectedIndex = 0;
                }
                else if ((string)ctrl.Tag == "hot")
                {
                    ((CheckBox)ctrl).CheckState = CheckState.Unchecked;
                }
                else if ((string)ctrl.Tag == "icon")
                {
                    ((PictureBox)ctrl).Image = null;
                }
            }
        }
        private void Lb_MouseUp(object sender, MouseEventArgs e)
        {
            ((Label)sender).Image = Properties.Resources._12;
        }
        private void Lb_MouseDown(object sender, MouseEventArgs e)
        {
            ((Label)sender).Image = Properties.Resources._112;
        }
        private void Lb_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Image = Properties.Resources._12;
        }
        private void Lb_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).Image = Properties.Resources._012;
        }
        private void SetPictureBoxBackgroud(int x, int y, int width, int height, Boolean drop, Image image)//设置修改背景图的图像接收框
        {
            PictureBox pb = new PictureBox();
            pb.Left = x;
            pb.Top = y;
            pb.Width = width;
            pb.Height = height;
            pb.BackColor = Color.White;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.AllowDrop = true;
            pb.BorderStyle = BorderStyle.FixedSingle;
            if (drop)
            {
                pb.Tag = "png";
                pb.DragEnter += Tb_DragEnter;
                pb.DragDrop += Pb_DragDrop1;
            }
            pb.Image = image;

            this.Controls.Add(pb);
        }
        private void Pb_DragDrop1(object sender, DragEventArgs e)//拖放背景的操作
        {
            string photo = "<jpg>,<jpeg>,<png>,<gif>,<bmp>";
            try
            {
                if (System.IO.File.Exists(((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString()))
                {
                    icoAddress = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                    string suname = icoAddress.Substring(icoAddress.LastIndexOf(".") + 1, icoAddress.Length - icoAddress.LastIndexOf(".") - 1);
                    if (photo.Contains("<" + suname + ">"))
                    {
                        foreach (Control ctrl in this.Controls)
                        {
                            if ((string)ctrl.Tag == "png")
                            {
                                ((PictureBox)ctrl).ImageLocation = icoAddress;
                                this.Tag = suname;
                                return;
                            }
                        }
                    }
                }
                MessageBox.Show(" Error : \r\n\t只支持 “" + photo + "” 类型的图像作为背景.！ ", "ForceTer");
            }
            catch
            {
                MessageBox.Show(" Error : \r\n\t文件已损坏，或者被修改.！ ", "ForceTer");
            }
        }
        private void SetPictureBox(int x, int y, int width, int height, Boolean max, Boolean drop, Image image, Boolean back)//设置图像接收框
        {
            PictureBox pb = new PictureBox();
            pb.Left = x;
            pb.Top = y;
            pb.Width = width;
            pb.Height = height;
            pb.BackColor = Color.White;
            pb.SizeMode = max ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.CenterImage;
            pb.AllowDrop = true;
            if (drop)
            {
                pb.Tag = "icon";
                pb.DragEnter += Tb_DragEnter;
                pb.DragDrop += Pb_DragDrop;
            }
            pb.Image = image;
            if (back) pb.BackColor = Color.Transparent;
            this.Controls.Add(pb);
        }
        private void Pb_DragDrop(object sender, DragEventArgs e)//更换显示的两个图标的拖入操作
        {
            icoAddress = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            string suname = icoAddress.Substring(icoAddress.LastIndexOf(".") + 1, icoAddress.Length - icoAddress.LastIndexOf(".") - 1);
            if (suname == "lnk")
            {
                WshShell shell = new WshShell();
                IWshShortcut iw = (IWshShortcut)shell.CreateShortcut(icoAddress);
                icoAddress = iw.TargetPath == "" ? icoAddress : iw.TargetPath;
            }
            Boolean fd = System.IO.File.Exists(icoAddress);
            suname = fd ? Path.GetFileNameWithoutExtension(icoAddress) : Path.GetFileName(icoAddress);
            foreach (Control ctrl in this.Controls)
            {
                if ((string)ctrl.Tag == "icon")
                {
                    ((PictureBox)ctrl).Image = fd ? Icon.ExtractAssociatedIcon(icoAddress).ToBitmap() : Properties.Resources.Project;
                }
            }
        }
        private void SetCheckBox(int x, int y, int width, string text)//设置候选框
        {
            CheckBox cb = new CheckBox();
            cb.Left = x;
            cb.Top = y;
            cb.Text = text;
            cb.Width = width;
            cb.BackColor = Color.Transparent;
            cb.Tag = "hot";
            this.Controls.Add(cb);
        }
        private void SetComboBox(int x, int y, int width, List<String> l)//设置下拉框
        {
            ComboBox cb = new ComboBox();
            cb.Left = x;
            cb.Top = y;
            cb.Width = width;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.FlatStyle = FlatStyle.Flat;
            cb.DataSource = l;
            cb.Tag = "type";
            this.Controls.Add(cb);
        }
        private void SetTextBox(int x, int y, int width, string text)//设置输入框
        {
            TextBox tb = new TextBox();
            tb.Left = x;
            tb.Top = y;
            tb.Width = width;
            tb.Tag = "name";
            tb.Text = text;
            this.Controls.Add(tb);
        }
        private void SetPanel(int x, int y, int width, int height)//设置方框
        {
            Panel pl = new Panel();
            pl.Left = x;
            pl.Top = y;
            pl.Width = width;
            pl.Height = height;
            pl.BackColor = Color.Transparent;
            pl.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pl);
        }
        private void SetLabel(string text, int x, int y, string font, int fontsize)//增加显示文字
        {
            Label title = new Label();
            title.Text = text;
            title.Left = x;
            title.Top = y;
            title.BackColor = Color.Transparent;
            title.Font = new Font(font, fontsize);
            title.AutoSize = true;
            this.Controls.Add(title);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]//拖动无窗体的控件
        public static extern bool ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private void ForceTer_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x0112, 0xF010 + 0x0002, 0);
        }

        public void SetSetting(object obj)//设置设置页面显示的内容
        {
            Setting set = (Setting)obj;
            foreach (Control ctrl in this.Controls)
            {
                if ((string)ctrl.Tag == "开机启动")
                {
                    ((CheckBox)ctrl).CheckState = set.Start ? CheckState.Checked : CheckState.Unchecked;
                }
                else if ((string)ctrl.Tag == "前台显示")
                {
                    ((CheckBox)ctrl).CheckState = set.Top ? CheckState.Checked : CheckState.Unchecked;
                }
                else if ((string)ctrl.Tag == "退出时提示")
                {
                    ((CheckBox)ctrl).CheckState = set.Exit ? CheckState.Checked : CheckState.Unchecked;
                }
                else if ((string)ctrl.Tag == "打开程序后隐藏")
                {
                    ((CheckBox)ctrl).CheckState = set.Openfile ? CheckState.Checked : CheckState.Unchecked;
                }
                else if ((string)ctrl.Tag == "打开文件位置后隐藏")
                {
                    ((CheckBox)ctrl).CheckState = set.Openfoder ? CheckState.Checked : CheckState.Unchecked;
                }
                else if ((string)ctrl.Tag == "type")
                {
                    ((ComboBox)ctrl).SelectedIndex = ((ComboBox)ctrl).Items.IndexOf(set.Desktop);
                }
            }
        }
    }
}
