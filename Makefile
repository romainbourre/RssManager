run-api:
	dotnet run --project RssManager.Api

build-administration-console:
	dotnet publish --use-current-runtime -p:DebugType=None -p:DebugSymbols=false  -p:PublishSingleFile=true -p:PublishReadyToRun=true -p:PublishTrimmed=true  -c Release --self-contained --output bin/ RssManager.AdministrationConsole