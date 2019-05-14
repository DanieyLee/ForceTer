using System;

namespace ForceTer
{
    class Cmd
    {
        public static string clean = "del /f /s /q  %systemdrive%\\*.tmp" + Environment.NewLine +
                                        "del /f /s /q  %systemdrive%\\*._mp" + Environment.NewLine +
                                        "del /f /s /q  %systemdrive%\\*.log" + Environment.NewLine +
                                        "del /f /s /q  %systemdrive%\\*.gid" + Environment.NewLine +
                                        "del /f /s /q  %systemdrive%\\*.chk" + Environment.NewLine +
                                        "del /f /s /q  %systemdrive%\\*.old" + Environment.NewLine +
                                        "del /f /s /q  %systemdrive%\\recycled\\*.*" + Environment.NewLine +
                                        "del /f /s /q  %windir%\\*.bak" + Environment.NewLine +
                                        "del /f /s /q  %windir%\\prefetch\\*.*" + Environment.NewLine +
                                        "rd /s /q %windir%\\temp & md  %windir%\\temp" + Environment.NewLine +
                                        "del /f /q  %userprofile%\\cookies\\*.*" + Environment.NewLine +
                                        "del /f /q  %userprofile%\\recent\\*.*" + Environment.NewLine +
                                        "del /f /s /q  \"%userprofile%\\Local Settings\\Temporary Internet Files\\*.*\"" + Environment.NewLine +
                                        "del /f /s /q  \"%userprofile%\\Local Settings\\Temp\\*.*\"" + Environment.NewLine +
                                        "del /f /s /q  \"%userprofile%\\recent\\*.*\"";
    }
}
