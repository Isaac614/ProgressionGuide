
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class ScrollablePanel : UIPanel
    {
        private float _width;
        private float _height;
        private float? _top = null;
        private float? _left = null;
        private bool _bottomAlign;
        private bool _rightAlign;

        private UIList? _contentList = new UIList();
        protected UIScrollbar _scrollbar;
        protected bool _needsScrollbar = false;
        public int Count => _contentList.Count;


        public ScrollablePanel(float width, float height, bool bottomAlign, bool rightAlign)
        {
            _width = width;
            _height = height;

            _bottomAlign = bottomAlign;
            _rightAlign = rightAlign;
        }
        public ScrollablePanel(float width, float height, float top, float left)
        {
            _width = width;
            _height = height;

            _top = top;
            _left = left;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            Width.Set(0, _width);
            Height.Set(0, _height);

            PaddingLeft = 2f;
            PaddingRight = 2f;
            PaddingTop = 2f;
            PaddingBottom = 2f;

            if (_bottomAlign)
                Top.Set(0, 1.0f - Height.Percent);
            else if (_top != null)
                Top.Set(0f, (float)_top);
            else
                Top.Set(0f, 0f);


            if (_rightAlign)
                Left.Set(0, 1.0f - Width.Percent);
            else if (_left != null)
                Left.Set(0f, (float)_left);
            else
                Left.Set(0f, 0f);


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

        public virtual void Clear()
        {
            _contentList?.Clear();
            UpdateScrollbar();
        }

        private void UpdateScrollbar()
        {
            if (_contentList != null)
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

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateScrollbar();
        }

        public void Unload()
        {
            _contentList?.Clear();
            _contentList = null;
        }


    }

    
}