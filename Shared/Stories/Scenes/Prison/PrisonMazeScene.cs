using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Sounds;
using Nsnbc.Stories.Scenes.Xml;
using Nsnbc.Texts;

namespace Nsnbc.Stories.Scenes.Prison
{
    [JsonObject(MemberSerialization.Fields)]
    public class PrisonMazeScene : XmlScene
    {
        public bool FigurePlaced;
        public bool TrapAlreadyTriggered;
        public Point WhereIsTheBall = new Point(1,1);
        public static Point StartPoint { get; set; } = new Point(1,1);

        static Rectangle rectTable = new Rectangle(101,20,1699,754);

        public override bool HideInventory => FigurePlaced;

        public override void Draw(AirSession airSession)
        {
            base.Draw(airSession);

            int sX = 170;
            int sY = 60;
            const int tilesize = 100;
            int thickness = 6;
            
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    Rectangle rectTile = new Rectangle(sX + x * tilesize, sY + y * tilesize, tilesize, tilesize);
                    Maze.MazeTile tile = Maze.Map[x, y];
                    if (tile.WallLeft)
                    {
                        Primitives.FillRectangle(new Rectangle(rectTile.X -thickness/2, rectTile.Y, thickness, rectTile.Height), Color.Black);
                    }
                    if (tile.WallRight)
                    {
                        Primitives.FillRectangle(new Rectangle(rectTile.Right -thickness/2, rectTile.Y, thickness, rectTile.Height), Color.Black);
                    }
                    if (tile.WallUp)
                    {
                        Primitives.FillRectangle(new Rectangle(rectTile.X, rectTile.Y - thickness/2, rectTile.Width, thickness), Color.Black);
                    }
                    if (tile.WallDown)
                    {
                        Primitives.FillRectangle(new Rectangle(rectTile.X, rectTile.Bottom - thickness/2, rectTile.Width, thickness), Color.Black);
                    }

                    if (x == WhereIsTheBall.X && y == WhereIsTheBall.Y)
                    {
                        Primitives.FillCircle(new Vector2(rectTile.X + tilesize /2, rectTile.Y + tilesize/2), 40, Color.Black );
                    }

                    if (tile.Hint != null)
                    {
                        Writer.DrawString(tile.Hint, rectTile.Extend(-4, -4), Color.Black, BitmapFontGroup.Main12, Writer.TextAlignment.Middle);
                    }
                }
            }

