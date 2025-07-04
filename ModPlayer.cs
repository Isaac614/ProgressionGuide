
using System.Diagnostics.Eventing.Reader;
using ProgressionGuide.UI;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Input;


public class UITogglePlayer : ModPlayer
{
    // public override void OnEnterWorld()
    // {

    //     // Player.QuickSpawnItem(null, ItemID.Zenith, 1);
    //     // Player.QuickSpawnItem(null, ItemID.TerraBlade, 1);
    //     // Player.QuickSpawnItem(null, ItemID.SolarFlareBreastplate);
    //     // Player.QuickSpawnItem(null, ItemID.SolarFlareHelmet);
    //     // Player.QuickSpawnItem(null, ItemID.SolarFlareLeggings);

    // }

    private bool activeKeyPreviousState = false;
    private bool populateCraftingInfoPreviousState = false;
    private bool getSearchTextPreviousState = false;


    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        ProgressionGuideUISystem uiSystem = ModContent.GetInstance<ProgressionGuideUISystem>();

        bool activeKeyState = Main.keyState.IsKeyDown(Keys.G);

        if (activeKeyState && !activeKeyPreviousState)
        {
            uiSystem.ToggleUI();
        }

        activeKeyPreviousState = activeKeyState;


        bool populateCraftingInfoState = Main.keyState.IsKeyDown(Keys.OemCloseBrackets);

        if (populateCraftingInfoState && !populateCraftingInfoPreviousState)
        {
            uiSystem.PopulateItemLookupWindow(new Item(ItemID.TerraBlade));
        }

        populateCraftingInfoPreviousState = populateCraftingInfoState;


        bool getSearchTextState = Main.keyState.IsKeyDown(Keys.RightShift);

        if (getSearchTextState && uiSystem.GetSearchBarActive() && !getSearchTextPreviousState)
        {
            uiSystem.Search();
        }

        getSearchTextPreviousState = getSearchTextState;
    }
}