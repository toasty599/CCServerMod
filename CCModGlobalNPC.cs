using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CCServerMod
{
	public class CCModGlobalNPC : GlobalNPC
	{
		public override void ModifyShop(NPCShop shop)
		{
			if (shop.NpcType != NPCID.Painter)
				return;

			// Loop through each entry and locate the echo coating.
			foreach (var entry in shop.Entries)
			{
				if (entry.Item.type != ItemID.EchoCoating)
					continue;

				// Attempt to use reflection to remove the plantera condition from the list of conditions.
				try
				{
					List<Condition> conditions = (List<Condition>)typeof(NPCShop.Entry).GetField("conditions", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).GetValue(entry);
					conditions.RemoveAll(condition => condition.Equals(Condition.DownedPlantera));
				}
				catch
				{
					Mod.Logger.Error("Error: Reflection to find the shop entry conditions list failed!");
				}
			}

			// Add the spectre goggles to the painters store.
			shop.Add(ItemID.SpectreGoggles);
		}

		public override void OnChatButtonClicked(NPC npc, bool firstButton)
		{
			if (npc.type != NPCID.Angler || Main.netMode is NetmodeID.Server)
				return;

			if (Main.netMode is NetmodeID.SinglePlayer)
				Main.AnglerQuestSwap();
			else
				CCServerMod.Instance.SendPacket(CCServerMod.ModPacketID.AnglerReset);
		}
	}
}
