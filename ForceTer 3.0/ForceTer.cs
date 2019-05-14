using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace ForceTer
{
    public partial class ForceTer : Form
    {
        public ListView lv = new ListView();//窗口lv
        private TextBox selecttb = new TextBox();//搜索框的tb
        private PictureBox selectpb = new PictureBox();//搜索框的tb
        private Setting setting = null;//设置的类

        public ForceTer()
        {
            InitializeComponent();
            this.DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
        }
        private void ForceTer_Load(object sender, EventArgs e)
        {
            LoadFile();
        }
        private void LoadSelect()//加载搜索框
        {
            selectpb.Width = 200;
            selectpb.Height = 25;
            selectpb.Left = setting.Width - 224;
            selectpb.Top = 75;
            selectpb.Image = Properties.Resources.select;
            selectpb.BackColor = Color.Transparent;
            selecttb.Width = 160;
            selecttb.Height = 20;
            selecttb.Left = setting.Width - 216;
            selecttb.Top = 79;
            selecttb.Text = "输入名称搜索";
            selecttb.Font = new Font("微软雅黑", 9);
            selecttb.ForeColor = Color.DarkGray;
            selecttb.BorderStyle = BorderStyle.None;
            selecttb.GotFocus += Tb_GotFocus;
            selecttb.LostFocus += Tb_LostFocus;
            selecttb.KeyUp += Tb_KeyUp;
            this.Controls.Add(selecttb);
            this.Controls.Add(selectpb);
        }
        private void Tb_KeyUp(object sender, KeyEventArgs e)//搜索功能
        {
            TextBox tb = (TextBox)(sender);
            if (tb.Text == "") return;
            string str = ((TextBox)(sender)).Text;
            LoadMenu(str, false);
        }
        private void Tb_LostFocus(object sender, EventArgs e)//焦点离开搜索框
        {
            selecttb.Text = "输入名称搜索";
            selecttb.ForeColor = Color.DarkGray;
            selecttb.Top = 79;
        }
        private void Tb_GotFocus(object sender, EventArgs e)//焦点进入了搜索框
        {
            selecttb.Text = "";
            selecttb.ForeColor = Color.Black;
            selecttb.Top = 80;
        }
        private void LoadFile()//判断这个文件夹是否存在，不存在则创建
        {
            try
            {
                if (!File.Exists("SQLite.Interop.dll") || !File.Exists("System.Data.SQLite.dll") || !File.Exists("databases.db") || !File.Exists("Advauthority.exe"))
                {
                    MessageBox.Show(" Error : \r\n\t文件丢失，请重新下载！ ", "ForceTer");
                    exit();
                    return;
                }
                else
                {
                    justFileAuth();
                    if (!Directory.Exists("image")) Directory.CreateDirectory("image");
                    setting = Sqlite.GetSetting();//加载设置信息
                    LoadForm();
                    this.Show();
                    this.Activate();
                    SetTitle(this.Name);
                    LoadListView();
                    SetNotify();
                    LoadImages();
                    LoadMenu("热门应用", true);
                    LoadSelect();
                }
            }
            catch
            {
                MessageBox.Show(" Error : \r\n\t没有权限，或文件损坏，请重新下载，或给予操作权限！ ", "ForceTer");
                exit();
                return;
            }
        }
        private void exit()
        {
            this.Dispose();
            this.Close();
            Environment.Exit(0);
        }
        private void justFileAuth()//鉴权操作
        {
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);
            AuthorizationRuleCollection accessRules = dir.GetAccessControl().GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            string temp = "";
            foreach (FileSystemAccessRule rule in accessRules)
            {
                temp += (rule.IdentityReference.Translate(typeof(NTAccount))).ToString() + rule.FileSystemRights + rule.AccessControlType;
            }
            if (temp.Contains("EveryoneFullControlAllow") && temp.Contains("BUILTIN\\UsersFullControlAllow")) return;
            MessageBox.Show(" 提醒 : \r\n\t请授予程序操作权限，否则第一次可能无法正常运行！ ", "ForceTer");
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = "Advauthority.exe";
            p.StartInfo.Arguments = "Authority";
            p.Start();
            exit();
        }
        private void LoadImages()//加载各种按钮
        {
            LoadImage(Properties.Resources._1, Properties.Resources._01, 30, 48, 55, 55, "热门应用");
            LoadImage(Properties.Resources._2, Properties.Resources._02, 100, 48, 55, 55, "娱乐游戏");
            LoadImage(Properties.Resources._3, Properties.Resources._03, 170, 48, 55, 55, "社交聊天");
            LoadImage(Properties.Resources._4, Properties.Resources._04, 240, 48, 55, 55, "影视音乐");
            LoadImage(Properties.Resources._5, Properties.Resources._05, 310, 48, 55, 55, "工具软件");
            LoadImage(Properties.Resources._6, Properties.Resources._06, 380, 48, 55, 55, "目录位置");
            LoadImage(Properties.Resources._7, Properties.Resources._07, 450, 48, 55, 55, "优化清理");
            LoadImage(Properties.Resources._8, this.Width - 45 - 4 - 35 - 35 - 35, 0, 35, 22, "设置");
            LoadImage(Properties.Resources._9, this.Width - 45 - 4 - 35 - 35, 0, 35, 22, "更换皮肤");
            LoadImage(Properties.Resources._10, this.Width - 45 - 4 - 35, 0, 35, 22, "隐藏到托盘");
            LoadImage(Properties.Resources._11, this.Width - 45 - 4, 0, 45, 22, "关闭");
        }
        public void LoadImage(Image image, int left, int top, int width, int height, string name)//装载右上角的按钮
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
            pb.Tag = name;
            if (name == "关闭")
            {
                pb.MouseEnter += Pb_MouseEnter4;
            }
            else
            {
                pb.MouseEnter += Pb_MouseEnter3;
            }
            pb.MouseLeave += Pb_MouseLeave2;
            switch (name)
            {
                case "设置":
                    pb.MouseClick += Pb_MouseClick1;
                    break;
                case "更换皮肤":
                    pb.MouseClick += Pb_MouseClick2;
                    break;
                case "隐藏到托盘":
                    pb.MouseClick += Pb_MouseClick3;
                    break;
                default:
                    pb.MouseClick += Pb_MouseClick4;
                    break;
            }
            this.Controls.Add(pb);
        }
        private void Pb_MouseClick1(object sender, MouseEventArgs e)//鼠标点击设置按钮后弹出菜单
        {
            this.contextMenuStrip4.Show((PictureBox)sender, new Point(e.X, e.Y));
        }
        private void Pb_MouseClick2(object sender, MouseEventArgs e)//鼠标点击了更换皮肤按钮
        {
            rebackgroud();
        }
        private void Pb_MouseClick3(object sender, MouseEventArgs e)//鼠标点击了隐藏到托盘
        {
            this.Hide();
        }
        private void Pb_MouseClick4(object sender, MouseEventArgs e)//鼠标点击了最后的关闭按钮
        {
            this.Dispose();
            this.Close();
        }
        public void LoadImage(Image image, Image image2, int left, int top, int width, int height, string name)//装载最上边圆形的一条图标
        {
            PictureBox pb = new PictureBox();
            pb.BackgroundImage = image;
            if (name == "热门应用") pb.Image = image;
            pb.Name = name;
            pb.Tag = image2;
            pb.Left = left;
            pb.Top = top;
            pb.Width = width;
            pb.Height = height;
            pb.BackColor = Color.Transparent;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            ToolTip tt = new ToolTip();
            tt.SetToolTip(pb, name);
            pb.MouseEnter += Pb_MouseEnter;
            pb.MouseLeave += Pb_MouseLeave;
            pb.MouseClick += Pb_MouseClick;
            this.Controls.Add(pb);
        }
        public void ResetKey()//重置当前显示的是第一个是热门应用
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is PictureBox)
                {
                    if (((PictureBox)ctrl).Width == 55)
                    {
                        if (((PictureBox)ctrl).Name == "热门应用")
                        {
                            ((PictureBox)ctrl).Image = ((PictureBox)ctrl).BackgroundImage;
                        }
                        else
                        {
                            ((PictureBox)ctrl).Image = null;
                        }
                    }
                }
            }
        }
        private void Pb_MouseClick(object sender, MouseEventArgs e)//鼠标点击圆形的分类图标
        {
            PictureBox pb = (PictureBox)sender;
            if (pb.Image == null)
            {
                pb.Image = pb.BackgroundImage;
                pb.BackgroundImage = (Image)pb.Tag;
                pb.Tag = pb.Image;
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is PictureBox)
                    {
                        if (((PictureBox)ctrl).Width == 55)
                        {
                            ((PictureBox)ctrl).Image = null;
                        }
                    }
                }
                pb.Image = pb.BackgroundImage;
                LoadMenu(((PictureBox)sender).Name, true);
                selecttb.Text = "输入名称搜索";
                selecttb.ForeColor = Color.DarkGray;
                selecttb.Top = 79;
                lv.Focus();
            }
        }
        private void Pb_MouseLeave(object sender, EventArgs e)//鼠标离开了这个圆形的分类图标
        {
            PictureBox pb = (PictureBox)sender;
            if (pb.Image == null)
            {
                pb.Image = pb.BackgroundImage;
                pb.BackgroundImage = (Image)pb.Tag;
                pb.Tag = null;
                pb.Tag = pb.Image;
                pb.Image = null;
            }
        }
        private void Pb_MouseEnter(object sender, EventArgs e)//鼠标进入了这个圆形的分类图标
        {
            PictureBox pb = (PictureBox)sender;
            if (pb.Image == null)
            {
                pb.Image = pb.BackgroundImage;
                pb.BackgroundImage = (Image)pb.Tag;
                pb.Tag = pb.Image;
                pb.Image = null;
            }
        }
        private void Pb_MouseLeave2(object sender, EventArgs e)//鼠标离开了右上角的最小化，菜单，更换皮肤，关闭，等图标
        {
            PictureBox pb = (PictureBox)sender;
            pb.BackColor = Color.FromArgb(0, 0, 0, 0);
        }
        private void Pb_MouseEnter3(object sender, EventArgs e)//鼠标进入了右上角除去（关闭）的其他图标。
        {
            PictureBox pb = (PictureBox)sender;
            pb.BackColor = Color.FromArgb(120, 230, 230, 230);
        }
        private void Pb_MouseEnter4(object sender, EventArgs e)//鼠标进入的是（右上角的关闭）图标
        {
            PictureBox pb = (PictureBox)sender;
            pb.BackColor = Color.FromArgb(180, 230, 10, 10);
        }
        public void LoadMenu(string name, Boolean type)//加载更新的菜单文件
        {
            lv.BeginUpdate();//数据更新锁
            if (name != "优化清理")
            {
                List<Menu> lm = type ? Sqlite.GetMenu(name) : Sqlite.FindByName(name);
                this.imageList1.ImageSize = new Size(40, 40);//出现的图标比较少的时候没有问题  
                this.imageList1.Images.Clear();
                lv.Clear();
                lv.Items.Clear();
                if (lm.Count > 84) this.imageList1.ImageSize = new Size(38, 38);
                int i = 0;
                foreach (Menu m in lm)
                {
                    this.imageList1.Images.Add(Files.ReadImageFile("image\\" + m.Icon));
                    ListViewItem lvi = new ListViewItem();
                    lvi.ImageIndex = i;
                    lvi.Text = m.Name;
                    lvi.Tag = m;
                    i++;
                    lv.Items.Add(lvi);
                }
                lv.ContextMenuStrip = this.contextMenuStrip3;
                this.contextMenuStrip2.Tag = null;
            }
            else//如果点击的是（优化清理）这个选项
            {
                this.imageList1.Images.Clear();
                lv.Clear();
                lv.Items.Clear();
                lv.ContextMenuStrip = null;
                this.imageList1.ImageSize = new Size(200, 200);
                this.imageList1.Images.Add(Properties.Resources.clean);
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = 0;
                lvi.Text = "清理系统垃圾文件";
                lvi.Tag = "clean";
                lv.Items.Add(lvi);
            }
            lv.EndUpdate();//数据更新完毕加载
        }
        private void LoadForm()//设置窗口首次启动的操作
        {
            this.Name = this.Name + " 3.0";
            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = setting.Width;
            this.Height = setting.Height;
            Rectangle rect = Screen.GetWorkingArea(this);
            this.Left = rect.Width / 2 - this.Width / 2;
            this.Top = rect.Height / 2 - this.Height / 2;
            this.BackgroundImage = Sqlite.GetBackgroundImage();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Icon = Properties.Resources.ForceTer;
            this.TopMost = setting.Top ? true : false;
        }
        public void ReloadForm()//重置窗口大小
        {
            setting = Sqlite.GetSetting();
            this.Width = setting.Width;
            this.Height = setting.Height;
            Rectangle rect = Screen.GetWorkingArea(this);
            this.Left = rect.Width / 2 - this.Width / 2;
            this.Top = rect.Height / 2 - this.Height / 2;
            lv.Width = this.Width - 16;
            lv.Height = this.Height - 128;
            selectpb.Left = setting.Width - 224;
            selecttb.Left = setting.Width - 216;
            this.TopMost = setting.Top ? true : false;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is PictureBox)
                {
                    if (ctrl.Width == 35 || ctrl.Width == 45)
                    {
                        switch ((string)ctrl.Tag)
                        {
                            case "设置":
                                ctrl.Left = this.Width - 45 - 4 - 35 - 35 - 35;
                                break;
                            case "更换皮肤":
                                ctrl.Left = this.Width - 45 - 4 - 35 - 35;
                                break;
                            case "隐藏到托盘":
                                ctrl.Left = this.Width - 45 - 4 - 35;
                                break;
                            case "关闭":
                                ctrl.Left = this.Width - 45 - 4;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        private void SetTitle(string name)//设置标题名称
        {
            Label title = new Label();
            title.Text = name;
            title.Left = 12;
            title.Top = 10;
            title.BackColor = Color.Transparent;
            title.Font = new Font("微软雅黑", 10);
            title.AutoSize = true;
            this.Controls.Add(title);
        }
        private void LoadListView()//设置项目加载框listview
        {
            this.imageList1.ColorDepth = ColorDepth.Depth32Bit;
            lv.LargeImageList = this.imageList1;
            lv.Left = 8;
            lv.Top = 120;
            lv.Width = this.Width - 16;
            lv.Height = this.Height - 128;
            lv.View = View.LargeIcon;//设置显示样式为图标
            lv.MultiSelect = false;
            lv.ContextMenuStrip = this.contextMenuStrip2;
            lv.SelectedIndexChanged += Lv_SelectedIndexChanged;
            lv.MouseDoubleClick += Lv_MouseDoubleClick;
            lv.MouseClick += Lv_MouseClick;
            lv.AllowDrop = true;
            lv.DragEnter += Lv_DragEnter;
            lv.DragDrop += Lv_DragDrop;
            this.Controls.Add(lv);
        }
        private void Lv_DragDrop(object sender, DragEventArgs e)//主页面的拖入操作完成后，进行的操作
        {
            Popup p = new Popup(this);
            p.LoadAddForm(480, 300);
            p.LoadButton(true, true, true, "添加", "取消", true);
            p.Show();
            p.Activate();
            this.Hide();
            p.Tb_DragDrop(sender, e);
        }
        private void Lv_MouseDoubleClick(object sender, MouseEventArgs e)//鼠标双击了listview里的某一个项目
        {
            if (((ListView)sender).SelectedItems[0].Tag.Equals("clean"))
            {
                if (!File.Exists("util\\clean.exe"))
                {
                    MessageBox.Show(" Error : \r\n\tClean文件丢失，请重新下载！ ", "ForceTer");
                }
                else
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.FileName = "util\\clean.exe";
                    p.StartInfo.Arguments = Cmd.clean;
                    p.Start();
                }
                return;
            }
            this.contextMenuStrip2.Tag = ((ListView)sender).SelectedItems[0].Tag;
            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            if (OpenFile.OpenMyFile(m.Id, m.Address, m.Filetype))
            {
                MessageBox.Show(" Error : \r\n\t文件已损坏，或<" + m.Filetype + ">格式不支持！ ", "ForceTer");
                return;
            }
            if (setting.Openfile)
            {
                this.Hide();
            }
            else
            {
                this.Dispose();
                this.Close();
            }
        }
        private void Lv_DragEnter(object sender, DragEventArgs e)//主页面的图标拖入操作
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Link;
            else e.Effect = DragDropEffects.None;
        }
        private void Lv_MouseClick(object sender, MouseEventArgs e)//装载鼠标右键
        {
            if (e.Button == MouseButtons.Right && lv.ContextMenuStrip != null)
            {
                ((ListView)sender).ContextMenuStrip = this.contextMenuStrip2;
                this.contextMenuStrip2.Tag = ((ListView)sender).SelectedItems[0].Tag;
            }
        }
        private void Lv_SelectedIndexChanged(object sender, EventArgs e)//响应点击位置
        {
            if (((ListView)sender).SelectedItems.Count == 0 && lv.ContextMenuStrip != null)
            {
                ((ListView)sender).ContextMenuStrip = this.contextMenuStrip3;
                this.contextMenuStrip2.Tag = null;
                return;
            }
        }
        private void SetNotify()//设置托盘
        {
            NotifyIcon ni = this.notifyIcon1;//实例化
            ni.Text = this.Name;//鼠标放在托盘时显示的文字
            ni.Icon = this.Icon;
            ni.ContextMenuStrip = this.contextMenuStrip1;
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]//拖动无窗体的控件
        public static extern bool ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private void ForceTer_MouseDown(object sender, MouseEventArgs e)//按下拖动
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x0112, 0xF010 + 0x0002, 0);
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)//点击托盘的退出按钮
        {
            if (setting.Exit)
            {
                this.Hide();
                if (MessageBox.Show("是否确认退出 ForceTer 3.0 ？", "ForceTer 3.0", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    this.Dispose();
                    this.Close();
                }else
                {
                    this.Show();
                }
            }
            else
            {
                this.Dispose();
                this.Close();
            }
        }
        private void 打开主界面ToolStripMenuItem_Click(object sender, EventArgs e)//点击托盘的打开主页面按钮
        {
            Form f = Application.OpenForms["Popup"];
            if (f != null) f.Close();
            LoadMenu(JudgeType(), true);
            this.Show();
            this.Activate();
        }
        private void 最近使用ToolStripMenuItem_Click(object sender, EventArgs e)//点击托盘的打开最近使用按钮
        {
            Form f = Application.OpenForms["Popup"];
            if (f != null) f.Close();
            LoadMenu("热门应用", true);
            ResetKey();
            this.Show();
            this.Activate();
        }
        private void 添加图标ToolStripMenuItem1_Click(object sender, EventArgs e)//点击托盘的添加图标按钮
        {
            Form f = Application.OpenForms["Popup"];
            if (f == null)
            {
                Popup p = new Popup(this);
                p.LoadAddForm(480, 300);
                p.LoadButton(true, true, true, "添加", "取消", true);
                p.Show();
                p.Activate();
                this.Hide();
            }
        }
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)//点击图标上右键菜单的打开按钮
        {
            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            if (OpenFile.OpenMyFile(m.Id, m.Address, m.Filetype))
            {
                MessageBox.Show(" Error : \r\n\t文件已损坏，或<" + m.Filetype + ">格式不支持！ ", "ForceTer");
                return;
            }
            if (setting.Openfile)
            {
                this.Hide();
            }
            else
            {
                this.Dispose();
                this.Close();
            }
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)//双击了托盘图标
        {
            Form f = Application.OpenForms["Popup"];
            if (f != null && f.Height == 300) f.Close();
            if (this.Enabled == false) return;
            LoadMenu(JudgeType(), true);
            this.Show();
            this.Activate();
        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)//点击了删除的按钮之后
        {
            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            if (MessageBox.Show("是否确认删除 <" + m.Name + "> ？", "ForceTer 3.0", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Files.DeleteFile(m.Icon);
                Sqlite.DelMenu(m.Id);
                LoadMenu(JudgeType(), true);
            }
        }
        public string JudgeType()//判断选中的项目是什么
        {
            PictureBox pb;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is PictureBox)
                {
                    pb = ((PictureBox)ctrl);
                    if (pb.Height == 55 && pb.Image != null)
                    {
                        return pb.Name;
                    }
                }
            }
            return "";
        }
        private void 打开文件位置ToolStripMenuItem_Click(object sender, EventArgs e)//打开文件所在的位置
        {
            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            if (OpenFile.OpenMyFileAddress(m.Address))
            {
                MessageBox.Show(" Error : \r\n\tFailed to locate !!!   获取位置失败！ ", "ForceTer");
            }
            else
            {
                if (setting.Openfoder)
                {
                    this.Hide();
                }
                else
                {
                    this.Dispose();
                    this.Close();
                }
            }
        }
        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)//重命名操作
        {
            this.Enabled = false;
            this.contextMenuStrip1.Enabled = false;
            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            Popup p = new Popup(this);
            p.LoadPopup(480, 200, "ForceTer rename", m.Name, m.Id);
            p.LoadButton(true, true, true, "确定", "取消", false);
            p.Show();
        }
        private void 更换图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.contextMenuStrip1.Enabled = false;
            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            Popup p = new Popup(this);
            p.LoadIcon(450, 250, "ForceTer remove ico", m.Name, m.Id);
            p.LoadButton(true, true, true, "确定", "取消", false);
            p.Show();
        }
        private void 娱乐游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            Sqlite.RemMenuType(1, m.Id);
            LoadMenu(JudgeType(), true);
        }
        private void 社交聊天ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            Sqlite.RemMenuType(2, m.Id);
            LoadMenu(JudgeType(), true);
        }
        private void 影视音乐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            Sqlite.RemMenuType(3, m.Id);
            LoadMenu(JudgeType(), true);
        }
        private void 工具软件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            Sqlite.RemMenuType(4, m.Id);
            LoadMenu(JudgeType(), true);
        }
        private void 目录地址ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            Sqlite.RemMenuType(5, m.Id);
            LoadMenu(JudgeType(), true);
        }
        private void 重置点击率ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            Sqlite.RemMenuHot(1, m.Id);
            LoadMenu(JudgeType(), true);
        }
        private void 移除最近使用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            Sqlite.RemMenuHot(0, m.Id);
            LoadMenu(JudgeType(), true);
        }
        private void 更换皮肤ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rebackgroud();
        }
        private void rebackgroud()//更换皮肤方法
        {
            this.Enabled = false;
            this.contextMenuStrip1.Enabled = false;
            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            Popup p = new Popup(this);
            p.LoadBackgroud(640, 340, "ForceTer rebackgroud");
            p.LoadButtonMany("还原默认", "确定", "取消", false);
            p.Show();
        }
        private void 属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.contextMenuStrip1.Enabled = false;
            Popup p = new Popup(this);
            p.LoadAttribute(500, 450, "ForceTer", this.contextMenuStrip2.Tag);
            p.LoadButtonAttribute("确定", false);
            p.Show();
        }
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Error : \r\n\t暂无帮助页面！ ", "ForceTer");
        }
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Success : \r\n\t图标文件管理系统.当前版本: 3.0 ！ ", "ForceTer");
        }
        private void 检测更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Error : \r\n\t没有更新服务器！ ", "ForceTer");
        }
        private void 系统设置ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.contextMenuStrip1.Enabled = false;
            Menu m = (Menu)(this.contextMenuStrip2.Tag);
            Popup p = new Popup(this);
            p.LoadSetting(420, 300, "ForceTer Setting");
            p.LoadButtonManySetting("初始化", "扫描桌面", "还原默认", "应用设置", "取消", false);
            p.Show();
            p.SetSetting(Sqlite.GetSetting());
        }
    }
}
