#!/bin/bash
NAME=$1
if [ -z $NAME ]
then
  echo "missing name for migration"
  echo "./ef_migration_add.sh <NAME>"
else
  echo "Adding new migration '${NAME}'"
  dotnet ef migrations add $NAME --project src/LagDinCv.Infrastructure/ -o Data/Migrations
fi