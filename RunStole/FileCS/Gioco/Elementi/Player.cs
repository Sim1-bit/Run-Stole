using System;
using System.Timers;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace RunStole
{
    class Player
    {
        const int Proporzione = 45;

        static SoundBuffer buffer = new SoundBuffer(@"..\..\..\RunStole\FileWAW\Gioco\Passo.wav");
        static Sound sound = new Sound(buffer);

        public int[] Posizione;

        public Sprite PG = new Sprite(new Texture(@"..\..\..\RunStole\FilePNG\Gioco\Astreion.png", new IntRect(0, 5, 250, 250)));

        public int[] Animazioni_Movimento;

        public Mosse Mossa = new Mosse();

        public int Coin = 0;

        public bool Permetti;

        public enum Mosse //Mosse che si possono usare
        {
            W,
            S,
            A,
            D
        }

        public void xHunter() //Assegna i valori iniziali al giocatore
        {
            Permetti = false;

            int[] Animazioni_Movimento1 = { 0, 0 };
            Animazioni_Movimento = Animazioni_Movimento1;

            int[] Posizione1 = { 10, 6 };
            Posizione = Posizione1;

            PG.Scale = new Vector2f((2 * Proporzione) / 3 / 10, (2 * Proporzione) / 3 / 10);
            PG.Position = new Vector2f(6 * Proporzione, 19 * Proporzione / 2);
            PG.TextureRect = new IntRect(Animazioni_Movimento[0] * 16, Animazioni_Movimento[1] * 32, 15, 22);

            Mosse Mossa1 = Mosse.S;
            Mossa = Mossa1;
        }

        public void Movimento(Mappa[] MAP, ref int nMap, Ghost Ghosts, int Volume, ref bool PrimaMossa, ref Timer timer) //Gestisce lo spostamento nella mappa
        {
            if (MAP[nMap].Map[Posizione[0], Posizione[1]] == 5) //Se la cella in cui il player sta ha il valore 5 la nMap va incrementata e le posizioni del bot e del player vanno resettate
            {
                PrimaMossa = false;
                nMap++;
                xHunter();
                Ghosts.xGhost();
                timer.Stop();
                return;
            }
            if (nMap == MAP.Length) //Se nMap è uguale ha 5 vuol dire che il gioco è finito
                return;
            Vector2f Pos = PG.Position;
            switch (Mossa)
            {
                case Mosse.A:
                    Animazioni_Movimento[1] = 3;
                    Posizione[1]--;
                    //Se fa la mossa ma la cella in cui si trova non è accesibile si annulla
                    if (MAP[nMap].Map[Posizione[0], Posizione[1]] == 0 || MAP[nMap].Map[Posizione[0], Posizione[1]] == 4 || MAP[nMap].Map[Posizione[0], Posizione[1]] >= 6.1f && MAP[nMap].Map[Posizione[0], Posizione[1]] < 6.8f || MAP[nMap].Map[Posizione[0], Posizione[1]] == 8 || MAP[nMap].Map[Posizione[0], Posizione[1]] > 8 && nMap == 2)
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
                    if (MAP[nMap].Map[Posizione[0], Posizione[1]] == 0 || MAP[nMap].Map[Posizione[0], Posizione[1]] == 4 || MAP[nMap].Map[Posizione[0], Posizione[1]] >= 6.1f && MAP[nMap].Map[Posizione[0], Posizione[1]] < 6.8f || MAP[nMap].Map[Posizione[0], Posizione[1]] == 8 || MAP[nMap].Map[Posizione[0], Posizione[1]] > 8 && nMap == 2)
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
                    if (MAP[nMap].Map[Posizione[0], Posizione[1]] == 0 || MAP[nMap].Map[Posizione[0], Posizione[1]] == 4 || MAP[nMap].Map[Posizione[0], Posizione[1]] >= 6.1f && MAP[nMap].Map[Posizione[0], Posizione[1]] < 6.8f || MAP[nMap].Map[Posizione[0], Posizione[1]] == 8 || MAP[nMap].Map[Posizione[0], Posizione[1]] > 8 && nMap == 2)
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
                    if (MAP[nMap].Map[Posizione[0], Posizione[1]] == 0 || MAP[nMap].Map[Posizione[0], Posizione[1]] == 4 || MAP[nMap].Map[Posizione[0], Posizione[1]] >= 6.1f && MAP[nMap].Map[Posizione[0], Posizione[1]] < 6.8f || MAP[nMap].Map[Posizione[0], Posizione[1]] == 8 || MAP[nMap].Map[Posizione[0], Posizione[1]] > 8 && nMap == 2)
                    {
                        Posizione[0]--;
                        break;
                    }
                    Pos.Y += Proporzione;
                    break;
            }
            //Cambio texture del pg per le animazioni
            PG.Position = Pos;
            Animazioni_Movimento[0] += 1;
            Animazioni_Movimento[0] %= 4;
            PG.TextureRect = new IntRect(Animazioni_Movimento[0] * 16, Animazioni_Movimento[1] * 32, 15, 22);

            if (MAP[nMap].Map[Posizione[0], Posizione[1]] == 2) //Se la cella è uguale vuol dire che ha preso una moneta e quindi va tolta
            {
                sound.Stop();

                MAP[nMap].Map[Posizione[0], Posizione[1]] = 1;
                MAP[nMap].nCoin--;

                sound.Volume = Volume;
                sound.Play();

                Coin++;

                if (MAP[nMap].nCoin == 0 && nMap == 0) //Se il numero di monete è 0 succedono cose in base alle mappe
                    MAP[nMap].Map[0, 6] = 5;
                else if (MAP[nMap].nCoin == 0 && (nMap == 1 || nMap == 2))
                    MAP[nMap].Map[2, 6] = 5;
                else if (MAP[nMap].nCoin == 0 && nMap == 3)
                {
                    MAP[nMap].Map[2, 6] = 5;
                    MAP[nMap].Map[1, 6] = 5.1f;
                }
                else if(MAP[nMap].nCoin == 0)
                    MAP[nMap].Map[0, 6] = 5;
            }
        }
    }
}
