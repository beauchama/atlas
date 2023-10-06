# Branches

## Develop

Develop branch is the default branch for the repository. It is the branch that is checked out when you clone/fork the repository.

* Branch is protected
* Pull request are required to merge into it

### Strategy

When pulling the branch, you should rebase your changes on top of it. This will ensure that your changes are always on top of the latest changes.

```bash
git pull --rebase <remote>
```

> You can also set this as the default behavior when pulling:

```bash
git config --global branch.autosetuprebase always
```

## Main

Main branch is the branch that contains all releases.

* Branch is protected
* Deploy NuGets from this branch
* Only release and hotfix branches can be squash into it

The commit message should follow the following convention:

```text
release: <package>-<version>, <package>-<version>, ...
```

### Strategy

When pulling the branch, you should rebase your changes on top of it. This will ensure that your changes are always on top of the latest changes.

```bash
git pull --rebase <remote>
```

> You can also set this as the default behavior when pulling:

```bash
git config --global branch.autosetuprebase always
```

## Feature

Feature branches are used to implement new features. They are branched off of the develop branch and merged back into it when the feature is complete.

### Strategy

Before pushing your feature branch, you should rebase your changes on top of the develop branch. This will ensure your branch is up to date with develop branch.

```bash
git rebase develop
```

### Naming

Feature branches should be named using the following convention:

`git switch -c feature/<issue>/<name>`

## Bugfix

Bugfix branches are used to fix bugs.

* Branch off of the develop branch
* Merge into the develop

### Strategy

Before pushing your bugfix branch, you should rebase your changes on top of the develop branch. This will ensure your branch is up to date with develop branch.

```bash
git rebase develop
```

### Naming

Bugfix branches should be named using the following convention:

`git switch -c bugfix/<issue>/<name>`

## Release

Release branches are used to prepare a new release. Only contains bugfixes and documentation updates.

* Branch off of the develop branch
* Merge into the develop and main branches

### Naming

Release branches should be named using the following convention:

`git switch -c release`

## Hotfix

Hotfix branches are used to fix critical bugs.

* Branch off of the main branch
* Merge into the develop and main branches

### Naming

Hotfix branches should be named using the following convention:

`git switch -c hotfix/<issue>/<name>`
