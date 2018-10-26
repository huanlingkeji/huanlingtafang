using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Model
{
    public class paotaArr
    {
        private static paotaArr Instance = new paotaArr();
        public paotaArr instance
        {
            get
            {
                return Instance;
            }
        }
        public static string[] introductions = {
        "坦克：单一稳定输出单位，爆发时可以加倍攻速并且无视目标一切防御",
        "三射炮：最多同时攻击三个目标 可减速目标，爆发时可以极大的提高减速效果（90%）",
                "高射炮：对目标造成范围伤害，爆发时略微增加攻击时间间隔但一次发射五发子弹",
                        "四攻塔：对目标发射激光，有一定几率短暂眩晕目标，爆发时四孔连射",
                                "地狱塔：对目标的伤害逐渐提升，爆发时转移目标后的初始伤害设定为50%最大伤害"
        };
        public static int[] attacks = { 999,999,999,999,999 };
        public static int[] attdistances = { 999, 999, 999, 999, 999 };
        public static int[] attackintervals = { 999, 999, 999, 999, 999 };
        public static int[] costs = { 999, 999, 999, 999, 999 };
    }
}
