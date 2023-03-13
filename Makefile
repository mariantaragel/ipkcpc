all: build

build:
	dotnet build src/ipkcpc.csproj

clean:
	rm -r src/bin
	rm -r src/obj