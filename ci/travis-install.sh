#!/bin/bash

travis_fold_start() {
  local name=$1
  echo -en "travis_fold:start:${name}\r"
}

travis_fold_end() {
  local name=$1
  echo -en "travis_fold:end:${name}\r"
}

travis_fold_start "restoring.FEMR.Commands"
echo "====== Restoring: FEMR.Commands ======"
pushd src/FEMR.Commands
dotnet restore
popd
travis_fold_end "restoring.FEMR.Commands"

travis_fold_start "restoring.FEMR.Commands.Tests"
echo "====== Restoring: FEMR.Commands.Tests ======"
pushd test/FEMR.Commands.Tests
dotnet restore
popd
travis_fold_end "restoring.FEMR.Commands.Tests"

travis_fold_start "restoring.FEMR.Core"
echo "====== Restoring: FEMR.Core ======"
pushd src/FEMR.Core
dotnet restore
popd
travis_fold_end "restoring.FEMR.Core"

travis_fold_start "restoring.FEMR.Core.Tests"
echo "====== Restoring: FEMR.Core.Tests ======"
pushd test/FEMR.Core.Tests
dotnet restore
popd
travis_fold_end "restoring.FEMR.Core.Tests"

travis_fold_start "restoring.FEMR.DomainModels"
echo "====== Restoring: FEMR.DomainModels ======"
pushd src/FEMR.DomainModels
dotnet restore
popd
travis_fold_end "restoring.FEMR.DomainModels"

travis_fold_start "restoring.FEMR.DomainModels.Tests"
echo "====== Restoring: FEMR.DomainModels.Tests ======"
pushd test/FEMR.DomainModels.Tests
dotnet restore
popd
travis_fold_end "restoring.FEMR.DomainModels.Tests"

# travis_fold_start "restoring.FEMR.Queries"
# echo "====== Restoring: FEMR.Queries ======"
# pushd src/FEMR.Queries
# dotnet restore
# popd
# travis_fold_end "restoring.FEMR.Queries"

# travis_fold_start "restoring.FEMR.Queries.Tests"
# echo "====== Restoring: FEMR.Queries.Tests ======"
# pushd test/FEMR.Queries.Tests
# dotnet restore
# popd
# travis_fold_end "restoring.FEMR.Queries.Tests"

travis_fold_start "restoring.FEMR.WebAPI"
echo "====== Restoring: FEMR.WebAPI ======"
pushd src/FEMR.WebAPI
dotnet restore
popd
travis_fold_end "restoring.FEMR.WebAPI"
