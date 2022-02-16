using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;

namespace algo1
{

    class Program
    {
        class BinaryVector
        {
            public byte[] bytes;

            public BinaryVector(string binaryNum)
            {
                List<byte> res = new List<byte>();
                for (int i = 0; i < binaryNum.Length; i++)
                {
                    res.Add(byte.Parse(binaryNum[i].ToString()));
                }
                bytes = res.ToArray();
            }

            public void Print()
            {
                Console.WriteLine(this.ToString());
            }

            public override string ToString()
            {
                string s = "";
                for (int i = 0; i < bytes.Length; i++)
                {
                    s += bytes[i].ToString();
                }
                return s;
            }

        }

        class GreysCode
        {
            public List<BinaryVector> sequence;

            public GreysCode(int n)
            {
                sequence = new List<BinaryVector>();
                GenerateSequence(n);
            }

            void GenerateSequence(int n)
            {
                if (n == 1)
                {
                    sequence.Add(new BinaryVector("0"));
                    sequence.Add(new BinaryVector("1"));
                   
                }
                else/* if (sequence.Count < Math.Pow(2, n))*/
                {
                    GenerateSequence(n - 1);
                    List<BinaryVector> subsequence = new List<BinaryVector>(sequence);

                    AddZeroes(sequence);

                    subsequence.Reverse();
                    AddOnes(subsequence);

                    this.Add(subsequence);
                   
                }

            }

            void AddZeroes(List<BinaryVector> seq)
            {
                for (int i = 0; i < sequence.Count; i++)
                {
                    seq[i] = new BinaryVector(seq[i].ToString() + '0');
                }
            }

            void AddOnes(List<BinaryVector> seq)
            {
                for (int i = 0; i < sequence.Count; i++)
                {
                    seq[i] = new BinaryVector(seq[i].ToString() + '1');
                }
            }

            void Add(List<BinaryVector> subsequence)
            {
                for (int i = 0; i < subsequence.Count; i++)
                {
                    sequence.Add(subsequence[i]);
                }
            }

           

            public void Print()
            {
                for (int i = 0; i < sequence.Count; i++)
                {
                    sequence[i].Print();
                }
            }
        }

        class BasePlacementProblem
        {
            byte[,] A;
            int[] c;
            List<BinaryVector> solutions;

            public BasePlacementProblem(string filepath)
            {
                ReadFile(filepath);
                SolveProblem();
            }

            void ReadFile(string filepath)
            {
                string[] f = File.ReadAllLines(filepath);
                int n = f.Length - 1;
                A = new byte[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        A[i, j] = byte.Parse(f[i][j].ToString());
                    }
                }

                c = new int[n];
                string[] cs = f[n].Split(' ');
                for (int i = 0; i < n; i++)
                {
                    c[i] = int.Parse(cs[i]);
                }
            }

            public void PrintProblem()
            {
                int n = A.GetLength(0);
                Console.WriteLine("Матрица А: ");
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(A[i, j] + " ");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("Вектор с: ");
                for (int j = 0; j < n; j++)
                {
                    Console.Write(c[j] + " ");
                }
                Console.WriteLine();

                PrintSolutions();

                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }

            class PossibleSolution
            {
                int[] c;
                public BinaryVector x;
                public int sumc { get => CalcSum(); }

                public PossibleSolution(int[] c, BinaryVector x)
                {
                    this.c = c;
                    this.x = x;
                }

