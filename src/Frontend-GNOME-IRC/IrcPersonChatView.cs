/*
 * $Id: GroupChatView.cs 188 2007-04-21 22:03:54Z meebey $
 * $URL: svn+ssh://svn.qnetp.net/svn/smuxi/smuxi/trunk/src/Frontend-GNOME/GroupChatView.cs $
 * $Rev: 188 $
 * $Author: meebey $
 * $Date: 2007-04-22 00:03:54 +0200 (Sun, 22 Apr 2007) $
 *
 * Smuxi - Smart MUltipleXed Irc
 *
 * Copyright (c) 2008 Mirco Bauer <meebey@meebey.net>
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
using System.Globalization;
using Smuxi.Engine;
using Smuxi.Common;

namespace Smuxi.Frontend.Gnome
{
    [ChatViewInfo(ChatType = ChatType.Person, ProtocolManagerType = typeof(IrcProtocolManager))]
    public class IrcPersonChatView : PersonChatView
    {
        private static readonly string _LibraryTextDomain = "smuxi-frontend-gnome-irc";
        private IrcProtocolManager _IrcProtocolManager;

        public IrcPersonChatView(PersonChatModel personChat) : base(personChat)
        {
            Trace.Call(personChat);

            _IrcProtocolManager = (IrcProtocolManager)personChat.ProtocolManager;

            OutputMessageTextView.PopulatePopup += _OnOutputMessageTextViewPopulatePopup;
        }
        
        private void _OnOutputMessageTextViewPopulatePopup (object o, Gtk.PopulatePopupArgs args)
        {
            if (OutputMessageTextView.IsAtUrlTag) {
                return;
            }

            Gtk.Menu popup = args.Menu;

            popup.Append(new Gtk.SeparatorMenuItem());

            Gtk.ImageMenuItem whois_item = new Gtk.ImageMenuItem(_("Whois"));
            whois_item.Activated += _OnMenuWhoisItemActivated;
            popup.Append(whois_item);

            Gtk.ImageMenuItem ctcp_item = new Gtk.ImageMenuItem(_("CTCP"));
            Gtk.Menu ctcp_menu_item = new CtcpMenu(_IrcProtocolManager,
                                                   Frontend.MainWindow.ChatViewManager,
                                                   PersonModel);
            ctcp_item.Submenu = ctcp_menu_item;
            popup.Append(ctcp_item);

            Gtk.ImageMenuItem invite_to_item = new Gtk.ImageMenuItem(_("Invite to"));
            Gtk.Menu invite_to_menu_item = new InviteToMenu(_IrcProtocolManager,
                                                            Frontend.MainWindow.ChatViewManager,
                                                            PersonModel);
            invite_to_item.Submenu = invite_to_menu_item;
            popup.Append(invite_to_item);

            popup.ShowAll();
        }

        void _OnMenuWhoisItemActivated (object sender, EventArgs e)
        {
            Trace.Call(sender, e);

            _IrcProtocolManager.CommandWhoIs(
                new CommandModel(
                    Frontend.FrontendManager,
                    ChatModel,
                    ChatModel.ID
                )
             );
        }

        private static string _(string msg)
        {
            return LibraryCatalog.GetString(msg, _LibraryTextDomain);
        }
    }
}
