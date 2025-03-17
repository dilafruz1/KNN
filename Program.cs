using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace MashUrganish
{
    public class Program
    {
        static double EvklidMasofa(double[] a, double[] b)
        {
            double summa = 0;
            for (int i = 0; i < a.Length; i++)
            {
                summa += Math.Pow(a[i] - b[i], 2);
            }
            return Math.Sqrt(summa);
        }

        static double ChebishevMasofa(double[] a, double[] b)
        {
            double maxFarq = 0;
            for (int i = 0; i < a.Length; i++)
            {
                double farq = Math.Abs(a[i] - b[i]);
                if (farq > maxFarq)
                {
                    maxFarq = farq;
                }
            }
            return maxFarq;
        }

        static double ManhettenMasofa(double[] a, double[] b)
        {
            double summa = 0;
            for (int i = 0; i < a.Length; i++)
            {
                summa += Math.Abs(a[i] - b[i]);
            }
            return summa;
        }

        public static void Main(string[] args)
        {
            string faylmatni = "C:\\Users\\V I C T U S\\Desktop\\Volki_Sobaki.tbl";
            try
            {
                int soni = 0;
                int alomat = 0;
                using (StreamReader sr = new StreamReader(faylmatni))
                {
                    string? birinchiqator = sr.ReadLine();
                    if (birinchiqator != null)
                    {
                        string[] elements = birinchiqator.Split(' ', '\t');
                        alomat = elements.Length - 1;
                        soni++;
                    }
                    while (sr.ReadLine() != null)
                    {
                        soni++;
                    }
                }

                double[,] sinflar = new double[soni, alomat];
                string[] sinfbelgilari = new string[soni];
                using (StreamReader sr = new StreamReader(faylmatni))
                {
                    for (int i = 0; i < soni; i++)
                    {
                        string? qator = sr.ReadLine();
                        string[] elements = qator.Split(' ', '\t');
                        sinfbelgilari[i] = elements[0];
                        for (int j = 1; j < alomat; j++)
                        {
                            sinflar[i, j - 1] = double.Parse(elements[j]);
                        }
                    }
                }


                Console.WriteLine("Har bir ustun maksimumlari:"); double[] max = new double[alomat];
                for (int i = 0; i < alomat; i++)
                {
                    max[i] = sinflar[0, i];
                    for (int j = 0; j < soni; j++)
                    {
                        if (sinflar[j, i] >= max[i])
                        {
                            max[i] = sinflar[j, i];
                        }
                    }
                    Console.WriteLine(max[i] + "\t");
                }
                Console.WriteLine("Har bir ustun minimumlari:");
                double[] min = new double[alomat];
                for (int i = 0; i < alomat; i++)
                {
                    min[i] = sinflar[0, i];
                    for (int j = 0; j < soni; j++)
                    {
                        if (sinflar[j, i] < min[i])
                        {
                            min[i] = sinflar[j, i];
                        }

                    }
                    Console.WriteLine(min[i] + "\t");

                }
                Console.Write("K= qiymatini kiriting:");
                int k = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Metrikani tanlang (1 - Evklid, 2 - Chebishev, 3 - Manhettem):");
                int metrikaTanlovi = Convert.ToInt32(Console.ReadLine());
                List<(double Distance, int Index)> masofalar = new List<(double Distance, int Index)>();
                Console.WriteLine("Alomatlarni massiv ko'rinishida kiritishingiz kerak");
                double[] example = new double[alomat];
                for (int i = 0; i < alomat; i++)
                {
                    Console.Write("alomat[{0}]=", i);
                    example[i] = Convert.ToDouble(Console.ReadLine());
                }


                for (int i = 0; i < soni; i++)
                {
                    double[] currentPoint = new double[alomat];
                    for (int j = 0; j < alomat; j++)
                    {
                        currentPoint[j] = sinflar[i, j];
                    }

                    double masofa = 0;

                    switch (metrikaTanlovi)
                    {
                        case 1:
                            masofa = EvklidMasofa(currentPoint, example);
                            break;
                        case 2:
                            masofa = ChebishevMasofa(currentPoint, example);
                            break;
                        case 3:
                            masofa = ManhettenMasofa(currentPoint, example);
                            break;
                        default:
                            Console.WriteLine("Noto'g'ri metrika tanlovi.");
                            return;
                    }
                    masofalar.Add((masofa, i));
                }

                masofalar.Sort((x, y) => x.Distance.CompareTo(y.Distance));
                Console.Write($"Eng yaqin {k} ta qo'shnilar:");

                for (int i = 0; i < k; i++)
                {
                    Console.WriteLine($"Index: {masofalar[i].Index}, Masofa: {masofalar[i].Distance}");
                }
                var masofasinfbelgilari = masofalar
    .Zip(sinfbelgilari, (masofa, sinf) => new { Masofa = masofa, Sinf = sinf })
    .OrderBy(juftlik => juftlik.Masofa)
    .ToList();

                var engyaqinqushnilar = masofasinfbelgilari.Take(k).ToList();

                // Guruhlash va sinf belgilari bo'yicha hisoblash
                var sinfbelgilarisoni = engyaqinqushnilar
                    .GroupBy(juftlik => juftlik.Sinf)
                    .Select(g => new { Sinf = g.Key, Soni = g.Count() })
                    .OrderByDescending(x => x.Soni)
                    .ToList();

                // Agar ro'yxat bo'sh bo'lmasa, eng ko'p uchragan sinfni olish
                if (sinfbelgilarisoni.Any())
                {
                    string engkupaniqlanganSinf = sinfbelgilarisoni[0].Sinf;
                    Console.WriteLine("Tashqaridan kiritilgan obyekt sinfi: {0}", engkupaniqlanganSinf);
                }
                else
                {
                    Console.WriteLine("Eng yaqin qo'shnilar topilmadi.");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
         
            Console.ReadKey();
        }
    }
}