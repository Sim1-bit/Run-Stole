using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.IO;

namespace RunStole
{
    class Program //Si occupa di gestire la finestra di gioco, decide quale classe potrà "disegnare" sulla finestra
    {
        const int Proporzione = 45;
        static VideoMode Schermata = new VideoMode(13 * Proporzione, 17 * Proporzione);
        static RenderWindow Finestra = new RenderWindow(Schermata, "Run & Stole");

        static Start Menu = new Start(); //Appena si avvia il gioco su va su questa schermata
        static Options Opzioni = new Options(); //E' la schermata che permette di regolare il volume di gioco e mostra i comandi, si può aprire dallo start o dal menù di pausa
        static Game Gioco = new Game(); //Si occupa di gestire il gioco vero e proprio
        static Pause Pausa = new Pause(); //Menu di pausa
        static Death GameOver = new Death(); //Schermata di game over

        static Scene Scena = Scene.Start; //Serve ad indicare che cosa disegnare (schermata iniziale, gioco, pausa ecc)
        static Scene PreScena; //In base al valore di prescena si decide lo sfondo di opzioni
        static int Volume; //Volume del gioco

        static Sound ColonnaSonora = new Sound(new SoundBuffer(@"..\..\..\RunStole\FileWAW\Menu\Boh.wav"));

        enum Scene
        {
            Start,
            Options,
            Game,
            Pause,
            Death
        }
        static void Main(string[] args)
        {
            FileStream stream = File.Open(@"..\..\..\RunStole\FileDAT\Volume.dat", FileMode.Open);
            BinaryReader br = new BinaryReader(stream);
            Volume = br.ReadInt32();

            br.Close();
            stream.Close();

            //Avvia la colonna sonora del gioco
            ColonnaSonora.Loop = true;
            ColonnaSonora.Play();
            ColonnaSonora.Volume = Convert.ToInt32(Volume);

            Finestra.SetVerticalSyncEnabled(true);
            Finestra.Closed += (sender, args) => Finestra.Close();

            Finestra.MouseButtonPressed += Premuto;
            Finestra.KeyPressed += ChiavePremuta;

            //Assegna i valori iniziali ai vari componenti delle classi
            Menu.xMenu();
            Opzioni.xOptions(Convert.ToInt32(Volume));
            Gioco.xGame();
            Pausa.xPause();
            
            while (Finestra.IsOpen)
            {
                Finestra.DispatchEvents();
                Finestra.Clear(Color.Black);
                switch (Scena)
                {
                    case Scene.Start:
                        Menu.Main(Proporzione, ref Finestra);
                        break;

                    case Scene.Options:
                        if (PreScena == Scene.Pause)//In base a Prescena deve stampare uno sfondo diverso
                        {
                            Gioco.Mappe[Gioco.nMap].DisegnoMappa(ref Finestra, Gioco.nMap);
                            Finestra.Draw(Gioco.Hunter.PG);
                            Finestra.Draw(Gioco.Ghosts.PG);
                        }
                        else if (PreScena == Scene.Start)
                        {
                            Finestra.Draw(Menu.Sfondo);
                        }
                        Opzioni.Main(ref Finestra);
                        break;

                    case Scene.Game:
                        Gioco.Main(ref Finestra, Volume);
                        if (Gioco.nMap == Gioco.Mappe.Length || Gioco.Hunter.PG.Position == Gioco.Ghosts.PG.Position)//se si prendono tutti i punti e si esce dal labirinto o si tocca il fandasfa si passa alla schermata di death
                        {
                            GameOver.xDeath(Gioco);
                            Scena = Scene.Death;
                            Gioco.TimerPlayer.Stop();
                            Gioco.TimerBot.Stop();
                        }
                        break;

                    case Scene.Pause:
                        //Il menù di pausa dovrà avere come sfondo il gioco ma bloccato (idem le options se si accede da qui)
                        Gioco.Mappe[Gioco.nMap].DisegnoMappa(ref Finestra, Gioco.nMap);
                        Finestra.Draw(Gioco.Hunter.PG);
                        Finestra.Draw(Gioco.Ghosts.PG);

                        Pausa.Main(ref Finestra);
                        break;

                    case Scene.Death:
                        GameOver.Main(ref Finestra);
                        break;
                }

                Finestra.Display();
            }
        }

