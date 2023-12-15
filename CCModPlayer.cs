using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static Terraria.GameContent.Bestiary.IL_BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions;

namespace CCServerMod
{
	public class CCModPlayer : ModPlayer
	{
		public override void PostUpdateMiscEffects()
		{
			switch (Main.SceneMetrics.ActiveFountainColor)
			{
				case 0:
					Player.ZoneDesert = false;
					Player.ZoneJungle = false;
					Player.ZoneSnow = false;
					break;
				case 2:
					Player.ZoneCorrupt = true;
					break;
				case 3:
					Player.ZoneJungle = true;
					break;
				case 4:
					Player.ZoneHallow = true;
					break;
				case 5:
					Player.ZoneSnow = true;
					break;
				case 10:
					Player.ZoneCrimson = true;
					break;
				case 12:
					Player.ZoneDesert = true;
					break;
			}
		}

		public override void PreUpdateBuffs()
		{
			void CheckCollectionForBuffs(IEnumerable<Item> collection)
			{
				foreach (var item in collection)
				{
					if (item.IsAir)
						continue;

					if (item.stack < 100 || item.buffType <= 0)
						continue;

					Player.AddBuff(item.buffType, 2);
				}
			}

			// Inventory.
			CheckCollectionForBuffs(Player.inventory);

			// Piggy bank.
			CheckCollectionForBuffs(Player.bank.item);

			// Safe.
			CheckCollectionForBuffs(Player.bank2.item);

			// Defender's forge
			CheckCollectionForBuffs(Player.bank3.item);

			// Void vault.
			CheckCollectionForBuffs(Player.bank4.item);
		}
	}
}
