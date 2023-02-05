# Wargame API

## Running locally (out of the box)
### Requirements
* Docker Desktop
* dotnet entity framework cli tool

### How to run
1. In the root directory, run ```docker compose up``` to spin up a local postgresql container pointed at port 5431
2. In [the api directory](./src/WargameApi), run ```dotnet ef database update --connection "Host=localhost;Port=5431;Database=kill-team;User ID=postgres;Password=sillylocalpassword"```
3. Run in debug mode with valid configs (see config section below)

## Config
Here are the necessary configs for running this api currently (with example values):
```
ConnectionStrings__KillTeamContext="Host=localhost;Port=5431;Database=kill-team;User ID=postgres;Password=sillylocalpassword"
```

## Testing
There are [integratino tests](./test/IntegrationTests) which are currently run against a local postgresql instance at port 5431