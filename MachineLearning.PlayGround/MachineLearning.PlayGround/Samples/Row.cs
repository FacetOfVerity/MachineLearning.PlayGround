namespace MachineLearning.PlayGround.Samples
{
    public class Row
    {
        public Gender Gender { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public override string ToString()
        {
            return $"{Gender}; Рост: {Height}; Вес: {Weight};";
        }
    }
}