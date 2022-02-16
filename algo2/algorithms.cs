using System;
using System.Collections.Generic;

class VectorSequence
{
    List<List<int>> sequence;

    public int Length { get => sequence.Count; }

    public VectorSequence(int n, int k)
    {
        GenerateSequence(n, k);
    }

    void GenerateSequence(int n, int k)
    {
        sequence = new List<List<int>>();
        List<int> A = new List<int>();
        for (int i = 1; i <= k; i++)
        {
            A.Add(i);
        }

        int p = k;
        while (true)
        {
            sequence.Add(new List<int>(A));
            if (k == n)
                break;

            if (A[k-1] == n)
            {
                p--;
            }
            else
            {
                p = k;
            }

            if (p >= 1)
                for (int i = k; i >= p; i--)
                {
                    A[i - 1] = A[p - 1] + i - p + 1;
                }
            else
                break;
        }
    }

    public List<int> Vector(int id)
    {
        return sequence[id];
    }

    public string StringVector(int id)
    {
        string s = "";//$"Вектор {id + 1}: ";
        for (int i = 0; i < sequence[id].Count; i++)
        {
            s += sequence[id][i];
        }
        return s;
    }

}

class Permutations
{
    List<List<int>> sequence;

    public int Length { get => sequence.Count; }

    public Permutations(int n)
    {
        sequence = new List<List<int>>();
        List<int> A = new List<int>();
        for (int i = 1; i <= n; i++)
        {
            A.Add(i);
        }

        while (true)
        {
            sequence.Add(new List<int>(A));

            int i = -1;
            for (int k = A.Count - 2; k >= 0; k--)
            {
                if (A[k] < A[k + 1])
                {
                    i = k;
                    break;
                }
            }
            if (i == -1)
                break;

            int j = i+1;
            int min = A[i+1];
            for (int k = i+1; k < A.Count; k++)
            {
                if (i < k && A[i] < A[k])
                {
                    if (A[k] < min)
                    {
                        min = A[k];
                        j = k;
                    }
                }
            }
            int c = A[i];
            A[i] = A[j];
            A[j] = c;

            A.Reverse(i + 1, A.Count - i - 1);
        }
    }

    public List<int> Permutation(int id)
    {
        return sequence[id];
    }

    public string StringPermutation(int id)
    {
        string s = "";//$"перестановка {id + 1}: ";
        for (int i = 0; i < sequence[id].Count; i++)
        {
            s += sequence[id][i];
        }
        return s;
    }

}