SetCompressor bzip2

# Defines
!define NAME "ComicPack"
!define NAMEWOOFY "Woofy"
!define REGKEY "SOFTWARE\${NAMEWOOFY}"
!define VERSION @COMICPACKVERSION@
!define COMPANY "Vlad Iliescu"
!define URL "http://woofy.sourceforge.net"
!define FILENAME "@COMICPACKEXEFILENAME@"


# MUI defines
!define MUI_ABORTWARNING
#!define MUI_LICENSEPAGE_CHECKBOX
!define MUI_STARTMENUPAGE_REGISTRY_ROOT HKLM
!define MUI_STARTMENUPAGE_REGISTRY_KEY ${REGKEY}
!define MUI_STARTMENUPAGE_DEFAULTFOLDER ${NAMEWOOFY}
!define MUI_FINISHPAGE_RUN $INSTDIR\Woofy.exe
!define MUI_FINISHPAGE_RUN_TEXT "Run ${NAMEWOOFY}"
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\orange-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\orange-uninstall.ico"
!define MUI_HEADERIMAGE_BITMAP "${NSISDIR}\Contrib\Graphics\Header\orange-nsis.bmp"
!define MUI_HEADERIMAGE_UNBITMAP "${NSISDIR}\Contrib\Graphics\Header\orange-uninstall-nsis.bmp"
!define MUI_WELCOMEFINISHPAGE_BITMAP "${NSISDIR}\Contrib\Graphics\Wizard\orange-nsis.bmp"
!define MUI_UNFINISHPAGE_NOAUTOCLOSE


# Included files
!include Sections.nsh
!include MUI.nsh
!include "MUI_EXTRAPAGES.nsh"

# Installer pages
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_README "changelog.txt"
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

# Installer languages
!insertmacro MUI_LANGUAGE English
  ;Set up install lang strings for 1st lang
  ${ReadmeLanguage} "${LANG_ENGLISH}" \
          "What's new" \
          "Please review the following changes" \
          "${name} updates ${namewoofy} with the following comics:" \
          "$\n  Click on scrollbar arrows or press Page Down to review the entire text."


# Installer attributes
Name "${NAME} ${VERSION}"
BrandingText "${NAME} ${VERSION}"

OutFile "${FILENAME}"
InstallDir $PROGRAMFILES\${NAMEWOOFY}
InstallDirRegKey HKLM "${REGKEY}" "Path"
CRCCheck on
XPStyle on
ShowInstDetails show

ShowUninstDetails show

# Installer sections
Section "-Default"
    SetOutPath $INSTDIR\definitions
    SetOverwrite on
    File /r definitions\*
SectionEnd