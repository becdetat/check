@echo off

if "%1"=="" goto USAGE

echo Pushing v%1 with message %2
git commit -m %2 --allow-empty
git push origin master:v%1
git reset --hard origin/master
goto END

:USAGE
	echo Usage: push-upstairs.bat 0.1.2 "Changelog message"

:END
