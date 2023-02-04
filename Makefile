all: clean restore build test report

clean:
	dotnet clean

restore:
	dotnet restore
	dotnet tool restore
   
build:
	dotnet build --no-restore

test:
	dotnet test --no-restore --no-build --collect "XPlat Code Coverage" --results-directory "./test-results"
    
report:
	dotnet tool run reportgenerator
