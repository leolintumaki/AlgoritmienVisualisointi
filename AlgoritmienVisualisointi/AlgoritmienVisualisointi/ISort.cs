using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmienVisualisointi
{
    public interface ISort
    {
        // void aloita(int[] taulu, Graphics grafiikka, int korkeus);
        void Seuraava();
        bool onJarjestetty();
        void piirraUudestaan();
    }
}
