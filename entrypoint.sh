#!/bin/sh -l

cd /app

dotnet restore
dotnet build
dotnet run --project src/GitHubActions.Teams.ConsoleApp -- \
    --input-parameter "$INPUT_PARAMETER"