                int CalcSum()
                {
                    int res = 0;
                    for (int i = 0; i < x.bytes.Length; i++)
                    {
                        res += x.bytes[i] * c[i];
                    }
                    return res;
                }
            }
            void SolveProblem()
            {
                List<PossibleSolution> possibleSolutions = new List<PossibleSolution>();

                int n = A.GetLength(0);
                GreysCode gc = new GreysCode(n);

                for (int k = 0; k < gc.sequence.Count; k++)
                {
                    bool ok = true;
                    for (int i = 0; i < n; i++)
                    {
                        int sum = 0;
                        for (int j = 0; j < n; j++)
                        {
                            sum += A[i, j] * gc.sequence[k].bytes[j];
                        }
                        if (sum < 1)
                        {
                            ok = false;
                            break;
                        }
                    }
                    if (ok)
                    {
                        possibleSolutions.Add(new PossibleSolution(c, gc.sequence[k]));
                    }
                }

                if (possibleSolutions.Count > 0)
                {
                    int min = possibleSolutions[0].sumc;
                    for (int i = 1; i < possibleSolutions.Count; i++)
                    {
                        if (possibleSolutions[i].sumc < min)
                        {
                            min = possibleSolutions[i].sumc;
                        }
                    }

                    solutions = new List<BinaryVector>();
                    for (int i = 0; i < possibleSolutions.Count; i++)
                    {
                        if (possibleSolutions[i].sumc == min)
                        {
                            solutions.Add(possibleSolutions[i].x);
                        }
                    }
                }
                
            }

            void PrintSolutions()
            {
                if (solutions == null)
                {
                    Console.WriteLine("Решений нет!");
                }
                else
                {
                    for (int i = 0; i < solutions.Count; i++)
                    {
                        Console.Write($"Решение {i + 1}: базы в районах ");

                        List<int> placements = new List<int>();
                        for (int j = 0; j < solutions[i].bytes.Length; j++)
                        {
                            if (solutions[i].bytes[j] == 1)
                                placements.Add(j + 1);
                        }

                        for (int j = 0; j < placements.Count - 1; j++)
                        {
                            Console.Write($"{placements[j]}, ");
                        }
                        Console.WriteLine((placements.Count != 1 ? "и ": "") + $"{placements[placements.Count - 1]} ({placements.Count} базы).");
                    }
                    Console.WriteLine($"Минимальные затраты: {CalcSum(0)}");
                }
            }

            int CalcSum(int id)
            {
                int res = 0;
                for (int i = 0; i < solutions[id].bytes.Length; i++)
                {
                    res += solutions[id].bytes[i] * c[i];
                }
                return res;
            }
        }

        static void Main(string[] args)
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            GreysCode gc = new GreysCode(22);
            time.Stop();
            Console.WriteLine(time.Elapsed);

            //gc.Print();

            if (!true)
            {
                Console.WriteLine("Решение примера: ");
                BasePlacementProblem bpp = new BasePlacementProblem(@"C:\Users\YaredFall\source\repos\algo\algo1\example.txt");
                bpp.PrintProblem();

                Console.WriteLine("Тест 1: ");
                bpp = new BasePlacementProblem(@"C:\Users\YaredFall\source\repos\algo\algo1\tests\test1.txt");
                bpp.PrintProblem();

                Console.WriteLine("Тест 2: ");
                bpp = new BasePlacementProblem(@"C:\Users\YaredFall\source\repos\algo\algo1\tests\test2.txt");
                bpp.PrintProblem();

                Console.WriteLine("Тест 3: ");
                bpp = new BasePlacementProblem(@"C:\Users\YaredFall\source\repos\algo\algo1\tests\test3.txt");
                bpp.PrintProblem();

                Console.WriteLine("Тест 4: ");
                bpp = new BasePlacementProblem(@"C:\Users\YaredFall\source\repos\algo\algo1\tests\test4.txt");
                bpp.PrintProblem();

                Console.WriteLine("Тест 5: ");
                bpp = new BasePlacementProblem(@"C:\Users\YaredFall\source\repos\algo\algo1\tests\test5.txt");
                bpp.PrintProblem();

                Console.WriteLine("Тест 6: ");
                bpp = new BasePlacementProblem(@"C:\Users\YaredFall\source\repos\algo\algo1\tests\test6.txt");
                bpp.PrintProblem();

                Console.WriteLine("Тест 7: ");
                bpp = new BasePlacementProblem(@"C:\Users\YaredFall\source\repos\algo\algo1\tests\test7.txt");
                bpp.PrintProblem();
            }
        }
    }
}