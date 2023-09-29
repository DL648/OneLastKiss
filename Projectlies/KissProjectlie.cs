using OneLastKiss.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace OneLastKiss.Projectlies
{
    internal abstract class KissProjectlie : ModProjectile
    {
        public Player player { get { return Main.player[Projectile.owner]; } }
        public enum StateType
        {
            Melee = 1,
            Ranged = 2,
            Magic = 3
        }
        public StateType State { get; set; } = StateType.Melee;
        public static int Time_E { get; set; }
        public static int Time_Q { get; set; }
        public virtual void E_Down() { }
        public virtual void Q_Down() { }
        public virtual void SetE_CD()
        {
            if (Time_E > 0)
            {
                Time_E--;
            }
        }
        public virtual void SetQ_CD()
        {
            if (Time_Q > 0)
            {
                Time_Q--;
            }
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            base.SetDefaults();
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.knockBack = 0;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = false;
            Projectile.hide = true;
            Projectile.friendly = true;
        }
        public override bool ShouldUpdatePosition() => false;
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
            overPlayers.Add(index);
        }

        public override void AI()
        {
            base.AI();
            if (KissSystem.E.JustReleased) E_Down();
            if (KissSystem.Q.JustPressed) Q_Down();

            SetE_CD();
            SetQ_CD();
        }

    }
}