            if (FigurePlaced)
            {
                int size = 180;
                int y = 350;
                int finalX = 1790 - size;
                Ux.DrawButton(new Rectangle(finalX - size, y, size,size),  G.T("nahoru"), () =>
                {
                    MoveBall(0, -1, airSession);
                }, alignment: Writer.TextAlignment.Middle, font: BitmapFontGroup.Main24);
                Ux.DrawButton(new Rectangle(finalX - 2*size, y+size, size,size),  G.T("doleva"), () =>
                {
                    MoveBall(-1, 0, airSession);
                    
                }, alignment: Writer.TextAlignment.Middle, font: BitmapFontGroup.Main24);
                Ux.DrawButton(new Rectangle(finalX - size, y+size, size,size),  G.T("dolu"), () =>
                {
                    
                    MoveBall(0, 1, airSession);
                }, alignment: Writer.TextAlignment.Middle, font: BitmapFontGroup.Main24);
                Ux.DrawButton(new Rectangle(finalX, y+size, size,size),  G.T("doprava"), () =>
                {
                    MoveBall(1, 0, airSession);
                    
                }, alignment: Writer.TextAlignment.Middle, font: BitmapFontGroup.Main24);
            }
        }

        private void MoveBall(int x, int y, AirSession airSession)
        {
            bool movedAtLeastOnce = false;
            while (true)
            {
                Maze.MazeTile currentTile = Maze.Map[WhereIsTheBall.X, WhereIsTheBall.Y];
                if (currentTile.Trapped)
                {
                    if (TrapAlreadyTriggered)
                    {
                        airSession.Enqueue(new Script
                        {
                            Events =
                            {
                                QSpeak.Quick("Spustila se past. Magnet se propadl."),
                                new QImmediateAction(() => { WhereIsTheBall = StartPoint; }),
                                QSpeak.Quick("a objevil se znovu na začátku.")
                            }
                        });
                    }
                    else
                    {
                        TrapAlreadyTriggered = true;
                        airSession.Enqueue(new Script
                        {
                            Events =
                            {
                                QSpeak.From("Tišík", Pose.Afraid, "Huh? Co se stalo? Proč se magnet zastavil?"),
                                QSpeak.From("Vědátor", Pose.Pointing, "Přestal držet na Brokkovi. Zdá se, že na tom políčku je ještě silnější magnet zespoda."),
                                new QImmediateAction(() => { WhereIsTheBall = new Point(-1,-1); }),
                                QSpeak.From("Vědátor", Pose.Afraid, "A teď se magnet propadnul do stolu!"),
                                new QImmediateAction(() => { WhereIsTheBall = StartPoint; }),
                                QSpeak.From("Tišík", Pose.Thinking, "A stůl ho vyplivnul zpátky na start."),
                                QSpeak.From("Tišík", Pose.Thinking, "Myslím, že to políčko je past. Museli jsme někde udělat chybu."),
                            }
                        });
                    }
                    return;
                }

                if (currentTile == Maze.Finish)
                {
                    airSession.Enqueue(new Script
                    {
                        Events =
                        {
                            new QSfx(SoundEffectName.TrezorOpen),
                           QSpeak.From("Tišík", Pose.Excited, "A jsme v cíli!"),
                           QSpeak.From("Vypravěč", Pose.Pointing, "Hele. Něco ze stolu vypadlo."),
                           new QSetInteractibleFirstAndSecondUse("bludiste", QSpeak.Quick("Bludištěm už jsme prošli. Nic dalšího z něj nedostanem.")),
                           new QPopScene(),
                           new QAddToInventory(ArtName.R1PrazdnyPapir, "Prázdný papír, ale jsou na něm skrvny vonící po citrónu."),
                           QSpeak.From("Tišík", Pose.Thinking, "To je... prázdný papír?"),
                           QSpeak.From("Vědátor", Pose.Amused, "Mno, tak to není moc užitečné."),
                           QSpeak.From("Tišík", Pose.Thinking, "Ale počkat... něco na něm přesto je. Jsou tu takové skoro neviditelné tekuté skvrny."),
                           QSpeak.From("Tišík", Pose.Thinking, "A... hm... voní to po citrónu?")
                        }
                    });
                    return;
                }
                
                bool isBlocked;
                if (x == -1) isBlocked = currentTile.WallLeft;
                else if (x == 1) isBlocked = currentTile.WallRight;
                else if (y == -1) isBlocked = currentTile.WallUp;
                else if (y == 1) isBlocked = currentTile.WallDown;
                else throw new Exception("Unknown direction.");
                if (isBlocked)
                {
                    if (!movedAtLeastOnce)
                    {
                        airSession.Enqueue(QSpeak.Quick("Cestu magnetu blokuje tímto směrem zeď."));
                    }
                    return;
                }

                WhereIsTheBall = new Point(WhereIsTheBall.X + x, WhereIsTheBall.Y + y);
                movedAtLeastOnce = true;
            }
        }

        public override bool Click(AirSession airSession)
        {
            if (!FigurePlaced)
            {
                if (airSession.Session.HeldItem != null)
                {
                    if (Root.IsMouseOver(rectTable))
                    {
                        if (airSession.Session.HeldItem.Art == ArtName.R1Figurka)
                        {
                            airSession.Enqueue(new Script
                            {
                                Events =
                                {
                                    new QRemoveHeldItem(),
                                    new QImmediateAction(() =>
                                    {
                                        FigurePlaced = true;
                                        WhereIsTheBall = StartPoint;
                                    }),
                                    QSpeak.From("Tišík", Pose.Excited, "Přisála se! Magnet vyskočil a drží se akční figurky Brokka!"),
                                    QSpeak.Quick("Teď můžu s magnetem pomocí figurky manipulovat."),
                                    QSpeak.Quick("Ale celkem to po skle klouže..."),
                                    QSpeak.Quick("...navíc, hm, možná záleží i na tom, jakou cestu zvolím?")
                                }
                            });
                        }
                        else
                        {
                            airSession.Enqueue(QSpeak.Quick("Tuhle věc u stolu s bludištěm nepoužiju."));
                        }
                        return true;
                    }
                }
            }
            return base.Click(airSession);
        }


        static class Maze
        {
            public class MazeTile
            {
                public bool WallLeft;
                public bool WallRight;
                public bool WallUp;
                public bool WallDown;
                public bool Trapped;

                public GString Hint { get; set; }
            }

            private static readonly string mazeSource = @"
  A B C D E F G H I J
 #####################
1#  a  #2       m .  #  
 # # # ######### ### #
2# #1#.#     #   #  .#
 # ### #   # #   ### #
3# #  b . f# #    k l#
 # # ###   # #     # #
4#  c#    e#      j# #
 ### #     ### #   ###
5#      . g#   #. i  #
 #     #   #   # ### #
6#  d  #  h         .#
 #####################".Trim();
            
            public static readonly MazeTile[,] Map = new MazeTile[10,6];

            public static readonly MazeTile Finish;

            static Maze()
            {
                string[] lines = mazeSource.Split('\n').Select(ln => ln.TrimEnd()).ToArray();
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 6; y++)
                    {
                        int tx = x * 2 + 2;
                        int ty = y * 2 + 2;
                        MazeTile tile = new MazeTile();
                        Map[x,y] = tile;
                        if (lines[ty][tx-1] == '#')
                        {
                            tile.WallLeft = true;
                        }
                        if (lines[ty][tx+1] == '#')
                        {
                            tile.WallRight = true;
                        }
                        if (lines[ty-1][tx] == '#')
                        {
                            tile.WallUp = true;
                        }
                        if (lines[ty+1][tx] == '#')
                        {
                            tile.WallDown = true;
                        }

                        switch (lines[ty][tx])
                        {
                            case ' ':
                                break;
                            case '.':
                                tile.Trapped = true;
                                break;
                            case '1':
                                tile.Hint = G.T("start");
                                break;
                            case '2':
                                Finish = tile;
                                tile.Hint = G.T("cíl");
                                break;
                            case 'a': tile.Hint = PL_Horizontal(1); break;
                            case 'b': tile.Hint = PL_Horizontal(5); break;
                            case 'c': tile.Hint = PL_Vertical(3); break;
                            case 'd': tile.Hint = PL_Horizontal(2); break;
                            case 'e': tile.Hint = PL_Vertical(4); break;
                            case 'f': tile.Hint = PL_Vertical(1); break;
                            case 'g': tile.Hint = PL_Vertical(4); break;
                            case 'h': tile.Hint = PL_Horizontal(2); break;
                            case 'i': tile.Hint = PL_Horizontal(5); break;
                            case 'j': tile.Hint = PL_Vertical(4); break;
                            case 'k': tile.Hint = PL_Horizontal(1); break;
                            case 'l': tile.Hint = PL_Vertical(2);
                                break;
                            case 'm': tile.Hint = PL_Horizontal(1); break;
                        }
                    }
                }
            }

            private static GString PL_Horizontal(int question)
            {
                return G.T("P " + question + " L");
            }
            private static GString PL_Vertical(int question)
            {
                return G.T("P\n" + question + "\nL");
            }
        }
    }
}