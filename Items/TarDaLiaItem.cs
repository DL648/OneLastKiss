using Microsoft.Xna.Framework;
using OneLastKiss.Projectlies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OneLastkiss.Items
{
    internal class TarDaLiaItem:ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 10;
            Item.knockBack = 2;
            Item.useTime = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 5;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.channel = true;
        }
        public override void HoldItem(Player player)
        {
            base.HoldItem(player);
            if (player.ownedProjectileCounts[ModContent.ProjectileType<TarDaLia>()]<1)
            {
                Projectile.NewProjectile(null,
                    (Vector2)player.HandPosition,
                    Vector2.Zero,
                    ModContent.ProjectileType<TarDaLia>(), 
                    Item.damage, Item.knockBack);
            }
        }
    }
}
