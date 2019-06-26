namespace Eco.Mods.CavRn.HappyRestart
{
    using System;
    using System.Linq;
    using Eco.Core.Plugins.Interfaces;
    using Eco.Core.Utils;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.Chat;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using System.Threading.Tasks;
    using Eco.Core.Plugins;
    using Eco.Shared.Utils;
    using Eco.Shared.Services;

    public class HappyRestartMod : IModKitPlugin, IInitializablePlugin
    {

        public static string ModVersion = "1.0";
        public string GetStatus()
        {
            return String.Empty;
        }

        public void Initialize(TimedTask timer)
        {
			DateTime dateNow = DateTime.Now;
			Log.Write(new LocString(string.Format("CitePerdue - Current date is: " + dateNow.ToString())));
			Console.WriteLine();

            WarnRestart("05:56:00", "3");
            WarnRestart("05:58:00", "1");
            SaveAndKick("05:59:00");

            WarnRestart("11:56:00", "3");
            WarnRestart("11:58:00", "1");
            SaveAndKick("11:59:00");

            WarnRestart("17:56:00", "3");
            WarnRestart("17:58:00", "1");
            SaveAndKick("17:59:00");

            WarnRestart("23:56:00", "3");
            WarnRestart("23:58:00", "1");
            SaveAndKick("23:59:00");
        }

        public void WarnRestart(string DailyTime, string minutes)
        {
            //Time when method needs to be called
            var timeParts = DailyTime.Split(new char[1] { ':' });

            var dateNow = DateTime.Now;
            var date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day,
                       int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));
            TimeSpan ts;
            if (date > dateNow)
                ts = date - dateNow;
            else
            {
                date = date.AddDays(1);
                ts = date - dateNow;
            }

            //waits certan time and run the code
            Task.Delay(ts).ContinueWith((x) => WarnRestartAction(minutes));
        }

        public void WarnRestartAction(string minutes)
        {
            Log.Write(new LocString(string.Format("CitePerdue - Server will restart in " + minutes + " Minutes !!")));
            ChatManager.ServerMessageToAll(new LocString(Text.Color(Color.Red, "Server will restart in " + minutes + " Minutes !!")), false, DefaultChatTags.Notifications);
        }

        public void SaveAndKick(string DailyTime)
        {
            //Time when method needs to be called
            var timeParts = DailyTime.Split(new char[1] { ':' });

            var dateNow = DateTime.Now;
            var date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));
            TimeSpan ts;
            if (date > dateNow)
                ts = date - dateNow;
            else
            {
                date = date.AddDays(1);
                ts = date - dateNow;
            }

            //waits certan time and run the code
            Task.Delay(ts).ContinueWith((x) => SaveAndKickAction());
        }

        public async Task SaveAndKickAction()
        {
			Log.Write(new LocString(string.Format("CitePerdue - Kicking players...")));
			Console.WriteLine();

            UserManager.Users.ToList().ForEach(user => {
                var player = user.Player;
                if (player != null)
                {
                    player.Client.Disconnect("Server will restart in 1 minute. Please don't reconnect in this interval !", "");
					Log.Write(new LocString(string.Format("CitePerdue - " + user.Name + " has been kicked.")));
					Console.WriteLine();
                }
            });

            Log.Write(new LocString(string.Format("CitePerdue - Saving the game...")));
			Console.WriteLine();
			
            await Task.Run(() =>
            {
                try
                {
                    var time = TickTimeUtil.TimeSubprocess(StorageManager.SaveAndFlush);
                    Log.Write(new LocString(string.Format("CitePerdue - Game saved !")));
					Console.WriteLine();
                }
				catch (Exception e)
				{
					Log.Write(new LocString(string.Format("CitePerdue - Error during same game !")));
					Console.WriteLine();
				}
            });
        }
    }
}
