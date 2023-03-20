all: build publish

build:
	dotnet build src/ipkcpc.csproj

publish:
	dotnet publish src/ipkcpc.csproj -c Release -o .

clean:
	rm -r src/bin
	rm -r src/obj
	rm ipkcpc
	rm *.pdb