﻿using MG_BLL;
using MG_BLL.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MG_WeChat
{
    public partial class Login : System.Web.UI.Page
    {
        public string wxCode="";
        public string backUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            wxCode = Request.QueryString["code"];

            if (!string.IsNullOrWhiteSpace(Request.QueryString["backurl"]))
            {
                backUrl = Request.QueryString["backurl"];
            }               
        }
    }
}