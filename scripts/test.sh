#!/bin/bash

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
  pushd ${name}
  dotnet test
  popd
}

for i in $( find ./test -mindepth 2 -maxdepth 2 -name "project.json" ); do
  test_package $(dirname "$i")
done
