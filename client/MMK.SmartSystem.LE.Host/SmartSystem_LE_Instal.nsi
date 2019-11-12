; �ýű�ʹ�� HM VNISEdit �ű��༭���򵼲���

; ��װ�����ʼ���峣��
!define PRODUCT_NAME "SmartSystem.Laser.Base"
!define PRODUCT_VERSION "2.0.0.0"
!define PRODUCT_PUBLISHER "bj-fanuc TECHNOLOGIES CORP. 2019 �� PRIVACY POLICY"
!define PRODUCT_WEB_SITE "http://www.bj-fanuc.com.cn/"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\SmartSystem.Laser.Base.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"
!define PRODUCT_Acccut_ROOT_KEY "HKCR"


SetCompressor /SOLID lzma
SetCompressorDictSize 32

; ------ MUI �ִ����涨�� (1.67 �汾���ϼ���) ------
!include "MUI.nsh"

; MUI Ԥ���峣��
!define MUI_ABORTWARNING
!define MUI_ICON "..\..\Funuc.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; ��ӭҳ��
!insertmacro MUI_PAGE_WELCOME
; ��װĿ¼ѡ��ҳ��
!insertmacro MUI_PAGE_DIRECTORY
; ��װ����ҳ��
!insertmacro MUI_PAGE_INSTFILES
; ��װ���ҳ��
!define MUI_FINISHPAGE_RUN "$INSTDIR\SmartSystem.Laser.Base.exe"
!insertmacro MUI_PAGE_FINISH

; ��װж�ع���ҳ��
!insertmacro MUI_UNPAGE_INSTFILES

; ��װ�����������������
!insertmacro MUI_LANGUAGE "English"

; ��װԤ�ͷ��ļ�
!insertmacro MUI_RESERVEFILE_INSTALLOPTIONS
; ------ MUI �ִ����涨����� ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "SmartSystem.Laser.Base_Setup.exe"
InstallDir "$PROGRAMFILES\FanucLaserBase"
InstallDirRegKey HKLM "${PRODUCT_UNINST_KEY}" "UninstallString"
ShowInstDetails show
ShowUnInstDetails show
BrandingText "Install MainLaserBase v2.0.0.0"

Section "MainSection" SEC01

  Delete "$INSTDIR\uninst.exe"
  
  Delete "$INSTDIR\AopSdk.dll"
  Delete "$INSTDIR\AxInterop.WMPLib.dll"
  Delete "$INSTDIR\DevExpress.BonusSkins.v16.1.dll"
  Delete "$INSTDIR\DevExpress.Data.v16.1.dll"
  Delete "$INSTDIR\DevExpress.Images.v16.1.dll"
  Delete "$INSTDIR\DevExpress.Office.v16.1.Core.dll"
  Delete "$INSTDIR\DevExpress.Pdf.v16.1.Core.dll"
  Delete "$INSTDIR\DevExpress.Pdf.v16.1.Drawing.dll"
  Delete "$INSTDIR\DevExpress.Printing.v16.1.Core.dll"
  Delete "$INSTDIR\DevExpress.RichEdit.v16.1.Core.dll"
  Delete "$INSTDIR\DevExpress.Sparkline.v16.1.Core.dll"
  Delete "$INSTDIR\DevExpress.Utils.v16.1.dll"
  Delete "$INSTDIR\DevExpress.XtraBars.v16.1.dll"
  Delete "$INSTDIR\DevExpress.XtraEditors.v16.1.dll"
  Delete "$INSTDIR\DevExpress.XtraGrid.v16.1.dll"
  Delete "$INSTDIR\DevExpress.XtraLayout.v16.1.dll"
  Delete "$INSTDIR\DevExpress.XtraNavBar.v16.1.dll"
  Delete "$INSTDIR\DevExpress.XtraPrinting.v16.1.dll"
  Delete "$INSTDIR\DevExpress.XtraTreeList.v16.1.dll"
  Delete "$INSTDIR\F2FPayDll.dll"
  Delete "$INSTDIR\Interop.WMPLib.dll"
  Delete "$INSTDIR\RAP.exe"
  Delete "$INSTDIR\RAP.exe.config"
  Delete "$INSTDIR\Newtonsoft.Json.dll"
  Delete "$INSTDIR\System.Net.Http.Formatting.dll"
  Delete "$INSTDIR\ThoughtWorks.QRCode.dll"
  Delete "$INSTDIR\OpenGL.Net.dll"

  Delete "$SMPROGRAMS\FanucLaserBase\Uninstall.lnk"
  Delete "$DESKTOP\SmartSystem.Laser.Base.lnk"
  Delete "$SMPROGRAMS\FanucLaserBase\SmartSystem.Laser.Base.lnk"

  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  
  CreateDirectory "$SMPROGRAMS\Configs"
  CreateDirectory "$SMPROGRAMS\Configs\MachineMonitor"
  Delete "$SMPROGRAMS\Configs\MachineMonitor\MachineMonitorPage.json"
  CreateDirectory "$SMPROGRAMS\Configs\MachineOperation"
  Delete "$SMPROGRAMS\Configs\MachineOperation\AutoCutterCleanPage.json"
  Delete "$SMPROGRAMS\Configs\MachineOperation\AutoFindSidePage.json"
  Delete "$SMPROGRAMS\Configs\MachineOperation\CutCenterPage.json"
  Delete "$SMPROGRAMS\Configs\MachineOperation\CutterResetCheckPage.json"
  Delete "$SMPROGRAMS\Configs\MachineOperation\MaualioPage.json"
  Delete "$SMPROGRAMS\Configs\MachineOperation\SimpleProfilePage.json"
  CreateDirectory "$SMPROGRAMS\Plugins"
  Delete "$SMPROGRAMS\Plugins\MMK.SmartSystem.Laser.Base.dll"
  Delete "$SMPROGRAMS\Plugins\MMK.SmartSystem.Laser.Base.json"
  CreateDirectory "$SMPROGRAMS\WebApp"
  CreateDirectory "$SMPROGRAMS\zh-Hans"
  
  CreateShortCut "$SMPROGRAMS\FanucLaserBase\SmartSystem.Laser.Base.lnk" "$INSTDIR\SmartSystem.Laser.Base.exe"
  CreateShortCut "$DESKTOP\RAP.lnk" "$INSTDIR\RAP.exe"
  File "RAP.exe"
