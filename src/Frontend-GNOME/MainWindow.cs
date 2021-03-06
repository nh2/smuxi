/*
 * $Id$
 * $URL$
 * $Rev$
 * $Author$
 * $Date$
 *
 * Smuxi - Smart MUltipleXed Irc
 *
 * Copyright (c) 2005-2008 Mirco Bauer <meebey@meebey.net>
 *
 * Full GPL License: <http://www.gnu.org/licenses/gpl.txt>
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307 USA
 */

using System;
using System.IO;
using System.Threading;
using System.Reflection;
using Mono.Unix;
using Smuxi.Common;
using Smuxi.Engine;

namespace Smuxi.Frontend.Gnome
{
    public class MainWindow : Gtk.Window
    {
#if LOG4NET
        private static readonly log4net.ILog f_Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
#endif
        private Gtk.Statusbar    _NetworkStatusbar;
        private Gtk.Statusbar    _Statusbar;
        private Gtk.ProgressBar  _ProgressBar;
        private Entry            _Entry;
        private Notebook         _Notebook;
        private bool             _CaretMode;
        private ChatViewManager  _ChatViewManager;
        private IFrontendUI      _UI;
        private EngineManager    _EngineManager;
        private Gtk.MenuItem     _CloseChatMenuItem;
        private NotificationAreaIconMode _NotificationAreaIconMode;
#if GTK_SHARP_2_10
        private StatusIconManager _StatusIconManager;
#endif
        private bool             _IsMinimized;
        private bool             _IsMaximized;
        
        public bool CaretMode {
            get {
                return _CaretMode;
            }
        }
        
        public Notebook Notebook {
            get {
                return _Notebook;
            }
        }
        
        public IFrontendUI UI {
            get {
                return _UI;
            }
        }
        
        public EngineManager EngineManager {
            get {
                return _EngineManager;
            }
        }
        
        public ChatViewManager ChatViewManager {
            get {
                return _ChatViewManager;
            }
        }
        
        public new Gtk.Statusbar NetworkStatusbar {
            get {
                return _NetworkStatusbar;
            }
        } 

        public new Gtk.Statusbar Statusbar {
            get {
                return _Statusbar;
            }
        } 

        public Gtk.ProgressBar ProgressBar {
            get {
                return _ProgressBar;
            }
        }
        
        public Entry Entry {
            get {
                return _Entry;
            }
        }
        
        public bool IsMaximized {
            get {
                return _IsMaximized;
            }
        }
        
        public bool IsMinimized {
            get {
                return _IsMinimized;
            }
        }

        public EventHandler Minimized;
        public EventHandler Unminimized;

