# Wargame API

## Running locally
### Requirements
* Docker Desktop
* dotnet entity framework cli tool

### How to run
1. In the root directory, run ```docker compose up``` to spin up a local SQL server
2. In [the api directory](./src/WargameApi), run ```dotnet ef database update --connection "Host=localhost;Port=5431;Database=kill-team;User ID=postgres;Password=sillylocalpassword"```
