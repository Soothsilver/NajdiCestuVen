using System;
using Nsnbc.Core;
using Nsnbc.Stories.Scenes.Xml;

namespace Nsnbc.Stories.Scenes.Courtyard
{
    public class KaplickaScene : XmlScene
    {
        private static readonly string mazeSource = @"
X.........X..sX
W.........1...X
V.....U..1B2XXX
X.XX..XXX.X...X
X.....3.XXX...X
X.....3...X...X
XY....3...X...X
X.....3.X.X...X
X.....CfXXX...X".Trim();

        
        public KaplickaScene()
        {
            
        }

        public override void Draw(AirSession airSession)
        {
            base.Draw(airSession);
        }

        public override bool Click(AirSession airSession)
        {

            return base.Click(airSession);
        }
    }
}