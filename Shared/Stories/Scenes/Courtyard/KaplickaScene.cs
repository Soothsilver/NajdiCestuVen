using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Sounds;
using Nsnbc.Stories.Scenes.Xml;

namespace Nsnbc.Stories.Scenes.Courtyard
{
    public class KaplickaScene : XmlScene
    {
        public bool WonOnce { get; set; }
        
        private class Tile
        {
            public int X { get; }
            public int Y { get; }
            public TileKind Kind { get; set; }
            public char Tag { get; }
            public const int MAX_WATER_LEVEL = 10;

            public bool AdmitsWater => (this.Kind == TileKind.Air || this.Kind == TileKind.ExtensibleWallOpen ||
                                        this.Kind == TileKind.Goal) &&
                                       this.WaterLevel < MAX_WATER_LEVEL;

            public WaterStyle WaterStyle { get; set; }
            public bool IsHardWall => Kind == TileKind.Button || Kind == TileKind.Wall || Kind == TileKind.ExtensibleWall;

            public int WaterLevel = 0;

            public Tile(int x, int y, TileKind kind, char tag)
            {
                X = x;
                Y = y;
                Kind = kind;
                Tag = tag;
            }
        }

        private static readonly string mazeSource = @"
X.........X..sX
X.........1...X
B......XX1A2XXX
XX..XXXXX.X...X
X.44..3.XXX...X
X.....3...X...X
X.....3...X...X
X.44..3.X.X...X
X...XXCfXXX...X".Trim();

