using System;

namespace ForceTer
{
    class Setting
    {
        private Boolean start;
        private Boolean top;
        private Boolean exit;
        private Boolean openfile;
        private Boolean openfoder;
        private string desktop;
        private int width;
        private int height;
        public Setting()
        {
        }
        public override string ToString()
        {
            return "(7, 7, '设置', '" + (start ? "1" : "0") +
                "'),(8, 8, '设置', '" + (top ? "1" : "0") +
                "'),(9, 9, '设置', '" + (exit ? "1" : "0") +
                "'),(10, 10, '设置', '" + (openfile ? "1" : "0") +
                "'),(11, 11, '设置', '" + (openfoder ? "1" : "0") +
                "'),(12, 12, '设置', '" + (desktop) + "')";
        }
        public Setting(bool start, bool top, bool exit, bool openfile, bool openfoder, string desktop, int width, int height)
        {
            this.start = start;
            this.top = top;
            this.exit = exit;
            this.openfile = openfile;
            this.openfoder = openfoder;
            this.desktop = desktop;
            this.width = width;
            this.height = height;
        }
        public bool Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
            }
        }
        public bool Top
        {
            get
            {
                return top;
            }

            set
            {
                top = value;
            }
        }
        public bool Exit
        {
            get
            {
                return exit;
            }

            set
            {
                exit = value;
            }
        }
        public bool Openfile
        {
            get
            {
                return openfile;
            }

            set
            {
                openfile = value;
            }
        }
        public bool Openfoder
        {
            get
            {
                return openfoder;
            }

            set
            {
                openfoder = value;
            }
        }
        public string Desktop
        {
            get
            {
                return desktop;
            }

            set
            {
                desktop = value;
            }
        }
        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }
        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }
    }
}