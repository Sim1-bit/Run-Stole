using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.IO;

namespace RunStole
{
    class Mappa
    {
        const int Proporzione = 45;

        public float[,] Map = new float[17, 13];  //Array che conterrà la mappa

        //Sprite che si occuperà del disegno della mappa
        static IntRect Rect = new IntRect(0, 0, 750, 750);
        static Texture TextureTerreno = new Texture(@"..\..\..\RunStole\FilePNG\Gioco\Overworld.png", Rect);
        public Sprite Terreno = new Sprite(TextureTerreno);

        //Numero di monete contenute nella mappa
        public int nCoin;

        public void xMappe(ref int Base, ref bool Presenza, int nMap)
        {
            //In base alla mappa avrà un'immagine di basw
            if (nMap == 2)
                Terreno = new Sprite(new Texture(@"..\..\..\RunStole\FilePNG\Gioco\Cave.png", Rect));
            else if (nMap == 3)
                Terreno = new Sprite(new Texture(@"..\..\..\RunStole\FilePNG\Gioco\Castel.png", Rect));

            Terreno.Scale = new Vector2f((2 * Proporzione) / 3 / 10, (2 * Proporzione) / 3 / 10);

            if (File.Exists(@"..\..\..\RunStole\FileDAT\Mappe.dat") && Presenza) //Se il file non esiste lo va a riscrevere
            {
                FileStream stream = File.Open(@"..\..\..\RunStole\FileDAT\Mappe.dat", FileMode.Open);
                BinaryReader br = new BinaryReader(stream);

                br.BaseStream.Position = Base;

                for (int i = 0; i < 17; i++)
                    for (int j = 0; j < 13; j++)
                    {
                        Map[i, j] = br.ReadSingle();
                    }
                Base = Convert.ToInt32(br.BaseStream.Position);
                br.Close();
            }
            else if (!File.Exists(@"..\..\..\RunStole\FileDAT\Mappe.dat") || !Presenza)
                switch (nMap)
                {
                    case 0:
                        Presenza = false;
                        xMappa0(ref Base);
                        break;
                    case 1:
                        xMappa1(ref Base);
                        break;
                    case 2:
                        xMappa2(ref Base);
                        break;
                    case 3:
                        xMappa3(ref Base);
                        break;
                }

            CountCoin();
        }
        public void xMappa0(ref int Base) //Si usa se il file non esisteva
        {
            float[,] Map0 = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                              { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                              { 0, 1, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 0 },
                              { 0, 1, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 0 },
                              { 0, 1, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 0 },
                              { 0, 1, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 0 },
                              { 0, 1, 0, 1, 1, 1, 7, 1, 1, 1, 0, 1, 0 },
                              { 0, 1, 0, 1, 0, 0, 4, 0, 0, 1, 0, 1, 0 },
                              { 0, 1, 1, 1, 0, 0, 4, 0, 0, 1, 1, 1, 0 },
                              { 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0 },
                              { 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0 },
                              { 0, 1, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 0 },
                              { 0, 1, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 0 },
                              { 0, 1, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 0 },
                              { 0, 1, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 0 },
                              { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                              { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };

            Map = Map0;

            //-----------------------------------------------------------------
            FileStream stream = File.Open(@"..\..\..\RunStole\FileDAT\Mappe.dat", FileMode.Create);
            BinaryWriter bw = new BinaryWriter(stream);

            bw.BaseStream.Position = Base;

            for (int i = 0; i < 17; i++)
                for (int j = 0; j < 13; j++)
                {
                    bw.Write(Map[i, j]);
                }
            Base = Convert.ToInt32(bw.BaseStream.Position);
            bw.Close();
            //-----------------------------------------------------------------
        }
        public void xMappa1(ref int Base) //Si usa se il file non esisteva
        {
            float[,] Map1 = { { 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f },
                              { 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f },
                              { 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f },
                              { 8.0f, 8.1f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 2.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 2.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 0.0f, 1.0f, 1.0f, 1.0f, 7.0f, 1.0f, 1.0f, 1.0f, 0.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 2.0f, 1.0f, 0.0f, 0.0f, 4.0f, 0.0f, 0.0f, 1.0f, 2.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 0.0f, 1.0f, 0.0f, 0.0f, 4.0f, 0.0f, 0.0f, 1.0f, 0.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 2.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 2.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f, 0.0f, 1.0f, 0.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 2.0f, 1.0f, 2.0f, 1.0f, 0.0f, 1.0f, 2.0f, 1.0f, 2.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 2.0f, 2.0f, 2.0f, 1.0f, 0.0f, 1.0f, 2.0f, 2.0f, 2.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 8.5f, 8.0f },
                              { 8.0f, 8.2f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.4f, 8.0f },
                              { 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f } };

            Map = Map1;

            //-----------------------------------------------------------------
            FileStream stream = File.Open(@"..\..\..\RunStole\FileDAT\Mappe.dat", FileMode.Append);
            BinaryWriter bw = new BinaryWriter(stream);

            bw.BaseStream.Position = Base;

            for (int i = 0; i < 17; i++)
                for (int j = 0; j < 13; j++)
                {
                    bw.Write(Map[i, j]);
                }
            Base = Convert.ToInt32(bw.BaseStream.Position);
            bw.Close();
            //-----------------------------------------------------------------
        }
        public void xMappa2(ref int Base) //Si usa se il file non esisteva
        {
            float[,] Map2 = { { 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f },
                              { 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f },
                              { 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f },
                              { 8.1f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 8.5f },
                              { 8.1f, 2.0f, 0.0f, 2.0f, 0.0f, 0.0f, 2.0f, 0.0f, 0.0f, 2.0f, 0.0f, 2.0f, 8.5f },
                              { 8.1f, 2.0f, 0.0f, 2.0f, 0.0f, 0.0f, 2.0f, 0.0f, 0.0f, 2.0f, 0.0f, 2.0f, 8.5f },
                              { 8.1f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 7.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 8.5f },
                              { 8.1f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 4.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 8.5f },
                              { 8.1f, 1.0f, 1.0f, 2.0f, 1.0f, 0.0f, 4.0f, 0.0f, 1.0f, 2.0f, 1.0f, 1.0f, 8.5f },
                              { 8.1f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 8.5f },
                              { 8.1f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 8.5f },
                              { 8.1f, 0.0f, 2.0f, 0.0f, 1.0f, 0.0f, 2.0f, 0.0f, 1.0f, 0.0f, 2.0f, 0.0f, 8.5f },
                              { 8.1f, 2.0f, 1.0f, 2.0f, 1.0f, 0.0f, 2.0f, 0.0f, 1.0f, 2.0f, 1.0f, 2.0f, 8.5f },
                              { 8.1f, 0.0f, 2.0f, 0.0f, 1.0f, 0.0f, 2.0f, 0.0f, 1.0f, 0.0f, 2.0f, 0.0f, 8.5f },
                              { 8.1f, 2.0f, 1.0f, 2.0f, 1.0f, 0.0f, 2.0f, 0.0f, 1.0f, 2.0f, 1.0f, 2.0f, 8.5f },
                              { 8.1f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 8.5f },
                              { 8.2f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.4f } };

            Map = Map2;

            //-----------------------------------------------------------------
            FileStream stream = File.Open(@"..\..\..\RunStole\FileDAT\Mappe.dat", FileMode.Append);
            BinaryWriter bw = new BinaryWriter(stream);

            bw.BaseStream.Position = Base;

            for (int i = 0; i < 17; i++)
                for (int j = 0; j < 13; j++)
                {
                    bw.Write(Map[i, j]);
                }
            Base = Convert.ToInt32(bw.BaseStream.Position);
            bw.Close();
            //-----------------------------------------------------------------
        }
        public void xMappa3(ref int Base) //Si usa se il file non esisteva
        {
            float[,] Map3 = { { 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f, 6.3f },
                              { 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.5f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f, 6.2f },
                              { 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.4f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f, 6.1f },
                              { 8.0f, 8.1f, 2.0f, 2.0f, 1.0f, 1.0f, 2.0f, 1.0f, 1.0f, 2.0f, 2.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 0.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 0.0f, 1.0f, 1.0f, 1.0f, 7.0f, 1.0f, 1.0f, 1.0f, 0.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 1.0f, 1.0f, 0.0f, 0.0f, 4.0f, 0.0f, 0.0f, 1.0f, 1.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 0.0f, 1.0f, 1.0f, 0.0f, 4.0f, 0.0f, 1.0f, 1.0f, 0.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 0.0f, 2.0f, 0.0f, 0.0f, 4.0f, 0.0f, 0.0f, 2.0f, 0.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 0.0f, 2.0f, 0.0f, 1.0f, 1.0f, 1.0f, 0.0f, 2.0f, 0.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 0.0f, 2.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 2.0f, 0.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 2.0f, 1.0f, 2.0f, 1.0f, 0.0f, 1.0f, 2.0f, 1.0f, 2.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 0.0f, 2.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 2.0f, 0.0f, 8.5f, 8.0f },
                              { 8.0f, 8.1f, 0.0f, 2.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 2.0f, 0.0f, 8.5f, 8.0f },
                              { 8.0f, 8.2f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.3f, 8.4f, 8.0f },
                              { 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f, 8.0f } };

            Map = Map3;

            //-----------------------------------------------------------------
            FileStream stream = File.Open(@"..\..\..\RunStole\FileDAT\Mappe.dat", FileMode.Append);
            BinaryWriter bw = new BinaryWriter(stream);

            bw.BaseStream.Position = Base;

            for (int i = 0; i < 17; i++)
                for (int j = 0; j < 13; j++)
                {
                    bw.Write(Map[i, j]);
                }
            Base = Convert.ToInt32(bw.BaseStream.Position);
            bw.Close();
            //-----------------------------------------------------------------
        }
 
        public void CountCoin() //Conta le monete della mappa
        {
            int nCoin1 = 0;
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    if (Map[j, i] == 2)
                        nCoin1++;    
                }
            }
            nCoin = nCoin1;
        }
        public void DisegnoMappa(ref RenderWindow Finestra, int nMap) //Disegna la mappa
        {
            for (int cont = 0; cont < 2; cont++)
            {
                if (cont == 0)
                    Terreno.TextureRect = new IntRect(0, 0, 16, 16);
                Vector2f Pos = new Vector2f(0, 0);
                for (int i = 0; i < 13; i++, Pos.X += Proporzione)
                {
                    for (int j = 0; j < 17; j++, Pos.Y += Proporzione)
                    {
                        if (cont == 0)
                            Terreno.TextureRect = new IntRect(0, 0, 16, 16);
                        Terreno.Position = Pos;

                        switch (Map[j, i])
                        {
                            case 0.0f: //Cespuglio
                                if (cont != 0)
                                    Terreno.TextureRect = new IntRect(32, 224, 16, 16);
                                break;
                            case 2.0f: //Moneta
                                if (cont != 0)
                                    Terreno.TextureRect = new IntRect(499, 455, 16, 16);
                                break;
                            case 5.0f: //Porta per fine livello
                                if (cont == 1 && (nMap >= 1 && nMap < 3))
                                    Terreno.TextureRect = new IntRect(112, 191, 16, 16);
                                else if (nMap == 3)
                                    Terreno.TextureRect = new IntRect(128, 191, 16, 16);
                                break;
                            case 5.1f:
                                if (cont == 1 && nMap == 3)
                                    Terreno.TextureRect = new IntRect(128, 176, 16, 16);
                                break;
                            case 6.1f: //Roccia 1 o Muro 1
                                if (cont != 0)
                                    Terreno.TextureRect = new IntRect(80, 224, 16, 16);
                                break;
                            case 6.2f: //Roccia 2 o Muro 2
                                if (cont != 0)
                                    Terreno.TextureRect = new IntRect(80, 208, 16, 16);
                                break;
                            case 6.3f: //Roccia con erba sopra o Sopre Mura
                                if (cont != 0)
                                    Terreno.TextureRect = new IntRect(80, 176, 16, 16);
                                break;
                            case 6.4f: //Roccia con erba sopra o Sopre Mura
                                if (cont != 0)
                                    Terreno.TextureRect = new IntRect(112, 191, 16, 16);
                                break;
                            case 6.5f: //Roccia con erba sopra o Sopre Mura
                                if (cont != 0)
                                    Terreno.TextureRect = new IntRect(112, 176, 16, 16);
                                break;
                            case 8.0f: //Acqua
                                Terreno.TextureRect = new IntRect(48, 64, 16, 16);
                                break;
                            case 8.1f: //Terra con acqua a sinistra
                                Terreno.TextureRect = new IntRect(64, 112, 16, 16);
                                break;
                            case 8.2f: //Acqua con angolo di terra in alto a destra
                                Terreno.TextureRect = new IntRect(32, 159, 16, 16);
                                break;
                            case 8.3f: //Terra con acqua in basso
                                Terreno.TextureRect = new IntRect(48, 96, 16, 16);
                                break;
                            case 8.4f: //Acqua con angolo di terra in alto a sinistra
                                Terreno.TextureRect = new IntRect(48, 160, 16, 16);
                                break;
                            case 8.5f: //Terra con acqua a destra
                                Terreno.TextureRect = new IntRect(32, 112, 16, 16);
                                break;
                        }

                        //Disegna solo se ci sono le celle che servono per il disegno
                        if (cont == 0 || Map[j, i] == 0.0f || Map[j, i] == 2 || Map[j, i] >= 6.0f || Map[j, i] >= 5.0f && (nMap == 1 || nMap == 2 || nMap == 3))  
                            Finestra.Draw(Terreno);
                    }
                    Pos.Y = 0;
                }
            }
        }
    }
}