        Tile[,] tiles;
        private Tile startTile;
        private int width;
        private int height;
        private Tile? mouseOverTile;
        public KaplickaScene()
        {
            string[] lines = mazeSource.Split(new[] { '\n'}, StringSplitOptions.RemoveEmptyEntries).Select(str => str.Trim()).ToArray();
            height = lines.Length;
            width = lines[0].Length;
            tiles = new Tile[width,height];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    char tag = lines[y][x];
                    TileKind tileKind = tag switch
                    {
                        'X' => TileKind.Wall,
                        '.' => TileKind.Air,
                        '2' => TileKind.ExtensibleWallOpen,
                        '1' or '3' or '4' => TileKind.ExtensibleWall,
                        'A' or 'B' or 'C' => TileKind.Button,
                        'f'=> TileKind.Goal,
                        's'=> TileKind.Origin,
                        _ => throw new Exception("Unexpected tile kind.")
                    };
                    tiles[x,y] = new Tile(x, y, tileKind, tag);
                    if (tileKind == TileKind.Origin)
                    {
                        startTile = tiles[x, y];
                    }
                }
            }
        }


        internal enum TileKind
        {
            Wall,
            Origin,
            Goal,
            ExtensibleWall,
            ExtensibleWallOpen,
            Button,
            Air
        }

        private void PutInMoreWater()
        {
            Sfxs.Play(SoundEffectName.WaterPour);
            timeSinceLastUpdate = 0;
            startTile.WaterLevel += 20;
        }

        private float timeSinceLastUpdate = 0;
        public override void Update(float elapsedSeconds, AirSession airSession)
        {
            timeSinceLastUpdate += elapsedSeconds;
            if (timeSinceLastUpdate >= 0.1f)
            {
                timeSinceLastUpdate -= 0.1f;
                int[,] newLevels = new int[width,height];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Tile t = tiles[x, y];
                        if (t.WaterLevel > 0)
                        {
                            int halfDown = t.WaterLevel / 2;
                            int halfUp = t.WaterLevel - halfDown;
                            
                            Tile tLeft = tiles[x - 1, y];
                            Tile tRight = tiles[x + 1, y];
                            Tile? tBottom = y == height - 1 ? null : tiles[x, y + 1];
                            if (tBottom == null)
                            {
                                if (t.Kind == TileKind.Goal)
                                {
                                    if (!this.WonOnce)
                                    {
                                        this.WonOnce = true;
                                        airSession.Session.QuickEnqueue(this.StorageScripts["KaplickaVictory"]);
                                    }
                                }
                                newLevels[t.X, t.Y] += halfDown;
                            }
                            else if (tBottom.AdmitsWater)
                            {
                                newLevels[tBottom.X, tBottom.Y] += halfUp;
                                newLevels[t.X, t.Y] += halfDown;
                            }
                            else if (tLeft.AdmitsWater || tRight.AdmitsWater)
                            {
                                int quarterDown = halfUp / 2;
                                int quarterUp = halfUp - quarterDown;
                                if (tLeft.AdmitsWater && tRight.AdmitsWater)
                                {
                                    int goesRight = tRight.AdmitsWater ? (R.Coin() ? quarterUp : quarterDown) : 0;
                                    int goesLeft = halfUp - goesRight;
                                
                                    newLevels[tLeft.X, tLeft.Y] += goesLeft;
                                    newLevels[tRight.X, tRight.Y] += goesRight;
                                    
                                }
                                else
                                {
                                    int goesRight = tRight.AdmitsWater ? halfUp : 0;
                                    int goesLeft = halfUp - goesRight;
                                    newLevels[tLeft.X, tLeft.Y] += goesLeft;
                                    newLevels[tRight.X, tRight.Y] += goesRight;
                                }

                                newLevels[t.X, t.Y] += halfDown;
                            }
                            else
                            {
                                newLevels[t.X, t.Y] += t.WaterLevel;
                            }
                        }
                    }
                }
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        tiles[x, y].WaterLevel = newLevels[x, y];
                    }
                }
                for (int y = height - 1; y >= 0; y--)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Tile self = tiles[x, y];
                        Tile? tBottom = y == height - 1 ? null : tiles[x, y + 1];
                        if (self.WaterLevel > 0)
                        {
                            if (tBottom == null)
                            {
                                self.WaterStyle = WaterStyle.Ball;
                            }
                            else if (tBottom.IsHardWall)
                            {
                                self.WaterStyle = self.WaterLevel < Tile.MAX_WATER_LEVEL ? WaterStyle.Waterlevel : WaterStyle.Midwater;
                            }
                            else if (tBottom.WaterStyle == WaterStyle.Midwater)
                            {
                                self.WaterStyle = self.WaterLevel < Tile.MAX_WATER_LEVEL ? WaterStyle.Waterlevel : WaterStyle.Midwater;
                            }
                            else
                            {
                                self.WaterStyle = WaterStyle.Ball;
                            }
                        }
                        else
                        {
                            self.WaterStyle = WaterStyle.None;
                        }
                    }
                }
            }
            base.Update(elapsedSeconds, airSession);
        }


        public override void Draw(AirSession airSession)
        {
            base.Draw(airSession);
            mouseOverTile = null;

            int topX = 267;
            int topY = 198;
            int fullWidth = 1272;
            int fullHeight = 472;
            int tw = fullWidth / width;
            int th = fullHeight / height;
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile t = tiles[x, y];
                    Rectangle rectangle = new Rectangle(topX + x * tw, topY + y * th, tw, th);
                    switch (t.Kind)
                    {
                        case TileKind.Button:
                            Primitives.DrawImage(Library.Art(ArtName.R2BrickButton), rectangle);
                            break;
                        case TileKind.Wall:
                            Primitives.DrawImage(Library.Art(ArtName.R2Brick), rectangle);
                            break;
                        case TileKind.ExtensibleWall:
                            Primitives.DrawImage(Library.Art(ArtName.R2BrickMovable), rectangle);
                            break;
                        case TileKind.ExtensibleWallOpen:
                            Primitives.DrawImage(Library.Art(ArtName.R2BrickMovable), rectangle, Color.White.Alpha(50));
                            break;
                        default:
                            Primitives.FillRectangle(rectangle, ToColor(t.Kind));
                            break;
                    }
                    if (t.WaterLevel > 0)
                    {
                        Color waterColor = Color.FromNonPremultiplied(71, 123, 255, 255);
                        int h = rectangle.Height * Math.Min(Tile.MAX_WATER_LEVEL, t.WaterLevel) / Tile.MAX_WATER_LEVEL;
                        switch (t.WaterStyle)
                        {
                            case WaterStyle.Midwater:
                            case WaterStyle.Waterlevel:
                                Primitives.FillRectangle(new Rectangle(rectangle.X, rectangle.Bottom - h, rectangle.Width, h), waterColor);
                                if (t.WaterStyle == WaterStyle.Waterlevel)
                                {
                                    Primitives.DrawImage(Library.Art(ArtName.R2Wave), new Rectangle(rectangle.X, rectangle.Bottom - h  - th,
                                        tw,th));
                                }
                                break;
                            case WaterStyle.Ball:
                                Primitives.FillCircle(new Vector2(rectangle.X+h/2, rectangle.Y + h /2), h / 2, waterColor);
                                break;
                        }
                    }
                    if (Root.IsMouseOver(rectangle))
                    {
                        mouseOverTile = t;
                    }
                }
            }
        }

        private Color ToColor(TileKind tKind)
        {
            return tKind switch
            {
                TileKind.Air => Color.Transparent,
                TileKind.Button => Color.Red,
                TileKind.Goal => Color.Transparent,
                TileKind.Origin => Color.LightBlue,
                TileKind.Wall => Color.Black,
                TileKind.ExtensibleWall => Color.Maroon,
                TileKind.ExtensibleWallOpen => Color.Maroon.Alpha(50),
                _ => throw new Exception("Unknown")
            };
        }

        public override bool Click(AirSession airSession)
        {
            if (airSession.Session.HeldItem?.Art == ArtName.R1HrnecekSVodou)
            {
                airSession.Session.RemoveHeldItemFromInventory();
                airSession.AddItemWithAnimation(new InventoryItem(ArtName.R1Hrnecek, "Hrneček, jehož voda šla do budky."));
                PutInMoreWater();
                return true;
            }
            else if (airSession.Session.HeldItem != null)
            {
                    airSession.QuickEnqueue(QSpeak.Quick("Ten drát je přerušený. Jaký vodič by mohl nahradit prázdný prostor mezi konci drátů"));
                return true;
            }
            else if (mouseOverTile != null && mouseOverTile.Kind == TileKind.Button)
            {
                switch (mouseOverTile.Tag)
                {
                    case 'A':
                        this.FlipTilesTagged('1');
                        this.FlipTilesTagged('2');
                        break;
                    case 'B':
                        this.FlipTilesTagged('4');
                        break;
                    case 'C':
                        this.FlipTilesTagged('3');
                        break;
                }
                return true;
            }
            else
            {
                return base.Click(airSession);
            }
        }

        private void FlipTilesTagged(char c)
        {
            foreach (Tile tile in tiles)
            {
                if (tile.Tag == c)
                {
                    if (tile.Kind == TileKind.ExtensibleWall)
                    {
                        tile.Kind = TileKind.ExtensibleWallOpen;
                    }
                    else
                    {
                        tile.Kind = TileKind.ExtensibleWall;
                    }
                }
            }
        }
    }

    internal enum WaterStyle
    {
        None,
        Ball,
        Waterlevel,
        Midwater
    }

    public class R
    {
        static Random rgen = new Random();
        public static bool Coin()
        {
            return rgen.Next(0, 2) == 0;
        }
    }
}