using System;
using System.IO;
namespace MashUrganish
{
    public class Program
    {  static double Masofa(double[] a, double[] b)
        {
            double summa = 0;
            for (int i = 0; i < a.Length; i++)
            {
                summa += Math.Pow((a[i] * a[i] - b[i] * b[i]),2);

            }
            return Math.Sqrt(summa);
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
                        alomat = elements.Length;
                        soni++;
                    }
                    while (sr.ReadLine() != null)
                    {
                        soni++;
                    }
                }

                double[,] sinflar = new double[soni, alomat];
                using (StreamReader sr = new StreamReader(faylmatni))
                {

                    for (int i = 0; i < soni; i++)
                    {
                        string? qator = sr.ReadLine();

                       
                            string[] elements = qator.Split(' ', '\t');
                            for (int j = 0; j < alomat; j++)
                            {
                                sinflar[i, j] = double.Parse(elements[j]);
                            }
                        
                    }

                }

                    for (int i = 0; i < soni; i++)
                    {
                        for (int j = 0; j < alomat; j++)
                        {
                            Console.Write(sinflar[i, j] + "\t");
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine("Har bir ustun maksimumlari:");                    double[] max = new double[alomat];
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
                    Console.WriteLine("Alomatlarni massiv ko'rinishida kiritishingiz kerak");
                    double[] example = new double[alomat];
                    for (int i = 0; i < alomat; i++)
                    {
                        Console.Write("alomat[{0}]=", i);
                        example[i] = Convert.ToDouble(Console.ReadLine());
                    }

                double[]s=new double[soni];
                double[] every=new double[alomat];
                for (int i = 0; i < soni; i++)
                {
                    for (int j = 0; j < alomat; j++)
                    {
                        every[j] = sinflar[i, j];
                        s[i] = Masofa(every, example);
                    }

                }
                double good = s[0], k = 0;
                for (int i = 1; i < soni; i++)
                {
                    if(s[i] <good)
                    {
                        good = s[i];
                        k = i;
                    }
                }
                Console.WriteLine("eng mos keladigani:{0} farqi:{1}",k,good);
            }



            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
   }
