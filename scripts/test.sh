#!/bin/bash
set -e

source "./scripts/travis-commands.sh"

test_package() {
  local name=$1

  travis_fold_start "testing.$(basename ${name})"
  do_package_test ${name}
  travis_fold_end "testing.$(basename ${name})"
}

do_package_test() {
  local name=$1

  echo "====== Running test suite: $(basename ${name}) ======"
  pushd ${name} > /dev/null
  dotnet test
  popd > /dev/null
}
for i in $( find ./test -path ./test/FEMR.TestHelpers -prune -o -name project.json -print ); do
  test_package $(dirname "$i")
done
