@echo off
echo %0 Begin

:: 2014/05/21 DAG Derive this script from Archive_SharedUtl2.CMD.

echo.
echo Project Assembly = %USERPROFILE%\My Documents\Visual Studio 2010\Projects\WizardWrx_Libs\BitMath\BitMath\bin\Release\WizardWrx.BitMath.dll
echo Backup Directory = C:\COMMON_DATA\Assorted_Zips\program_source\Visual Studio 2010
echo.

"%USERPROFILE%\My Documents\Visual Studio 2010\Projects\Tools_for_Programmers\WWSourceCodeArchiver\WWSourceCodeArchiver\bin\Release\WWSourceCodeArchiver.exe" "-p%USERPROFILE%\My Documents\Visual Studio 2010\Projects\WizardWrx_Libs\BitMath\BitMath\bin\Release\WizardWrx.BitMath.dll" "-tC:\COMMON_DATA\Assorted_Zips\program_source\Visual Studio 2010"

wwpause.exe
echo %0 Done!