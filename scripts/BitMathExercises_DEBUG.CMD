@echo off
goto SKIPREM

::  ============================================================================
::
::  Name:               BitMathExercises_DEBUG.CMD
::
::  Synopsis:           Document and exercise the Debug build of class library
::                      WizardWrx.BitMath.dll and make a permanent record of the
::                      outcome.
::
::  Arguments:          This shell script has no arguments.
::
::  Dependencies:       BithMathTestStand.exe       = This is the program to run
::                                                    and document the outcome.
::
::                      Date2FN.exe                 = This 32-bit character mode
::                                                    Windows program appends a
::                                                    formatted string represen-
::                                                    ting the LastWriteTime of
::                                                    a file to its base name to
::                                                    create a name that is
::                                                    intended to be locally
::                                                    unique. The optional R
::                                                    switch instructs it to
::                                                    mark the file as read only
::                                                    by setting the read only
::                                                    attribute in the file
::                                                    system.
::
::                      ShowTime.CMD                = This little shell script
::                                                    displays the current time
::                                                    per the machine clock.
::
::                      WWPause.exe                 = This 32-bit character mode
::                                                    Windows program replaces
::                                                    the lame intrinsic pause
::                                                    command implemented by the
::                                                    command processor. Unlike
::                                                    Pause, it insists on the
::                                                    RETURN key being pressed.
::
::                      BithMathTestStand.exe must be installed into the
::                      following directory relative to the script directory.
::
::                          ..\TestStand\bin\Debug
::
::                      All other dependent programs must be installed into a
::                      directory that is in the Windows PATH list.
::
::  Remarks:            Companion script BitMathExercises_RELEASE.CMD performs
::                      the same task for the release build.
::
::  ----------------------------------------------------------------------------
::  Revision History
::  ----------------------------------------------------------------------------
::
::  Date       By  Synopsis
::  ---------- --- -------------------------------------------------------------
::  2016/05/01 DAG Create this script essentially from scratch, working from
::                 data captured from the last few interactive sessions.
::  ============================================================================

:SKIPREM

    if CMDEXTVERSION 1  goto CHK_REQ1

    echo.
    echo     --------------------------------------------------------------------
    echo     This script requires command extensions to be enabled. Since they
    echo     are enabled, by default, they have been disabled, directly or by GPO.
    echo     --------------------------------------------------------------------

    goto ERR_DONE


:CHK_REQ1

    echo.
    echo BOJ %0, version %~t0
    echo.
    call ShowTime.CMD
    echo.

    setlocal
    pushd "%~dp0"
    set BLDTYP=DEBUG
    set PGMDIR=..\TestStand\bin\%BLDTYP%
    set LOGDIR=..\NOTES

    if exist %PGMDIR%\BithMathTestStand.exe (
        if exist %LOGDIR%\* (
            if exist %LOGDIR%\BithMathTestStand_%BLDTYP%.LOG (
                Date2FN.exe -r %LOGDIR%\BithMathTestStand_%BLDTYP%.LOG

                if exist %LOGDIR%\BithMathTestStand_%BLDTYP%.LOG (
                    sdelete.exe -a %LOGDIR%\BithMathTestStand_%BLDTYP%.LOG
                )
            )

            if exist %LOGDIR%\BithMathTestStand_%BLDTYP%.LOG (
                echo ERROR: Unable to remove or preserve the last log.
            ) else (
                %PGMDIR%\BithMathTestStand.exe > %LOGDIR%\BithMathTestStand_%BLDTYP%.LOG

                if exist %LOGDIR%\BithMathTestStand_%BLDTYP%.LOG (
                    Date2FN.exe -r %LOGDIR%\BithMathTestStand_%BLDTYP%.LOG
                ) else (
                    echo There is no log file %LOGDIR%\BithMathTestStand_%BLDTYP%.LOG to save.
                )

                if exist BitArray32TestStand.CSV (
                    SmartRen.exe  BitArray32TestStand.CSV %LOGDIR%\BitArray32TestStand_%BLDTYP%.CSV
                    Date2FN.exe -r %LOGDIR%\BitArray32TestStand_%BLDTYP%.CSV
                ) else (
                    echo Report file BitArray32TestStand.CSV was not created.
                )
            )
        ) else (
            echo Log directory %~dp0%LOGDIR% not found
        )
    ) else (
        echo Test program BithMathTestStand.exe not found in directory %~dp0%PGMDIR%
    )

:ERR_DONE

    echo.
    echo EOJ %~nx0
    echo.

:DONE

    WWPause.exe
