using Terraria;
using Terraria.ModLoader;

namespace CCServerMod
{
	public class CCModGlobalItem : GlobalItem
	{
		public override void UpdateInventory(Item item, Player player)
		{
			if (item.IsAir)
				return;

			if (item.stack < 100 || item.buffType <= 0)
				return;

			player.AddBuff(item.buffType, 2);
		}
	}
}
