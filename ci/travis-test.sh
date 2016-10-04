#!/bin/bash

travis_fold_start() {
  local name=$1
  echo -en "travis_fold:start:${name}\r"
}

travis_fold_end() {
  local name=$1
  echo -en "travis_fold:end:${name}\r"
}

travis_fold_start "testing.FEMR.Commands"
echo "====== Running test suite: FEMR.Commands.Tests ======"
pushd test/FEMR.Commands.Tests
dotnet test
popd
travis_fold_end "testing.FEMR.Commands"

travis_fold_start "testing.FEMR.Core"
echo "====== Running test suite: FEMR.Core.Tests ======"
pushd test/FEMR.Core.Tests
dotnet test
popd
travis_fold_end "testing.FEMR.Core"

travis_fold_start "testing.FEMR.DomainModels"
echo "====== Running test suite: FEMR.DomainModels.Tests ======"
pushd test/FEMR.DomainModels.Tests
dotnet test
popd
travis_fold_end "testing.FEMR.DomainModels"

# travis_fold_start "testing.FEMR.Queries"
# echo "====== Running test suite: FEMR.Queries ======"
# pushd test/FEMR.Queries.Tests
# dotnet test
# popd
# travis_fold_end "testing.FEMR.Queries"
