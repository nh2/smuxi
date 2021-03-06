// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Smuxi.Frontend.Gnome {
    
    
    public partial class FindGroupChatDialog {
        
        private Gtk.VBox vbox2;
        
        private Gtk.HBox hbox1;
        
        private Gtk.HBox hbox2;
        
        private Gtk.Label label1;
        
        private Gtk.Entry f_NameEntry;
        
        private Gtk.Button f_FindButton;
        
        private Gtk.ScrolledWindow GtkScrolledWindow;
        
        private Gtk.TreeView f_TreeView;
        
        private Gtk.Button f_CancelButton;
        
        private Gtk.Button f_OKButton;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget Smuxi.Frontend.Gnome.FindGroupChatDialog
            this.Name = "Smuxi.Frontend.Gnome.FindGroupChatDialog";
            this.Title = Mono.Unix.Catalog.GetString("Smuxi - Find Group Chat");
            this.Icon = Stetic.IconLoader.LoadIcon(this, "gtk-find", Gtk.IconSize.Menu, 16);
            this.WindowPosition = ((Gtk.WindowPosition)(4));
            this.DefaultWidth = 640;
            this.DefaultHeight = 480;
            // Internal child Smuxi.Frontend.Gnome.FindGroupChatDialog.VBox
            Gtk.VBox w1 = this.VBox;
            w1.Name = "dialog1_VBox";
            w1.BorderWidth = ((uint)(2));
            // Container child dialog1_VBox.Gtk.Box+BoxChild
            this.vbox2 = new Gtk.VBox();
            this.vbox2.Name = "vbox2";
            this.vbox2.Spacing = 6;
            // Container child vbox2.Gtk.Box+BoxChild
            this.hbox1 = new Gtk.HBox();
            this.hbox1.Name = "hbox1";
            this.hbox1.Spacing = 6;
            // Container child hbox1.Gtk.Box+BoxChild
            this.hbox2 = new Gtk.HBox();
            this.hbox2.Name = "hbox2";
            this.hbox2.Spacing = 6;
            // Container child hbox2.Gtk.Box+BoxChild
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.LabelProp = Mono.Unix.Catalog.GetString("_Name:");
            this.label1.UseUnderline = true;
            this.hbox2.Add(this.label1);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.hbox2[this.label1]));
            w2.Position = 0;
            w2.Expand = false;
            w2.Fill = false;
            // Container child hbox2.Gtk.Box+BoxChild
            this.f_NameEntry = new Gtk.Entry();
            this.f_NameEntry.CanDefault = true;
            this.f_NameEntry.CanFocus = true;
            this.f_NameEntry.Name = "f_NameEntry";
            this.f_NameEntry.IsEditable = true;
            this.f_NameEntry.InvisibleChar = '●';
            this.hbox2.Add(this.f_NameEntry);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.hbox2[this.f_NameEntry]));
            w3.Position = 1;
            this.hbox1.Add(this.hbox2);
            Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.hbox1[this.hbox2]));
            w4.Position = 0;
            // Container child hbox1.Gtk.Box+BoxChild
            this.f_FindButton = new Gtk.Button();
            this.f_FindButton.CanFocus = true;
            this.f_FindButton.Name = "f_FindButton";
            this.f_FindButton.UseStock = true;
            this.f_FindButton.UseUnderline = true;
            this.f_FindButton.Label = "gtk-find";
            this.hbox1.Add(this.f_FindButton);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.hbox1[this.f_FindButton]));
            w5.Position = 1;
            w5.Expand = false;
            w5.Fill = false;
            this.vbox2.Add(this.hbox1);
            Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(this.vbox2[this.hbox1]));
            w6.Position = 0;
            w6.Expand = false;
            w6.Fill = false;
            // Container child vbox2.Gtk.Box+BoxChild
            this.GtkScrolledWindow = new Gtk.ScrolledWindow();
            this.GtkScrolledWindow.Name = "GtkScrolledWindow";
            this.GtkScrolledWindow.ShadowType = ((Gtk.ShadowType)(1));
            // Container child GtkScrolledWindow.Gtk.Container+ContainerChild
            this.f_TreeView = new Gtk.TreeView();
            this.f_TreeView.CanFocus = true;
            this.f_TreeView.Name = "f_TreeView";
            this.GtkScrolledWindow.Add(this.f_TreeView);
            this.vbox2.Add(this.GtkScrolledWindow);
            Gtk.Box.BoxChild w8 = ((Gtk.Box.BoxChild)(this.vbox2[this.GtkScrolledWindow]));
            w8.Position = 1;
            w1.Add(this.vbox2);
            Gtk.Box.BoxChild w9 = ((Gtk.Box.BoxChild)(w1[this.vbox2]));
            w9.Position = 0;
            // Internal child Smuxi.Frontend.Gnome.FindGroupChatDialog.ActionArea
            Gtk.HButtonBox w10 = this.ActionArea;
            w10.Name = "dialog1_ActionArea";
            w10.Spacing = 6;
            w10.BorderWidth = ((uint)(5));
            w10.LayoutStyle = ((Gtk.ButtonBoxStyle)(4));
            // Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.f_CancelButton = new Gtk.Button();
            this.f_CancelButton.CanDefault = true;
            this.f_CancelButton.CanFocus = true;
            this.f_CancelButton.Name = "f_CancelButton";
            this.f_CancelButton.UseStock = true;
            this.f_CancelButton.UseUnderline = true;
            this.f_CancelButton.Label = "gtk-cancel";
            this.AddActionWidget(this.f_CancelButton, -6);
            Gtk.ButtonBox.ButtonBoxChild w11 = ((Gtk.ButtonBox.ButtonBoxChild)(w10[this.f_CancelButton]));
            w11.Expand = false;
            w11.Fill = false;
            // Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.f_OKButton = new Gtk.Button();
            this.f_OKButton.CanDefault = true;
            this.f_OKButton.CanFocus = true;
            this.f_OKButton.Name = "f_OKButton";
            this.f_OKButton.UseStock = true;
            this.f_OKButton.UseUnderline = true;
            this.f_OKButton.Label = "gtk-ok";
            this.AddActionWidget(this.f_OKButton, -5);
            Gtk.ButtonBox.ButtonBoxChild w12 = ((Gtk.ButtonBox.ButtonBoxChild)(w10[this.f_OKButton]));
            w12.Position = 1;
            w12.Expand = false;
            w12.Fill = false;
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.label1.MnemonicWidget = this.f_NameEntry;
            this.Show();
            this.f_NameEntry.Activated += new System.EventHandler(this.OnNameEntryActivated);
            this.f_FindButton.Clicked += new System.EventHandler(this.OnFindButtonClicked);
        }
    }
}
