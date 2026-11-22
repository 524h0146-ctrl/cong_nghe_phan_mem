using System;using System.Data;using System.Windows.Forms;using System.Drawing;using System.Linq;using School;
class SchoolForm:Form{
    readonly SchoolService service=new SchoolService(); readonly Panel content=new Panel{Dock=DockStyle.Fill};
    [STAThread]static void Main(){Application.EnableVisualStyles();Application.Run(new SchoolForm());}
    Button Btn(string title,Action a){var b=new Button{Text=title,AutoSize=true};b.Click+=(s,e)=>{try{a();}catch(Exception ex){MessageBox.Show(ex.Message);}};return b;}
    SchoolForm(){Text="School Management";Width=980;Height=560;Controls.Add(content);var box=new FlowLayoutPanel{Dock=DockStyle.Top};
        var u=new TextBox();var p=new TextBox{UseSystemPasswordChar=true};box.Controls.AddRange(new Control[]{new Label{Text="Username"},u,new Label{Text="Password"},p,Btn("Login",()=>{
            if(!service.Login(u.Text,p.Text)){MessageBox.Show("Invalid login");return;}content.Controls.Clear();var menu=new MenuStrip();
            foreach(var key in Repository.Fields.Keys){var k=key;menu.Items.Add(k,null,(s,e)=>Open(k));}
            menu.Items.Add("Exit",null,(s,e)=>Close());Controls.Add(menu);MainMenuStrip=menu;Open("tblStudents");})});content.Controls.Add(box);}
    void Open(string table){content.Controls.Clear();var grid=new DataGridView{Dock=DockStyle.Fill,ReadOnly=true,AllowUserToAddRows=false,SelectionMode=DataGridViewSelectionMode.FullRowSelect};
        var bar=new FlowLayoutPanel{Dock=DockStyle.Bottom,Height=120,AutoScroll=true};content.Controls.Add(grid);content.Controls.Add(bar);
        var fields=Repository.Fields[table];var inputs=fields.Select(f=>new TextBox{Width=115}).ToArray();
        for(int i=0;i<fields.Length;i++){bar.Controls.Add(new Label{AutoSize=true,Text=fields[i]});bar.Controls.Add(inputs[i]);}
        var filter=new ComboBox{DropDownStyle=ComboBoxStyle.DropDownList};
        Action refresh=()=>grid.DataSource=service.List(table,filter.SelectedValue is int?(int)filter.SelectedValue:0);
        if(table=="tblStudentCourses"){filter.DisplayMember="CourseName";filter.ValueMember="CourseID";filter.DataSource=service.List("tblCourses");bar.Controls.Add(filter);filter.SelectedIndexChanged+=(s,e)=>refresh();}
        else inputs[0].ReadOnly=true;
        grid.SelectionChanged+=(s,e)=>{if(grid.CurrentRow!=null)for(int i=0;i<fields.Length;i++)inputs[i].Text=Convert.ToString(grid.CurrentRow.Cells[i].Value);};
        foreach(var op in new[]{"Add","Update","Delete"})bar.Controls.Add(Btn(op,()=>{
            if(op!="Add"&&grid.CurrentRow==null)return;
            var keys=fields.Select((f,i)=>grid.CurrentRow==null?"":Convert.ToString(grid.CurrentRow.Cells[i].Value)).ToArray();
            service.Change(table,op,inputs.Select(x=>x.Text).ToArray(),keys);refresh();}));
        bar.Controls.Add(Btn("Refresh",refresh));refresh();}
}
