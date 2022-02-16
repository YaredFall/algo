using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



static class Problem
{
    public static List<Point> Solution(List<Point> points)
    {
        List<Point> res = new List<Point>(4);

        List<List<Point>> rhombyses = new List<List<Point>>();

        int n = points.Count;
        int k = 4;

        int gid = -1;

        VectorSequence sets = new VectorSequence(n, k);

        for (int i = 0; i < sets.Length; i++)
        {
            Point p1 = points[sets.Vector(i)[0]-1];
            Point p2 = points[sets.Vector(i)[1]-1];
            Point p3 = points[sets.Vector(i)[2]-1];
            Point p4 = points[sets.Vector(i)[3]-1];

            if ((p3.Y - p1.Y) * (p2.X - p1.X) == (p3.X - p1.X) * (p2.Y - p1.Y)
            &&  (p4.Y - p1.Y) * (p2.X - p1.X) == (p4.X - p1.X) * (p2.Y - p1.Y))
            {
                Debug.WriteLine($"set {i + 1} is on single line!");
            }
            else
            {
                Point[] p = new Point[] { p1, p2, p3, p4 };
                Permutations perms = new Permutations(k);

                int r = 0;
                int rid = 0;

                for (int j = 0; j < perms.Length; j++)
                {
                    int a = (int)Math.Pow(p[perms.Permutation(j)[0] - 1].X - p[perms.Permutation(j)[1] - 1].X, 2) +
                        (int)Math.Pow(p[perms.Permutation(j)[0] - 1].Y - p[perms.Permutation(j)[1] - 1].Y, 2);
                    int b = (int)Math.Pow(p[perms.Permutation(j)[1] - 1].X - p[perms.Permutation(j)[2] - 1].X, 2) +
                        (int)Math.Pow(p[perms.Permutation(j)[1] - 1].Y - p[perms.Permutation(j)[2] - 1].Y, 2);
                    int c = (int)Math.Pow(p[perms.Permutation(j)[2] - 1].X - p[perms.Permutation(j)[3] - 1].X, 2) +
                        (int)Math.Pow(p[perms.Permutation(j)[2] - 1].Y - p[perms.Permutation(j)[3] - 1].Y, 2);
                    int d = (int)Math.Pow(p[perms.Permutation(j)[3] - 1].X - p[perms.Permutation(j)[0] - 1].X, 2) +
                        (int)Math.Pow(p[perms.Permutation(j)[3] - 1].Y - p[perms.Permutation(j)[0] - 1].Y, 2);
                    if ( a == b && a == c && a == d)
                    {
                        Debug.WriteLine($"set {i + 1} ({sets.StringVector(i)}) with perm {j+1}" +
                            $" ({p[perms.Permutation(j)[0] - 1]}{p[perms.Permutation(j)[1] - 1]}{p[perms.Permutation(j)[2] - 1]}" +
                            $"{ p[perms.Permutation(j)[3] - 1]}) is a rhombus");
                        r++;
                        rid = j;
                    }
                }

                if (r == 8)
                {
                    rhombyses.Add(new List<Point>() { p[perms.Permutation(rid)[0]-1], p[perms.Permutation(rid)[1]-1],
                        p[perms.Permutation(rid)[2]-1], p[perms.Permutation(rid)[3]-1] });
                }
            }
        }

        double max = -1;

        for (int i = 0; i < rhombyses.Count; i++)
        {
            double d1 = Math.Sqrt(Math.Pow(rhombyses[i][0].X - rhombyses[i][2].X, 2) + Math.Pow(rhombyses[i][0].Y - rhombyses[i][2].Y, 2));
            double d2 = Math.Sqrt(Math.Pow(rhombyses[i][3].X - rhombyses[i][1].X, 2) + Math.Pow(rhombyses[i][3].Y - rhombyses[i][1].Y, 2));

            if (d1 * d2 > max)
            {
                max = d1 * d2;
                res = rhombyses[i];
            }
        }

        Debug.WriteLine($"Solution is set {res[0]}{res[1]}{res[2]}{res[3]}!");
        return res;
    }

}

