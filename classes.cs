using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace NT7PTO
{
    
    static class RandomGenerator
    {
        public static Random rnd = new Random();
    }

    class AlaprajzClass
    {

        char[,] alaprajz;

        public AlaprajzClass(string file)
        {
            alaprajz = Beolvas(file);
        }

        List<int[]> AP_Poz = new List<int[]>();

        

        public void APBehelyez(int sor, int oszlop)
        {
            int r = 15;
            double rad = (2 * Math.PI);
            AP_Poz.Add(new int[] { sor, oszlop });
            
            for (int i = 1; i < Math.Round(rad * r)+1; i++)
            {
                double X = (oszlop + (Math.Sin(rad * (i / (2 * r * Math.PI)))) * r);
                double Y = (sor + (Math.Cos(rad * (i / (2 * r * Math.PI)))) * r);
                //Console.WriteLine($"Oszlop:{(int)X} Sor:{(int)Y}");

                try
                {
                    if (alaprajz[(int)Y, (int)X] != '#')
                    {
                        alaprajz[(int)Y, (int)X] = 'S';
                    }
                }
                catch
                { }
                
                
            }

            int counterSor = 0;
            
            int[] koord2 = new int[2];
            
            while (counterSor != 15 )
            {

                //most csak oszlopra
                int counterOszlop = 0;
                
                try//nagyon fapados, lehetne indexet nezni
                {
                    //elso siknegyed
                    while (
                         alaprajz[sor - counterOszlop, oszlop + counterSor] != '#' &&
                         alaprajz[sor - counterOszlop, oszlop + counterSor] != 'S')
                    {
                        koord2[0] = sor - counterOszlop;
                        koord2[1] = oszlop + counterSor;
                        alaprajz[koord2[0], koord2[1]] = '0';
                        counterOszlop++;
                    }

                    //negyedik siknegyed
                    counterOszlop = 0;
                    while (
                         alaprajz[sor + counterOszlop, oszlop + counterSor] != '#' &&
                         alaprajz[sor + counterOszlop, oszlop + counterSor] != 'S')
                    {
                        koord2[0] = sor + counterOszlop;
                        koord2[1] = oszlop + counterSor;
                        alaprajz[koord2[0], koord2[1]] = '0';
                        counterOszlop++;
                    }

                    //masodik sn
                    counterOszlop = 0;
                    while (
                         alaprajz[sor - counterOszlop, oszlop - (counterSor)] != '#' &&
                         alaprajz[sor - counterOszlop, oszlop - ( counterSor)] != 'S')
                    {
                        koord2[0] = sor - counterOszlop;
                        koord2[1] = oszlop - (counterSor);
                        alaprajz[koord2[0], koord2[1]] = '0';
                        counterOszlop++;
                    }


                    //harmadik
                    counterOszlop = 0;
                    while (
                         alaprajz[sor + counterOszlop, oszlop - counterSor] != '#' &&
                         alaprajz[sor + counterOszlop, oszlop - counterSor] != 'S')
                    {
                        koord2[0] = sor + counterOszlop;
                        koord2[1] = oszlop - counterSor;
                        alaprajz[koord2[0], koord2[1]] = '0';
                        counterOszlop++;
                    }
                }
                catch (Exception exe)
                {

                }
                counterSor++;

            }

            for (int i = 1; i < Math.Round(rad * r) + 1; i++)
            {
                double X = (oszlop + (Math.Sin(rad * (i / (2 * r * Math.PI)))) * r);
                double Y = (sor + (Math.Cos(rad * (i / (2 * r * Math.PI)))) * r);
                //Console.WriteLine($"Oszlop:{(int)X} Sor:{(int)Y}");

                try
                {
                    if (alaprajz[(int)Y, (int)X] != '#')
                    {
                        alaprajz[(int)Y, (int)X] = '0';
                    }
                }
                catch
                { }


            }

            foreach (int[] AP in AP_Poz)
            {
                alaprajz[AP[0], AP[1]] = '*'; //sor az y-koord, oszlop -> x
            }

        }

        char[,] Beolvas(string file)
        {
            string[] be = File.ReadAllLines(file);
            int sor = be.Length;
            int oszlop = be[0].Length;

            char[,] alaprajz = new char[sor, oszlop];

            for (int i = 0; i < sor; i++)
            {
                for (int j = 0; j < oszlop; j++)
                {
                    alaprajz[i, j] = be[i][j];
                }
            }

            return alaprajz;
        }

        public void Kiir()
        {
            int sor = alaprajz.GetLength(0);
            int oszlop = alaprajz.GetLength(1);
            Console.SetWindowSize(oszlop + 5, sor + 5);

            for (int i = 0; i < sor; i++)
            {
                for (int j = 0; j < oszlop; j++)
                {
                    Console.Write(alaprajz[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void Kiir(List<int[]> AP_poz)
        {
            foreach(int[] ap in AP_poz)
            {
                alaprajz[ap[0], ap[1]] = '*';
            }
            Kiir();
        }

        public bool SugarbanVan(List<int[]> APk, int[] koord) //false ha sugarban van
        {
            bool sugarbanVan = false;

            if(APk==null)
            {
                return true;
            }
            
            foreach(int[] ap in APk)
            {
                if(!sugarbanVan && (Math.Abs(koord[0] - ap[0]) + Math.Abs(koord[1] - ap[1]) <= 13))
                {
                    sugarbanVan = true;
                }
            }
            return sugarbanVan;
        }

        public int getCharNum(char c)
        {
            int lefedett = 0;
            for (int i = 0; i < alaprajz.GetLength(0); i++)
            {
                for (int j= 0; j< alaprajz.GetLength(1); j++)
                {
                    if(alaprajz[i, j] == c)
                    {
                        lefedett++;
                    }
                }
            }
            return lefedett;
        }

        public int[] getDim()
        {
            return new int[] { alaprajz.GetLength(0), alaprajz.GetLength(1) };
        }

    }
}
