using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for CommonFunction
/// </summary>
public class CommonFunction
{
    public static void CheckPurchaseService(int ServiceID,string Msrno,Page page)
    {
        cls_connection ObjConnection = new cls_connection();
        Int32 Val = ObjConnection.select_data_scalar_int("select count(*) from MEMBERSERVICE where MSRNO="+ Msrno + " and "+ ServiceID + " in(select * from dbo.Function_Split(service,','))");
        if (Val == 0)
        {
            //ErrorShow.AlertMessageWithRedirect(page, "You are not authorized. So please Active your Service", "ActiveService.aspx");
        }
    }
}