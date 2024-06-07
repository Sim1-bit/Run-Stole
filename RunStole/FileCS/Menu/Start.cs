using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RunStole
{
    class Start
    {
        const int Proporzione = 45;

        public Text RUNSTOLE = new Text();

        public Text START = new Text();
        public Text OPTIONS = new Text();
        public Text QUIT = new Text();

        public Sprite Sfondo = new Sprite(new Texture(@"..\..\..\RunStole\FilePNG\Menu\Start.png", new IntRect(385, 0, 1420, 1763)));
        public void Main(int Proporzione, ref RenderWindow Finestra) //Si occupa di disegnare la finestra
        {
            Finestra.Draw(Sfondo);

            Finestra.Draw(RUNSTOLE);
            Finestra.Draw(START);
            Finestra.Draw(OPTIONS);
            Finestra.Draw(QUIT);
        }
        public void xMenu() //Inizializza i valori che permettono una corretta visualizzazione del menu
        {
            Font font = new Font("../../../RunStole/FileTTF/PressStart2P-Regular.ttf");

            Sfondo.Position = new Vector2f(0, 0);
            Sfondo.Scale = new Vector2f(0.412f, 0.435f);

            RUNSTOLE = new Text("Run & Stole", font, 50);
            RUNSTOLE.FillColor = new Color(255, 255, 255);
            RUNSTOLE.Position = new Vector2f(0.5f * Proporzione, 0.5f * Proporzione);

            START = new Text("Start", font, 45);
            START.FillColor = new Color(255, 0, 0);
            START.Position = new Vector2f(4 * Proporzione, 5 * Proporzione);

            OPTIONS = new Text("Options", font, 45);
            OPTIONS.FillColor = new Color(255, 0, 0);
            OPTIONS.Position = new Vector2f(3 * Proporzione, 8 * Proporzione);

            QUIT = new Text("Quit", font, 45);
            QUIT.FillColor = new Color(255, 0, 0);
            QUIT.Position = new Vector2f(4.5f * Proporzione, 11 * Proporzione);
        }

        public bool PressSTART(object sender, MouseButtonEventArgs Tasto) //Controlla se il bottone START è premuto
        {
            if (Tasto.X >= START.Position.X && Tasto.X <= START.Position.X + 5 * Proporzione && Tasto.Y >= START.Position.Y && Tasto.Y <= START.Position.Y + Proporzione)
                return true;
            else
                return false;
        }
        public bool PressOPTIONS(object sender, MouseButtonEventArgs Tasto) //Controlla se il bottone OPTIONS è premuto
        {
            if (Tasto.X >= OPTIONS.Position.X && Tasto.X <= OPTIONS.Position.X + 7 * Proporzione && Tasto.Y >= OPTIONS.Position.Y && Tasto.Y <= OPTIONS.Position.Y + Proporzione)
                return true;
            else
                return false;
        }
        public bool PressQUIT(object sender, MouseButtonEventArgs Tasto) //Controlla se il bottone QUIT è premuto
        {
            if (Tasto.X >= QUIT.Position.X && Tasto.X <= QUIT.Position.X + 4 * Proporzione && Tasto.Y >= QUIT.Position.Y && Tasto.Y <= QUIT.Position.Y + Proporzione)
                return true;
            else
                return false;
        }
    }
}
