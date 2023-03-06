build:
	cd ipkcpc && dotnet build

run:
	cd ipkcpc/bin/Debug/net6.0/ && ./ipkcpc -h 0.0.0.0 -p 2023 -m tcp