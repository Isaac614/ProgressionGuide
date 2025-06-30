using Terraria.GameContent.UI;

namespace ProgressionGuide
{
    public class ItemData
    {
        private int _id;
        private string _name;
        private string _rarity;

        public int Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Rarity
        {
            get { return _rarity; }
        }

        public ItemData(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public ItemData(int id, string name, string rarity)
        {
            _id = id;
            _name = name;
            _rarity = rarity;
        }
    }
}