        public MainWindow() : base("Smuxi")
        {
            // restore window size / position
            int width, heigth;
            if (Frontend.FrontendConfig[Frontend.UIName + "/Interface/Width"] != null) {
                width  = (int) Frontend.FrontendConfig[Frontend.UIName + "/Interface/Width"];
            } else {
                width = 800;
            }
            if (Frontend.FrontendConfig[Frontend.UIName + "/Interface/Heigth"] != null) {
                heigth = (int) Frontend.FrontendConfig[Frontend.UIName + "/Interface/Heigth"];
            } else {
                heigth = 600;
            }
            if (width == -1 && heigth == -1) {
                SetDefaultSize(800, 600);
                Maximize();
            } else {
                SetDefaultSize(width, heigth);
            }
            
            int x, y;
            if (Frontend.FrontendConfig[Frontend.UIName + "/Interface/XPosition"] != null) {
                x = (int) Frontend.FrontendConfig[Frontend.UIName + "/Interface/XPosition"];
            } else {
                x = 0;
            }
            if (Frontend.FrontendConfig[Frontend.UIName + "/Interface/YPosition"] != null) {
                y = (int) Frontend.FrontendConfig[Frontend.UIName + "/Interface/YPosition"];
            } else {
                y = 0;
            }
            if (x == 0 && y == 0) {
                SetPosition(Gtk.WindowPosition.Center);
            } else {
                Move(x, y);
            }
            
            DeleteEvent += OnDeleteEvent;
            FocusInEvent += OnFocusInEvent;
            WindowStateEvent += OnWindowStateEvent;
                    
            
            
            Gtk.AccelGroup agrp = new Gtk.AccelGroup();
            Gtk.AccelKey   akey;
            AddAccelGroup(agrp);
            
            // Menu
            Gtk.MenuBar mb = new Gtk.MenuBar();
            Gtk.Menu menu;
            Gtk.MenuItem item;
            Gtk.ImageMenuItem image_item;
            
            // Menu - File
            menu = new Gtk.Menu();
            item = new Gtk.MenuItem(_("_File"));
            item.Submenu = menu;
            mb.Append(item);

            item = new Gtk.ImageMenuItem(Gtk.Stock.Preferences, agrp);
            item.Activated += new EventHandler(_OnPreferencesButtonClicked);
            menu.Append(item);
            
            menu.Append(new Gtk.SeparatorMenuItem());
            
            item = new Gtk.ImageMenuItem(Gtk.Stock.Quit, agrp);
            item.Activated += new EventHandler(_OnQuitButtonClicked);
            menu.Append(item);
            
            // Menu - Server
            menu = new Gtk.Menu();
            item = new Gtk.MenuItem(_("_Server"));
            item.Submenu = menu;
            mb.Append(item);
            
            image_item = new Gtk.ImageMenuItem(_("_Quick Connect"));
            image_item.Image = new Gtk.Image(Gtk.Stock.Connect, Gtk.IconSize.Menu);
            image_item.Activated += OnServerQuickConnectButtonClicked;
            menu.Append(image_item);
            
            menu.Append(new Gtk.SeparatorMenuItem());
                    
            image_item = new Gtk.ImageMenuItem(Gtk.Stock.Add, agrp);
            image_item.Activated += OnServerAddButtonClicked;
            menu.Append(image_item);
            
            image_item = new Gtk.ImageMenuItem(_("_Manage"));
            image_item.Image = new Gtk.Image(Gtk.Stock.Edit, Gtk.IconSize.Menu);
            image_item.Activated += OnServerManageServersButtonClicked;
            menu.Append(image_item);
            
            // Menu - Chat
            menu = new Gtk.Menu();
            item = new Gtk.MenuItem(_("_Chat"));
            item.Submenu = menu;
            mb.Append(item);
            
            image_item = new Gtk.ImageMenuItem(_("Open / Join Chat"));
            image_item.Image = new Gtk.Image(Gtk.Stock.Open, Gtk.IconSize.Menu);
            image_item.Activated += OnChatOpenChatButtonClicked;
            menu.Append(image_item);
                    
            image_item = new Gtk.ImageMenuItem(_("_Find Group Chat"));
            image_item.Image = new Gtk.Image(Gtk.Stock.Find, Gtk.IconSize.Menu);
            image_item.Activated += OnChatFindGroupChatButtonClicked;
            menu.Append(image_item);
            
            image_item = new Gtk.ImageMenuItem(_("C_lear All Activity"));
            image_item.Image = new Gtk.Image(Gtk.Stock.Clear, Gtk.IconSize.Menu);
            image_item.Activated += OnChatClearAllActivityButtonClicked;
            menu.Append(image_item);
            
            menu.Append(new Gtk.SeparatorMenuItem());
                    
            image_item = new Gtk.ImageMenuItem(_("_Next Chat"));
            image_item.Image = new Gtk.Image(Gtk.Stock.GoForward, Gtk.IconSize.Menu);
            image_item.Activated += OnNextChatMenuItemActivated;
            akey = new Gtk.AccelKey();
            akey.AccelFlags = Gtk.AccelFlags.Visible;
            akey.AccelMods = Gdk.ModifierType.ControlMask;
            akey.Key = Gdk.Key.Page_Down;
            image_item.AddAccelerator("activate", agrp, akey);
            menu.Append(image_item);
            
            image_item = new Gtk.ImageMenuItem(_("_Previous Chat"));
            image_item.Image = new Gtk.Image(Gtk.Stock.GoBack, Gtk.IconSize.Menu);
            image_item.Activated += OnPreviousChatMenuItemActivated;
            akey = new Gtk.AccelKey();
            akey.AccelFlags = Gtk.AccelFlags.Visible;
            akey.AccelMods = Gdk.ModifierType.ControlMask;
            akey.Key = Gdk.Key.Page_Up;
            image_item.AddAccelerator("activate", agrp, akey);
            menu.Append(image_item);
            
            menu.Append(new Gtk.SeparatorMenuItem());
                    
            /*
            // TODO: make a radio item for each chat hotkey
            Gtk.RadioMenuItem radio_item;
            radio_item = new Gtk.RadioMenuItem();
            radio_item = new Gtk.RadioMenuItem(radio_item);
            radio_item = new Gtk.RadioMenuItem(radio_item);
                    
            menu.Append(new Gtk.SeparatorMenuItem());
            */
            
            /*
            image_item = new Gtk.ImageMenuItem(Gtk.Stock.Find, agrp);
            image_item.Activated += OnFindChatMenuItemActivated;
            menu.Append(image_item);
            
            item = new Gtk.MenuItem(_("Find _Next"));
            item.Activated += OnFindNextChatMenuItemActivated;
            akey = new Gtk.AccelKey();
            akey.AccelFlags = Gtk.AccelFlags.Visible;
            akey.AccelMods = Gdk.ModifierType.ControlMask;
            akey.Key = Gdk.Key.G;
            item.AddAccelerator("activate", agrp, akey);
            menu.Append(item);
            
            item = new Gtk.MenuItem(_("Find _Previous"));
            item.Activated += OnFindPreviousChatMenuItemActivated;
            akey = new Gtk.AccelKey();
            akey.AccelFlags = Gtk.AccelFlags.Visible;
            akey.AccelMods = Gdk.ModifierType.ControlMask | Gdk.ModifierType.ShiftMask;
            akey.Key = Gdk.Key.G;
            item.AddAccelerator("activate", agrp, akey);
            menu.Append(item);
            */
            
            _CloseChatMenuItem = new Gtk.ImageMenuItem(Gtk.Stock.Close, agrp);
            _CloseChatMenuItem.Activated += OnCloseChatMenuItemActivated;
            menu.Append(_CloseChatMenuItem);

            // Menu - Engine
            menu = new Gtk.Menu();
            item = new Gtk.MenuItem(_("_Engine"));
            item.Submenu = menu;
            mb.Append(item);

            item = new Gtk.MenuItem(_("_Use Local Engine"));
            item.Activated += new EventHandler(_OnUseLocalEngineButtonClicked);
            menu.Append(item);
            
            menu.Append(new Gtk.SeparatorMenuItem());
                    
            image_item = new Gtk.ImageMenuItem(_("_Add Remote Engine"));
            image_item.Image = new Gtk.Image(Gtk.Stock.Add, Gtk.IconSize.Menu);
            image_item.Activated += new EventHandler(_OnAddRemoteEngineButtonClicked);
            menu.Append(image_item);
            
            image_item = new Gtk.ImageMenuItem(_("_Switch Remote Engine"));
            image_item.Image = new Gtk.Image(Gtk.Stock.Refresh, Gtk.IconSize.Menu);
            image_item.Activated += new EventHandler(_OnSwitchRemoteEngineButtonClicked);
            menu.Append(image_item);
            
            // Menu - View
            menu = new Gtk.Menu();
            item = new Gtk.MenuItem(_("_View"));
            item.Submenu = menu;
            mb.Append(item);
            
            item = new Gtk.CheckMenuItem(_("_Caret Mode"));
            item.Activated += new EventHandler(_OnCaretModeButtonClicked);
            akey = new Gtk.AccelKey();
            akey.AccelFlags = Gtk.AccelFlags.Visible;
            akey.Key = Gdk.Key.F7;
            item.AddAccelerator("activate", agrp, akey);
            menu.Append(item);
            
            // Menu - Help
            menu = new Gtk.Menu();
            item = new Gtk.MenuItem(_("_Help"));
            item.Submenu = menu;
            mb.Append(item);
            
            image_item = new Gtk.ImageMenuItem(Gtk.Stock.About, agrp);
            image_item.Activated += new EventHandler(_OnAboutButtonClicked);
            menu.Append(image_item);
            
            // TODO: network treeview
            _Notebook = new Notebook();
            _Notebook.SwitchPage += OnNotebookSwitchPage;
            
            _ChatViewManager = new ChatViewManager(_Notebook, null);
            Assembly asm = Assembly.GetExecutingAssembly();
            _ChatViewManager.Load(asm);
            _ChatViewManager.LoadAll(System.IO.Path.GetDirectoryName(asm.Location),
                                     "smuxi-frontend-gnome-*.dll");
            _ChatViewManager.ChatAdded += OnChatViewManagerChatAdded;
            _ChatViewManager.ChatRemoved += OnChatViewManagerChatRemoved;
            
#if GTK_SHARP_2_10
            _StatusIconManager = new StatusIconManager(this, _ChatViewManager);
#endif

            _UI = new GnomeUI(_ChatViewManager);
            
            // HACK: Frontend.FrontendConfig out of scope
            _EngineManager = new EngineManager(Frontend.FrontendConfig, _UI);

            _Entry = new Entry(_Notebook);
            
            _ProgressBar = new Gtk.ProgressBar();
            
            Gtk.VBox vbox = new Gtk.VBox();
            vbox.PackStart(mb, false, false, 0);
            vbox.PackStart(_Notebook, true, true, 0);
            vbox.PackStart(_Entry, false, false, 0);

            _NetworkStatusbar = new Gtk.Statusbar();
            _NetworkStatusbar.WidthRequest = 300;
            _NetworkStatusbar.HasResizeGrip = false;
            
            _Statusbar = new Gtk.Statusbar();
            
            Gtk.HBox status_bar_hbox = new Gtk.HBox();
            status_bar_hbox.Homogeneous = true;
            status_bar_hbox.PackStart(_NetworkStatusbar, false, true, 0);
            status_bar_hbox.PackStart(_Statusbar, true, true, 0);

            Gtk.HBox status_hbox = new Gtk.HBox();
            status_hbox.PackStart(status_bar_hbox);
            status_hbox.PackStart(_ProgressBar, false, false, 0);

            vbox.PackStart(status_hbox, false, false, 0);
            Add(vbox);
        }

