@echo off
if exist "%VS120COMNTOOLS%vsvars32.bat" call "%VS120COMNTOOLS%vsvars32.bat" & goto VARSSET
IF EXIST "%VS110COMNTOOLS%vsvars32.bat" call "%VS110COMNTOOLS%vsvars32.bat" & goto VARSSET
IF EXIST "%VS100COMNTOOLS%vsvars32.bat" call "%VS100COMNTOOLS%vsvars32.bat" & goto VARSSET
echo "Could not detect VS version!" & goto ERROR
:VARSSET

mkdir nupkg_archive

msbuild.exe "src\check\check.csproj" /p:configuration=Release
if %ERRORLEVEL% neq 0 goto ERROR

.nuget\nuget.exe pack src\Check\Check.csproj -Prop Configuration=Release
if %ERRORLEVEL% neq 0 goto ERROR

for %%f in (*.nupkg) do (
	.nuget\nuget.exe push %%f
	if %ERRORLEVEL% neq 0 goto ERROR
)

copy *.nupkg nupkg_archive\
del *.nupkg

goto SUCCESS


:SUCCESS
echo                             8"b,""""""Ya
echo                             8  "b,    "Ya
echo      PUSHED          aaaaaaa8,   "b,    "Ya
echo      UPSTAIRS        8"b,    "Ya   "8"""""" 
echo                      8  "b,    "Ya  8       
echo               aaaaaaa8,   "b,    "Ya8       
echo               8"b,    "Ya   "8"""""""       
echo               8  "b,    "Ya  8              
echo        aaaaaa88,   "b,    "Ya8              
echo        8"b,    "Ya   "8"""""""              
echo        8  "b,    "Ya  8                     
echo aaaaaaa8,   "b,    "Ya8                     
echo 8"b,    "Ya   "8"""""""                     
echo 8  "b,    "Ya  8                            
echo 8,   "b,    "Ya8   Tina lives in Berlin     
echo  "Ya   "8"""""""   her voice so seldom      
echo    "Ya  8          on my machine is here tonight
echo      "Ya8                                   
goto END                                             

:ERROR
echo Lipstick fodder
echo .
echo Ffffff    Aaa     Iiii  Ll                                                               
echo Ff       Aa Aa     Ii   Ll                                                               
echo Fffff   Aa   Aa    Ii   Ll                                                              
echo Ff      Aaaaaaa    Ii   Ll                                                               
echo Ff     Aa     Aa  Iiii  Llllll                                                           
echo .
echo The boyfriend blond
                                                                                         
:END
