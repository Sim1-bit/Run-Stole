using System;
using SFML.Graphics;
using SFML.System;

namespace RunStole
{
    class Ghost
    {
        const int Proporzione = 45;

        public Sprite PG;
        public int[] Animazioni_Movimento;

        public Mosse Mossa = new Mosse();

        public int[] Posizione;

        public bool Permetti;

        public enum Mosse
        {
            W,
            S,
            A,
            D
        }
        public void xGhost() //Assegna i valori iniziali al bot
        {
            Permetti = false;

            int[] Animazioni_Movimento1 = { 0, 0 };
            Animazioni_Movimento = Animazioni_Movimento1;

            int[] Posizione1 = { 8, 6 };
            Posizione = Posizione1;

            IntRect Rect = new IntRect(0, 7, 250, 250);

            Texture TexturePG = new Texture(@"..\..\..\RunStole\FilePNG\Gioco\NPC_test.png", Rect);

            PG = new Sprite(TexturePG);
            PG.Scale = new Vector2f((2 * Proporzione) / 3 / 10, (2 * Proporzione) / 3 / 10);
            PG.Position = new Vector2f(6 * Proporzione, 15 * Proporzione / 2);
            PG.TextureRect = new IntRect(Animazioni_Movimento[0] * 16, Animazioni_Movimento[1] * 32, 16, 22);
        }

        public void Movimento(Mappa MAP) //Gestisce lo spostamento nella mappa
        {
            Vector2f Pos = PG.Position;
            switch (Mossa)
            {
                case Mosse.A:
                    Animazioni_Movimento[1] = 3;
                    Posizione[1]--;
                    //Se fa la mossa ma la cella in cui si trova non è accesibile si annulla
                    if (MAP.Map[Posizione[0], Posizione[1]] == 0 || MAP.Map[Posizione[0], Posizione[1]] >= 6 && MAP.Map[Posizione[0], Posizione[1]] < 6.8 || MAP.Map[Posizione[0], Posizione[1]] == 8)
                    {
                        Posizione[1]++;
                        break;
                    }
                    Pos.X -= Proporzione;
                    break;
                case Mosse.D:
                    Animazioni_Movimento[1] = 1;
                    Posizione[1]++;
                    //Se fa la mossa ma la cella in cui si trova non è accesibile si annulla
                    if (MAP.Map[Posizione[0], Posizione[1]] == 0 || MAP.Map[Posizione[0], Posizione[1]] >= 6 && MAP.Map[Posizione[0], Posizione[1]] < 6.8 || MAP.Map[Posizione[0], Posizione[1]] == 8)
                    {
                        Posizione[1]--;
                        break;
                    }
                    Pos.X += Proporzione;
                    break;
                case Mosse.W:
                    Animazioni_Movimento[1] = 2;
                    Posizione[0]--;
                    //Se fa la mossa ma la cella in cui si trova non è accesibile si annulla
                    if (MAP.Map[Posizione[0], Posizione[1]] >= 0 && MAP.Map[Posizione[0], Posizione[1]] < 1 || MAP.Map[Posizione[0], Posizione[1]] >= 5.0f && MAP.Map[Posizione[0], Posizione[1]] < 6.8f && MAP.Map[Posizione[0], Posizione[1]] != 6.0f)
                    {
                        Posizione[0]++;
                        break;
                    }
                    Pos.Y -= Proporzione;
                    break;
                case Mosse.S:
                    Animazioni_Movimento[1] = 0;
                    Posizione[0]++;
                    //Se fa la mossa ma la cella in cui si trova non è accesibile si annulla
                    if (MAP.Map[Posizione[0], Posizione[1]] >= 0 && MAP.Map[Posizione[0], Posizione[1]] < 1 || MAP.Map[Posizione[0], Posizione[1]] >= 6.1f && MAP.Map[Posizione[0], Posizione[1]] < 7 || MAP.Map[Posizione[0], Posizione[1]] == 4)
                    {
                        Posizione[0]--;
                        break;
                    }
                    Pos.Y += Proporzione;
                    break;
            }
            PG.Position = Pos;
            Animazioni_Movimento[0] += 1;
            Animazioni_Movimento[0] %= 4;
            PG.TextureRect = new IntRect(Animazioni_Movimento[0] * 16, Animazioni_Movimento[1] * 32, 15, 22);
        }

        public void IAghost(ref Mappa MAP, Player Utente, int nMap, int nMappe) //In base alla cella della mappa in cui è deve prendere una scelta
        {
            if (nMap == nMappe)
                return;

            if (MAP.Map[Posizione[0], Posizione[1]] == 4)
            {
                Mossa = Mosse.W;
            }
            else if (MAP.Map[Posizione[0], Posizione[1]] == 7)
            {
                Random Aux = new Random();
                switch(Aux.Next(0, 2))
                {
                    case 0:
                        Mossa = Mosse.A;
                        break;
                    case 1:
                        Mossa = Mosse.D;
                        break;
                }
                MAP.Map[Posizione[0], Posizione[1]] = 1;
            }
            else //Confronta le mosse fattibili, se la diff delle x è maggiore di quelle delle y allora da priorità alla x, altriementi fa il contrario
            {
                int x = Posizione[1] - Utente.Posizione[1];
                int y = Posizione[0] - Utente.Posizione[0];
                if (x < 0)
                    x *= -1;
                if (y < 0)
                    y *= -1;

                bool Asse;

                if (x >= y && (MAP.Map[Posizione[0], Posizione[1] + 1] != 0 || MAP.Map[Posizione[0], Posizione[1] - 1] != 0))
                    Asse = true;
                else
                    Asse = false;

                if (y > x && (MAP.Map[Posizione[0] + 1, Posizione[1]] != 4 && MAP.Map[Posizione[0] + 1, Posizione[1]] != 0 || MAP.Map[Posizione[0] - 1, Posizione[1]] != 5 && MAP.Map[Posizione[0] - 1, Posizione[1]] != 0))
                    Asse = false;
                else
                    Asse = true;

                if (Asse)
                {
                    if (Posizione[1] < Utente.Posizione[1] && MAP.Map[Posizione[0], Posizione[1] + 1] != 0)
                        Mossa = Mosse.D;
                    else if (Posizione[1] > Utente.Posizione[1] && MAP.Map[Posizione[0], Posizione[1] - 1] != 0)
                        Mossa = Mosse.A;
                }
                else
                {
                    if (Posizione[0] < Utente.Posizione[0] && (MAP.Map[Posizione[0] + 1, Posizione[1]] != 0 && MAP.Map[Posizione[0] + 1, Posizione[1]] != 4))
                        Mossa = Mosse.S;
                    else if (Posizione[0] > Utente.Posizione[0] && (MAP.Map[Posizione[0] - 1, Posizione[1]] != 0 && MAP.Map[Posizione[0] - 1, Posizione[1]] != 5))
                        Mossa = Mosse.W;
                }
            }
            Movimento(MAP);
        }
    }
}