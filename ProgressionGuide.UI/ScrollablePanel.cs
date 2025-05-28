
using Microsoft.Xna.Framework;
using rail;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class ScrollablePanel : UIPanel
    {
        protected UIList _contentList = new UIList();
        protected UIScrollbar _scrollbar;
        protected bool _needsScrollbar = false;
        public int Count => _contentList.Count;

        public UIList ContentList
        {
            get { return _contentList; }
        }

        public ScrollablePanel()
        {
            Width.Set(0, 0.3f);
            Height.Set(0, 0.85f);

            Top.Set(0, 1.0f - Height.Percent);
            Left.Set(0, 0.0f);
        }

        public ScrollablePanel(float width, float height, bool bottomAlign, bool rightAlign)
        {
            Width.Set(0, width);
            Height.Set(0, height);
            PaddingLeft = 5f;
            PaddingRight = 5f;
            PaddingTop = 5f;
            PaddingBottom = 5f;

            if (bottomAlign)
            {
                Top.Set(0, 1.0f - Height.Percent);
            }
            else
            {
                Top.Set(0, 0.0f);
            }

            if (rightAlign)
            {
                Left.Set(0, 1.0f - Width.Percent);
            }
            else
            {
                Left.Set(0, 0.0f);
            }
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            // Creating list for the content
            _contentList = new UIList();
            _contentList.Width.Set(0, 1f);
            _contentList.Height.Set(0f, 1f);
            _contentList.Left.Set(0f, 0f);
            _contentList.Top.Set(0f, 0f);

            _contentList.PaddingTop = 5f;
            _contentList.PaddingBottom = 5f;
            _contentList.PaddingLeft = 5f;
            _contentList.PaddingRight = 5f;
            _contentList.ListPadding = 5f; // Space between list items
            Append(_contentList);

            // Creating scrollbar
            _scrollbar = new UIScrollbar();
            _scrollbar.Width.Set(20f, 0f);
            _scrollbar.Height.Set(0f, .9f);
            _scrollbar.Left.Set(-20f, 1f); // Positions at right edge
            // _scrollbar.Top.Set(0f, 0f);
            _scrollbar.VAlign = 0.5f;
            _scrollbar.PaddingLeft = 0;
            _scrollbar.PaddingRight = 0;
            _scrollbar.PaddingTop = 8f;
            _scrollbar.PaddingBottom = 8f;

            // Links scrollbar to the list
            _contentList.SetScrollbar(_scrollbar);
        }

        public void AddItem(UIElement element)
        {
            _contentList.Add(element);
            UpdateScrollbar();
        }

        public void RemoveItem(UIElement element)
        {
            _contentList.Remove(element);
            UpdateScrollbar();
        }

        public void Clear()
        {
            _contentList.Clear();
            UpdateScrollbar();
        }

        private void UpdateScrollbar()
        {
            Recalculate(); // checks to see if we need the scrollbar

            float listHeight = _contentList.GetTotalHeight();
            float panelHeight = GetDimensions().Height;

            _needsScrollbar = listHeight > panelHeight;

            if (_needsScrollbar)
            {
                if (!HasChild(_scrollbar))
                {
                    _contentList.Width.Set(-25f, 1f);
                    Append(_scrollbar);
                }
            }
            else
            {
                if (HasChild(_scrollbar))
                {
                    RemoveChild(_scrollbar);
                    _contentList.Width.Set(0, 1f);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateScrollbar();
        }
    }
}