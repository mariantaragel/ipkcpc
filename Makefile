all: build

build:
	dotnet build src/ipkcpc.csproj -o build

clean:
	rm -r build