        static void Premuto(object sender, MouseButtonEventArgs Tasto)
        {
            if (Scena == Scene.Start) //Se si è nella schermata di Start gestisce i sui bottoni
            {
                if (Menu.PressSTART(sender, Tasto)) //Controlla se il bottone START è premuto e fa avviare il gioco
                {
                    Scena = Scene.Game;
                }
                else if (Menu.PressOPTIONS(sender, Tasto)) //Controlla se il bottone OPTIONS è premuto
                {
                    Scena = Scene.Options;
                    PreScena = Scene.Start; //Se si accede ad Options da Start bisognerà stampare lo sfondo di Start
                }
                else if (Menu.PressQUIT(sender, Tasto)) //Controlla se il bottone QUIT è premuto
                    Finestra.Close();
            }
            else if (Scena == Scene.Options) //Se si è nella schermata di Options gestisce i sui bottoni
            {
                FileStream stream = File.Open(@"..\..\..\RunStole\FileDAT\Volume.dat", FileMode.Open);
                BinaryWriter bw = new BinaryWriter(stream);
                if (Opzioni.ControlloRETURN(sender, Tasto) && PreScena == Scene.Start) //Controlla se il bottone RETURN è premuto, se si è entrati da Start ritorna in Start
                    Scena = Scene.Start;
                else if (Opzioni.ControlloRETURN(sender, Tasto) && PreScena == Scene.Pause) //Controlla se il bottone RETURN è premuto, se si è entrati da Pause ritorna in Pause
                    Scena = Scene.Pause;
                else if (Opzioni.ControlloMENO(sender, Tasto)) //Controlla se il bottone MENO è premuto, se premuto diminuisce di 10 l'audio
                {
                    if (Volume != 0) //L'audio non può scendere sotto lo 0
                    {
                        Volume -= 10;
                        Opzioni.ScrittaVolume = new Text(Convert.ToString(Volume), Opzioni.font, 45);
                        Opzioni.ScrittaVolume.Position = new Vector2f(5 * Proporzione, 8.2f * Proporzione);
                    }
                }
                else if (Opzioni.ControlloPIU(sender, Tasto)) //Controlla se il bottone PIU' è premuto, se premuto aumenta di 10 l'audio
                {
                    if (Volume != 100) //L'audio non può salire sopra il 100
                    {
                        Volume += 10;
                        Opzioni.ScrittaVolume = new Text(Convert.ToString(Volume), Opzioni.font, 45);
                        Opzioni.ScrittaVolume.Position = new Vector2f(5 * Proporzione, 8.2f * Proporzione);
                    }
                }
                bw.Write(Volume);
                ColonnaSonora.Volume = Volume;
                bw.Close();
                stream.Close();
            }
            else if (Scena == Scene.Pause) //Se si è nella schermata di Pause gestisce i sui bottoni
            {
                if (Pausa.PressCONTINUE(sender, Tasto)) //Controlla se il bottone CONTINUE è premuto, se premuto fa ripartire il gioco
                {
                    Gioco.TimerPlayer.Start();
                    Scena = Scene.Game;
                }
                else if (Pausa.PressOPTIONS(sender, Tasto)) //Controlla se il bottone OPTIONS è premuto
                {
                    Scena = Scene.Options;
                    PreScena = Scene.Pause; //Se si accede ad Options da Pause bisognerà stampare lo sfondo di Pause
                }
                else if (Pausa.PressQUIT(sender, Tasto))
                    Finestra.Close();
            }
            else if (Scena == Scene.Death)
            {
                if (GameOver.PressReTry(sender, Tasto)) //Controllo se il bottone ReTry è premuto, se premuto fa ripartire il gioco
                {
                    Gioco.xGame();
                    Scena = Scene.Game;
                    Gioco.Hunter.Coin = 0;
                }
                else if (GameOver.PressQUIT(sender, Tasto)) //Controlla se il bottone QUIT è premuto
                    Finestra.Close();
            }
        }

        //Se la tastiera è premuta è Scena=Scene.Game dovrà succedere qualcosa
        static void ChiavePremuta(object sender, KeyEventArgs Tasto)
        {
            if (Scena == Scene.Game)
            {
                if (Tasto.Code != Keyboard.Key.P)
                {
                    Gioco.ChiavePremuta(sender, Tasto);
                }
                else if (Tasto.Code == Keyboard.Key.P) //Se si preme Invio il gioco va messo in pausa
                {
                    Gioco.TimerPlayer.Stop();
                    Scena = Scene.Pause;
                }
            }
            else if (Scena == Scene.Pause && Tasto.Code == Keyboard.Key.P)
            {
                Gioco.TimerPlayer.Start();
                Scena = Scene.Game;
            }
            else if (Scena == Scene.Options && PreScena == Scene.Pause && Tasto.Code == Keyboard.Key.P) 
            {
                Gioco.TimerPlayer.Start();
                Scena = Scene.Game;
            }
        }
    }
}
