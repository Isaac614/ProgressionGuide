
using System.Diagnostics.Eventing.Reader;
using ProgressionGuide.UI;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;


public class UITogglePlayer : ModPlayer
{
    // public override void OnEnterWorld()
    // {

    //     Player.QuickSpawnItem(null, Terraria.ID.ItemID.Zenith, 1);
    //     Player.QuickSpawnItem(null, Terraria.ID.ItemID.SolarFlareBreastplate);
    //     Player.QuickSpawnItem(null, Terraria.ID.ItemID.SolarFlareHelmet);
    //     Player.QuickSpawnItem(null, Terraria.ID.ItemID.SolarFlareLeggings);

    // }

    private bool activeKeyPreviousState = false;


    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        bool activeKeyCurrentState = Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.G);

        if (activeKeyCurrentState && !activeKeyPreviousState)
        {
            ProgressionGuideUISystem.ToggleUI();
        }

        activeKeyPreviousState = activeKeyCurrentState;
    }


}