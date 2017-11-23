using System;
using System.IO;
using System.Linq;
using Accord.IO;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;

namespace MachineLearning.PlayGround.Samples
{
    public class DecisionTreeSample //TODO Плохой пример
    {
        public static void Start()
        {
            var rawData = File.ReadAllLines("myData.csv");

            var data = rawData.Skip(1).Select(row => row.Split(',')).Select(row => new Row
            {
                Gender = Enum.Parse<Gender>(row[0]),
                Height = double.Parse(row[1])/1000,
                Weight = double.Parse(row[2])/100
            }).ToArray();

            Console.WriteLine("Данные:");
            foreach (var row in data)
            {
                Console.WriteLine(row);
            }

            var train = data.Take(12).ToArray();
            var test = data.Skip(12).Take(6).ToArray();

            var values = train.Select(a => new double[] { a.Height, a.Weight }).ToArray();
            var cases = train.Select(a => (int)a.Gender).ToArray();

            DecisionVariable[] variables =
            {
                new DecisionVariable("Height", DecisionVariableKind.Discrete),
                new DecisionVariable("Weight", DecisionVariableKind.Discrete)
            };

            var c45 = new C45Learning(variables);
            var tree = c45.Learn(values, cases);

            Console.WriteLine("\nТест:");
            foreach (var row in test)
            {
                Console.WriteLine(row);
                var tr = new double[] { row.Height, row.Weight };

                var decision = tree.Decide(tr);
                Console.WriteLine($"Ответ классификатора: {(Gender)decision}");
            }

            var bytes = tree.Save();
            using (var stream = File.OpenWrite("myTree"))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
