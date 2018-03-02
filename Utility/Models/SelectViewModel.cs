namespace Utility.Models
{
    public class SelectViewModel
    {
        public SelectViewModel(int value, string type)
        {
            Value = value;
            Type = type;
        }

        public int Value { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return Type;
        }
    }
}
