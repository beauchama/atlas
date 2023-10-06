# Commits

A standard for adding a better meaning to your commits. Help to understand the changes made in the code and the git history.

## Commit message format

```text
<type>[optional scope]: <description>
```

### Type

Must be one of the following:

|     Type      | Description                                                                                        |
| :-----------: | :------------------------------------------------------------------------------------------------- |
|    `build`    | Changes that affect the build system or external dependencies (example scopes: tools, csproj, etc) |
|    `docs`     | Introduces documentations or changes to the documentation                                          |
| `dependency`  | Changes that add, update or remove dependencies (example scopes: nugets)                           |
|   `feature`   | Introduces a new feature to the codebase                                                           |
|     `fix`     | patches a bug to the codebase                                                                      |
| `performance` | Changes that improve performance                                                                   |
|  `pipelines`  | Changes to our CI configuration files and scripts                                                  |
|  `refactor`   | A code change that neither fixes a bug nor adds a feature                                          |
|   `release`   | Mark a release version                                                                             |
|   `revert`    | Reverts a previous commit                                                                          |
|    `rules`    | Changes to .editorconfig, linting, etc                                                             |
|    `test`     | Adding tests or missing tests or correcting existing tests                                         |

> When there is a breaking change. Add `!` after the `type` or `type[scope]` of the commit message.

### Scope

The scope could be anything specifying the place of the commit change to provide additional contextual information and is contained within square bracket. For example `docs[changelog]: update changelog to 1.0.0`.

### Examples

Commit message with description

```md
feature: add new feature
```

Commit message with breaking change

```md
feature!: add new feature
```

Commit message with scope

```md
docs[commits]: Fix typo in Scope section
```

Commit message with scope and breaking change

```md
build[dotnet]!: drop .NET 7.0 support
```
