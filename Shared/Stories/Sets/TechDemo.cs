using System.Collections;
using System.Collections.Generic;
using Nsnbc.Events;
using Nsnbc.Phases;

namespace Nsnbc.Stories.Sets
{
    public class TechDemo
    {
        public static IEnumerable<Script> CreateScripts()
        {
            yield return new Script(BookmarkId.TechDemoStart, new QEvent[]
            {
                
            });
        }
    }
}