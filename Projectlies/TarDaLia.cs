using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OneLastkiss.Items;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Default;
using Terraria.WorldBuilding;

namespace OneLastKiss.Projectlies
{
    internal class TarDaLia:KissProjectlie
    {
        private Texture2D Ranged;
        private Texture2D Melee;
        private Vector2 orgin=Vector2.Zero;
        public override void SetDefaults()
        {
            State = StateType.Ranged;
            Ranged= Mod.Assets.Request<Texture2D>("Projectlies/TarDaLia").Value;
            Melee=Mod.Assets.Request<Texture2D>("Projectlies/TarDaLiaMelee").Value;
            Projectile.damage = 10;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 pos = Projectile.Center - Main.screenPosition;
              
            Vector2 scale = new Vector2(1f, 1f);
            float rot = Projectile.rotation;
            SpriteEffects spriteEffects;
            spriteEffects = player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            switch (State)
            {
                case StateType.Ranged:
                    orgin = Ranged.Size() / 2;
                    spriteEffects = SpriteEffects.None;
                    rot =player.channel? Vector2.Normalize(Main.MouseWorld - Projectile.Center).ToRotation():
                        player.direction==1 ? MathHelper.PiOver4 * player.direction :
                        MathHelper.Pi+MathHelper.PiOver4 * player.direction;
                    Main.EntitySpriteDraw(Ranged, pos, null, Color.White, rot, orgin, scale, spriteEffects);
                    break;
                case StateType.Melee:
                    orgin = Melee.Size() / 2;
                    scale = new Vector2(1f,3f);
                    Main.EntitySpriteDraw(Melee, pos, null, Color.White, rot, orgin, scale, spriteEffects);



                    Vector2 start_pos = Projectile.Center + Vector2.Normalize(Projectile.velocity) * -Melee.Height / 2;
                    Vector2 end_pos = Projectile.Center + Vector2.Normalize(Projectile.velocity) * Melee.Height / 2;
                    Texture2D texture2D=

                    break;
            }
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            switch (State)
            {
                case StateType.Ranged:
                    return false;
                case StateType.Melee:
                    float point = 0;
                    Vector2 start_pos = Projectile.Center + Vector2.Normalize(Projectile.velocity) * -Melee.Height / 2;
                    Vector2 end_pos = Projectile.Center + Vector2.Normalize(Projectile.velocity) * Melee.Height / 2;
                    if (Collision.CheckAABBvLineCollision(
                        targetHitbox.Center(),
                        targetHitbox.Size(),
                        start_pos,
                        end_pos,
                        50,
                       ref point))
                    {
                        return true;
                    }else return false;
            }
            return false;
        }
        public override void E_Down()
        {
            base.E_Down();
            switch (State)
            {
                case StateType.Ranged:
                    State = StateType.Melee;
                    break;
                case StateType.Melee:
                    State = StateType.Ranged;
                    break;
            }
        }
        private float time;
        public override void AI()
        {
            base.AI();
            if (player.HeldItem.type == ModContent.ItemType<TarDaLiaItem>())
            {
                Projectile.timeLeft = 60;
            }
            else Projectile.Kill();
            Projectile.Center = (Vector2)player.HandPosition;
            if (player.channel)
            {
                Projectile.timeLeft = 60;
                Projectile.rotation =(float) Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
                switch (State)
                {
                    case StateType.Ranged:
                        Projectile.velocity =Vector2.Normalize(Main.MouseWorld - Projectile.Center);
                        break;
                    case StateType.Melee:
                        time +=0.2f;
                        Vector2 vector = new Vector2((float)Math.Sin(time),(float)Math.Cos(time*0.5f));
                        Projectile.velocity = vector;
                        break;
                }
            }
            else
            {
                time = 0f;
                Projectile.velocity = Vector2.Zero;
                Projectile.rotation = MathHelper.PiOver4 * player.direction;
            }
          


        }

    }
}
