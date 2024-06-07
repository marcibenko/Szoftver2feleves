using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NT7PTO
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string alaprajzPath = "alaprajz.txt";


            AlaprajzClass alaprajz = new AlaprajzClass(alaprajzPath);
            int OsszSzabadHely = alaprajz.getCharNum(' ');
            List<int[]> AP_poz = new List<int[]>();
            AP_poz = Backtrack(alaprajzPath,alaprajz, AP_poz, 0, OsszSzabadHely);
            AlaprajzClass alaprajzVegleges = new AlaprajzClass(alaprajzPath);
            alaprajzVegleges.Kiir(AP_poz);
            
        }

        static List<int[]> Backtrack(string path,AlaprajzClass alaprajz, List<int[]> AP_pozi, double ElozoLefedettsege,int OsszSzabadHely)
        {
            if(ElozoLefedettsege<0.9)
            {
                int[] dim = alaprajz.getDim();
                int Xrnd = RandomGenerator.rnd.Next(1, dim[0] - 2);
                int Yrnd = RandomGenerator.rnd.Next(1, dim[1] - 2);

                while(alaprajz.SugarbanVan(AP_pozi,new int[] { Xrnd,Yrnd}))
                {
                     Xrnd = RandomGenerator.rnd.Next(1, dim[0] - 2);
                     Yrnd = RandomGenerator.rnd.Next(1, dim[1] - 2);
                }
                alaprajz.APBehelyez(Xrnd, Yrnd);

                foreach (int[] AP in AP_pozi)
                {
                    alaprajz.APBehelyez(AP[0], AP[1]);
                    
                }
                double lefedetteseg = alaprajz.getCharNum('0') * 1.0 / OsszSzabadHely  ;
                
                
                if (ElozoLefedettsege > lefedetteseg)
                {
                    alaprajz = new AlaprajzClass(path); 
                    foreach (int[] AP in AP_pozi)
                    {
                        alaprajz.APBehelyez(AP[0], AP[1]);
                    }
                }
                else
                {
                    AP_pozi.Add(new int[] { Xrnd, Yrnd });
                    
                }
                Backtrack(path,alaprajz, AP_pozi, lefedetteseg, OsszSzabadHely);
                ;
            }
            return AP_pozi;
        }





    }
}
