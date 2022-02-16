using System;
using System.Collections.Generic;
using System.IO;

class Граф
{

    List<List<int>> Ni;

    unsafe public int*[] M;

    unsafe public Граф(string filepath)
    {
        string[] f = File.ReadAllLines(filepath);
        Ni = new List<List<int>>(f.Length);

        foreach (var line in f)
        {
            List<int> N = new List<int>();
            foreach (string n in line.Split(' '))
            {
                N.Add(int.Parse(n));
            }
            Ni.Add(N);
        }

        M = new int*[Ni.Count];
        for (int i = 0; i < M.Length; i++)
        {
            *M[i] = Ni[i][0];
        }
    }

}