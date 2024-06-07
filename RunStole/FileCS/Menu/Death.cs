using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.IO;

namespace RunStole
{
    class Death
    {
        const int Proporzione = 45;

        public Font font = new Font("../../../RunStole/FileTTF/PressStart2P-Regular.ttf");
        
        public Text GameOver = new Text();
        public Text Points = new Text();
        public Text ReTry = new Text();
        public Text QUIT = new Text();

        public bool Vittoria;

        public Sprite Sfondo = new Sprite(new Texture(@"..\..\..\RunStole\FilePNG\Menu\End.png", new IntRect(0, 0, 208, 272)));

        public void xDeath(Game Gioco) //Inizializza i valori che permettono una corretta visualizzazione del menu di game over
        {
            Sfondo.Position = new Vector2f(0, 0);
            Sfondo.Scale = new Vector2f(2.82f, 2.82f);

            GameOver = new Text("Game Over", font, 50);
            GameOver.Position = new Vector2f(1.5f * Proporzione, 1.5f * Proporzione);

            Points = new Text(("Points: " + Convert.ToString(Gioco.Hunter.Coin)), font, 30);
            Points.Position = new Vector2f(1.5f * Proporzione, 3.5f * Proporzione);
            Points.FillColor = new Color(255, 255, 255);

            ReTry = new Text("Try Again", font, 30);
            ReTry.Position = new Vector2f(1.5f * Proporzione, 11.5f * Proporzione);

            QUIT = new Text("Quit", font, 30);
            QUIT.Position = new Vector2f(1.5f * Proporzione, 12.5f * Proporzione);

            //In base all'esito (se si vince e si perde) il colore delle scritte cambia
            if (Gioco.nMap == 4)
            {
                GameOver.FillColor = new Color(0, 255, 0);
                ReTry.FillColor = new Color(0, 0, 0);
                QUIT.FillColor = new Color(0, 0, 0);

                Vittoria = true;
            }
            else
            {
                GameOver.FillColor = new Color(255, 0, 0); 
                ReTry.FillColor = new Color(255, 0, 0);
                QUIT.FillColor = new Color(255, 0, 0);

                Vittoria = false;
            }
        }

        public void Main(ref RenderWindow Finestra) //Si occupa di disegnare la finestra
        {
            if (Vittoria) //Se si vince di stampa lo sfondo altrimenti no
                Finestra.Draw(Sfondo);

            Finestra.Draw(GameOver);
            Finestra.Draw(Points);
            Finestra.Draw(ReTry);
            Finestra.Draw(QUIT);
        }

        public bool PressReTry(object sender, MouseButtonEventArgs Tasto)
        {
            if (Tasto.X >= ReTry.Position.X && Tasto.X <= ReTry.Position.X + 9 * 30 && Tasto.Y >= ReTry.Position.Y && Tasto.Y <= ReTry.Position.Y + 30)
                return true;
            else
                return false;
        }

        public bool PressQUIT(object sender, MouseButtonEventArgs Tasto)
        {
            if (Tasto.X >= QUIT.Position.X && Tasto.X <= QUIT.Position.X + 4 * 30 && Tasto.Y >= QUIT.Position.Y && Tasto.Y <= QUIT.Position.Y + 30)
                return true;
            else
                return false;
        }
    }
}
