cls
set initialPath=%cd%
set apiPath=%cd%\src\backend\CatFactory.UI\CatFactory.UI.API
set clientPath=%cd%\src\frontend\cat-factory-ui
cd %apiPath%
start dotnet restore
cd %clientPath%
start npm install
cd %initialPath%
pause
