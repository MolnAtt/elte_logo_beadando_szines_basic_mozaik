using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LogoKaresz
{
	public partial class Form1 : Form
	{
		void FELADAT()
		{
            várakozás = 0;
            Tollvastagság(1);
			/*
			Háromszög(100, Color.Red);
            Téglalap(100, 50, Color.Yellow);
			Hatszög(50, Color.Green);
            */
			Mozaik(2,3,20);
			/*
            */
		}

        void Odatölt(double fok, double r, Color szín)
        {
            using (new Rajzol(false))
            using (new Átmenetileg(Jobbra, fok))
            using (new Átmenetileg(Előre, r))
                Tölt(szín, false);
        }
        private void Háromszög(double a, Color szín)
        {
            Ismétlés(3, delegate () 
            {
                Előre(a);
                Jobbra(120);
            });
            Odatölt(30, a / 2, szín);
        }

        private void Téglalap(double a, double b, Color szín)
        {
            Ismétlés(2, delegate ()
            {
                Előre(a);
                Jobbra(90);
                Előre(b);
                Jobbra(90);
            });
            Odatölt(45, Math.Min(a, b), szín);
        }
        private void Hatszög(double a, Color szín)
        {
            Ismétlés(6, delegate ()
            {
                Előre(a);
                Jobbra(60);
            });
            Odatölt(60, a, szín);
        }

        void Elem(double a)
        {
            Ismétlés(3, delegate () 
            {
                Téglalap(a/2, a, Color.Yellow); Előre(a/2); Jobbra(30);
                Háromszög(a, Color.Red); Előre(a); Jobbra(30);
                Téglalap(a/2, a, Color.Yellow); Előre(a/2); Jobbra(30);
                Háromszög(a, Color.Blue); Előre(a); Jobbra(30);
            });
            using (new Átmenetileg(Jobbra, 90))
            using (new Átmenetileg(Előre, a))
            using (new Átmenetileg(Balra, 90))
                Hatszög(a/2, Color.Green);
        }
        void Mozaik(int N, int M, double a)
        {
            Ismétlés(M, delegate ()
            {
                bool páros = true;
                Ismétlés(N, delegate ()
                {
                    Elem(a);
                    using (new Rajzol(false))
                    {
                        if (páros)
                            Jobbra_fel_mászik(a);
                        else
                            Balra_fel_mászik(a);
                    }
                    páros = !páros;
                });
                using (new Rajzol(false))
                using (new Átmenetileg(Előre, a / 2))
                    Ismétlés(N, delegate ()
                    {
                        if (páros)
                            Jobbra_fel_mászik(-a);
                        else
                            Balra_fel_mászik(-a);
                        páros = !páros;
                    });

                Jobbramászik(a);
            });
            using (new Rajzol(false))
            {
                Ismétlés(M, delegate () { Jobbramászik(-a); });
            }
            
        }

        private void Jobbra_fel_mászik(double a)
        {
            int i = a >= 0 ? 1 : -1;
            Előre(a / 2);
            Jobbra(i * 30);
            Előre(a);
            Jobbra(i * 30);
            Előre(a / 2);
            Balra(i * 60);
        }
        private void Jobbramászik(double a)
        {
            int i = a >= 0 ? 1 : -1;
            Jobbra(90);
            Előre(a);
            Jobbra(30);
            Ismétlés(2, delegate () 
            {
                Előre(a / 2);
                Balra(60);
            });
        }
        private void Balra_fel_mászik(double a)
        {
            int i = a >= 0 ? 1 : -1;
            Előre(a / 2);
            Balra(i * 60);
            Előre(a / 2);
            Jobbra(i * 30);
            Előre(a);
            Jobbra(i * 30);
        }
    }
}
