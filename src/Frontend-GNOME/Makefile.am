
EXTRA_DIST =  

ICON = smuxi-frontend-gnome.svg

pixmapdir =  $(datadir)/pixmaps
pixmap_DATA = $(ICON)

desktopdir = $(datadir)/applications
desktop_DATA = smuxi-frontend-gnome.desktop

FRONTEND_GNOME_EXE_CONFIG_SOURCE = smuxi-frontend-gnome.exe.config
FRONTEND_GNOME_EXE_CONFIG = $(BUILD_DIR)/smuxi-frontend-gnome.exe.config

# Warning: This is an automatically generated file, do not edit!

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = @MCS@
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ "-define:CONFIG_NINI,UI_GNOME,GTK_SHARP_2_8" @FRONTEND_GNOME_COMPILER_FLAGS@

ASSEMBLY = ../../bin/release/smuxi-frontend-gnome.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../../bin/release/smuxi-engine.dll \
	../../bin/release/smuxi-common.dll \
	../../bin/release/smuxi-frontend.dll
BUILD_DIR = ../../bin/release

LOG4NET_DLL_SOURCE=../../lib/log4net.dll
SMUXI_ENGINE_DLL_MDB=
NINI_DLL_SOURCE=../../lib/Nini.dll
SMUXI_ENGINE_DLL_SOURCE=../../bin/release/smuxi-engine.dll
SMUXI_FRONTEND_DLL_MDB=
SMUXI_FRONTEND_DLL_SOURCE=../../bin/release/smuxi-frontend.dll
SMUXI_COMMON_DLL_SOURCE=../../bin/release/smuxi-common.dll

endif

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = @MCS@
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug -define:DEBUG "-define:DEBUG,TRACE,LOG4NET,CONFIG_NINI,UI_GNOME,GTK_SHARP_2_8" @FRONTEND_GNOME_COMPILER_FLAGS@

ASSEMBLY = ../../bin/debug/smuxi-frontend-gnome.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../../bin/debug/smuxi-engine.dll \
	../../bin/debug/smuxi-common.dll \
	../../bin/debug/smuxi-frontend.dll
BUILD_DIR = ../../bin/debug

LOG4NET_DLL_SOURCE=../../lib/log4net.dll
SMUXI_ENGINE_DLL_MDB_SOURCE=../../bin/debug/smuxi-engine.dll.mdb
SMUXI_ENGINE_DLL_MDB=$(BUILD_DIR)/smuxi-engine.dll.mdb
NINI_DLL_SOURCE=../../lib/Nini.dll
SMUXI_ENGINE_DLL_SOURCE=../../bin/debug/smuxi-engine.dll
SMUXI_FRONTEND_DLL_MDB_SOURCE=../../bin/debug/smuxi-frontend.dll.mdb
SMUXI_FRONTEND_DLL_MDB=$(BUILD_DIR)/smuxi-frontend.dll.mdb
SMUXI_FRONTEND_DLL_SOURCE=../../bin/debug/smuxi-frontend.dll
SMUXI_COMMON_DLL_SOURCE=../../bin/debug/smuxi-common.dll

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=.resources.dll

PROGRAMFILES = \
	$(SMUXI_ENGINE_DLL_MDB) \
	$(SMUXI_ENGINE_DLL) \
	$(SMUXI_FRONTEND_DLL_MDB) \
	$(SMUXI_FRONTEND_DLL) \
	$(SMUXI_COMMON_DLL)  \
	$(FRONTEND_GNOME_EXE_CONFIG)

LINUX_DESKTOPAPPLICATIONS = \
	$(FRONTEND_GNOME_DESKTOP_DESKTOP)  

BINARIES = \
	$(FRONTEND_GNOME)  

all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_DESKTOPAPPLICATIONS) $(BINARIES) $(ICON)

$(ICON):
	cp $(top_srcdir)/images/icon.svg $(ICON)
	
