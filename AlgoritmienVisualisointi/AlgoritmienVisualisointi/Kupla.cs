using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace AlgoritmienVisualisointi
{
    class Kupla : ISort
    {
        //private bool jarjestetty = false;
        private int[] taulu;
        private Graphics grafiikka;
        private int korkeus;
        Brush punkku = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
        Brush musta = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        public Kupla(int[] taulu_p, Graphics grafiikka_p, int korkeus_p)
        {
            taulu = taulu_p;
            grafiikka = grafiikka_p;
            korkeus = korkeus_p;
        }

        public void Seuraava()
        {           

            
            for (int i = 0; i < taulu.Count() - 1; i++)
                {
                if(taulu[i] > taulu[i + 1])
                {
                     Swap(i, i + 1);
                }
            }
            
        }

        private void Swap(int i, int v)
        {
            int temp = taulu[i];
            taulu[i] = taulu[i + 1];
            taulu[i + 1] = temp;

            grafiikka.FillRectangle(musta, i, 0, 1, korkeus);
            grafiikka.FillRectangle(musta, v, 0, 1, korkeus);

            grafiikka.FillRectangle(punkku, i,korkeus - taulu[i],1,korkeus);
            grafiikka.FillRectangle(punkku, v,korkeus - taulu[v],1,korkeus);

        }
        public bool onJarjestetty()
        {
            for(int i = 0; i < taulu.Count() - 1; i++)
            {
                if(taulu[i] > taulu[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        public void piirraUudestaan()
        {
            for(int i = 0; i < (taulu.Count() - 1); i++)
            {
                grafiikka.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, korkeus - taulu[i],1, korkeus);
            }
        }
    }
}
