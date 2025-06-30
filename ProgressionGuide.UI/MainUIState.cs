using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class MainUIState : UIState
    {
        public ItemLookupWindow mainWindow;


        public override void OnInitialize()
        {
            mainWindow = new ItemLookupWindow(new Item(ItemID.TerraBlade));
            Append(mainWindow);
        }

        public string GetSearchText()
        {
            return mainWindow?.GetSearchText() ?? "";
        }

        public bool GetSearchBarActive()
        {
            return mainWindow.GetSearchBarActive();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ContainsPoint(Main.MouseScreen)) // for cursor disappearing
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }

        public void Clear()
        {
            mainWindow.Clear();
        }

    }
}