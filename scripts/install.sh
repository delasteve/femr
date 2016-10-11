#!/bin/bash
set -e

source "./scripts/travis-commands.sh"

restore_package() {
  local name=$1

  travis_fold_start "restoring.$(basename ${name})"
  do_package_restore ${name}
  travis_fold_end "restoring.$(basename ${name})"
}

do_package_restore() {
  local name=$1

  echo "====== Restoring: $(basename ${name}) ======"
  pushd ${name} > /dev/null
  dotnet restore
  popd > /dev/null
}

for i in $( find ./src ./test -mindepth 2 -maxdepth 2 -name "project.json" ); do
  restore_package $(dirname "$i")
done
