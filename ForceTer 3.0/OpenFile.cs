using System;

namespace ForceTer
{
    class OpenFile
    {
        private static string file = "<exe>,<rar>,<bat>,<cmd>,<zip>,<>,<txt>,<ini>,<log>,<xml>,<config>,<inf>," +
            "<jpg>,<jpeg>,<png>,<gif>,<bmp>,<ico>,<mp4>,<avi>,<rmvb>,<rm>,<3gp>,<mov>,<flv>,<swf>,<mp3>,<wma>," +
            "<m4a>,<flac>,<ape>,<wav>,<doc>,<docx>,<xls>,<ppt>,<xlsx>,<pptx>,<pdf>";
        public static Boolean OpenMyFile(string id, string address, string filetype)//打开文件的操作
        {
            try
            {
                Boolean fd = System.IO.File.Exists(address);
                if (fd)//是文件的操作
                {
                    if (file.Contains("<" + filetype + ">"))
                    {
                        System.Diagnostics.Process.Start(address);
                        Sqlite.RemMenuHotPlus(id);
                    }
                    else
                    {
                        return true;
                    }
                }
                else//不是文件的操作
                {
                    System.Diagnostics.Process.Start(address);
                    Sqlite.RemMenuHotPlus(id);
                }
                return false;
            }
            catch
            {
                return true;
            }
        }
        public static Boolean OpenMyFileAddress(string address)//打开文件位置的操作
        {
            try
            {
                address = address.Substring(0, address.LastIndexOf("\\") + 1);
                System.Diagnostics.Process.Start(address);
                return false;
            }
            catch
            {
                return true;
            }
        }
    }
}