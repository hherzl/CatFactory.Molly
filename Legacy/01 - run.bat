cls
set initialPath=%cd%
set apiPath=%cd%\src\backend\CatFactory.UI\CatFactory.UI.API
set clientPath=%cd%\src\frontend\cat-factory-ui
cd %apiPath%
start dotnet run
cd %clientPath%
start ng serve
cd %initialPath%
pause