SectionEnd

Section -AdditionalIcons
  CreateShortCut "$SMPROGRAMS\FanucLaserBase\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\SmartSystem.Laser.Base.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\SmartSystem.Laser.Base.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
  /*
  WriteRegStr  ${PRODUCT_Acccut_ROOT_KEY} "RAP" "" "AcccutProtocol"
  WriteRegStr  ${PRODUCT_Acccut_ROOT_KEY} "RAP" "URL Protocol" "$INSTDIR\RAP.exe"
  WriteRegStr  ${PRODUCT_Acccut_ROOT_KEY} "RAP\DefaultIcon" "" "$INSTDIR\RAP.exe,1"
  WriteRegStr  ${PRODUCT_Acccut_ROOT_KEY} "RAP\shell\open\command" "" '"$INSTDIR\RAP.exe" "%1"'
  */
SectionEnd

/******************************
 *  �����ǰ�װ�����ж�ز���  *
 ******************************/

Section Uninstall
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\AopSdk.dll"
  Delete "$INSTDIR\AxInterop.WMPLib.dll"
  Delete "$INSTDIR\DevExpress.BonusSkins.v16.1.dll"
  Delete "$INSTDIR\DevExpress.Data.v16.1.dll"
  Delete "$INSTDIR\DevExpress.Images.v16.1.dll"
  Delete "$INSTDIR\DevExpress.Office.v16.1.Core.dll"
  Delete "$INSTDIR\DevExpress.Pdf.v16.1.Core.dll"
  Delete "$INSTDIR\DevExpress.Pdf.v16.1.Drawing.dll"
  Delete "$INSTDIR\DevExpress.Printing.v16.1.Core.dll"
  Delete "$INSTDIR\DevExpress.RichEdit.v16.1.Core.dll"
  Delete "$INSTDIR\DevExpress.Sparkline.v16.1.Core.dll"
  Delete "$INSTDIR\DevExpress.Utils.v16.1.dll"
  Delete "$INSTDIR\DevExpress.XtraBars.v16.1.dll"
  Delete "$INSTDIR\DevExpress.XtraEditors.v16.1.dll"
  Delete "$INSTDIR\DevExpress.XtraGrid.v16.1.dll"
  Delete "$INSTDIR\DevExpress.XtraLayout.v16.1.dll"
  Delete "$INSTDIR\DevExpress.XtraNavBar.v16.1.dll"
  Delete "$INSTDIR\DevExpress.XtraPrinting.v16.1.dll"
  Delete "$INSTDIR\DevExpress.XtraTreeList.v16.1.dll"
  Delete "$INSTDIR\F2FPayDll.dll"
  Delete "$INSTDIR\Interop.WMPLib.dll"
  Delete "$INSTDIR\RAP.exe"
  Delete "$INSTDIR\RAP.exe.config"
  Delete "$INSTDIR\Newtonsoft.Json.dll"
  Delete "$INSTDIR\System.Net.Http.Formatting.dll"
  Delete "$INSTDIR\ThoughtWorks.QRCode.dll"
  Delete "$INSTDIR\OpenGL.Net.dll"

  Delete "$SMPROGRAMS\FanucLaserBase\Uninstall.lnk"
  Delete "$DESKTOP\SmartSystem.Laser.Base.lnk"
  Delete "$SMPROGRAMS\FanucLaserBase\SmartSystem.Laser.Base.lnk"

  RMDir "$SMPROGRAMS\FanucLaserBase"

  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd

