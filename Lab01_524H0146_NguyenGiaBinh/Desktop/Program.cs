using System;
using System.Drawing;
using System.Windows.Forms;
class ControlsLab : Form {
    readonly FlowLayoutPanel body = new FlowLayoutPanel {Dock=DockStyle.Fill};
    readonly ComboBox choose = new ComboBox {Dock=DockStyle.Top,DropDownStyle=ComboBoxStyle.DropDownList};
    readonly Timer timer = new Timer {Interval=100};
    [STAThread] static void Main(){Application.EnableVisualStyles(); var form=new ControlsLab();
        if(Array.IndexOf(Environment.GetCommandLineArgs(),"--capture")>=0)form.Shown+=(s,e)=>form.CaptureOutputs();
        Application.Run(form);}
    async void CaptureOutputs(){
        var folder=System.IO.Path.GetFullPath("Evidence");System.IO.Directory.CreateDirectory(folder);
        try{
            for(int exercise=11;exercise<=20;exercise++){
                choose.SelectedIndex=exercise-1;Activate();Refresh();await System.Threading.Tasks.Task.Delay(500);
                using(var image=new Bitmap(Width,Height))
                using(var g=Graphics.FromImage(image)){
                    g.CopyFromScreen(Left,Top,0,0,Size);
                    image.Save(System.IO.Path.Combine(folder,"Ex"+exercise+".png"),System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }finally{Close();}
    }
    ControlsLab(){
        Text="Windows Forms Controls"; Width=840; Height=560;
        Controls.Add(body); Controls.Add(choose); Controls.Add(new Label{Dock=DockStyle.Right,Width=150,Text="Developed by Nguyễn Gia Bình"});
        string[] names={"Hello","Button","Input","Calculator","Selection","Grid","Add row","Edit / delete","Grid category","Date and progress","TreeView","ListView","Tabs","MenuStrip","Context menu","Toolbar","Status","Split","Flow","Table"};
        for(int i=0;i<names.Length;i++)choose.Items.Add((i+1)+" · "+names[i]);
        choose.SelectedIndexChanged+=(s,e)=>ShowExercise(choose.SelectedIndex+1);choose.SelectedIndex=0;
    }
    Button Button(string text, Action action){var b=new Button{Text=text,AutoSize=true};b.Click+=(s,e)=>action();return b;}
    void ShowExercise(int n){
        timer.Stop();while(body.Controls.Count>0)body.Controls[0].Dispose();
        var label=new Label{Text="Hello, World!",AutoSize=true};
        var entry=new TextBox();
        if(n<=3){body.Controls.Add(label);if(n==2)body.Controls.Add(Button("Click",()=>label.Text="Button clicked!"));
            if(n==3){body.Controls.Add(entry);body.Controls.Add(Button("Show",()=>MessageBox.Show(entry.Text)));}return;}
        if(n==4){var b=new TextBox();body.Controls.AddRange(new Control[]{entry,b});
            foreach(var op in new[]{"+","-","*","/"})body.Controls.Add(Button(op,()=>{
                decimal x,y;if(!decimal.TryParse(entry.Text,out x)||!decimal.TryParse(b.Text,out y)||(op=="/"&&y==0)){label.Text="Invalid input";return;}
                label.Text=(op=="+"?x+y:op=="-"?x-y:op=="*"?x*y:x/y).ToString();}));
            body.Controls.Add(label);return;}
        if(n==5){var list=new ListBox();list.Items.AddRange(new[]{"Item 1","Item 2"});var cb=new ComboBox();cb.Items.AddRange(new[]{"Option A","Option B"});
            body.Controls.AddRange(new Control[]{list,cb,Button("Show",()=>MessageBox.Show(list.SelectedItem+" / "+cb.SelectedItem))});return;}
        if(n>=6&&n<=9){var g=new DataGridView{Width=580,Height=270,AllowUserToAddRows=false};foreach(var col in new[]{"ID","Name","Age"})g.Columns.Add(col,col);
            g.Rows.Add(1,"Nguyễn Gia Bình",20);g.Rows.Add(2,"Mai Anh",21);body.Controls.Add(g);
            if(n>=7)body.Controls.Add(Button("Add",()=>g.Rows.Add(null,"New student",18)));
            if(n>=8)body.Controls.Add(Button("Delete selected",()=>{if(g.CurrentRow!=null)g.Rows.Remove(g.CurrentRow);}));
            if(n==9){var c=new DataGridViewComboBoxColumn{HeaderText="Category"};c.Items.AddRange("A","B");g.Columns.Add(c);}return;}
        if(n==10){var p=new ProgressBar();var t=new Timer{Interval=100};p.Disposed+=(s,e)=>t.Dispose();t.Tick+=(s,e)=>{p.Value+=10;if(p.Value==100)t.Stop();};
            body.Controls.AddRange(new Control[]{new DateTimePicker(),p,Button("Start",()=>{p.Value=0;t.Start();})});return;}
        if(n==11){var tree=new TreeView();var r=tree.Nodes.Add("Root");r.Nodes.Add("Child 1");r.Nodes.Add("Child 2");tree.ExpandAll();body.Controls.Add(tree);}
        if(n==12){var v=new ListView{View=View.Details,Width=350};v.Columns.Add("Name",150);v.Columns.Add("Role",120);v.Items.Add(new ListViewItem(new[]{"Bình","Student"}));body.Controls.Add(v);}
        if(n==13){var tabs=new TabControl();foreach(var text in new[]{"Home","Settings"}){var page=new TabPage(text);page.Controls.Add(new Label{Text=text});tabs.TabPages.Add(page);}body.Controls.Add(tabs);}
        if(n==14){var m=new MenuStrip();var f=new ToolStripMenuItem("File");f.DropDownItems.Add("Exit",null,(s,e)=>Close());m.Items.Add(f);body.Controls.Add(m);}
        if(n==15){entry.Text="Right click";var m=new ContextMenuStrip();m.Items.Add("Clear",null,(s,e)=>entry.Clear());entry.ContextMenuStrip=m;body.Controls.Add(entry);}
        if(n==16){var t=new ToolStrip();t.Items.Add("New",null,(s,e)=>entry.Clear());t.Items.Add("Show",null,(s,e)=>MessageBox.Show(entry.Text));body.Controls.AddRange(new Control[]{t,entry});}
        if(n==17){var status=new StatusStrip();var msg=status.Items.Add("Ready");body.Controls.AddRange(new Control[]{status,Button("Update",()=>msg.Text="Updated")});}
        if(n==18){var split=new SplitContainer{Width=550,Height=200};split.Panel1.Controls.Add(new Label{Text="Navigation"});split.Panel2.Controls.Add(new Label{Text="Details"});body.Controls.Add(split);}
        if(n>=19){Panel panel=n==19?(Panel)new FlowLayoutPanel():new TableLayoutPanel{ColumnCount=2,RowCount=2};panel.Size=new Size(280,140);for(int i=1;i<=4;i++)panel.Controls.Add(new Button{Text="Button "+i});body.Controls.Add(panel);}
    }
}
