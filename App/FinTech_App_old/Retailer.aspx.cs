﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Retailer : System.Web.UI.Page
{
    public Company company;

    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
    }
}