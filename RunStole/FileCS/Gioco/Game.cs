using SFML.Graphics;
using SFML.Window;
using System;
using System.Timers;

namespace RunStole
{
    class Game
    {
        static int Volume;

        public int nMap; //La mappa in cui ci si trova
        public Mappa[] Mappe = new Mappa[4]; //Array contenente le mappe del gioco

        public Player Hunter = new Player();
        public Ghost Ghosts = new Ghost();

        public bool PrimaMossa;

        public Timer TimerPlayer = new Timer();
        public Timer TimerBot = new Timer();

        public void xGame() //Inizializza i valori di base della classe
        {
            int Base = 0;
            bool Presenza = true;
            PrimaMossa = false;

            //Timer che gestisce ogni quanto il bot si dovrà muovere
            TimerPlayer.Elapsed += MovPlayer;
            TimerPlayer.Interval = 150;
            TimerPlayer.AutoReset = true;

            //Timer che gestisce ogni quanto il player si dovrà muovere
            TimerBot.Elapsed += MovBot;
            TimerBot.Interval = 350;
            TimerBot.AutoReset = true;

            nMap = 0;
            Hunter.xHunter();

            for (int i = 0; i < Mappe.Length; i++)
            {
                Mappa Aux = new Mappa();
                Aux.xMappe(ref Base, ref Presenza, i);
                Mappe[i] = Aux;
            }

            Ghost AuxGhost = new Ghost();
            AuxGhost.xGhost();
            Ghosts = AuxGhost;
        }

        public void MovPlayer(object sender, ElapsedEventArgs e) //Appena il timer scade darà l'ok per procedere a farre un passo
        {
            Hunter.Permetti = true;
        }

        public void MovBot(object sender, ElapsedEventArgs e) //Appena il timer scade darà l'ok per procedere a farre un passo
        {
            Ghosts.Permetti = true;
        }

        public void Main(ref RenderWindow Finestra, int Volume1) //Si occupa di disegnare la finestra
        {
            Mappe[nMap].DisegnoMappa(ref Finestra, nMap);
            Volume = Volume1;

            if (PrimaMossa)
            {
                PermettiMov();
            }

            Finestra.Draw(Hunter.PG);
            Finestra.Draw(Ghosts.PG);
        }

        public void PermettiMov()
        {
            if (nMap < Mappe.Length) //Se nMap arriva al suo massimo non deve più procedere con il controllo
            {
                if (Hunter.Permetti) //Se ha l'ok fa muovere il player
                {
                    Hunter.Movimento(Mappe, ref nMap, Ghosts, Volume, ref PrimaMossa, ref TimerPlayer);
                    Hunter.Permetti = false;
                }
                if (Ghosts.Permetti) //Se ha l'ok fa muovere il bot
                {
                    if (nMap == Mappe.Length)
                        return;
                    Ghosts.IAghost(ref Mappe[nMap], Hunter, nMap, Mappe.Length);
                    Ghosts.Permetti = false;
                }
            }
        }

        public void ChiavePremuta(object sender, KeyEventArgs Tasto)
        {
            if (!PrimaMossa && (Tasto.Code == Keyboard.Key.W || Tasto.Code == Keyboard.Key.S || Tasto.Code == Keyboard.Key.A || Tasto.Code == Keyboard.Key.D)) //Non fa partire il timer fino a quando non gli viene dato un tasto accettabile in input
            {
                TimerPlayer.Start();
                TimerBot.Start();
                PrimaMossa = true;
            }
                
            //Ad ogni tasto valido premuto corrisponde un enum in Player.cs
            if (Tasto.Code == Keyboard.Key.W || Tasto.Code == Keyboard.Key.S || Tasto.Code == Keyboard.Key.A || Tasto.Code == Keyboard.Key.D)
            {
                switch (Tasto.Code)
                {
                    case Keyboard.Key.W:
                        Hunter.Mossa = Player.Mosse.W;
                        break;
                    case Keyboard.Key.S:
                        Hunter.Mossa = Player.Mosse.S;
                        break;
                    case Keyboard.Key.A:
                        Hunter.Mossa = Player.Mosse.A;
                        break;
                    case Keyboard.Key.D:
                        Hunter.Mossa = Player.Mosse.D;
                        break;
                }
                PermettiMov();
            }
        }
    }
}