FILES = \
	$(top_srcdir)/src/AssemblyVersion.cs \
	AssemblyInfo.cs \
	AboutDialog.cs \
	CrashDialog.cs \
	EngineManagerDialog.cs \
	Entry.cs \
	Frontend.cs \
	GnomeUI.cs \
	Main.cs \
	MainWindow.cs \
	NotImplementedMessageDialog.cs \
	Notebook.cs \
	SplashScreenWindow.cs \
	ChatTypeWidget.cs \
	ChatViewManager.cs \
	ColorTools.cs \
	ColorContrast.cs \
	PangoTools.cs \
	Preferences/ServerListView.cs \
	Preferences/PreferencesDialog.cs \
	Views/MessageTextView.cs \
	Views/ServerWidget.cs \
	Views/ServerDialog.cs \
	Views/FilterListWidget.cs \
	Views/Chats/ChatView.cs \
	Views/Chats/GroupChatView.cs \
	Views/Chats/PersonChatView.cs \
	Views/Chats/ProtocolChatView.cs \
	Views/Chats/SessionChatView.cs \
	Views/Assistants/Engine/EngineAssistantNameWidget.cs \
	Views/Assistants/Engine/EngineAssistantCredentialsWidget.cs \
	Views/Assistants/Engine/EngineAssistantConnectionWidget.cs \
	Views/Assistants/Engine/EngineAssistantIntroWidget.cs \
	Views/Assistants/Engine/EngineAssistant.cs \
	QuickConnectDialog.cs \
	SteticPreferencesDialog.cs \
	FindGroupChatDialog.cs \
	ChatFindDialog.cs \
	OpenChatDialog.cs \
	ThemeSettings.cs \
	StatusIconManager.cs \
	gtk-gui/generated.cs \
	gtk-gui/Smuxi.Frontend.Gnome.EngineAssistantConnectionWidget.cs \
	gtk-gui/Smuxi.Frontend.Gnome.EngineAssistantCredentialsWidget.cs \
	gtk-gui/Smuxi.Frontend.Gnome.EngineAssistantIntroWidget.cs \
	gtk-gui/Smuxi.Frontend.Gnome.EngineAssistantNameWidget.cs \
	gtk-gui/Smuxi.Frontend.Gnome.OpenChatDialog.cs \
	gtk-gui/Smuxi.Frontend.Gnome.SteticPreferencesDialog.cs \
	gtk-gui/Smuxi.Frontend.Gnome.QuickConnectDialog.cs \
	gtk-gui/Smuxi.Frontend.Gnome.FindGroupChatDialog.cs \
	gtk-gui/Smuxi.Frontend.Gnome.ChatFindDialog.cs \
	gtk-gui/Smuxi.Frontend.Gnome.ChatTypeWidget.cs \
	gtk-gui/Smuxi.Frontend.Gnome.ServerDialog.cs \
	gtk-gui/Smuxi.Frontend.Gnome.ServerWidget.cs \
	gtk-gui/Smuxi.Frontend.Gnome.FilterListWidget.cs
	
DATA_FILES = \
	smuxi-frontend-gnome.desktop

RESOURCES = \
	gtk-gui/gui.stetic \
	$(top_srcdir)/images/about.png \
	$(top_srcdir)/images/splashscreen.png \
	$(top_srcdir)/glade/smuxi-frontend-gnome.glade \
	$(top_srcdir)/images/connect.png \
	$(top_srcdir)/images/edit.png \
	$(top_srcdir)/images/icon.svg \
	$(top_srcdir)/images/group-chat.svg \
	$(top_srcdir)/images/person-chat.svg \
	$(top_srcdir)/images/protocol-chat.svg \
	$(top_srcdir)/images/session-chat.svg 

EXTRAS = \
	smuxi-frontend-gnome.in \
	$(FRONTEND_GNOME_EXE_CONFIG_SOURCE)

REFERENCES =  \
	System.Runtime.Remoting \
	System \
	Mono.Posix \
	System.Drawing \
	$(GLIB_SHARP_20_LIBS) \
	$(GLADE_SHARP_20_LIBS) \
	$(GTK_SHARP_20_LIBS) \
	$(LOG4NET_LIBS)

DLL_REFERENCES = 

CLEANFILES = $(LINUX_DESKTOPAPPLICATIONS) $(PROGRAMFILES) $(BINARIES) $(ICON)

include $(top_srcdir)/Makefile.include

SMUXI_ENGINE_DLL = $(BUILD_DIR)/smuxi-engine.dll
FRONTEND_GNOME = $(BUILD_DIR)/smuxi-frontend-gnome
FRONTEND_GNOME_DESKTOP = $(BUILD_DIR)/smuxi-frontend-gnome.desktop
SMUXI_FRONTEND_DLL = $(BUILD_DIR)/smuxi-frontend.dll
SMUXI_COMMON_DLL = $(BUILD_DIR)/smuxi-common.dll

$(eval $(call emit-deploy-wrapper,FRONTEND_GNOME,smuxi-frontend-gnome,x))
$(eval $(call emit-deploy-target,FRONTEND_GNOME_DESKTOP))
$(eval $(call emit-deploy-target,FRONTEND_GNOME_EXE_CONFIG))
$(eval $(call emit-deploy-target,SMUXI_ENGINE_DLL_MDB))
$(eval $(call emit-deploy-target,SMUXI_ENGINE_DLL))
$(eval $(call emit-deploy-target,SMUXI_FRONTEND_DLL_MDB))
$(eval $(call emit-deploy-target,SMUXI_FRONTEND_DLL))
$(eval $(call emit-deploy-target,SMUXI_COMMON_DLL))

$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(build_resx_resources) : %.resources: %.resx
	resgen2 '$<' '$@'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(dir $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
