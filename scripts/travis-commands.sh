#!/bin/bash
set -e

running_on_travis() {
  [[ -n "${TRAVIS}" ]]
  return $?
}

travis_fold_start() {
  local name=$1

  if running_on_travis; then
    echo "travis_fold:start:$(basename ${name})"
  fi
}

travis_fold_end() {
  local name=$1

  if running_on_travis; then
    echo "travis_fold:end:$(basename ${name})"
  fi
}