        public void ApplyConfig(UserConfig userConfig)
        {
            Trace.Call(userConfig);
            
            if (userConfig == null) {
                throw new ArgumentNullException("userConfig");
            }
                    
            string modeStr = (string) userConfig["Interface/Notification/NotificationAreaIconMode"];
            NotificationAreaIconMode mode = (NotificationAreaIconMode) Enum.Parse(
                typeof(NotificationAreaIconMode),
                modeStr
            );
            _NotificationAreaIconMode = mode;
            
#if GTK_SHARP_2_10
            _StatusIconManager.ApplyConfig(userConfig);
#endif
            _Entry.ApplyConfig(userConfig);
            _Notebook.ApplyConfig(userConfig);
            _ChatViewManager.ApplyConfig(userConfig);
        }

        public void UpdateTitle()
        {
            UpdateTitle(null, null);
        }

        public void UpdateTitle(ChatView chatView, string protocolStatus)
        {
            Trace.Call(chatView, protocolStatus);

            if (chatView == null) {
                chatView = Notebook.CurrentChatView;
            }
            if (chatView == null) {
                return;
            }

            string title;
            if (chatView is SessionChatView) {
                title = String.Empty;
            } else if (chatView is ProtocolChatView) {
                title = protocolStatus;
            } else {
                title = String.Format("{0} @ {1}",
                                      chatView.Name,
                                      protocolStatus);
            }
            if (!String.IsNullOrEmpty(title)) {
                title += " - ";
            }
            title += "Smuxi";

            Title = title;
        }

