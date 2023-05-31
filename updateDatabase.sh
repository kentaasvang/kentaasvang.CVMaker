#!/bin/bash
echo "Updating database schema"
dotnet ef database update --project src/LagDinCv.Infrastructure/
