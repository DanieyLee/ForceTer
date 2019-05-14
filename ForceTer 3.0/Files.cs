using System;
using System.Drawing;
using System.IO;

namespace ForceTer
{
    class Files
    {
        public static Bitmap ReadImageFile(string path)//加载图片
        {
            try
            {
                FileStream fs = File.OpenRead(path); //OpenRead
                int filelength = 0;
                filelength = (int)fs.Length; //获得文件长度 
                Byte[] image = new Byte[filelength]; //建立一个字节数组
                fs.Read(image, 0, filelength); //按字节流读取 
                Image result = Image.FromStream(fs);
                fs.Close();
                Bitmap bit = new Bitmap(result);
                return bit;
            }
            catch
            {
                return null;
            }
        }

        public static void SaveImageFile(string name, Image image)//保存一个image文件到image目录下
        {
            image.Save("image\\" + name, System.Drawing.Imaging.ImageFormat.Png);
        }

        public static void DeleteFile(string name)//删除一个文件
        {
            File.Delete("image\\" + name);
        }

        public static void DeleteFileAll()//删除所有的文件
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo("image\\");
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch
            {
                return;
            }
        }

    }
}