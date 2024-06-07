using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RunStole
{
    class Pause
    {
        const int Proporzione = 45;

        public Text CONTINUE = new Text();
        public Text OPTIONS = new Text();
        public Text QUIT = new Text();
        public void Main(ref RenderWindow Finestra) //Si occupa di disegnare la finestra
        {
            Finestra.Draw(CONTINUE);
            Finestra.Draw(OPTIONS);
            Finestra.Draw(QUIT);
        }
        public void xPause() //Inizializza i valori che permettono una corretta visualizzazione del menu pausa
        {
            Font font = new Font("../../../RunStole/FileTTF/PressStart2P-Regular.ttf");
            CONTINUE = new Text("Continue", font, 45);
            CONTINUE.FillColor = new Color(255, 255, 255);
            CONTINUE.Position = new Vector2f(2.5f * Proporzione, 5 * Proporzione);

            OPTIONS = new Text("Options", font, 45);
            OPTIONS.FillColor = new Color(255, 255, 255);
            OPTIONS.Position = new Vector2f(3 * Proporzione, 8 * Proporzione);

            QUIT = new Text("Quit", font, 45);
            QUIT.FillColor = new Color(255, 255, 255);
            QUIT.Position = new Vector2f(4.5f * Proporzione, 11 * Proporzione);
        }

        public bool PressCONTINUE(object sender, MouseButtonEventArgs Tasto) //Controlla se il bottone continua è premuto
        {
            if (Tasto.X >= CONTINUE.Position.X && Tasto.X <= CONTINUE.Position.X + 8 * Proporzione && Tasto.Y >= CONTINUE.Position.Y && Tasto.Y <= CONTINUE.Position.Y + Proporzione)
                return true;
            else
                return false;
        }

        public bool PressOPTIONS(object sender, MouseButtonEventArgs Tasto) //Controllo se il bottone options è premuto
        {
            if (Tasto.X >= OPTIONS.Position.X && Tasto.X <= OPTIONS.Position.X + 7 * Proporzione && Tasto.Y >= OPTIONS.Position.Y && Tasto.Y <= OPTIONS.Position.Y + Proporzione)
                return true;
            else
                return false;
        }

        public bool PressQUIT(object sender, MouseButtonEventArgs Tasto) //Controllo se il bottone quit è premuto
        {
            if (Tasto.X >= QUIT.Position.X && Tasto.X <= QUIT.Position.X + 4 * Proporzione && Tasto.Y >= QUIT.Position.Y && Tasto.Y <= QUIT.Position.Y + Proporzione)
                return true;
            else
                return false;
        }
    }
}
