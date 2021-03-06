﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Gem.UX
{
    public class MaterialStyleKit : StyleKit
    {
        #region PrimaryColors

        public Color PrimaryColor
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        public Color PrimaryColorDark
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        public Color PrimaryColorLight
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        #endregion PrimaryColors

        #region SecondaryColors

        public Color SecondaryColor
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        public Color SecondaryColorDark
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        public Color SecondaryColorLight
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        public Color AccentColor
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        #endregion SecondaryColors

        #region BackgroundColors

        public Color BackgroundColorSystem
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        public Color BackgroundColorAppBar
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        public Color BackgroundColorPage
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        public Color BackgroundColorModal
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        #endregion BackgroundColors

        #region TextColors

        public Color PrimaryTextColor
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        public Color SecondaryTextColor
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        public Color DisabledTextColor
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        public Color DividerColor
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        public Color TextColorOfPrimaryColor
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        public Color TextColorOfAccentColor
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        #endregion TextColors

        #region IconColors

        public Color ActiveIconColor
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        public Color InactiveIconColor
        {
            get { return GetIfExistsValue<Color>(); }
            set { SetValue(value); }
        }

        #endregion IconColors
    }
}
