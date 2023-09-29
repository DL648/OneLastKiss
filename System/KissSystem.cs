using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace OneLastKiss.System
{
    internal class KissSystem:ModSystem
    {
        public static ModKeybind E { get; private set; }
        public static ModKeybind Q {  get; private set; }
        public override void Load()
        {
            base.Load();
            E = KeybindLoader.RegisterKeybind(Mod, "战技", "E");
            Q = KeybindLoader.RegisterKeybind(Mod, "爆发", "Q");
        }
        public override void Unload()
        {
            base.Unload();
            E = null;
            Q = null;
        }
    }
}
