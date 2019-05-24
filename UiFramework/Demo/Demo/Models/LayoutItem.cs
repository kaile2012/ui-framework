﻿using System;
using UiFramework.V2.Interfaces;

namespace Demo.Models
{
    public class LayoutItem : UiFramework.V2.Forms.Models.LayoutItem
    {
        [SQLite.PrimaryKey]
        public new Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        [SQLite.Ignore]
        public new ILayoutItemParameter[] Parameters
        {
            get => _parameters;
            set => SetProperty(ref _parameters, value);
        }
    }
}