#-- ���� NSIS �ű��༭�������� Function ���α�������� Section ����֮���д���Ա��ⰲװ�������δ��Ԥ֪�����⡣--#


Section -.NET Framework
  ;����Ƿ�����Ҫ��.NET Framework�汾
  Call GetNetFrameworkVersion
  Pop $R1
  ;${If} $R1 < '2.0.50727'
  ;${If} $R1 < '3.5.30729.4926'
  ;${If} $R1 < '4.0.30319'
  ${If} $R1 < '4.5.52747'
    MessageBox MB_YESNO|MB_ICONQUESTION "This software needs to run .NET Framework 4.5 Running environment��But this environment does not appear to be installed on your machine��$\r$\nYou have two choices��$\r$\n1.Download it yourself on the Internet .NET Framework 4.5 install��Then run the software$\r$\n2.Use this installer to download and install online .NET Framework 4.0$\r$\n$\r$\nDownload and install online now .NET Framework 4.5,Make sure your machine is connected to the Internet.Whether to continue?" IDNO +2
      Call DownloadNetFramework45
    ${ENDIF}
SectionEnd

Function GetNetFrameworkVersion
;��ȡ.Net Framework�汾֧��
    Push $1
    Push $0
    ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" "Install"
    ReadRegDWORD $1 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" "Version"
    StrCmp $0 1 KnowNetFrameworkVersion +1
    ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5" "Install"
    ReadRegDWORD $1 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5" "Version"
    StrCmp $0 1 KnowNetFrameworkVersion +1
    ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.0\Setup" "InstallSuccess"
    ReadRegDWORD $1 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.0\Setup" "Version"
    StrCmp $0 1 KnowNetFrameworkVersion +1
    ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v2.0.50727" "Install"
    ReadRegDWORD $1 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v2.0.50727" "Version"
    StrCmp $1 "" +1 +2
    StrCpy $1 "2.0.50727.832"
    StrCmp $0 1 KnowNetFrameworkVersion +1
    ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v1.1.4322" "Install"
    ReadRegDWORD $1 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v1.1.4322" "Version"
    StrCmp $1 "" +1 +2
    StrCpy $1 "1.1.4322.573"
    StrCmp $0 1 KnowNetFrameworkVersion +1
    ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\.NETFramework\policy\v1.0" "Install"
    ReadRegDWORD $1 HKLM "SOFTWARE\Microsoft\.NETFramework\policy\v1.0" "Version"
    StrCmp $1 "" +1 +2
    StrCpy $1 "1.0.3705.0"
    StrCmp $0 1 KnowNetFrameworkVersion +1
    StrCpy $1 "not .NetFramework"
    KnowNetFrameworkVersion:
    Pop $0
    Exch $1
FunctionEnd

Function DownloadNetFramework45
;���� .NET Framework 4.5
  NSISdl::download /TRANSLATE2 'Downloading %s' 'on connection...' '(surplus  1 second)' '(surplus 1 Minute)' '(surplus 1 hour)' '(surplus %u second)' '(surplus %u Minute)' '(surplus %u hour)' 'Completed��%skB(%d%%) Size��%skB speed��%u.%01ukB/s' /TIMEOUT=7500 /NOIEPROXY 'https://download.microsoft.com/download/D/5/C/D5C98AB0-35CC-45D9-9BA5-B18256BA2AE6/NDP462-KB3151802-Web.exe' '$TEMP\NDP462-KB3151802-Web.exe'
  Pop $R0
  StrCmp $R0 "success" 0 +2
  SetDetailsPrint textonly
  DetailPrint "Installing .NET Framework 4.5.2 ..."
  SetDetailsPrint listonly
  ExecWait '$TEMP\NDP462-KB3151802-Web.exe /quiet /norestart' $R1
  Delete "$TEMP\NDP462-KB3151802-Web.exe"
FunctionEnd


Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to remove completely $(^Name), and all the components?" IDYES +2
  Abort
FunctionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) Successfully removed from your computer."
FunctionEnd
