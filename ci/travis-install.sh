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

restore_packages() {
  local name=$1

  travis_fold_start "restoring.$(basename ${name})"
  echo "====== Restoring: $(basename ${name}) ======"
  pushd ${name}
  dotnet restore
  popd
  travis_fold_end "restoring.$(basename ${name})"
}

for i in $( find ./src ./test -mindepth 2 -maxdepth 2 -name "project.json" ); do
  restore_packages $(dirname "$i")
done
