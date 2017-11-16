﻿using System;
using System.IO;
using System.Linq;
using Accord.MachineLearning;

namespace MachineLearning.PlayGround
{
    class Program
    {
        static void Main(string[] args)
        {
            var rawData = File.ReadAllLines("myData.csv");

            var data = rawData.Skip(1).Select(row => row.Split(',')).Select(row => new Row
            {
                Gender = Enum.Parse<Gender>(row[0]),
                Height = int.Parse(row[1]),
                Weight = int.Parse(row[2])
            }).ToArray();

            Console.WriteLine("Данные:");
            foreach (var row in data)
            {
                Console.WriteLine(row);
            }

            var train = data.Take(10).ToArray();
            var test = data.Skip(10).Take(5).ToArray();

            var classifier = new KNearestNeighbors(2);
            var values = train.Select(a => new double[] {a.Weight, a.Height}).ToArray();
            var cases = train.Select(a => (int)a.Gender).ToArray();
            classifier.Learn(values, cases);
            

            Console.WriteLine("Тест:");
            foreach (var row in test)
            {
                Console.WriteLine(row);
                var tr = new double[] {row.Weight, row.Height};

                var decision = classifier.Decide(tr);
                Console.WriteLine($"Ответ классификатора: {(Gender)decision}");
            }

            Console.ReadKey();
        }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

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