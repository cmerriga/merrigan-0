@echo off
setlocal
set workingdir=.
set archivedir=C:\merrigan\chadd\projects\archive\merrigan-0\Executions\%~1
if not exist "%archivedir%" (
    mkdir "%archivedir%"
)
xcopy /e /exclude:save-exclusions.txt "%workingdir%" "%archivedir%"
