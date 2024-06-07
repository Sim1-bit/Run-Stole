using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RunStole
{
    class Options
    {
        const int Proporzione = 45;

        public Text RETURN = new Text();
        public Text COMMANDS = new Text();

        public Font font = new Font("../../../RunStole/FileTTF/PressStart2P-Regular.ttf");

        struct Comandi //Per mostrare i comandi è necessario che la lettera sia stampata su un quadrato bianco
        {
            public RectangleShape Tasto; //Quadrato
            public Text Pulsante; //Lettera
        }
        static Comandi[] Commands = new Comandi[5]; //% come i possibili comandi

        struct SetVolume
        {
            public CircleShape Rotella;
            public Text Segno;
        }
        static SetVolume[] Manopole = new SetVolume[2];

        public Text ScrittaVolume = new Text();
        public Text VOLUME = new Text();

        
        public void Main(ref RenderWindow Finestra) //Si occupa di disegnare la finestra
        {
            Finestra.Draw(RETURN);
            Finestra.Draw(COMMANDS);
            for (int i = 0; i < Commands.Length; i++)
            {
                Finestra.Draw(Commands[i].Tasto);
                Finestra.Draw(Commands[i].Pulsante);
            }
            for (int i = 0; i < Manopole.Length; i++)
            {
                Finestra.Draw(Manopole[i].Rotella);
                Finestra.Draw(Manopole[i].Segno);
            }
            Finestra.Draw(VOLUME);
            Finestra.Draw(ScrittaVolume);
        }
        public void xOptions(int Volume) //Inizializza i valori che permettono una corretta visualizzazione del menu
        {
            RETURN = new Text("Return", font, 45);
            RETURN.FillColor = new Color(255, 0, 0);
            RETURN.Position = new Vector2f(3.5f * Proporzione, 11 * Proporzione);

            COMMANDS = new Text("Commands", font, 30);
            COMMANDS.FillColor = new Color(255, 255, 255);
            COMMANDS.Position = new Vector2f(3.75f * Proporzione, 1.5f * Proporzione);

            VOLUME = new Text("Volume", font, 30);
            VOLUME.FillColor = new Color(255, 255, 255);
            VOLUME.Position = new Vector2f(4.5f * Proporzione, 6.5f * Proporzione);

            ScrittaVolume = new Text(Convert.ToString(Volume), font, 45);
            ScrittaVolume.FillColor = new Color(255, 255, 255);
            ScrittaVolume.Position = new Vector2f(5 * Proporzione, 8.2f * Proporzione);

            for (int i = 0; i < Commands.Length; i++)
            {
                Commands[i].Tasto = new RectangleShape(new Vector2f(50, 50));
                Commands[i].Tasto.FillColor = new Color(255, 255, 255);

                switch (i)
                {
                    case 0:
                        Commands[i].Tasto.Position = new Vector2f(5.75f * Proporzione, 3.25f * Proporzione);
                        Commands[i].Pulsante = new Text("W", font, 40);
                        break;
                    case 1:
                        Commands[i].Tasto.Position = new Vector2f(4.5f * Proporzione, 4.5f * Proporzione);
                        Commands[i].Pulsante = new Text("A", font, 40);
                        break;
                    case 2:
                        Commands[i].Tasto.Position = new Vector2f(5.75f * Proporzione, 4.5f * Proporzione);
                        Commands[i].Pulsante = new Text("S", font, 40);
                        break;
                    case 3:
                        Commands[i].Tasto.Position = new Vector2f(7 * Proporzione, 4.5f * Proporzione);
                        Commands[i].Pulsante = new Text("D", font, 40);
                        break;
                    default:
                        Commands[i].Tasto.Position = new Vector2f(9 * Proporzione, 4.5f * Proporzione);
                        Commands[i].Pulsante = new Text("P", font, 40);
                        break;
                }
                Commands[i].Pulsante.Position = new Vector2f(Commands[i].Tasto.Position.X + 0.17f * Proporzione, Commands[i].Tasto.Position.Y + 0.17f * Proporzione);
                Commands[i].Pulsante.FillColor = new Color(0, 0, 0);
            }

            for (int i = 0; i < Manopole.Length; i++)
            {
                Manopole[i].Rotella = new CircleShape(25);
                Manopole[i].Rotella.FillColor = new Color(255, 0, 0);
                switch (i)
                {
                    case 0:
                        Manopole[i].Segno = new Text("-", font, 40);
                        Manopole[i].Rotella.Position = new Vector2f(3.5f * Proporzione, 8 * Proporzione);
                        break;
                    case 1:
                        Manopole[i].Segno = new Text("+", font, 40);
                        Manopole[i].Rotella.Position = new Vector2f(8.4f * Proporzione, 8 * Proporzione);
                        break;
                }
                Manopole[i].Segno.Position = new Vector2f(Manopole[i].Rotella.Position.X + 0.1f * Proporzione, Manopole[i].Rotella.Position.Y + 0.15f * Proporzione);
                Manopole[i].Segno.FillColor = new Color(0, 0, 0);
            }
        }

        public bool ControlloRETURN(object sender, MouseButtonEventArgs Tasto) //Controlla se il bottone RETURN è premuto
        {
            if (Tasto.X >= RETURN.Position.X && Tasto.X <= RETURN.Position.X + 6 * Proporzione && Tasto.Y >= RETURN.Position.Y && Tasto.Y <= RETURN.Position.Y + Proporzione)
                return true;
            else
                return false;
        }
        public bool ControlloMENO(object sender, MouseButtonEventArgs Tasto) //Controlla se il bottone MENO per il volume è premuto
        {
            if (Tasto.X >= Manopole[0].Rotella.Position.X && Tasto.X <= Manopole[0].Rotella.Position.X + 50 && Tasto.Y >= Manopole[0].Rotella.Position.Y && Tasto.Y <= Manopole[0].Rotella.Position.Y + 50)
                return true;
            else
                return false;
        }
        public bool ControlloPIU(object sender, MouseButtonEventArgs Tasto) //Controlla se il bottone PIU' per il volume è premuto
        {
            if (Tasto.X >= Manopole[1].Rotella.Position.X && Tasto.X <= Manopole[1].Rotella.Position.X + 50 && Tasto.Y >= Manopole[1].Rotella.Position.Y && Tasto.Y <= Manopole[1].Rotella.Position.Y + 50)
                return true;
            else
                return false;
        }
    }
}
