
namespace ForceTer
{
    class Menu
    {
        private string id;
        private string name;
        private string filetype;
        private string address;
        private string icon;
        private int type;
        private int hot;

        public override string ToString()
        {
            return "'" + id + "','" + name + "','" + filetype + "','" + address + "','" + icon + "'," + type + "," + hot;
        }
        public Menu() { }
        public Menu(string id, string name, string filetype, string address, string icon, int type, int hot)
        {
            this.id = id;
            this.name = name;
            this.filetype = filetype;
            this.address = address;
            this.icon = icon;
            this.type = type;
            this.hot = hot;
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Filetype
        {
            get
            {
                return filetype;
            }

            set
            {
                filetype = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

        public string Icon
        {
            get
            {
                return icon;
            }

            set
            {
                icon = value;
            }
        }

        public int Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public int Hot
        {
            get
            {
                return hot;
            }

            set
            {
                hot = value;
            }
        }
    }
}
