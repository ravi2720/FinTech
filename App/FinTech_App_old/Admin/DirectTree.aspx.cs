using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default2 : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtEmployee = new DataTable();
    DataTable dtTree = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
     
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                dtEmployee = (DataTable)Session["dtEmployee"];
               
            }
            else
            {
                Response.Redirect("default.aspx");
            }
        }

    }
    private void SetTreeView()
    {

        TreeView1.Nodes.Clear();
        try
        {
            TreeNode mNode = new TreeNode();
            string mtooltip;
            DataTable dtToolTip = new DataTable();
            int msrno = Convert.ToInt32(hidMsrNo.Value);
            if (msrno > 0)
            {
                dtToolTip = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where msrno=" + msrno + "");
                dtTree = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where ParentID=" + msrno + "");
                //DataRow[] dr = dtTree.Select("MsrNo='" + msrno + "'");
                mNode.Expanded = false;
                mNode.Text = dtToolTip.Rows[0]["LoginID"].ToString() + " | " + dtToolTip.Rows[0]["Name"].ToString() + " | " + dtToolTip.Rows[0]["TotalDirect"].ToString();
                mNode.Value = dtToolTip.Rows[0]["MsrNo"].ToString();
                mtooltip = "";
                mtooltip = "<table cellpadding='0' cellspacing='0' width='450px' id='mytable'>";
                mtooltip = mtooltip + "<tr><td class='myhead'>Member Name</td><td class='mycontent'>" + dtToolTip.Rows[0]["Name"].ToString() + "</td><td class='myhead'>Package</td><td class='mycontent'>" + dtToolTip.Rows[0]["PackageName"].ToString().Replace("-", ":") + "</td></tr>";
                mtooltip = mtooltip + "<tr><td class='myhead'>Email</td><td class='mycontent'>" + dtToolTip.Rows[0]["Email"].ToString() + "</td><td class='myhead'>Mobile</td><td class='mycontent'>" + dtToolTip.Rows[0]["Mobile"].ToString().Replace("-", ":") + "</td></tr>";
                //mtooltip = mtooltip + "<tr><td class='myhead'>DOA</td><td class='mycontent'>" + string.Format("{0:dd- MMM -yyyy}", dtToolTip.Rows[0]["DOA"]) + "</td><td class='myhead'>DOJ</td><td class='mycontent'>" + string.Format("{0:dd- MMM -yyyy}", dtToolTip.Rows[0]["DOJ"]) + "</td></tr>";

                mtooltip += "</table>";
                mNode.ToolTip = mtooltip;

                mNode.SelectAction = TreeNodeSelectAction.Expand;
                mNode.ImageUrl = "./images/TreeIcon/G.png";
                mNode.PopulateOnDemand = true;
                mNode.ExpandAll();
                TreeView1.Nodes.Add(mNode);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('LoginID dose not exist....  !');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Database Error....  !');", true);
        }

    }

    public void ShowData(TreeNode Tnode)
    {
        string mtooltip;

        DataTable dtToolTip = new DataTable();


        //DataRow[] drnod = dtTree.Select("ParentID='" + Tnode.Value + "'");
        dtTree = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where ParentID=" + Tnode.Value + "");

        //for (int j = 0; j < drnod.Length; j++)
        for (int j = 0; j < dtTree.Rows.Count; j++)
        {
            //int msrno = Convert.ToInt32(drnod[j]["MsrNo"].ToString());
            int msrno = Convert.ToInt32(dtTree.Rows[j]["MsrNo"].ToString());
            dtToolTip = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where msrno=" + msrno + "");
            TreeNode nod = new TreeNode();
            nod.Value = dtTree.Rows[j]["MsrNo"].ToString();

            nod.ImageUrl = "./images/TreeIcon/G.png";
            mtooltip = "";
            mtooltip = "<table cellpadding='0' cellspacing='0' width='450px' id='mytable'>";
            mtooltip = mtooltip + "<tr><td class='myhead'>Member Name</td><td class='mycontent'>" + dtToolTip.Rows[0]["Name"].ToString() + "</td><td class='myhead'>Package</td><td class='mycontent'>" + dtToolTip.Rows[0]["PackageName"].ToString().Replace("-", ":") + "</td></tr>";
            mtooltip = mtooltip + "<tr><td class='myhead'>Email</td><td class='mycontent'>" + dtToolTip.Rows[0]["Email"].ToString() + "</td><td class='myhead'>Mobile</td><td class='mycontent'>" + dtToolTip.Rows[0]["Mobile"].ToString().Replace("-", ":") + "</td></tr>";

            //mtooltip = mtooltip + "<tr><td class='myhead'>Leg</td><td class='mycontent'>" + string.Format("{0:dd- MMM -yyyy}", dtToolTip.Rows[0]["Leg"]) + "</td><td class='myhead'></td><td class='mycontent'></td></tr>";
            mtooltip += "</table>";
            nod.Text = dtTree.Rows[j]["LoginID"].ToString() + " | " + dtTree.Rows[j]["Name"].ToString();
            nod.ToolTip = mtooltip;
            if (int.Parse(dtTree.Rows[j]["TotalDirect"].ToString()) > 0)
            {
                nod.PopulateOnDemand = true;
            }
            nod.Expanded = false;
            nod.SelectAction = TreeNodeSelectAction.Expand;
            Tnode.ChildNodes.Add(nod);
            //nod.ExpandAll();
        }

    }

    protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        ShowData(e.Node);
    }




    protected void txtMemberID_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (txtMemberID.Text != "")
        {

            dt = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where LoginID='" + txtMemberID.Text.Trim() + "'");
            if (dt.Rows.Count > 0)
            {
                lblMemberName.Text = Convert.ToString(dt.Rows[0]["Name"]);
                hidMsrNo.Value = Convert.ToString(dt.Rows[0]["MsrNo"]);

                divdata.Visible = true;
                SetTreeView();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Enter Valid MemberID  !');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Enter MemberID  !');", true);
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        //
    }

    [WebMethod]
    public static List<string> GetMember(string Search)
    {
        List<string> empResult = new List<string>();
        cls_connection ObjData = new cls_connection();
        DataTable dt = new DataTable();
        dt=ObjData.select_data_dt("select top 10 LoginID from member where IsActive=1 and loginid like '%"+ Search + "%'");
        foreach(DataRow dr in dt.Rows)
        {
            empResult.Add(dr["LoginID"].ToString());
        }
        return empResult;
    }
    public static string ConvertDataTabletoString(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows).ToString();
    }
}