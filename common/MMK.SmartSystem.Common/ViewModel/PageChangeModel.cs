﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMK.SmartSystem.Common.ViewModel
{
    public enum PageEnum
    {
        WPFPage,
        WebPage,
        WebComponet
    }
    public class PageChangeModel
    {
        public Type FullType { get; set; }

        public PageEnum Page { get; set; }

        public string Title { get; set; }
        public string Url { get; set; }


        public WebRouteComponentDto ComponentDto { get; set; }

    }
}
