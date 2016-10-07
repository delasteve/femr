#!/bin/bash

travis_fold_start() {
  local name=$1

  if [ -z ${TRAVIS-x} ]; then
    echo "Yep"
    echo -en "travis_fold:start:${name}\r"
  fi
}

travis_fold_end() {
  local name=$1

  if [ -z ${TRAVIS-x} ]; then
    echo -en "travis_fold:end:$(basename ${name})\r"
  fi
}

test_packages() {
  local name=$1

  travis_fold_start "testing.$(basename ${name})"
  echo "====== Running test suite: $(basename ${name}) ======"
  pushd ${name}
  dotnet test
  popd
  travis_fold_end "testing.$(basename ${name})"
}

for i in $( find ./test -mindepth 2 -maxdepth 2 -name "project.json" ); do
  test_packages $(dirname "$i")
done
