using System.IO;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace CCServerMod
{
	public class CCServerMod : Mod
	{
		public enum ModPacketID
		{
			None = 0,
			AnglerReset = 1
		}

		public static CCServerMod Instance
		{
			get;
			private set;
		}

		public override void Load() => Instance = this;

		public override void Unload() => Instance = null;

		public void SendPacket(ModPacketID id)
		{
			var packet = GetPacket();
			packet.Write((byte)id);
			packet.Send();
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			// If used for more things, a proper system for sending specific packets would be a good idea.
			var id = (ModPacketID)reader.ReadByte();

			switch (id)
			{
				// This checks for not being a MP client itself, so no checks are required here.
				case ModPacketID.AnglerReset:
					Main.AnglerQuestSwap();
					break;

				default:
					Logger.Warn($"Warning: An invalid ModPacketID {id} was sent!");
					break;
			}
		}
	}
}