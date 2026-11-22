<%@ Page Language="C#" %><%@ Import Namespace="School" %><%@ Import Namespace="System.Linq" %>
<script runat="server">
SchoolService service=new SchoolService();
string TableName {get{return tables.SelectedValue;}}
protected void Page_Load(object sender,EventArgs e){
 if(!IsPostBack){tables.DataSource=Repository.Fields.Keys;tables.DataBind();}
 panel.Visible=Session["user"]!=null;login.Visible=!panel.Visible;
 if(panel.Visible)BuildInputs();
}
void BuildInputs(){inputs.Controls.Clear();foreach(var f in Repository.Fields[TableName]){inputs.Controls.Add(new LiteralControl("<label>"+f+"</label>"));inputs.Controls.Add(new TextBox{ID=f});}}
void SignIn(object s,EventArgs e){if(service.Login(user.Text,pass.Text)){Session["user"]=user.Text;Response.Redirect("Default.aspx");}else msg.Text="Invalid login";}
protected void Page_Init(object s,EventArgs e){ViewStateUserKey=Session.SessionID;}
void Rebind(){course.Visible=TableName=="tblStudentCourses";if(course.Visible && course.Items.Count==0){course.DataValueField="CourseID";course.DataTextField="CourseName";course.DataSource=service.List("tblCourses");course.DataBind();}int id;int.TryParse(course.SelectedValue,out id);grid.DataSource=service.List(TableName,course.Visible?id:0);grid.DataBind();}
protected void Page_PreRender(object s,EventArgs e){if(panel.Visible)Rebind();}
void Switch(object s,EventArgs e){ViewState.Remove("keys");BuildInputs();}
void SelectRow(object s,EventArgs e){var row=grid.SelectedRow;var fs=Repository.Fields[TableName];var keys=new string[fs.Length];for(int i=0;i<fs.Length;i++){keys[i]=Server.HtmlDecode(row.Cells[i+1].Text);((TextBox)inputs.FindControl(fs[i])).Text=keys[i];}ViewState["keys"]=keys;}
void Change(object s,CommandEventArgs e){if(Session["user"]==null)return;try{var fs=Repository.Fields[TableName];var vals=fs.Select(f=>Request.Form[((TextBox)inputs.FindControl(f)).UniqueID]??"").ToArray();service.Change(TableName,e.CommandName,vals,(string[])ViewState["keys"]??new string[fs.Length]);msg.Text="Saved";}catch(Exception){msg.Text="Check selected record, values and references.";}}
</script><!doctype html><html><body><form runat="server"><h1>School Management</h1><asp:Panel ID="login" runat="server"><asp:TextBox ID="user" runat="server"/><asp:TextBox ID="pass" TextMode="Password" runat="server"/><asp:Button runat="server" Text="Login" OnClick="SignIn"/></asp:Panel>
<asp:Panel ID="panel" runat="server"><asp:DropDownList ID="tables" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Switch"/><asp:DropDownList ID="course" runat="server" AutoPostBack="true"/><asp:GridView ID="grid" runat="server" AutoGenerateSelectButton="true" OnSelectedIndexChanged="SelectRow"/><asp:Panel ID="inputs" runat="server"/><asp:Button runat="server" Text="Add" CommandName="Add" OnCommand="Change"/><asp:Button runat="server" Text="Update" CommandName="Update" OnCommand="Change"/><asp:Button runat="server" Text="Delete" CommandName="Delete" OnCommand="Change"/></asp:Panel><asp:Label ID="msg" runat="server"/></form></body></html>