        private void _OnQuitButtonClicked(object obj, EventArgs args)
        {
            Trace.Call(obj, args);
            
            try {
                Frontend.Quit();
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }

        protected virtual void OnDeleteEvent(object sender, Gtk.DeleteEventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
                if (_NotificationAreaIconMode == NotificationAreaIconMode.Closed) {
                    // showing the tray icon is handled in OnWindowStateEvent
                    Hide();
                    
                    // don't destroy the window nor quit smuxi!
                    e.RetVal = true;
                    return;
                }
                
                Frontend.Quit();
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
    
        protected virtual void OnFocusInEvent(object sender, EventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
                UrgencyHint = false;
                ChatView chatView = _Notebook.CurrentChatView;
                if (chatView != null) {
                    // clear activity and highlight
                    chatView.HasHighlight = false;
                    chatView.HasActivity = false;
                    var lastMsg = chatView.OutputMessageTextView.LastMessage;
                    // update last seen highlight
                    ThreadPool.QueueUserWorkItem(delegate {
                        try {
                            if (lastMsg == null) {
                                return;
                            }
                            // REMOTING CALL 1
                            chatView.ChatModel.LastSeenHighlight = lastMsg.TimeStamp;
                        } catch (Exception ex) {
#if LOG4NET
                            f_Logger.Error("OnFocusInEvent(): Exception", ex);
#endif
                        }
                    });
                }
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        protected virtual void OnServerQuickConnectButtonClicked(object sender, EventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
                QuickConnectDialog dialog = new QuickConnectDialog(this);
                dialog.Load();
                int res = dialog.Run();
                ServerModel server = dialog.Server;
                dialog.Destroy();
                if (res != (int) Gtk.ResponseType.Ok) {
                    return;
                }
                if (server == null) {
#if LOG4NET
                    f_Logger.Error("OnServerQuickConnectButtonClicked(): server is null!");
                    return;
#endif
                }
                
                Frontend.Session.Connect(server, Frontend.FrontendManager);
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }

#region Item Event Handlers
        protected virtual void OnServerAddButtonClicked(object sender, EventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
                ServerListController controller = new ServerListController(Frontend.UserConfig);
                ServerDialog dialog = new ServerDialog(this,
                                                       null,
                                                       Frontend.Session.GetSupportedProtocols(),
                                                       controller.GetNetworks());
                int res = dialog.Run();
                ServerModel server = dialog.GetServer();
                dialog.Destroy();
                if (res != (int) Gtk.ResponseType.Ok) {
                    return;
                }
                
                controller.AddServer(server);
                controller.Save();
            } catch (InvalidOperationException ex) {
                Frontend.ShowError(this, _("Unable to add server: "), ex);
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }

        protected virtual void OnServerManageServersButtonClicked(object sender, EventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
                PreferencesDialog dialog = new PreferencesDialog(this);
                dialog.CurrentPage = PreferencesDialog.Page.Servers;
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        protected virtual void OnChatOpenChatButtonClicked(object sender, EventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
                OpenChatDialog dialog = new OpenChatDialog(this);
                int res = dialog.Run();
                
                IProtocolManager manager = Frontend.FrontendManager.CurrentProtocolManager;
                ChatModel chat;
                switch (dialog.ChatType) {
                    case ChatType.Group:
                        chat = new GroupChatModel(
                            dialog.ChatName, 
                            dialog.ChatName,
                            manager
                        );
                        break;
                    case ChatType.Person:
                        chat = new PersonChatModel(
                            null,
                            dialog.ChatName, 
                            dialog.ChatName,
                            manager
                        );
                        break;
                    default:
                        throw new ApplicationException(
                            String.Format(
                                _("Unknown ChatType: {0}"),
                                dialog.ChatType
                            )
                        );
                }
                
                dialog.Destroy();
                if (res != (int) Gtk.ResponseType.Ok) {
                    return;
                }
                
                manager.OpenChat(Frontend.FrontendManager, chat);
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        protected virtual void OnChatFindGroupChatButtonClicked(object sender, EventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
                IProtocolManager manager = Frontend.FrontendManager.CurrentProtocolManager;
                FindGroupChatDialog dialog = new FindGroupChatDialog(
                    this, manager
                );
                int res = dialog.Run();
                GroupChatModel groupChat = dialog.GroupChat;
                dialog.Destroy();
                if (res != (int) Gtk.ResponseType.Ok) {
                    return;
                }
                
                manager.OpenChat(Frontend.FrontendManager, groupChat);
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        protected virtual void OnChatClearAllActivityButtonClicked(object sender, EventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
                _Notebook.ClearAllActivity();
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        protected virtual void OnFindChatMenuItemActivated(object sender, EventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        protected virtual void OnFindNextChatMenuItemActivated(object sender, EventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        protected virtual void OnFindPreviousChatMenuItemActivated(object sender, EventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        protected virtual void OnCloseChatMenuItemActivated(object sender, EventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
                _Notebook.CurrentChatView.Close();
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        protected virtual void OnNextChatMenuItemActivated(object sender, EventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
                if (_Notebook.CurrentPage < _Notebook.NPages) {
                    _Notebook.CurrentPage++;
                }
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        protected virtual void OnPreviousChatMenuItemActivated(object sender, EventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
                if (_Notebook.CurrentPage > 0) {
                    _Notebook.CurrentPage--;
                }
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
                
        protected virtual void OnWindowStateEvent(object sender, Gtk.WindowStateEventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
                // handle minimize / un-minimize
                if ((e.Event.ChangedMask & Gdk.WindowState.Iconified) != 0) {
                    _IsMinimized = (e.Event.NewWindowState & Gdk.WindowState.Iconified) != 0;
#if LOG4NET
                    f_Logger.Debug("OnWindowStateEvent(): _IsMinimized: " + _IsMinimized);
#endif
                    #if DISABLED
                    // BUG: metacity is not allowing us to use the minimize state
                    // to hide and enable the notfication area icon as switching
                    // to a different workspace sets WindowState.Iconified on all
                    // windows, thus this code is disabled. For more details see:
                    // http://projects.qnetp.net/issues/show/158
                    Hide();
                    #endif
                    if (_IsMinimized) {
                        if (Minimized != null) {
                            Minimized(this, EventArgs.Empty);
                        }
                    } else {
                        if (Unminimized != null) {
                            Unminimized(this, EventArgs.Empty);
                        }
                    }
                }

                // handle maximize / un-maximize
                if ((e.Event.ChangedMask & Gdk.WindowState.Maximized) != 0) {
                    _IsMaximized = (e.Event.NewWindowState & Gdk.WindowState.Maximized) != 0;
#if LOG4NET
                    f_Logger.Debug("OnWindowStateEvent(): _IsMaximized: " + _IsMaximized);
#endif
                }
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        protected virtual void OnNotebookSwitchPage(object sender, EventArgs e)
        {
            Trace.Call(sender, e);
            
            try {
                _CloseChatMenuItem.Sensitive = !(_Notebook.CurrentChatView is SessionChatView);
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        private void _OnAboutButtonClicked(object obj, EventArgs args)
        {
            Trace.Call(obj, args);
            
            try {
                AboutDialog ad = new AboutDialog(this);
                ad.Run();
                ad.Destroy();
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        private void _OnPreferencesButtonClicked(object obj, EventArgs args)
        {
            Trace.Call(obj, args);
            
            try {
                new PreferencesDialog(this);
                /*
                SteticPreferencesDialog dialog = new SteticPreferencesDialog();
                dialog.Run();
                */
            } catch (Exception e) {
#if LOG4NET
                f_Logger.Error(e);
#endif
                Frontend.ShowException(this, e);
            }
        }
        
        private void _OnUseLocalEngineButtonClicked(object obj, EventArgs args)
        {
            Trace.Call(obj, args);
            
            try {
                Gtk.MessageDialog md = new Gtk.MessageDialog(null, Gtk.DialogFlags.Modal,
                    Gtk.MessageType.Warning, Gtk.ButtonsType.YesNo,
                    _("Switching to local engine will disconnect you from the current engine!\n"+
                      "Are you sure you want to do this?"));
                int result = md.Run();
                md.Destroy();
                if ((Gtk.ResponseType)result == Gtk.ResponseType.Yes) {
                    Frontend.DisconnectEngineFromGUI();
                    Frontend.InitLocalEngine();
                    Frontend.ConnectEngineToGUI();
                }
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        private void _OnAddRemoteEngineButtonClicked(object obj, EventArgs args)
        {
            Trace.Call(obj, args);
            
            try {
                EngineAssistant assistant = new EngineAssistant(
                    this,
                    Frontend.FrontendConfig
                );
                assistant.Cancel += delegate {
                    assistant.Destroy();
                };
                assistant.Close += delegate {
                    assistant.Destroy();
                };
                assistant.ShowAll();
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
        
        private void _OnSwitchRemoteEngineButtonClicked(object obj, EventArgs args)
        {
            Trace.Call(obj, args);
            
            try {
                Gtk.MessageDialog md = new Gtk.MessageDialog(null, Gtk.DialogFlags.Modal,
                    Gtk.MessageType.Warning, Gtk.ButtonsType.YesNo,
                    _("Switching the remote engine will disconnect you from the current engine!\n"+
                      "Are you sure you want to do this?"));
                int result = md.Run();
                md.Destroy();
                if ((Gtk.ResponseType)result == Gtk.ResponseType.Yes) {
                    Frontend.DisconnectEngineFromGUI();
                    Frontend.ShowEngineManagerDialog();
                }
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }

        private void _OnCaretModeButtonClicked(object obj, EventArgs args)
        {
            Trace.Call(obj, args);
            
            try {
                _CaretMode = !_CaretMode;
                
                for (int i = 0; i < _Notebook.NPages; i++) {
                    ChatView chatView = _Notebook.GetChat(i);
                    chatView.OutputMessageTextView.CursorVisible = _CaretMode;
                }
                
                if (_CaretMode) {
                    _Notebook.CurrentChatView.OutputMessageTextView.HasFocus = true;
                } else {
                    _Entry.HasFocus = true;
                }
            } catch (Exception ex) {
                Frontend.ShowException(this, ex);
            }
        }
#endregion
        
        protected void OnChatViewManagerChatAdded(object sender, ChatViewManagerChatAddedEventArgs e)
        {
            Trace.Call(sender, e);
            
            e.ChatView.OutputMessageTextView.MessageHighlighted += OnChatViewMessageHighlighted;
        }
        
        protected void OnChatViewManagerChatRemoved(object sender, ChatViewManagerChatRemovedEventArgs e)
        {
            Trace.Call(sender, e);
            
            e.ChatView.OutputMessageTextView.MessageHighlighted -= OnChatViewMessageHighlighted;
        }
        
        protected void OnChatViewMessageHighlighted(object sender, MessageTextViewMessageHighlightedEventArgs e)
        {
            Trace.Call(sender, e);
            
            if (!HasToplevelFocus) {
                UrgencyHint = true;
            }
        }

        private static string _(string msg)
        {
            return Mono.Unix.Catalog.GetString(msg);
        }
    }
}
