using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurifyLog
{
    public class Countdata
    {
        public PurifyBoss akelon;
        public PurifyBoss tiaga;
        public PurifyBoss krieg;

        public Countdata()
        {
            akelon = new PurifyBoss();
            tiaga = new PurifyBoss();
            krieg = new PurifyBoss();
        }
    }

    public class PurifyBoss
    {
        public decimal killed = 0;
        public decimal mineral = 0; //神聖な鉱物の欠片
        public decimal material = 0; //甲羅/足かせ/石板
        public decimal parchment = 0; //羊皮紙
        public decimal es = 0; //珍しい/孤独/誘因
    }
}
